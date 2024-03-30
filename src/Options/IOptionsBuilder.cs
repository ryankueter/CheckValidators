namespace CheckValidators.Options;

public interface IOptionsBuilder
{
    bool IsVerbose { get; set; }
    public string? StartText { get; set; }
}
