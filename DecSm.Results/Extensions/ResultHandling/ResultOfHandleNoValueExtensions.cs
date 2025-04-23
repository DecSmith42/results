// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultOfHandleNoValueExtensions
{
    [Pure]
    public static Result HandleToResult<T>(this Result<T> result, Action handleSuccess, Action handleFailure) =>
        result.IsFailed
            ? Result.From(handleFailure)
            : Result.From(handleSuccess);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Result<T> result, Func<Task> handleSuccess, Action handleFailure) =>
        result.IsFailed
            ? Result.From(handleFailure)
            : await Result.From(handleSuccess);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Result<T> result, Action handleSuccess, Func<Task> handleFailure) =>
        result.IsFailed
            ? await Result.From(handleFailure)
            : Result.From(handleSuccess);

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Result<T> result, Func<Task> handleSuccess, Func<Task> handleFailure) =>
        result.IsFailed
            ? await Result.From(handleFailure)
            : await Result.From(handleSuccess);

    // - - - - -

    [Pure]
    public static Result HandleToResult<T>(this Result<T> result, Action<T> handleSuccess, Action handleFailure) =>
        result.IsFailed
            ? Result.From(handleFailure)
            : Result.From(() => handleSuccess(result.Value));

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Result<T> result, Func<T, Task> handleSuccess, Action handleFailure) =>
        result.IsFailed
            ? Result.From(handleFailure)
            : await Result.From(async () => await handleSuccess(result.Value));

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Result<T> result, Action<T> handleSuccess, Func<Task> handleFailure) =>
        result.IsFailed
            ? await Result.From(handleFailure)
            : Result.From(() => handleSuccess(result.Value));

    [Pure]
    public static async Task<Result> HandleToResult<T>(this Result<T> result, Func<T, Task> handleSuccess, Func<Task> handleFailure) =>
        result.IsFailed
            ? await Result.From(handleFailure)
            : await Result.From(async () => await handleSuccess(result.Value));

    // - - - - -

    [Pure]
    public static Result HandleResult<T>(this Result<T> result, Func<Result> handleSuccess, Func<Result> handleFailure) =>
        result.IsFailed
            ? Result.FromResult(handleFailure)
            : Result.FromResult(handleSuccess);

    [Pure]
    public static async Task<Result> HandleResult<T>(this Result<T> result, Func<Task<Result>> handleSuccess, Func<Result> handleFailure) =>
        result.IsFailed
            ? Result.FromResult(handleFailure)
            : await Result.FromResult(handleSuccess);

    [Pure]
    public static async Task<Result> HandleResult<T>(this Result<T> result, Func<Result> handleSuccess, Func<Task<Result>> handleFailure) =>
        result.IsFailed
            ? await Result.FromResult(handleFailure)
            : Result.FromResult(handleSuccess);

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Result<T> result,
        Func<Task<Result>> handleSuccess,
        Func<Task<Result>> handleFailure) =>
        result.IsFailed
            ? await Result.FromResult(handleFailure)
            : await Result.FromResult(handleSuccess);

    // - - - - -

    [Pure]
    public static Result HandleResult<T>(this Result<T> result, Func<T, Result> handleSuccess, Func<Result> handleFailure) =>
        result.IsFailed
            ? Result.FromResult(handleFailure)
            : Result.FromResult(() => handleSuccess(result.Value));

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Result<T> result,
        Func<T, Task<Result>> handleSuccess,
        Func<Result> handleFailure) =>
        result.IsFailed
            ? Result.FromResult(handleFailure)
            : await Result.FromResult(async () => await handleSuccess(result.Value));

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Result<T> result,
        Func<T, Result> handleSuccess,
        Func<Task<Result>> handleFailure) =>
        result.IsFailed
            ? await Result.FromResult(handleFailure)
            : Result.FromResult(() => handleSuccess(result.Value));

    [Pure]
    public static async Task<Result> HandleResult<T>(
        this Result<T> result,
        Func<T, Task<Result>> handleSuccess,
        Func<Task<Result>> handleFailure) =>
        result.IsFailed
            ? await Result.FromResult(handleFailure)
            : await Result.FromResult(async () => await handleSuccess(result.Value));
}
