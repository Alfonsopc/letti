using System.Collections.Generic;

namespace ManualImporter.Entities
{
    public class FileAnalysis
    {
        public int Errors { get; set; }
        public string Organization { get; set; }
        public string FileName { get; set; }
        public List<ColumnName> Columns { get; set; } = new List<ColumnName>();
        
    }
}
