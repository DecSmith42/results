// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultHandleNoValueExtensions
{
    [Pure]
    public static Result HandleToResult(
        this Result result,
        Action bindSuccess,
        Action bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : Result.From(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleToResult(
        this Result result,
        Func<Task> bindSuccess,
        Action bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : await Result.From(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleToResult(
        this Result result,
        Action bindSuccess,
        Func<Task> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : Result.From(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleToResult(
        this Result result,
        Func<Task> bindSuccess,
        Func<Task> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : await Result.From(bindSuccess, exceptionHandler);

    // - - - - -

    [Pure]
    public static Result HandleResult(
        this Result result,
        Func<Result> bindSuccess,
        Func<Result> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleResult(
        this Result result,
        Func<Task<Result>> bindSuccess,
        Func<Result> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleResult(
        this Result result,
        Func<Result> bindSuccess,
        Func<Task<Result>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleResult(
        this Result result,
        Func<Task<Result>> bindSuccess,
        Func<Task<Result>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(bindSuccess, exceptionHandler);
}
