using System.Text;

namespace CheckValidators.Options;

public class OptionsBuilder : IOptionsBuilder
{
    private readonly IList<string> _messages;
    private readonly string _caller;
    private readonly string _type;
    public OptionsBuilder(IList<string> messages, string caller, string type) =>
        (_messages, _caller, _type) = (messages, caller, type);

    /// <summary>
    /// Specifies whether to include the caller and the type
    /// in the error.
    /// </summary>
    public bool IsVerbose { get; set; }

    /// <summary>
    /// Default is "Errors:"
    /// </summary>
    public string? StartText { get; set; } = "Errors: ";
    private string GetStartText() =>
        StartText != null ? StartText : String.Empty;

    public ArgumentException ThrowErrors() =>
        IsVerbose ?
            new ArgumentException($"{GetStartText()}{GetErrors()}, {_caller}.", _type) :
            new ArgumentException($"{GetStartText()}{GetErrors()}.");

    public string ReturnErrors() =>
        IsVerbose ?
            $"{GetStartText()}{GetErrors()}, {_caller}. (Parameter '{_type}')" :
            $"{GetStartText()}{GetErrors()}.";

    public ArgumentException ThrowFirstError() =>
    IsVerbose ?
            new ArgumentException($"{GetStartText()}{_messages!.First()}, {_caller}.", _type) :
            new ArgumentException($"{GetStartText()}{_messages!.First()}.");
    public string ReturnFirstError() =>
    IsVerbose ?
            $"{GetStartText()}{_messages!.First()}, {_caller}. (Parameter '{_type}')" :
            $"{GetStartText()}{_messages!.First()}.";

    private string GetErrors()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < _messages.Count(); i++)
        {
            if (i is 0)
            {
                sb.Append($"{i + 1}) {_messages[i]}");
                continue;
            }
            sb.Append($", {i + 1}) {_messages[i]}");
        }
        return sb.ToString();
    }
}
