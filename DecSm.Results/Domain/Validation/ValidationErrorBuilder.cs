namespace DecSm.Results.Domain.Validation;

[PublicAPI]
public sealed record ValidationErrorBuilder
{
    private List<IValidationError> _errors = [];

    public ValidationErrorBuilder Add(IValidationError error)
    {
        _errors.Add(error);

        return this;
    }

    public ValidationErrorBuilder Add(string message) =>
        Add(new ValidationError(message));

    public IReason Evaluate() =>
        _errors.Count switch
        {
            0 => Success.Default,
            1 => _errors[0],
            _ => new AggregateValidationError(_errors),
        };
}
