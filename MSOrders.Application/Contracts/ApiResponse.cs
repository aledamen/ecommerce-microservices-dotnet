namespace MSOrders.Application.Contracts
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public List<string>? Details { get; set; }
        public T? Data { get; set; }
    }
}
