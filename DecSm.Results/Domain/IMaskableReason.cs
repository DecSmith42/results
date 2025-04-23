namespace DecSm.Results.Domain;

[PublicAPI]
public interface IMaskableReason : IReason
{
    public IReason Mask();
}
