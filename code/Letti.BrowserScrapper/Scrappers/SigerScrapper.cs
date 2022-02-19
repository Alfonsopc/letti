using CefSharp.MinimalExample.Wpf.Contracts;
using CefSharp.MinimalExample.Wpf.Entities;
using CefSharp.Wpf;
using Leeti.Algorithms;
using Letti.Model;
using Letti.Parsers;
using Letti.ServiceProxies.Contracts;
using Letti.ServiceProxies.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CefSharp.MinimalExample.Wpf.Scrappers
{
    public class SigerScrapper : IScrapperAlgorithm
    {
        public readonly ChromiumWebBrowser browser;
        public bool IsLogged { get; set; }
        private readonly string uri = "https://rpc.economia.gob.mx/siger2/xhtml/login/login.xhtml";
        string api = "http://XXXX.net:8020/LettiAPI/api/";
        private int Step;
        private Sources source;
        private string userName = "eli@newsweekbaja.com";
        private string password = "EnRg6750J0V9yB";
        private Contractor contractor;
        private HttpClient httpClient;
        private TextBox searchBox;
        private IContractorScansService scansService;
        private readonly Stack<SiegerCompanyRecord> companyRecordsToScan;
        private readonly SigerParser sigerParser;
        public SigerScrapper(ChromiumWebBrowser browser,Sources source, TextBox searchBox)
        {
            this.browser = browser;
            this.source = source;
            this.searchBox = searchBox;
            companyRecordsToScan = new Stack<SiegerCompanyRecord>();
            sigerParser = new SigerParser();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(api);
            scansService = new ContractorScansService(httpClient);
        }
        public void SetSource(Sources source)
        {
            this.source = source;
        }
        public async Task<bool> GetNext()
        {
            searchBox.Text = string.Empty;
            Step = 0;
            Contractor contractor = await scansService.GetNext();
            if (contractor != null)
            {
                this.contractor = contractor;
                searchBox.Text = contractor.OfficialName;
                return true;
            }
            return false;
        }

        public void Login()
        {

            // browser.ExecuteScriptAsync("document.getElementById('j_idt9:ingresaSiger').click();");
            browser.ExecuteScriptAsync("document.getElementById('formulario:usuario').value=" + '\'' + userName + '\'');
            browser.ExecuteScriptAsync("document.getElementById('formulario:contrasenia').value=" + '\'' + password + '\'');
            browser.ExecuteScriptAsync("document.getElementById('formulario:ingresarBtn').click();");
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
            switch (Step)
            {
                case 0:
                    Step_0();
                    break;
                case 1:
                    Step_1();
                    break;
                case 2:
                    await Step_2();
                    break;
                case 3:
                    await Step_3();
                    break;


            }
            return true;
        }

        private void Step_0()
        {
            string nextUri = "https://rpc.economia.gob.mx/siger2/xhtml/index.xhtml";
            browser.Load(nextUri);
            Step++;
        }
        private void Step_1()
        {
            browser.ExecuteScriptAsync("document.getElementById('formValores:rsocial').value=" + '\'' + contractor.OfficialName + '\'');
      
            browser.ExecuteScriptAsync("document.getElementById('formValores:consultaa').click();");
            Step++;
        }

        private async Task Step_2()
        {
            string source=await browser.GetSourceAsync();
            List<SiegerCompanyRecord> companies=sigerParser.GetCompanies(source);
            foreach(SiegerCompanyRecord companyRecord in companies)
            {
                this.companyRecordsToScan.Push(companyRecord);
            }
            Step++;
        }
        private async Task Step_3()
        {
            if(companyRecordsToScan.Count==0)
            {
                Step = 1;
                return;
            }
            else
            {
                SiegerCompanyRecord companyRecord = companyRecordsToScan.Pop();
                string script = "document.getElementsByName('formValores:dataTableCaratula_radio')[" + companyRecord.ControlIndex.ToString() + "].click()";
                browser.ExecuteScriptAsync(script);
                browser.ExecuteScriptAsync("document.getElementById('formValores:revisarPago').click();");
                Step++;
            }
        }
        private async Task Step_5()
        {
            await scansService.MarkAsScanned(contractor.Id);
            await GetNext();
            Step = 1;
        }
    }
}
