using CefSharp.MinimalExample.Wpf.Contracts;
using CefSharp.MinimalExample.Wpf.Entities;
using CefSharp.MinimalExample.Wpf.Scrappers;
using System.Windows;
using System.Windows.Controls;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {


        private ScrapperFactory scrapperFactory;
        private IScrapperAlgorithm scrapperAlgorithm;
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private async void Button_Parse(object sender, RoutedEventArgs e)
        {
            string source=await this.Browser.GetSourceAsync();
            
            Parse(source);
           
        }
        private void Parse(string page)
        {
            string finder = string.Empty;
        }

        private void RPP_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton control = sender as RadioButton;
            switch(control.Name)
            {
                case "RPP":
                    Load(Sources.RegistroPublico);
                    break;            
                case "PT":
                    Load(Sources.PlataformaTransparencia);
                    break;
                case "SG":
                    Load(Sources.Siger);
                    break;
            }
        }
        private void Load(Sources source)
        {
            if(Browser!=null)
            {
                scrapperAlgorithm = scrapperFactory.GetScrapperAlgorithm(source, Browser);
                scrapperAlgorithm.Load();
            }
            
        }

      
       

        private void Click_GetNext(object sender, RoutedEventArgs e)
        {

            scrapperAlgorithm.GetNext();
        }

        private void Click_Login(object sender, RoutedEventArgs e)
        {
            scrapperAlgorithm.Login();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            scrapperFactory = new ScrapperFactory(ToSearch,StepBox);
        }

        private void Click_Next_Step(object sender, RoutedEventArgs e)
        {
            scrapperAlgorithm.NextStep();
        }
    }
}
