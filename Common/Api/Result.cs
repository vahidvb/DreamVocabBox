namespace Common.Api
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class Result<TData> : Result where TData : class
    {
        public TData Data { get; set; }

    }
}