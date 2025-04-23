// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultHandleWithValueExtensions
{
    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Task<Result> result,
        Func<T> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Task<Result> result,
        Func<Task<T>> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Task<Result> result,
        Func<T> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Task<Result> result,
        Func<Task<T>> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleToResult(bindSuccess, bindFailure, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Task<Result> result,
        Func<Result<T>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Task<Result> result,
        Func<Task<Result<T>>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Task<Result> result,
        Func<Result<T>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Task<Result> result,
        Func<Task<Result<T>>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result.FromResult(result)).HandleResult(bindSuccess, bindFailure, exceptionHandler);
}
