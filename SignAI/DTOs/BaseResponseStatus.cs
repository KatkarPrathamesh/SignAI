namespace SignAI.DTOs
{
    public class BaseResponseStatus
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object ResponseData { get; set; }
    }
}
