// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultOfHandleNoValueExtensions
{
    [Pure]
    public static async Task<Result> HandleToResult<T>(this Task<Result<T>> result, Action handleSuccess, Action handleFailure) =>
        (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Task<Result<T>> result, Func<Task> handleSuccess, Action handleFailure) =>
        await (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Task<Result<T>> result, Action handleSuccess, Func<Task> handleFailure) =>
        await (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Task<Result<T>> result, Func<Task> handleSuccess, Func<Task> handleFailure) =>
        await (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    // - - - - -

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Task<Result<T>> result, Action<T> handleSuccess, Action handleFailure) =>
        (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Task<Result<T>> result, Func<T, Task> handleSuccess, Action handleFailure) =>
        await (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Task<Result<T>> result, Action<T> handleSuccess, Func<Task> handleFailure) =>
        await (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleToResult<T>(
        this Task<Result<T>> result,
        Func<T, Task> handleSuccess,
        Func<Task> handleFailure) =>
        await (await Result.FromResult(result)).HandleToResult(handleSuccess, handleFailure);

    // - - - - -

    [Pure]
    public static async Task<Result> HandleResult<T>(this Task<Result<T>> result, Func<Result> handleSuccess, Func<Result> handleFailure) =>
        (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Task<Result<T>> result,
        Func<Task<Result>> handleSuccess,
        Func<Result> handleFailure) =>
        await (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Task<Result<T>> result,
        Func<Result> handleSuccess,
        Func<Task<Result>> handleFailure) =>
        await (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Task<Result<T>> result,
        Func<Task<Result>> handleSuccess,
        Func<Task<Result>> handleFailure) =>
        await (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);

    // - - - - -

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Task<Result<T>> result,
        Func<T, Result> handleSuccess,
        Func<Result> handleFailure) =>
        (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Task<Result<T>> result,
        Func<T, Task<Result>> handleSuccess,
        Func<Result> handleFailure) =>
        await (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Task<Result<T>> result,
        Func<T, Result> handleSuccess,
        Func<Task<Result>> handleFailure) =>
        await (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Task<Result<T>> result,
        Func<T, Task<Result>> handleSuccess,
        Func<Task<Result>> handleFailure) =>
        await (await Result.FromResult(result)).HandleResult(handleSuccess, handleFailure);
}
