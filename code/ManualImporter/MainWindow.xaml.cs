using Letti.Model;
using ManualImporter.Controller;
using ManualImporter.Entities;
using ManualImporter.Helpers;
using ManualImporter.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;


namespace ManualImporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContractController controller;
        private List<string> filesToMove = new List<string>();
        private string targetPath = string.Empty;
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
            filesToMove.Clear();
            targetPath = string.Empty;
            List<FileAnalysis> listOfFiles = new List<FileAnalysis>();
           
            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Time to select a folder",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        + Path.DirectorySeparatorChar,
                ShowNewFolderButton = true
            };        
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filesPath = dialog.SelectedPath;
                targetPath = $"{filesPath}\\ready";
                System.IO.Directory.CreateDirectory(targetPath);
                string[] xls = Directory.GetFiles(filesPath, "*.xlsx");
              
                foreach(string dataFile in xls)
                {
                    TransparenciaParser contractsParser = new TransparenciaParser();

                    contractsParser.Load(dataFile);

                    contractsParser.LoadColumns();
                    //definition files
                    string definitionsDirectory =$"{Directory.GetCurrentDirectory()}\\definitions";
                    string[] dirs = Directory.GetFiles(definitionsDirectory, "*.xml");
                    foreach (string definitionFile in dirs)
                    {
                        string definitions = definitionFile.Replace(".xml", "");
                        FileDefinitionReader definitionReader = new FileDefinitionReader();
                        definitionReader.Load(definitions);
                        FileAnalysis changes = DefinitionMatch(contractsParser, definitionReader);
                        listOfFiles.Add(changes);                  
                    }

                    //primero checamos que no exista ya un archivo para esta organización
                    if(listOfFiles.Where(c=>c.FileOrganization==c.Organization).Any())
                    {
                        //nothing else to do but to mark the data file to move
                        filesToMove.Add(dataFile);
                    }
                    else
                    {
                        FileAnalysis betterFile = listOfFiles.OrderBy(c => c.Errors).First();
                        if(betterFile.Errors==0)
                        {
                            //es una nueva dependencia hay que copiar el archivo
                            string destFile = System.IO.Path.Combine(definitionsDirectory, $"{contractsParser.Organization}.xml");
                            System.IO.File.Copy(betterFile.FileName, destFile, true);
                            filesToMove.Add(dataFile);
                            MessageBox.Show($"Match: {contractsParser.Organization} and {betterFile.FileOrganization}");               
                        }
                        else
                        {
                            DefinitionsSetup definitionsSetup = new DefinitionsSetup(betterFile, contractsParser.Columns);
                            definitionsSetup.ShowDialog();
                            if (definitionsSetup.FileSaved)
                            {
                                filesToMove.Add(dataFile);
                            }
                        }
                    }                                      
                }

            }
           
            MessageBox.Show("Done");
            
        }
        private void Move_Click(object sender, RoutedEventArgs e)
        {
            int filecount = 0;
            foreach (string filetoMove in filesToMove)
            {
                string fileName = Path.GetFileName(filetoMove);
                string destinationFile = $"{targetPath}\\{fileName}";
                try
                {
                    System.IO.File.Move(filetoMove, destinationFile);
                }
                catch (Exception exp)
                {
                    filecount++;
                }
            }
        }
        private FileAnalysis DefinitionMatch(TransparenciaParser parser,FileDefinitionReader definitionReader)
        {
            FileAnalysis fileAnalysis = new FileAnalysis();
            fileAnalysis.Errors = 0;
            fileAnalysis.FileName = definitionReader.FileName;
            fileAnalysis.Organization = parser.Organization;
            foreach(ColumnName column in definitionReader.Columns)
            {
                string columnName = column.FilePropertyName.ToUpper();
                var foundColumn = parser.Columns.Where(c => c.Value.ToUpper() == columnName).Any();
                if(!foundColumn)
                {
                    fileAnalysis.Errors++;
                    fileAnalysis.Columns.Add(column);
                    //MessageBox.Show(definitionReader.FileName+":"+ columnName);
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
