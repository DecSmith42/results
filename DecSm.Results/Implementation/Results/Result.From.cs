namespace DecSm.Results.Implementation.Results;

public sealed partial record Result
{
    [Pure]
    public static Result From(Action from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            from();

            return Ok();
        }
        catch (Exception ex)
        {
            return Failure(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static Result FromResult(Func<Result> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return from();
        }
        catch (Exception ex)
        {
            return Failure(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static Result<TValue> From<TValue>(Func<TValue> func, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return Ok(func());
        }
        catch (Exception ex)
        {
            return Failure<TValue>(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static Result<TValue> FromResult<TValue>(Func<Result<TValue>> func, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            return Failure<TValue>(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result> From(Task from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            await from.ConfigureAwait(false);

            return Ok();
        }
        catch (Exception ex)
        {
            return Failure(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result> FromResult(Task<Result> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return await from.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return Failure(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result> From(Func<Task> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            await from()
                .ConfigureAwait(false);

            return Ok();
        }
        catch (Exception ex)
        {
            return Failure(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result> FromResult(Func<Task<Result>> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return await from()
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return Failure(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result<T>> From<T>(Task<T> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return Ok(await from.ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Failure<T>(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result<T>> FromResult<T>(Task<Result<T>> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return await from.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return Failure<T>(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result<T>> From<T>(Func<Task<T>> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return Ok(await from()
                .ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Failure<T>(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }

    [Pure]
    public static async Task<Result<T>> FromResult<T>(Func<Task<Result<T>>> from, Func<Exception, IError>? exceptionHandler = null)
    {
        try
        {
            return await from()
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return Failure<T>(exceptionHandler?.Invoke(ex) ?? new ExceptionError(ex));
        }
    }
}
