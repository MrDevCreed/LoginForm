using System;

namespace LoginForm.Models
{
    public class ErrorViewModel
    {
        public string Requestid { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(Requestid);
    }
}
