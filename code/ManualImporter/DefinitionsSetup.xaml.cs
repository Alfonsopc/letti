using ManualImporter.Entities;
using ManualImporter.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManualImporter
{
    /// <summary>
    /// Interaction logic for DefinitionsSetup.xaml
    /// </summary>
    public partial class DefinitionsSetup : Window
    {
        public ObservableCollection<ColumnName> Columns { get; set; }
        public List<ColumnName> DefinitionColumns { get; set; }
        public bool FileSaved { get; set; }
        private ObservableCollection<string> DataColumns;
        private FileDefinitionReader fileDefinitionReader;
        private FileAnalysis fileAnalysis;
        public DefinitionsSetup()
        {
            InitializeComponent();
        }
        public DefinitionsSetup(FileAnalysis fileAnalysis, Dictionary<int, string> dataColumns)
        {
            InitializeComponent();
            FileSaved = false;
            Columns = new ObservableCollection<ColumnName>();
            fileDefinitionReader = new FileDefinitionReader();
            DefinitionColumns = new List<ColumnName>();
            this.fileAnalysis = fileAnalysis;
            this.DataColumns = new ObservableCollection<string>();
            foreach(var  headers in dataColumns)
            {
                this.DataColumns.Add(headers.Value);
            }
            OrganizationName.Text = fileAnalysis.Organization;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fileDefinitionReader.Load(fileAnalysis.FileName);
            foreach(var definitionColumn in fileDefinitionReader.Columns)
            {
                Columns.Add(definitionColumn);
            }
            
            //vacía las columnas en las que se encontraron diferencias
            foreach(var column in fileAnalysis.Columns)
            {
                ColumnName columnToClear=Columns.FirstOrDefault(c => c.PropertyName == column.PropertyName);
                if(columnToClear!=null)
                {
                    columnToClear.FilePropertyName = string.Empty;
                }
            }
            //remueve de las opciones las columnas que sí se encontraron
            var validColumns = Columns.Where(c => !string.IsNullOrEmpty(c.FilePropertyName));
           
            foreach(ColumnName column in validColumns)
            {
                var toremove=DataColumns.FirstOrDefault(c => c == column.FilePropertyName);
                DataColumns.Remove(toremove);
                DefinitionColumns.Add(column);
            }
            ColumnsView.ItemsSource = Columns;
            DataColumnsView.ItemsSource = DataColumns;
        }

        private void Bind_Click(object sender, RoutedEventArgs e)
        {
            ColumnName selectedDest = ColumnsView.SelectedItem as ColumnName;
            string selectedSource = DataColumnsView.SelectedItem.ToString();
            selectedDest.FilePropertyName = selectedSource;
            Columns.Remove(selectedDest);
            DataColumns.Remove(selectedSource);
            DefinitionColumns.Add(selectedDest);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            FileSaved = false;
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(Columns.Any())
            {
                MessageBox.Show("Necesitas relacionar todas las propiedades");
                return;
            }
            FileSaved = true;
            FileDefinitionReader fileDefinitionReader = new FileDefinitionReader();
            fileDefinitionReader.Columns.AddRange(DefinitionColumns);
            string definitionsFile = $"{Directory.GetCurrentDirectory()}\\definitions\\{fileAnalysis.Organization}.xml";
            fileDefinitionReader.WriteToXmlFile(definitionsFile);
            Close();
        }

        private void BindIgnore_Click(object sender, RoutedEventArgs e)
        {
            ColumnName selectedDest = ColumnsView.SelectedItem as ColumnName;           
            selectedDest.FilePropertyName = "Ignorar";
            Columns.Remove(selectedDest);
            DefinitionColumns.Add(selectedDest);
        }
    }
}
