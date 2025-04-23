// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultHandleWithValueExtensions
{
    [Pure]
    public static Result<T> HandleToResult<T>(
        this Result result,
        Func<T> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : Result.From(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result result,
        Func<Task<T>> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : await Result.From(async () => await bindSuccess(), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result result,
        Func<T> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : Result.From(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result result,
        Func<Task<T>> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : await Result.From(async () => await bindSuccess(), exceptionHandler);

    // - - - - -

    [Pure]
    public static Result<T> HandleResult<T>(
        this Result result,
        Func<Result<T>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result result,
        Func<Task<Result<T>>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result result,
        Func<Result<T>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result result,
        Func<Task<Result<T>>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(bindSuccess, exceptionHandler);
}
