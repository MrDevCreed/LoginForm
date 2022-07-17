using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Domain
{
    public class Comment
    {
        public Comment() { }

        public Comment(string title, string text,Account from)
        {
            this.Title = title;
            this.Text = text;
            this.From = from;
            this.CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Title is Null , Title Must Not Null");

                _Title = value;
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

        public virtual Account From { get; set; }

        public DateTime CreatedAt { get; private set; }
    }
}
