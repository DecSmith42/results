// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultOfHandleWithValueExtensions
{
    [Pure]
    public static Result<T> HandleToResult<T>(
        this Result<T> result,
        Func<T> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : Result.From(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result<T> result,
        Func<Task<T>> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : await Result.From(async () => await bindSuccess(), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result<T> result,
        Func<T> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : Result.From(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result<T> result,
        Func<Task<T>> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : await Result.From(async () => await bindSuccess(), exceptionHandler);

    // - - - - -

    [Pure]
    public static Result<T> HandleToResult<T>(
        this Result<T> result,
        Func<T, T> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : Result.From(() => bindSuccess(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result<T> result,
        Func<T, Task<T>> bindSuccess,
        Func<T> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.From(bindFailure, exceptionHandler)
            : await Result.From(async () => await bindSuccess(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result<T> result,
        Func<T, T> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : Result.From(() => bindSuccess(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleToResult<T>(
        this Result<T> result,
        Func<T, Task<T>> bindSuccess,
        Func<Task<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.From(bindFailure, exceptionHandler)
            : await Result.From(async () => await bindSuccess(result.Value), exceptionHandler);

    // - - - - -

    [Pure]
    public static Result<T> HandleResult<T>(
        this Result<T> result,
        Func<Result<T>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result<T> result,
        Func<Task<Result<T>>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(async () => await bindSuccess(), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result<T> result,
        Func<Result<T>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(bindSuccess, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result<T> result,
        Func<Task<Result<T>>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(async () => await bindSuccess(), exceptionHandler);

    // - - - - -

    [Pure]
    public static Result<T> HandleResult<T>(
        this Result<T> result,
        Func<T, Result<T>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(() => bindSuccess(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result<T> result,
        Func<T, Task<Result<T>>> bindSuccess,
        Func<Result<T>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(async () => await bindSuccess(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result<T> result,
        Func<T, Result<T>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : Result.FromResult(() => bindSuccess(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<T>> HandleResult<T>(
        this Result<T> result,
        Func<T, Task<Result<T>>> bindSuccess,
        Func<Task<Result<T>>> bindFailure,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? await Result.FromResult(bindFailure, exceptionHandler)
            : await Result.FromResult(async () => await bindSuccess(result.Value), exceptionHandler);
}
