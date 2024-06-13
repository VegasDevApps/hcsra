
namespace ApplicationAPI.Errors
{
    public class ApiResponse<T>
    {
        public ApiResponse(T result)
        {
            Result = result;
        }

        public bool Error { get; set; } = false;
        public T Result { get; set; }
    }
}