namespace LoginForm.Services.Dto
{
    public partial class ResponseBase
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccses => (int)StatusCode >= 200 && (int)StatusCode <= 299;
    }
}
