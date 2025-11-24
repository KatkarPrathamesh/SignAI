namespace SignAI.Models
{
    public class ApiResponse
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object? ResponseData { get; set; }
    }

}
