namespace ApplicationAPI.Errors
{
    public class ApiError
    {
        public ApiError(int statusCode, string message = null)
        {
            Error = true;
            StatusCode = statusCode;
            Message = message ?? GetMessageByCode(statusCode);
        }


        public int StatusCode { get; set; }
        public bool Error { get; private set; }
        public string Message { get; set; }
        private string GetMessageByCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}