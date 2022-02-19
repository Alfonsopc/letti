using CefSharp.MinimalExample.Wpf.Contracts;
using CefSharp.MinimalExample.Wpf.Entities;
using CefSharp.MinimalExample.Wpf.Helpers;
using CefSharp.Wpf;
using HtmlAgilityPack;
using Letti.Model;
using Letti.Model.Enums;
using Letti.ServiceProxies.Contracts;
using Letti.ServiceProxies.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.IO;

namespace CefSharp.MinimalExample.Wpf.Scrappers
{
    public class TransparenciaScrapper : IScrapperAlgorithm
    {
        public readonly ChromiumWebBrowser browser;
        public bool IsLogged { get; set; }
        private readonly string uri = "https://consultapublicamx.inai.org.mx/vut-web/faces/view/consultaPublica.xhtml#sujetosObligados";
        string api = "http://XXXXXX.net:8020/LettiAPI/api/";
        private int Step;
        private bool firstStep;
        private Sources source;
        private HttpClient httpClient;
        private IOrganizationService organizationService;
        private IContractsService contractsService;
        private List<Organization> organizations;
        private TextBox searchBox;
        private TextBox stepBox;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        private int organizationIndex = 0;
        private int organizationsCount = 0;
        private List<string> missingOrganizations = new List<string>();
        public TransparenciaScrapper(ChromiumWebBrowser browser, Sources source, TextBox searchBox,TextBox stepBox)
        {
            this.browser = browser;
            this.source = source;
            this.searchBox = searchBox;
            this.stepBox = stepBox;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(api);
            organizationService = new OrganizationService(httpClient);
            contractsService = new ContractsService(httpClient);
            organizations = new List<Organization>();
            Step = 0;
            firstStep = true;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 20);
        }
        public void SetSource(Sources source)
        {
            this.source = source;
        }
        public async Task<bool> GetNext()
        {
          
            if (!organizations.Any())
            {
                var response = await organizationService.GetAll();
                if (response != null && response.Any())
                {
                    organizations.AddRange(response);
                    searchBox.Text = organizations.First().OrganizationName;

                }
                organizationIndex = 1;
                return true;
            }
            else
            {
                organizationIndex++;
                if(organizationIndex<organizationsCount)
                {                                     
                    return true;
                }
                else
                {
                    WriteToXmlFile<List<string>>($"c:\\temp\\dependencias_faltantes.xml", missingOrganizations);
                    dispatcherTimer.Stop();
                    return false;
                }
              
            }

        }

