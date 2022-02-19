using System;
using System.Collections.Generic;
using System.Text;

namespace CefSharp.MinimalExample.Wpf.Helpers
{
    public class StringCleaner
    {
        public string RemueveAcentos(string source)
        {
            string response = string.Empty;
            if(source is null)
            {
                return response;
            }
            //response = source.Replace('a', 'a');
            //response = source.Replace('é', 'e');
            //response = source.Replace('í', 'i');
            //response = source.Replace('ó', 'o');
            //response = source.Replace('ú', 'u');

            response = source.Replace('á', 'a');
            response = response.Replace('é', 'e');
            response = response.Replace('í', 'i');
            response = response.Replace('ó', 'o');
            response = response.Replace('ú', 'u');

            response = response.Replace('Á', 'A');
            response = response.Replace('É', 'E');
            response = response.Replace('Í', 'I');
            response = response.Replace('Ó', 'O');
            response = response.Replace('Ú', 'U');
            return response;
        }
    }
}
