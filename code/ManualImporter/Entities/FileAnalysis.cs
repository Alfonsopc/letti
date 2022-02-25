using System.Collections.Generic;
using System.IO;

namespace ManualImporter.Entities
{
    public class FileAnalysis
    {
        private string fileName;

        public int Errors { get; set; }
        public string Organization { get; set; }
        public string FileOrganization { get; private set; }
        public string FileName 
        { 
            get => fileName;
            set 
            {
                fileName = value;
                FileOrganization= Path.GetFileNameWithoutExtension(fileName);
            }
        }
        public List<ColumnName> Columns { get; set; } = new List<ColumnName>();

    }
}
