using Letti.Model;
using ManualImporter.Controller;
using ManualImporter.Helpers;
using ManualImporter.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Shapes;
using ManualImporter.Entities;
using System.Xml.Serialization;

namespace ManualImporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContractController controller;
        public MainWindow()
        {
            InitializeComponent();
            controller = new ContractController();
        }

        private async void Compranet_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "Archivos XLS Files (*.xlsx)|*.xlsx" };
            var result = ofd.ShowDialog();
            if (result.Value)
            {
                try
                {
                    CompranetParser departmentsParser = new CompranetParser();
                   
                     departmentsParser.Load(ofd.FileName);
                    List<string> dependencies = new List<string>();
                    ICollection<Organization> organizations = await controller.GetOrganizations();
                    dependencies.AddRange(organizations.Select(c => c.OrganizationName));
                   // dependencies.Add("Ayuntamiento de Mexicali");
                   // dependencies.Add("Ayuntamiento de Ensenada");
                    
                    IList<Contract> contracts = departmentsParser.Parse(dependencies);
                    foreach (Contract contract in contracts)
                    {
                        if (!contract.Contractor.OfficialName.ToLower().Contains("ver nota") && !contract.Number.ToLower().Contains("ver nota"))
                        {
                            await controller.Create(contract);
                        }
                    }
                    
                   
                }
                catch (Exception)
                {
               
                    throw;
                }
                finally
                {
                  
                    MessageBox.Show("Datos importados");
                }
            }
        }

        private async void Transparencia_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "XLSX Files (*.xlsx)|*.xlsx|XLS files (*.xls)|*.xls" };
            var result = ofd.ShowDialog();
            if (result.Value)
            {
                try
                {
                    TransparenciaParser departmentsParser = new TransparenciaParser();

                    departmentsParser.Load(ofd.FileName);
                    List<string> dependencies = new List<string>();
                    ICollection<Organization> organizations = await controller.GetOrganizations();
                    dependencies.AddRange(organizations.Select(c => c.OrganizationName));
                    // dependencies.Add("Ayuntamiento de Mexicali");
                    // dependencies.Add("Ayuntamiento de Ensenada");

                    IList<Contract> contracts = departmentsParser.Parse(dependencies);
                    foreach (Contract contract in contracts)
                    {
                        if (!contract.Contractor.OfficialName.ToLower().Contains("ver nota") && !contract.Number.ToLower().Contains("ver nota"))
                        {
                            await controller.Create(contract);
                        }
                    }


                }
                catch (Exception exp)
                {

                    throw exp;
                }
                finally
                {

                    MessageBox.Show("Datos importados");
                }
            }
        }

        private void Transparencia_Setup_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "XLSX Files (*.xlsx)|*.xlsx|XLS files (*.xls)|*.xls" };
            var result = ofd.ShowDialog();
            List<FileAnalysis> listOfFiles = new List<FileAnalysis>();
            if (result.Value)
            {
                TransparenciaParser contractsParser = new TransparenciaParser();

                contractsParser.Load(ofd.FileName);
               
                contractsParser.LoadColumns();
               
                string dir=Directory.GetCurrentDirectory();
                string[] dirs = Directory.GetFiles(dir, "*.xml");
                bool found = false;
                foreach(string definitionFile in dirs)
                {
                    string definitions = definitionFile.Replace(".xml", "");
                    FileDefinitionReader definitionReader = new FileDefinitionReader();
                    definitionReader.Load(definitions);
                    FileAnalysis changes = DefinitionMatch(contractsParser, definitionReader);
                    if(changes.Errors==0)
                    {
                        found = true;
                        WriteToXmlFile<FileAnalysis>($"c:\\temp\\{contractsParser.Organization}.xml", changes);
                        MessageBox.Show("Match:" + definitionFile);
                        return;
                    }
                    else
                    {
                        listOfFiles.Add(changes);
                    }
                }
                if(!found)
                {
                    FileAnalysis betterFile=listOfFiles.OrderBy(c => c.Errors).First();
                    WriteToXmlFile<FileAnalysis>($"c:\\temp\\{contractsParser.Organization}.xml", betterFile);
                    MessageBox.Show("Best Match:" + betterFile.FileName);
                }

            }

        }
        private FileAnalysis DefinitionMatch(TransparenciaParser parser,FileDefinitionReader definitionReader)
        {
            FileAnalysis fileAnalysis = new FileAnalysis();
            fileAnalysis.Errors = 0;
            fileAnalysis.FileName = definitionReader.FileName;
            fileAnalysis.Organization = parser.Organization;
            foreach(ColumnName column in definitionReader.ColumnNames)
            {
                string columnName = column.FilePropertyName.ToUpper();
                var foundColumn = parser.Columns.Where(c => c.Value.ToUpper() == columnName).Any();
                if(!foundColumn)
                {
                    fileAnalysis.Errors++;
                    fileAnalysis.Columns.Add(column);
                    MessageBox.Show(definitionReader.FileName+":"+ columnName);
                }
            }
            return fileAnalysis;
        }

        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
