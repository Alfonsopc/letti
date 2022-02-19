using ManualImporter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ManualImporter.Helpers
{
    public class FileDefinitionReader
    {
        public List<ColumnName> ColumnNames = new List<ColumnName>();
        public string FileName;
        public void Load(string organizationName)
        {
            ColumnNames.Clear();
            FileName = $"{organizationName}.xml";
            XDocument document = XDocument.Load(FileName);
            var found = document.XPathEvaluate("/Columns/ColumnName") as IEnumerable<object>;

            if (found == null)
            {
                MessageBox.Show("No se encontraron aplicaciones");
                return;
            }
            foreach (XElement node in found)
            {
                Entities.ColumnName application = new Entities.ColumnName();
                application.PropertyName = ((XElement)node.FirstNode).Value;
                application.FilePropertyName = ((XElement)node.FirstNode.NextNode).Value;
                ColumnNames.Add(application);
            }
        }

        public string GetFilePropertyName(string propertyName)
        {
            return ColumnNames.First(columnNames => columnNames.PropertyName == propertyName).FilePropertyName;
        }
    }
}
