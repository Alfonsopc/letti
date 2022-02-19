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
using System.Windows;
using System.Windows.Controls;

namespace CefSharp.MinimalExample.Wpf.Scrappers
{
    public class RegPubScrapper : IScrapperAlgorithm
    {
        public readonly ChromiumWebBrowser browser;
        public bool IsLogged { get ; set ; }
        private readonly string uri = "https://rppcweb.ebajacalifornia.gob.mx/rppweb/produccion/rppapp/inicio?remoto=1";
        string api = "http://XXXX.net:8020/LettiAPI/api/";
        private string userName = "XXXXX";
        private string password = "XXXXX";
        private int Step;
        private Sources source;
        private IPersonScansService scansService;
        private IContractorService contractorService;
        private IPersonService personService;
        private PersonOfInterest person;
        private HttpClient httpClient;
        private TextBox searchBox;
        private TextBox stepBox;
        private List<Contractor> contractors;
        private List<Property> properties;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        private HtmlNodeCollection cityTables;
        private int cityCounter = 0;
        private bool reloaded = false;

        public RegPubScrapper(ChromiumWebBrowser browser,Sources source,TextBox searchBox,TextBox stepBox)
        {
            this.browser = browser;
            this.source = source;
            contractors = new List<Contractor>();
            properties = new List<Property>();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(api);
            scansService = new PersonScansService(httpClient);
            contractorService = new ContractorService(httpClient);
            personService = new PersonService(httpClient);
            this.searchBox = searchBox;
            this.stepBox = stepBox;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
            IsLogged = false;

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            CheckNextStep();
        }

        public void SetSource(Sources source)
        {
            this.source = source;
        }
        public async Task<bool> GetNext()
        {
            searchBox.Text = string.Empty;
            Step = 0;
            PersonOfInterest person = await scansService.GetNext();
            if(person!=null)
            {
                this.person = person;
                searchBox.Text = person.FullName;
                return true;
            }
            return false;
                
        }

        public void Login()
        {
            Step = 0;
            stepBox.Text = Step.ToString();
            if (NeedsReloading())
            {
                Load();
                return;
            }
            browser.ExecuteScriptAsync("document.getElementById('textfield-1024-inputEl').value=" + '\'' + userName + '\'');
            browser.ExecuteScriptAsync("document.getElementById('textfield-1025-inputEl').value=" + '\'' + password + '\'');
            browser.ExecuteScriptAsync("document.getElementById('button-1029-btnInnerEl').click();");
            
            IsLogged = true;
         
        }

        private bool NeedsReloading()
        {
            Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
            string response = taskHtml.Result;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);
            HtmlNode hasResults = htmlDoc.GetElementbyId("textfield-1024-inputEl");
            return hasResults is null;
        }
        public void Load()
        {       
            if (browser != null)
            {
                browser.Load(uri);
            }
        }

        public void Destroy()
        {
            dispatcherTimer.Stop();
        }

        public async Task<bool> NextStep()
        {
            
            stepBox.Text = Step.ToString();
            switch (Step)
            {
                case 0:
                    Step_0();
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
                    Step_4();
                    break;
                case 5:
                    Step_5();
                    break;
                case 6:
                    await Step_6();
                    break;
            }
            return true;
        }
        private void Step_0()
        {
            CheckNextStep();
            browser.ExecuteScriptAsync("document.getElementsByClassName('icon-buscar')[0].click();");
            Step++;
        }
        private void Step_1()
        {
            browser.ExecuteScriptAsync("document.getElementById('tab-1137-btnWrap').click();");
            
            Step++;
        }

        private void Step_2()
        {
            properties.Clear();
            contractors.Clear();
            StringCleaner stringCleaner = new StringCleaner();
            string nombre = stringCleaner.RemueveAcentos(person.FirstName);
            string apellido = stringCleaner.RemueveAcentos(person.LastName);
            string segundoApellido = stringCleaner.RemueveAcentos(person.SecondLastName);
            browser.ExecuteScriptAsync("document.getElementById('textfield-1113-inputEl').value=" + '\'' + nombre + '\'');
            browser.ExecuteScriptAsync("document.getElementById('textfield-1114-inputEl').value=" + '\'' + apellido + '\'');
            browser.ExecuteScriptAsync("document.getElementById('textfield-1115-inputEl').value=" + '\'' + segundoApellido + '\'');
            browser.ExecuteScriptAsync("document.getElementById('button-1121').click();");

            Step++;
        }

