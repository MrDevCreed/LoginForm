using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.ViewModels
{
    public class UserActivityViewModel
    {
        public UserActivityViewModel() { }
        public UserActivityViewModel(string Ip, string userAgent, string cookie, string url,string body)
        {
            this.IP = Ip;
            this.UserAgent = userAgent;
            this.Cookie = cookie;
            this.Url = url;
            this.Body = body;
        }

        public string IP { get; set; }

        public string UserAgent { get; set; }

        public string Cookie { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }
    }
}
