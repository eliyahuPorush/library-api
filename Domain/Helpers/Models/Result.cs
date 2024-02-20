namespace Domain.Helpers.Models;

public class Result
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get; private set; }
    public string ErrorMessage { get; private set; }

    protected Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static Result Success() => new Result(true, null);
    public static Result Failure(string message) => new Result(false, message);
}