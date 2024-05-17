namespace LawyerWebSiteMVC.Utilities;

   public interface IDataResult<T> : IResult
{
    T Data { get; }
}

public class DataResult<T> : Result, IDataResult<T>
{
    public DataResult(T data, bool success, string message) : base(success, message)
    {
        Data = data;
    }

    public T Data { get; }
}

public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data, string message) : base(data, true, message)
    {
    }

    public SuccessDataResult(T data) : base(data, true, null)
    {
    }
}

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(T data, string message) : base(data, false, message)
    {
    }

    public ErrorDataResult(T data) : base(data, false, null)
    {
    }
}

public interface IResult
{
    bool Success { get; }
    string Message { get; }
}

public class Result : IResult
{
    public Result(bool success, string message) : this(success)
    {
        Message = message;
    }

    public Result(bool success)
    {
        Success = success;
    }

    public bool Success { get; }
    public string Message { get; }
}

public static class Messages
{
    public static string SuccessfulLogin = "Login successful";
    public static string UserNotFound = "User not found";
}

