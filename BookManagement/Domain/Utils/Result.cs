namespace Domain.Utils;

public class Result<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }

    protected Result(bool isSuccess, T data, string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccess = isSuccess;
        Data = data;
    }
    
    public static Result<T> Success(T data) => new (true, data, default);
    public static Result<T> Failure(string error) => new (false, default, error);
}