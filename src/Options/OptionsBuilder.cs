namespace CheckValidators.Options;

public class OptionsBuilder : IOptionsBuilder
{
    private readonly string _error;
    private readonly string _caller;
    private readonly string _type;
    public OptionsBuilder(string error, string caller, string type)
    {
        _error = error;
        _caller = caller;
        _type = type;
    }
    public bool IsVerbose { get; set; }

    public ArgumentException ThrowErrors() => 
        IsVerbose ? 
            new ArgumentException($"Errors: {_error}, {_caller}.", _type) : 
            new ArgumentException($"Errors: {_error}.");
    
    public string ReturnErrors() => 
        IsVerbose ? 
            $"Errors: {_error}, {_caller}. (Parameter '{_type}')" : 
            $"Errors: {_error}.";
}