        /// <summary>
        /// Obtiene los datos de las ciudades
        /// </summary>
        public void Step_3()
        {
            try
            {
                Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
                string response = taskHtml.Result;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response);

                // VERIFICA SI SE MOSTRÓ EL MENSAJE DE 'NO ENCONTRADO'
                HtmlNode notFoundMessage = htmlDoc.GetElementbyId("messagebox-1001");
                if (notFoundMessage == null || notFoundMessage.HasClass("x-hidden-offsets")) // Si el mensaje no está en pantalla buscará la información de la persona
                {
                    // SI NO SE MOSTRÓ EL MENSAJE SEGUIRÁ CON EL PROCESO
                    HtmlNode modal = htmlDoc.GetElementbyId("container-1182"); // MODAL                    
                    HtmlNodeCollection Containers = modal.SelectNodes("//div[@class='x-grid-item-container']");
                    HtmlNode divContainer = Containers[0];

                    HtmlNodeCollection tables = divContainer.SelectNodes(".//table[contains(@class, 'x-grid-item')]");
                    cityTables = tables;
                    cityCounter = 0;

                    HtmlNodeCollection tr = cityTables[cityCounter].SelectNodes(".//tr");
                    browser.ExecuteScriptAsync("document.getElementById('" + cityTables[cityCounter].Id + "').childNodes[0].childNodes[0].childNodes[1].click();");

                    Step++;
                }
                else // Si el mensaje de 'no encontrado' está en pantalla
                {
                    // Cerrará el mensaje y pasará hasta el paso 5
                    browser.ExecuteScriptAsync("document.getElementById('button-1005').click();");
                    Step = 6;
                }

            }
            catch (Exception ex)
            {
                Step = 0;
               // MessageBox.Show("Error al extraer las propiedades: " + ex.Message + ":" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Obtiene los datos de propiedades
        /// </summary>
        private void Step_4()
        {
            try
            {
                Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
                string response = taskHtml.Result;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response);

                // SI NO SE MOSTRÓ EL MENSAJE SEGUIRÁ CON EL PROCESO

                // Obtiene el listado de propiedades
                int colCount = 0;
                HtmlNode modal = htmlDoc.GetElementbyId("container-1182"); // MODAL                    
                HtmlNodeCollection Containers = modal.SelectNodes("//div[@class='x-grid-item-container']");
                HtmlNode divContainer = Containers[1];

                HtmlNodeCollection tables = divContainer.SelectNodes(".//table[contains(@class, 'x-grid-item')]");

                if (tables != null)
                {
                    foreach (HtmlNode table in tables)
                    {
                        Property property = new Property();
                        HtmlNodeCollection tds = table.SelectNodes(".//td[contains(@class, 'x-wrap-cell')]"); // obtiene los rows con datos de la persona
                        property.OwnerId = person.Id;
                        property.Source = "Registro Público de la Propiedad B.C.";
                        foreach (HtmlNode td in tds)
                        {
                            HtmlNode column = td.SelectSingleNode(".//div[contains(@class, 'x-grid-cell-inner')]"); // obtiene el div que contiene el valor de la columna
                            var valor = column.InnerText;
                            valor = valor == "&nbsp;" ? "" : valor;

                            switch (colCount)
                            {
                                case 0: // Folio Real                                
                                    property.Record = valor;
                                    break;
                                case 1: // Inmutable                                
                                    property.Description = valor;
                                    break;
                                case 2: // Colonia/Reg                                
                                    property.Colony = valor;
                                    break;
                                case 3: // Tipo Dueño                                
                                    property.OwnerType = valor;
                                    break;
                                case 4: // Porcentaje                                
                                    break;
                                case 5: // Domicilio                                
                                    break;
                                case 6: // Clave Catastral                                
                                    property.CadastralCode = valor;
                                    break;
                                case 7: // CURT                                
                                    break;
                                case 8: // Denomina                                
                                    break;
                                case 9: // Partida
                                    property.Certificate = valor;
                                    break;
                                case 10: // Sección
                                    break;
                                case 11: // Tomo
                                    break;
                                case 12: // Inscripción
                                    break;
                                case 13: // Acto
                                    break;
                            }

                            colCount++;
                        }

                        properties.Add(property);
                        colCount = 0;
                    }
                }

                // Abre el tab de sociedades para que cargue la información que se necesitará en el siguiente paso
                HtmlNode tabSociedades = modal.SelectSingleNode("//a[@class='x-tab x-unselectable x-box-item x-tab-default x-top x-tab-top x-tab-default-top' and @style='right: auto; left: 333px; top: 0px; margin: 0px;']");
                if (tabSociedades != null)
                {
                    browser.ExecuteScriptAsync("document.getElementById('" + tabSociedades.Id + "').click();");
                }
              

                Step++;

            }
            catch(Exception ex) 
            {
                Step = 0;
                //MessageBox.Show("Error al extraer las propiedades: " + ex.Message+":"+ex.StackTrace);
            }            
        }