        public void Login()
        {

        }
        public void Load()
        {

            if (browser != null)
            {
                browser.Load(uri);
            }
        }
        public async Task<bool> NextStep()
        {
            stepBox.Text = Step.ToString();
            switch (Step)
            {
                case 0:
                    await Step_0();
                    break;
                case 1:
                    Step_1();
                    break;
                case 2:
                    Step_2();
                    break;
                case 3:
                    Step_3();
                    break;
                case 4:               
                    await Step_4();
                    break;
                case 5:
                    Step_5();
                    break;
                case 6:
                    Step_6();
                    break;
                case 7:
                    Step_7();
                    break;
                case 8:
                    Step_8();
                    break;
                case 9:
                    await Step_9();
                    break;
                case 10:
                    await Step_10();
                    break;


            }
            return true;
        }
        //seleciona primera dependencia y settea número de dependencias
        private async Task Step_0()
        {
            missingOrganizations.Clear();
            
            browser.ExecuteScriptAsync("document.getElementsByClassName('seleccionSujetoObligado')[0].click();");
            await GetNext();
            Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
            string response = taskHtml.Result;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            HtmlNode hasResults = htmlDoc.GetElementbyId("tooltipInst");
            if (hasResults != null)
            {
                var nodeCollection = hasResults.SelectNodes(".//li");
                if(nodeCollection!=null)
                {
                    organizationsCount = nodeCollection.Count;
                }
                else
                {
                    organizationsCount = 175;
                }
                
             }
            dispatcherTimer.Start();
            string pathIndex = $"{Directory.GetCurrentDirectory()}\\iteration.txt";
            if (File.Exists(pathIndex))
            {
                organizationIndex = ReadFromXmlFile<int>(pathIndex);
                Step = 9;
                await MoveToNextOrganization();
            }
            else
            {
                Step++;
            }
           
           
        }
        //selecciona año y navega a los contratos
        private void Step_1()
        {
            //esto es el año, hay que tomarlo del UI
            browser.ExecuteScriptAsync("document.getElementById('formEntidadFederativa:cboEjercicio').selectedIndex = 1");
            browser.ExecuteScriptAsync("document.getElementById('formEntidadFederativa:cboEjercicio').onchange();");
            browser.ExecuteScriptAsync("document.getElementsByClassName('grid6Obligaciones')[5].click();");
            Step++;
        }
        //selecciona los cuatro trimestres
        private void Step_2()
        {
            string checkbox = "document.getElementById('formInformacionNormativa:checkPeriodos:4').click();";
            browser.ExecuteScriptAsync(checkbox);
            //await GetNext();
            //string checkbox = "document.getElementById('formInformacionNormativa:checkPeriodos:0').click();";
            //browser.ExecuteScriptAsync(checkbox);
            //checkbox = "document.getElementById('formInformacionNormativa:checkPeriodos:1').click();";
            //browser.ExecuteScriptAsync(checkbox);
            //checkbox = "document.getElementById('formInformacionNormativa:checkPeriodos:2').click();";
            //browser.ExecuteScriptAsync(checkbox);
           // checkbox = "document.getElementById('formInformacionNormativa:checkPeriodos:3').click();";
            //browser.ExecuteScriptAsync(checkbox);
          
            Step++;
        }
        //ejecuta consulta
        private void Step_3()
        {
            Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
            string response = taskHtml.Result;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            List<HtmlNode> modal = htmlDoc.DocumentNode.SelectNodes("//a").Where(x => x.InnerHtml.Contains("CONSULTAR")).ToList();
           if(modal.Any())
            {
                string id=modal.First().Id;
                string button = "document.getElementById(" + "'" + id + "').click();";
                browser.ExecuteScriptAsync(button);
            }

            //document.getElementsByClassName('glyphicon-chevron-down')[1].click()
            browser.ExecuteScriptAsync("document.getElementsByClassName('botonRealizarConsulta')[0].click();");
            Step++;
        }
        private async Task Step_4()
        {
            Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
            string response = taskHtml.Result;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            // Verifica si la consulta tuvo resultados
            HtmlNode hasResults = htmlDoc.GetElementbyId("integraInformacion_wrapper");
            if(hasResults==null)
            {
                Step = 9;
            }
            else
            {
                browser.ExecuteScriptAsync("document.getElementsByClassName('botonRealizarConsulta')[3].click();");
                Step++;
            }
           
        }
        private void Step_5()
        {
            browser.ExecuteScriptAsync("document.getElementsByClassName('icon-excel-correo')[0].click();");       
            Step++;
        }
        private void Step_6()
        {
            string email = "letti@borderhub.org";
            browser.ExecuteScriptAsync("document.getElementById('formEnvioInfoCorreo:email').value=" + '\'' + email + '\'');
            browser.ExecuteScriptAsync("document.getElementById('formEnvioInfoCorreo:email').onkeyup();");
            Step++;
        }
        private void Step_7()
        {
           
           browser.ExecuteScriptAsync("document.getElementById('formEnvioInfoCorreo:btnEnviarCorreo').click();");
           Step++;
        }
        private void Step_8()
        {
            
            
            browser.ExecuteScriptAsync("document.getElementsByClassName('closeModal')[8].click();");
            Step++;
           

        }
        private async Task Step_9()
        {
            if (firstStep)
            {
                firstStep = false;
                browser.ExecuteScriptAsync("document.getElementsByClassName('containerCheck')[1].click();");
                Step = 2;
            }
            else
            {
                string pathIndex =$"{Directory.GetCurrentDirectory()}\\iteration.txt";
                WriteToXmlFile<int>(pathIndex, organizationIndex);
                WriteToXmlFile<List<string>>($"c:\\temp\\dependencias_faltantes.xml", missingOrganizations);
                await MoveToNextOrganization();
            }
            
        }
        public async Task Step_10()
        {
            Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
            string response = taskHtml.Result;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            
            HtmlNode hasResults = htmlDoc.GetElementbyId("tooltipInst");
            if (hasResults != null)
            {
                var nodeCollection = hasResults.SelectNodes(".//li").Where(c => c.HasClass("selected"));
                HtmlNode selected = nodeCollection.First();
                string selectedOrganization = selected.InnerText.Trim();
                Organization dataOrganization = organizations.Where(c => c.OrganizationName == selectedOrganization).FirstOrDefault();              
                if (dataOrganization != null)
                {
                    searchBox.Text = dataOrganization.OrganizationName;
                    firstStep = true;
                    Step = 1;
                }
                else
                {
                    missingOrganizations.Add(selectedOrganization);
                    Step = 9;
                }
                HtmlNode sinObligaciones = htmlDoc.GetElementbyId("modalSinObligaciones");
                if (sinObligaciones != null && !sinObligaciones.Closed)
                {
                    browser.ExecuteScriptAsync("document.getElementsByClassName('closeModal simulalink')[0].click();");
                    firstStep=false;
                    Step = 9;
                }
            }
        }
        private async Task MoveToNextOrganization()
        {
            bool continueReading= await GetNext();
            if(continueReading)
            {

                string command = $"document.getElementById('formEntidadFederativa:cboSujetoObligado').selectedIndex={organizationIndex};";
                browser.ExecuteScriptAsync(command);
                command = "document.getElementById('formEntidadFederativa:cboSujetoObligado').onchange();";
                browser.ExecuteScriptAsync(command);
                Step++;              
            }
            
        }
        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            await NextStep();
        }

        public void Destroy()
        {
            dispatcherTimer.Stop();
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
        public static int ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            int lastValue = -1;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                object storedObject=serializer.Deserialize(reader);
                if(storedObject is int)
                {
                    lastValue = (int)storedObject;
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return lastValue;
        }
        /*
        private decimal ContractValue(string value)
        {
            value = value.Substring(1);
            decimal contractValue;
            value = value.Replace(",","");
            bool succesfull = decimal.TryParse(value, out contractValue);
            return succesfull ? contractValue : 0.0m;
        }
        private DateTime InitDate(string initDate)
        {
            return DateTime.Now.AddDays(-30);
        }
        private DateTime EndDate(string endDate)
        {
            return DateTime.Now.AddDays(-30);
        }*/
            //private void Step_1()
            //{
            //    browser.ExecuteScriptAsync("document.getElementById('estadoFederacion').value='Baja California';");
            //    Step++;
            //}
            //private void Step_2()
            //{
            //    browser.ExecuteScriptAsync("document.getElementById('idSujetoObligado').selectedIndex=3;");
            //    Step++;
            //}
    }
}
