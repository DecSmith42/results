namespace DecSm.Results.Domain.Errors;

[PublicAPI]
public record DatabaseError(Exception Exception) : ExceptionError(Exception)
{
    public DatabaseError(string message) : this(new Exception(message)) { }

    public static DatabaseError Default { get; } = new(new Exception("Database error"));

    [Pure]
    public override IReason Mask() =>
        new Error("Database error");
}
