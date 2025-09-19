namespace MSOrders.Presentation.Contracts
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public List<string>? Details { get; set; }
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? TraceId { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, int statusCode = 200)
            => new ApiResponse<T> { Success = true, StatusCode = statusCode, Data = data };

        public static ApiResponse<T> ErrorResponse(string error, int statusCode = 400, List<string>? details = null)
            => new ApiResponse<T> { Success = false, StatusCode = statusCode, Error = error, Details = details };
    }
}
