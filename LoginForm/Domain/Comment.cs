using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Domain
{
    public class Comment
    {
        public Comment(string titel,string text)
        {
            this.Titel = titel;
            this.Text = text;
        }

        public int Id { get; set; }

        private string _Titel;

        public string Titel
        {
            get { return _Titel; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Titel is Null , Titel Must Not Null");

                _Titel = value;
            }
        }

        private string _Text;

        public string Text
        {
            get { return _Text; }
            set 
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Titel is Null , Titel Must Not Null");

                if (value.Length < 3 || value.Length >= 500)
                    throw new ArgumentOutOfRangeException("Character of Comment Text Must Between [3-500]");

                _Text = value;
            }
        }
    }
}
