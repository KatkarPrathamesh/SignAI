namespace SignAI.DTOs
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; } = "200";

        public static OperationResult Ok(string message = "Success")
            => new OperationResult { Success = true, Message = message, StatusCode = "200" };

        public static OperationResult Fail(string message, string statusCode = "400")
            => new OperationResult { Success = false, Message = message, StatusCode = statusCode };
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }

        public static OperationResult<T> Ok(T data, string message = "Success")
            => new OperationResult<T> { Success = true, Message = message, Data = data, StatusCode = "200" };

        public static new OperationResult<T> Fail(string message, string statusCode = "400")
            => new OperationResult<T> { Success = false, Message = message, StatusCode = statusCode };
    }


}
