﻿using Letti.Model;
using ManualImporter.Controller;
using ManualImporter.Entities;
using ManualImporter.Helpers;
using ManualImporter.Parser;
using System;
using System.Collections.Generic;
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
          
            List<FileAnalysis> listOfFiles = new List<FileAnalysis>();
            List<string> filesToMove = new List<string>();
            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Time to select a folder",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        + Path.DirectorySeparatorChar,
                ShowNewFolderButton = true
            };
            string targetPath = string.Empty;
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
                    bool found = false;
                    foreach (string definitionFile in dirs)
                    {
                        string definitions = definitionFile.Replace(".xml", "");
                        FileDefinitionReader definitionReader = new FileDefinitionReader();
                        definitionReader.Load(definitions);
                        FileAnalysis changes = DefinitionMatch(contractsParser, definitionReader);
                        if (changes.Errors == 0)
                        {
                            found = true;
                            //es una nueva dependencia hay que copiar archivo de definiciones
                            if(contractsParser.Organization!=definitions)
                            {
                                string destFile = System.IO.Path.Combine(definitionsDirectory, $"{contractsParser.Organization}.xml");
                                System.IO.File.Copy(definitionFile, destFile, true);
                            }
                            //mover el data file to ready
                            filesToMove.Add(dataFile);
                            
                            MessageBox.Show("Match:" + definitionFile);
                            break;
                        }
                        else
                        {
                            listOfFiles.Add(changes);
                        }
                    }
                    if (!found)
                    {
                        FileAnalysis betterFile = listOfFiles.OrderBy(c => c.Errors).First();
                        DefinitionsSetup definitionsSetup = new DefinitionsSetup(betterFile,contractsParser.Columns);
                        definitionsSetup.ShowDialog();
                        if(definitionsSetup.FileSaved)
                        {
                            filesToMove.Add(dataFile);
                        }
                        //WriteToXmlFile<FileAnalysis>($"c:\\temp\\letti\\{contractsParser.Organization}.xml", betterFile);
                        //MessageBox.Show("Best Match:" + betterFile.FileName);
                    }
                }

            }
            foreach(string filetoMove in filesToMove)
            {
                string fileName = Path.GetFileName(filetoMove);
                string destinationFile = $"{targetPath}\\{fileName}";
                System.IO.File.Move(filetoMove, destinationFile);
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
