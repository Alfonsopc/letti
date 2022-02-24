using ManualImporter.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace ManualImporter.Helpers
{
    public class FileDefinitionReader
    {
        public List<ColumnName> Columns = new List<ColumnName>();
        public string FileName;
        public void Load(string organizationName)
        {
            Columns.Clear();
            if(organizationName.Contains(".xml"))
            {
                FileName = organizationName;
            }
            else
            {
                FileName = $"{organizationName}.xml";
            }        
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
                Columns.Add(application);
            }
        }

        public string GetFilePropertyName(string propertyName)
        {
            return Columns.First(columnNames => columnNames.PropertyName == propertyName).FilePropertyName;
        }

        public void WriteToXmlFile(string filePath) 
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(List<ColumnName>));
                writer = new StreamWriter(filePath, false);
                serializer.Serialize(writer, Columns);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
