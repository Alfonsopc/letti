using Letti.Web.Contracts;
using Letti.Web.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Letti.Web.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IUserService userService;

        public AuthStateProvider(IUserService userService)
        {
            this.userService = userService;
            this.userService.OnSessionChanged += (sender, session) => ProcessSession(session);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var session = await userService.GetSession();
            return ProcessSession(session);
        }

        private AuthenticationState ProcessSession(Session session)
        {
            var state = GetSessionState(session);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private AuthenticationState GetSessionState(Session session)
        {
            if(session==null)
            {
                Console.WriteLine("Sesion nula");
                return new AuthenticationState(new ClaimsPrincipal());
            }
            bool doNotContainsToken = string.IsNullOrEmpty(session?.access_token);
            bool isExpired = DateTimeOffset.FromUnixTimeSeconds(session.Expires).DateTime < DateTime.UtcNow;
            if (doNotContainsToken || isExpired) return new AuthenticationState(new ClaimsPrincipal());
            try
            {
                var handler = new JwtSecurityTokenHandler();
                Console.WriteLine("token");
                Console.WriteLine(session.access_token);
                //string test = Base64UrlEncoder.Decode(session.access_token);
                bool isValid=handler.CanReadToken(session.access_token);
               
                Console.WriteLine("can read token");
                Console.WriteLine(isValid);
                //SecurityToken securityToken = handler.ReadToken(session.access_token);
                //Console.WriteLine("read token");
                //Console.WriteLine(securityToken.ToString());
                //JwtSecurityToken token = securityToken as JwtSecurityToken;
                //JwtSecurityToken token = handler.ReadJwtToken(session.access_token);
                //Console.WriteLine("claims");
                //foreach (Claim claim in token.Claims)
                //{
                //    Console.WriteLine(claim.ToString());
                //}
                List<Claim> claims = new List<Claim>();
                //return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "jwt")));
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
            }
            catch(Exception exp)
            {
                Console.WriteLine("error desencriptando el token");
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
               
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }
    }
}
