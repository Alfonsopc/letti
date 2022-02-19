using System;
using System.Collections.Generic;
using System.Text;

namespace Letti.Web.Entities
{
    public class Session
    {
        //public string AccessToken { get; set; }

        //public string RefreshToken { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }

        public long Expires { get; set; }
        //public Session(string serviceResponse)
        //{
        //    int indexOf = serviceResponse.IndexOf(':');
        //    int nextIndex = serviceResponse.IndexOf("token_type");
        //    AccessToken = serviceResponse.Substring(indexOf + 2, nextIndex - indexOf - 5);

        //    indexOf = serviceResponse.IndexOf("expires_in");
        //    indexOf = serviceResponse.IndexOf(':', indexOf);
        //    nextIndex = serviceResponse.IndexOf("refresh_token");
        //    string tmtExpire = serviceResponse.Substring(indexOf + 1, nextIndex - indexOf - 3);
        //    long duration = long.Parse(tmtExpire);
        //    DateTime foo = DateTime.UtcNow;
        //    Expires = ((DateTimeOffset)foo).ToUnixTimeSeconds()+duration;

        //    indexOf = serviceResponse.IndexOf("refresh_token");
        //    indexOf = serviceResponse.IndexOf(':', indexOf);
        //    nextIndex = serviceResponse.Length;
        //    RefreshToken = serviceResponse.Substring(indexOf + 1, nextIndex - indexOf - 3);

        //}
        public Session()
        {

        }

    }
}
