using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
namespace LoginForm.Domain
{
    public class UserActivity
    {
        public UserActivity() { }
        public UserActivity(string Ip,string userAgent,string cookie,string url,string body)
        {
            this.IP = Ip;
            this.UserAgent = userAgent;
            this.Cookie = cookie;
            this.Url = url;
            this.Body = body;
            this.CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }

        private string _IP;
        public string IP
        {
            get { return _IP; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("IP Must Be Not Null");

                if (!IPAddress.TryParse(value, out IPAddress Address))
                    throw new ArgumentException("Passed value Is Not IP Address");

                _IP = value;
            }
        }

        private string _UserAgent;
        public string UserAgent
        {
            get { return _UserAgent; }
            private set
            {
                if (value == null)
                    throw new ArgumentException("UserAgent Must Be Not Null");

                _UserAgent = value;
            }
        }

        private string _Cookie;

        public string Cookie
        {
            get { return _Cookie; }
            private set
            {
                if (value == null)
                    throw new ArgumentException("Cookie Must Be Not Null");

                _Cookie = value;
            }
        }

        private string _Url;

        public string Url
        {
            get { return _Url; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Cookie Must Be Not Null");

                _Url = value;
            }
        }
        public string Body { get; set; }

        public DateTime CreatedAt { get;private set; }

    }
}
