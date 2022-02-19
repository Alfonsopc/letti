using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ManualImporter.Parser
{
    public abstract class ExcelParser<T> : IDisposable
   where T : class
    {
        private SharedStringTable stringSharedTable;
        protected Dictionary<string, string> Headers;
        protected IEnumerable<Row> Rows { get; set; }
        private Stream file;
        private SpreadsheetDocument document;
        private bool loaded;
        protected string[] headerLabels;

        public ExcelParser(Stream stream, params string[] headerLabels)
        {
            this.headerLabels = headerLabels;
            file = new MemoryStream();
            stream.CopyTo(file);
            Load();
        }

        public ExcelParser(string filePath,string initializer, params string[] headerLabels)
        {
            this.headerLabels = headerLabels;
            file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Load();
        }

        public ExcelParser()
        {

        }

        public void Initialize(string filePath, string initialize,params string[] headerLabels)
        {
            this.headerLabels = headerLabels;
            file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            if(string.IsNullOrEmpty(initialize))
            {
                Load();
            }
            else
            {
                Load(initialize);
            }
            
        }
       
        public abstract IList<T> Parse(List<string> filter);

        protected string GetRowValue(Row row, int columnId)
        {
            int columnCount = row.ChildElements.Count;
            if (columnId >= columnCount)
            {
                return string.Empty;
            }
            return GetValueString(row.ChildElements[columnId] as Cell);
        }
        protected string GetValueString(Cell cell)
        {
            string value = string.Empty;

            if (cell?.CellValue != null)
            {
                switch (cell.DataType?.Value)
                {
                    case CellValues.SharedString:
                        value = stringSharedTable.ElementAt(int.Parse(cell.CellValue.InnerText)).InnerText;
                        break;
                    case CellValues.Date:
                        value = cell.CellValue.InnerText;
                        break;
                    default:
                        value = cell.CellValue.InnerText;
                        break;
                }
            }
            else
            {
                value = cell.InnerText;
            }

            return value;
        }

        protected DateTime? GetDate(string str)
        {
            DateTime? response = null;
            if (!string.IsNullOrEmpty(str))
            {
                double d;
                bool isDouble = double.TryParse(str, out d);
                if (isDouble)
                {
                    response = DateTime.FromOADate(d).Date;
                }
            }
            return response;
        }
        private void Load()
        {
            document = SpreadsheetDocument.Open(file, false);
            var workbookPart = document.WorkbookPart;
            List<WorksheetPart> tmplist=workbookPart.WorksheetParts.ToList();
                      
            var worksheetPart = workbookPart.WorksheetParts.First();
            var sheet = worksheetPart.Worksheet;
            var sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
            stringSharedTable = sharedStringTablePart.SharedStringTable;
            Rows = sheet.Descendants<Row>();
            if (headerLabels != null)
            {
                Headers = GetHeaders(Rows.First());
                Rows = Rows.Skip(1);
            }
                         
            loaded = true;
        }
        private void Load(string initializer)
        {
            document = SpreadsheetDocument.Open(file, false);
            var workbookPart = document.WorkbookPart;
            List<WorksheetPart> tmplist = workbookPart.WorksheetParts.ToList();
            int sheetCount = 0;
            foreach (var worksheetPart in tmplist)
            {               
                var sheet = worksheetPart.Worksheet;
                var sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                stringSharedTable = sharedStringTablePart.SharedStringTable;
                Rows = sheet.Descendants<Row>();
                if (headerLabels != null)
                {
                    Headers = GetHeaders(Rows.First());
                    Rows = Rows.Skip(1);
                }
                Row row = Rows.First();
                string tmp = GetValueString(row.ChildElements[0] as Cell);
                if(tmp.Contains(initializer))
                {
                    break;
                }
                sheetCount++;
            }

            loaded = true;
        }

        public void Dispose()
        {
            if (loaded)
            {
                file.Dispose();
                document.Dispose();
                loaded = false;
            }
        }

        private Dictionary<string, string> GetHeaders(Row row)
        {
            var headers = new Dictionary<string, string>();

            foreach (var column in headerLabels)
            {
                var cell = row.Elements<Cell>().FirstOrDefault(c =>
                {
                    if (c.CellValue != null)
                    {
                        return c.CellValue.Text.ToUpper().Equals(column);
                    }
                    return false;
                });
                if (cell != null)
                {
                    string letter = cell.GetColumn();
                    headers.Add(column, letter);
                }
            }

            return headers;
        }
    }
}
