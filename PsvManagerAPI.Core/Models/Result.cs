using Microsoft.AspNetCore.Mvc;

namespace PsvManagerAPI.Core.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }

        public static Result<T> Success(T value)
        {
            return new Result<T> { IsSuccess = true, Value = value };
        }

        public static Result<T> Failure(ProblemDetails problemDetails)
        {
            return new Result<T> { IsSuccess = false, ProblemDetails = problemDetails };
        }
    }
}