        /// <summary>
        /// Obtiene listado de sociedades
        /// </summary>
        private void Step_5()
        {
            try
            {
                Task<String> taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
                string response = taskHtml.Result;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(response);

              
                int colCount = 0;

                HtmlNode modal = htmlDoc.GetElementbyId("container-1182"); // MODAL                                
                HtmlNodeCollection Containers = modal.SelectNodes("//div[@class='x-grid-item-container']");
                HtmlNode divContainer = Containers[2];

                HtmlNodeCollection tables = divContainer.SelectNodes(".//table[contains(@class, 'x-grid-item')]");                

                if (tables != null)
                {
                    foreach (HtmlNode table in tables)
                    {
                        Contractor contractor = new Contractor();
                        HtmlNodeCollection tds = table.SelectNodes(".//td[contains(@class, 'x-wrap-cell')]"); // obtiene los rows con datos de la persona
                        foreach (HtmlNode td in tds)
                        {
                            HtmlNode column = td.SelectSingleNode(".//div[contains(@class, 'x-grid-cell-inner')]"); // obtiene el div que contiene el valor de la columna
                            var valor = column.InnerText;
                            valor = valor == "&nbsp;" ? "" : valor;

                            switch (colCount)
                            {
                                case 0: // Sociedad
                                    contractor.OfficialName = valor;
                                    break;
                                case 1: // Estatus
                                    break;
                                case 2: // Municipio
                                    contractor.Record = valor;
                                    break;
                                case 3: // Partida
                                    //contractor.Record = valor;
                                    break;
                                case 4: // Sección
                                    break;
                                case 5: // Tomo
                                    break;
                                case 6: // Inscripción
                                    contractor.FoundedOn = strToDatetime(valor);
                                    break;
                                case 7: // Fecha de Inscripción                                
                                    break;
                                case 8: // Acto
                                    break;
                            }

                            colCount++;
                        }


                        contractors.Add(contractor);
                        colCount = 0;
                    }
                }

                // Limpia los controles para la siguiente persona                
                browser.ExecuteScriptAsync("document.getElementById('textfield-1113-inputEl').value=" + '\'' + "" + '\'');
                browser.ExecuteScriptAsync("document.getElementById('textfield-1114-inputEl').value=" + '\'' + "" + '\'');
                browser.ExecuteScriptAsync("document.getElementById('textfield-1115-inputEl').value=" + '\'' + "" + '\'');


                if (cityCounter == (cityTables.Count - 1)) // Si ya se recorrieron todas las ciudades pasará al paso 6
                {
                    Step = 6;                    
                } 
                else // Si no se han recorrido todas las ciudades aumentará el contador y volverá al paso 4
                {
                    cityCounter++;
                    browser.ExecuteScriptAsync("document.getElementById('" + cityTables[cityCounter].Id + "').childNodes[0].childNodes[0].childNodes[1].click();");

                    // Abre el tab de propiedades
                    HtmlNode tabPropiedades = modal.SelectSingleNode("//a[@class='x-tab x-unselectable x-box-item x-tab-default x-top x-tab-top x-tab-default-top' and @style='right: auto; left: 2px; top: 0px; margin: 0px;']");
                    browser.ExecuteScriptAsync("document.getElementById('" + tabPropiedades.Id + "').click();");
                    Step = 4;
                }
            }
            catch(Exception ex) 
            {
                Step = 0;
                //MessageBox.Show("Error al extraer las sociedades: " + ex.Message);
            }
        }

