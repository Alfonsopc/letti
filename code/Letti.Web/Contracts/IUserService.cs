using Letti.Web.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Letti.Web.Contracts
{
    public interface IUserService
    {
        event Action<object, Session> OnSessionChanged;

        Task<Session> GetSession();

        Task<bool> Login(string username, string password);

        Task<bool> Refresh();

        Task Logout();
    }
}
