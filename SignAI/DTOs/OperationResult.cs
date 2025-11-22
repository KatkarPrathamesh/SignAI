namespace SignAI.DTOs
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string StatusCode { get; set; } = "";
        public string Message { get; set; } = "";
        public static OperationResult Ok(string message = "Success") => new OperationResult { Success = true, StatusCode = "200", Message = message };
        public static OperationResult Fail(string message, string statusCode = "500") => new OperationResult { Success = false, StatusCode = statusCode, Message = message };
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
        public static OperationResult<T> Ok(T data, string message = "Success") => new OperationResult<T> { Success = true, StatusCode = "200", Message = message, Data = data };
        public new static OperationResult<T> Fail(string message, string statusCode = "500") => new OperationResult<T> { Success = false, StatusCode = statusCode, Message = message };
    }

}