        private async Task Step_6()
        {
            foreach (Contractor contractor in contractors)
            {
                Contractor dataContractor = await contractorService.Create(contractor);
                if (dataContractor != null && dataContractor.Id > 0)
                {
                    PersonCompanyRelationship personCompanyRelationship = new PersonCompanyRelationship();
                    personCompanyRelationship.CompanyId = dataContractor.Id;
                    personCompanyRelationship.PersonOfInterestId = person.Id;
                    personCompanyRelationship.Description = "Accionista";
                    personCompanyRelationship.ContractorRelationshipTypeId = (int)ContractorRelationship.Shareholder;
                    await contractorService.AddRelationship(personCompanyRelationship);
                }
            }
            foreach (Property property in properties)
            {
                Property dataProperty = await personService.AddProperty(property);
                if (dataProperty == null || dataProperty.Id <= 0)
                {
                    //retry
                }
            }
            await scansService.MarkAsScanned(person.Id);

            // Obtiene a la siguiente persona
            await GetNext();

            Step = 2;
        }

       
        
      
        public async void CheckNextStep()
        {
            Task<String> taskHtml;
            string response;
            HtmlNode modal;
            HtmlNode divContainer;
            HtmlNodeCollection Containers;

            try
            {
                bool getLogged = await IsUserLogged();
                if (!IsLogged) // Si no está logueado
                {
                    // Verifica si el contról de nombre de usuario está en pantalla
                    if (await CheckForElementById("textfield-1024-inputEl"))
                    {
                        await GetNext();
                        Login();
                        Step = 0;
                    }
                    else
                    {
                        Step = 0;
                        if (!reloaded)
                        {   //reload only once
                            reloaded = true;
                            browser.Load(uri);
                        }
                    }
                }
                else // Si está logueado
                {
                    reloaded = false;
                    switch (Step)
                    {
                        case 0:
                            if (await CheckForElementByClassName("icon-buscar")) // Verifica si el botón de búsqueda de persona está en pantalla
                            {
                                await NextStep();
                            }
                            break;
                        case 1:
                            if (await CheckForElementById("tab-1137-btnWrap")) // Verifica si el tab de Propietarios existe
                            {
                                await NextStep();
                            }
                            break;
                        case 2:
                            if (await CheckForElementById("textfield-1113-inputEl")) // Verifica si el control de NOMBRE de persona existe
                            {
                                await NextStep();
                            }
                            break;
                        case 3: // Verifica si se mostró el modal de 'no encontrado' o el listado de ciudades
                            taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
                            response = taskHtml.Result;

                            var htmlDoc_City = new HtmlDocument();
                            htmlDoc_City.LoadHtml(response);

                            // VERIFICA SI SE MOSTRÓ EL MENSAJE DE 'NO ENCONTRADO'
                            HtmlNode notFoundMessage = htmlDoc_City.GetElementbyId("messagebox-1001");
                            if (notFoundMessage == null || notFoundMessage.HasClass("x-hidden-offsets")) 
                            {
                                modal = htmlDoc_City.GetElementbyId("container-1182"); // MODAL
                                if (modal == null)
                                {
                                    IsLogged = false;
                                    Step = 0;
                                    Load();
                                }
                                else
                                {
                                    Containers = modal.SelectNodes("//div[@class='x-grid-item-container']");
                                    if (Containers.Count < 2)
                                    {
                                        Step = 6;
                                        await NextStep();
                                    }
                                    divContainer = Containers[0];

                                    if (divContainer != null)
                                    {
                                        await NextStep();
                                    }
                                }
                            }
                            else
                            {
                                await NextStep();
                            }

                            break;
                        case 4: // Verifica si se mostró el listado de propiedades de la persona
                            taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
                            response = taskHtml.Result;

                            var htmlDoc_Prop = new HtmlDocument();
                            htmlDoc_Prop.LoadHtml(response);

                            modal = htmlDoc_Prop.GetElementbyId("container-1182"); // MODAL
                            if(modal==null)
                            {
                                await GetNext();
                                break;
                            }
                            Containers = modal.SelectNodes("//div[@class='x-grid-item-container']");
                            if (Containers.Count < 2)
                            {
                                Step = 6;
                                await NextStep();
                            }
                            divContainer = Containers[1];

                            if (divContainer != null)
                            {
                                await NextStep();
                            }

                            break;
                        case 5: // Verifica si se mostró el listado de sociedades de la persona
                            taskHtml = browser.GetBrowser().MainFrame.GetSourceAsync();
                            response = taskHtml.Result;

                            var htmlDoc_Soc = new HtmlDocument();
                            htmlDoc_Soc.LoadHtml(response);

                            modal = htmlDoc_Soc.GetElementbyId("container-1182"); // MODAL   
                            if(modal==null)
                            {                              
                                await GetNext();
                                break;
                            }
                            Containers = modal.SelectNodes("//div[@class='x-grid-item-container']");
                            divContainer = Containers[2];                            

                            if (divContainer != null)
                            {
                                await NextStep();
                            }
                            break;
                        case 6:
                            await NextStep();
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Step = 0;
                //MessageBox.Show("Error al ejecutar el paso " + Step + ": " + ex.Message);
            }            

                      
        }

        public async Task<bool> CheckForElementById(string elementId)
        {
            bool Exist = false;
            string script;

            script = string.Format("$('#{0}').length;", elementId);
            await browser.EvaluateScriptAsync(script).ContinueWith(x =>
            {
                var response = x.Result;

                if (response.Success && response.Result != null)
                {
                    var resultres = Convert.ToString(response.Result);
                    if (resultres == "1")
                    {
                        Exist = true;
                    }
                    else
                    {
                        Exist = false;
                    }
                }
                else
                {
                    Exist = false;
                }
            });

            return Exist;
        }

        public async Task<bool> CheckForElementByClassName(string elementClass)
        {
            bool Exist = false;
            string script;

            script = string.Format("$('.{0}').length;", elementClass);
            await browser.EvaluateScriptAsync(script).ContinueWith(x =>
            {
                var response = x.Result;

                if (response.Success && response.Result != null)
                {
                    var resultres = Convert.ToString(response.Result);
                    if (resultres == "1")
                    {
                        Exist = true;
                    }
                    else
                    {
                        Exist = false;
                    }
                }
                else
                {
                    Exist = false;
                }
            });

            return Exist;
        }


        private DateTime strToDatetime(string val)
        {
            string[] dateArray = val.Split(' ');
            //scrapped for culture dependency
            //string dateStr = dateArray[0] + "/" + dateArray[2] + "/" + dateArray[4];
            //DateTime date = Convert.ToDateTime(dateStr);
            //return date;
            int day = Convert.ToInt32(dateArray[0]);
            int month = GetMonthNumber(dateArray[2]);
            int year = Convert.ToInt32(dateArray[4]);
            DateTime date = new DateTime(year, month, day);

            return date;
        }

        private int GetMonthNumber(string val)
        {
            val = val.ToUpper();
            switch (val)
            {
                case "ENERO":
                    return 1;
                case "FEBRERO":
                    return 2;
                case "MARZO":
                    return 3;
                case "ABRIL":
                    return 4;
                case "MAYO":
                    return 5;
                case "JUNIO":
                    return 6;
                case "JULIO":
                    return 7;
                case "AGOSTO":
                    return 8;
                case "SEPTIEMBRE":
                    return 9;
                case "OCTUBRE":
                    return 10;
                case "NOVIEMBRE":
                    return 11;
                case "DICIEMBRE":
                    return 12;
                default:
                    return 1;
            }
        }

        private async Task<bool> IsUserLogged()
        {
            bool Exist = false;
            string script;
            string elementId = "password";
            script = string.Format("$('#{0}').length;", elementId);
            await browser.EvaluateScriptAsync(script).ContinueWith(x =>
            {
                var response = x.Result;

                if (response.Success && response.Result != null)
                {
                    var resultres = Convert.ToString(response.Result);
                    if (resultres == "1")
                    {
                        Exist = true;
                    }
                    else
                    {
                        Exist = false;
                    }
                }
                else
                {
                    Exist = false;
                }
            });

            return !Exist;
        }
    }
}