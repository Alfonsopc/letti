using CefSharp.MinimalExample.Wpf.Contracts;
using CefSharp.MinimalExample.Wpf.Entities;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CefSharp.MinimalExample.Wpf.Scrappers
{
    public class ScrapperFactory
    {
        private IScrapperAlgorithm pubScrapper;
        private TextBox searchBox;
        private TextBox stepBox;
        public ScrapperFactory(TextBox searchBox,TextBox stepBox)
        {
            this.searchBox = searchBox;
            this.stepBox = stepBox;
        }

        public IScrapperAlgorithm GetScrapperAlgorithm(Sources source, ChromiumWebBrowser browser)
        {
            IScrapperAlgorithm scrapperAlgorithm = null;
            switch (source)
            {
                case Sources.RegistroPublico:
                    scrapperAlgorithm = GetRegistroPublico(browser,source);              
                    break;
                case Sources.PlataformaTransparencia:
                    scrapperAlgorithm = GetTransparencia(browser, source);
                    break;
                case Sources.Siger:
                    scrapperAlgorithm = GetSiger(browser, source);
                    break;
            }
            return scrapperAlgorithm;
        }

        private IScrapperAlgorithm GetRegistroPublico(ChromiumWebBrowser browser,Sources source)
        {
            //if(pubScrapper==null)
            //{
                pubScrapper = new RegPubScrapper(browser,source,searchBox,stepBox);
            //}
            //else
            //{
            //    pubScrapper.SetSource(source);
            //}
            return pubScrapper;
        }

        private IScrapperAlgorithm GetTransparencia(ChromiumWebBrowser browser, Sources source)
        {
            //if (pubScrapper == null)
            //{
                pubScrapper = new TransparenciaScrapper(browser, source,searchBox,stepBox);
            //}
            //else
            //{
            //    pubScrapper.SetSource(source);
            //}
            return pubScrapper;
        }

        private IScrapperAlgorithm GetSiger(ChromiumWebBrowser browser, Sources source)
        {
            //if (pubScrapper == null)
            //{
                pubScrapper = new SigerScrapper(browser, source,searchBox);
            //}
            //else
            //{
            //    pubScrapper.SetSource(source);
            //}
            return pubScrapper;
        }
    }
}
