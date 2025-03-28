namespace CarWebAPI.Common;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }
    public static Result<T> Success(T result) => new(result);
    public static Result<T> Fail(string errorMessage) => new(false, errorMessage);
    public Result(T? result) : this(true, null, result) { }
    public Result(bool isSuccess, string errorMessage) : this(isSuccess, errorMessage, default) { }
    public Result(bool isSuccess, string? errorMessage, T? data)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Data = data;
    }
}
