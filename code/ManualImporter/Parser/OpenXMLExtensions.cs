using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ManualImporter.Parser
{



    public static class OpenXMLExtensions
        {
            public static int GetColumnIndex(this Cell cell)
            {
                var column = cell.GetColumn();
                var power = 0;
                int columnIndex = column.ToCharArray().Reverse().Select(c => (c - 64) * (int)Math.Pow(26, power++)).Sum() - 1;
                return columnIndex;
            }

            public static string GetColumn(this Cell cell)
            {
                if (string.IsNullOrEmpty(cell.CellReference?.Value)) throw new ArgumentNullException("CellReference");

                return Regex.Replace(cell.CellReference.Value.ToUpper(), @"[\d]", string.Empty);
            }

            public static Cell FindCell(this Row row, string column)
            {
                var columnReference = string.Format("{0}{1}", column, row.RowIndex.Value);

                return row.FindCellByRef(columnReference);
            }

            public static Cell FindCell(this Row row, int columnIndex)
            {
                var column = string.Empty;

                columnIndex += 1;

                while (columnIndex > 0)
                {
                    column += $"{columnIndex % 26 + 64}";
                    columnIndex = columnIndex / 26;
                }

                return row.FindCell(column);
            }

            public static Cell FindCellByRef(this Row row, string cellReference)
            {
                return row.Descendants<Cell>().FirstOrDefault(c => c.CellReference.Value.Equals(cellReference.ToUpper()));
            }

            public static Cell FindCellByRef(this Worksheet worksheet, string cellReference)
            {
                return worksheet.Elements<Cell>().FirstOrDefault(c => c.CellReference.Value == cellReference);
            }
        }
    

}
