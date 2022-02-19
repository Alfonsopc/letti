using CefSharp.MinimalExample.Wpf.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CefSharp.MinimalExample.Wpf.Contracts
{
    public interface IScrapperAlgorithm
    {
       
        bool IsLogged { get; set; }
        void Login();
        Task<bool> NextStep();
        Task<bool> GetNext();
        void Load();
        void SetSource(Sources source);
    }
}
