// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultHandleNoValueExtensions
{
    [Pure]
    public static async Task<Result> HandleToResult(
        this Task<Result> result,
        Action bindSuccess,
        Action bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleToResult(
        this Task<Result> result,
        Func<Task> bindSuccess,
        Action bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleToResult(
        this Task<Result> result,
        Action bindSuccess,
        Func<Task> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleToResult(
        this Task<Result> result,
        Func<Task> bindSuccess,
        Func<Task> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result> HandleResult(
        this Task<Result> result,
        Func<Result> bindSuccess,
        Func<Result> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleResult(
        this Task<Result> result,
        Func<Task<Result>> bindSuccess,
        Func<Result> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleResult(
        this Task<Result> result,
        Func<Result> bindSuccess,
        Func<Task<Result>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result> HandleResult(
        this Task<Result> result,
        Func<Task<Result>> bindSuccess,
        Func<Task<Result>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);
}
