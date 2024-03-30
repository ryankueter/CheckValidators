/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
using System.Text;
using System.Runtime.CompilerServices;
using CheckValidators.Options;
using System.Data.SqlTypes;

namespace CheckValidators;

public sealed class Check<T> : IDisposable
{
    internal readonly bool IsNull;
    private bool _isValid;
    private bool _ifValid;

    internal readonly T Value;
    internal readonly string Type;

    private IList<string> _messages;

    /// <summary>
    /// Gets the file and line number
    /// on which the exception occurs
    /// </summary>
    private readonly string _caller;

    private bool IsNullCheck(T t) =>
        t is null ? true : false;

    private bool IsValidCheck(T t) =>
        t is null ? false : true;

    public Check(T t,
        [CallerArgumentExpression("t")] string expression = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0) =>
        (IsNull, _isValid, _ifValid, Value, Type, _messages, _caller) =
        (IsNullCheck(t), IsValidCheck(t), true, t, $"{expression} <{typeof(T).Name}>", new List<string>(), $"in {Path.GetFileName(file)}:line {line}");

    /// <summary>
    /// Throws an error if the value is null
    /// </summary>
    /// <param name="msg">Custom error message.</param>
    /// <returns></returns>
    public Check<T> IfNull(string? msg = null)
    {
        _ifValid = true;
        if (IsNull)
        {
            ThrowError("The value is null", msg);
        }
        return this;
    }

    /// <summary>
    /// Throws an error if the value is NOT null
    /// </summary>
    /// <param name="msg">Custom error message.</param>
    /// <returns></returns>
    public Check<T> IfNotNull(string? msg = null)
    {
        _ifValid = true;
        if (!IsNull)
        {
            ThrowError("The value is not null", msg);
        }
        return this;
    }

    /// <summary>
    /// An If Validation rule
    /// </summary>
    /// <param name="condition">Condition being evaluated.</param>
    /// <param name="msg">Custom error message.</param>
    /// <returns></returns>
    public Check<T> If(Func<T, bool> condition, string? msg = null, [CallerArgumentExpression("condition")] string expression = "")
    {
        if (InvalidModel()) { return this; }
        try
        {
            // Check if condition is true
            if (condition(Value))
            {
                ThrowError($"If({expression})", msg);
            }
        }
        catch
        {
            // Condition was not true
        }
        return this;
    }

    /// <summary>
    /// An IfNot validation rule.
    /// </summary>
    /// <param name="condition">Condition being evaluated.</param>
    /// <param name="msg">Custom error message.</param>
    /// <returns></returns>
    public Check<T> IfNot(Func<T, bool> condition, string? msg = null, [CallerArgumentExpression("condition")] string expression = "")
    {
        if (InvalidModel()) { return this; }
        try
        {
            // Check if condition is false
            if (!condition(Value))
            {
                ThrowError($"IfNot({expression})", msg);
            }
        }
        catch
        {
            // Condition was false
            ThrowError($"IfNot({expression})", msg);
        }
        return this;
    }

    /// <summary>
    /// AndIf only executes when the preceding If statement is valid.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Check<T> AndIf(Func<T, bool> condition, string? msg = null, [CallerArgumentExpression("condition")] string expression = "")
    {
        if (InvalidIf()) { return this; }
        try
        {
            // Check if condition is true
            if (condition(Value))
            {
                ThrowError($"AndIf({expression})", msg);
            }
        }
        catch
        {
            // Condition was not true
        }
        return this;
    }

    /// <summary>
    /// AndIfNot only executes when the preceding If statement is valid.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Check<T> AndIfNot(Func<T, bool> condition, string? msg = null, [CallerArgumentExpression("condition")] string expression = "")
    {
        if (InvalidIf()) { return this; }
        try
        {
            // Check if condition is false
            if (!condition(Value))
            {
                ThrowError($"AndIfNot({expression})", msg);
            }
        }
        catch
        {
            // Condition was false
            ThrowError($"AndIfNot({expression})", msg);
        }
        return this;
    }


    /// <summary>
    /// OrIf only executes when the preceding If statement is invalid.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Check<T> OrIf(Func<T, bool> condition, string? msg = null, [CallerArgumentExpression("condition")] string expression = "")
    {
        if (!InvalidIf()) { return this; }
        try
        {
            // Check if condition is true
            if (condition(Value))
            {
                ThrowError($"OrIf({expression})", msg);
            }
        }
        catch
        {
            // Condition was not true
        }
        return this;
    }

    /// <summary>
    /// OrIfNot only executes when the preceding If statement is invalid.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Check<T> OrIfNot(Func<T, bool> condition, string? msg = null, [CallerArgumentExpression("condition")] string expression = "")
    {
        if (!InvalidIf()) { return this; }
        try
        {
            // Check if condition is false
            if (!condition(Value))
            {
                ThrowError($"OrIfNot({expression})", msg);
            }
        }
        catch
        {
            // Condition was false
            ThrowError($"OrIfNot({expression})", msg);
        }
        return this;
    }

    /// <summary>
    /// Gets the list of errors
    /// </summary>
    /// <returns></returns>
    public IList<string> GetErrors() => _messages;

    /// <summary>
    /// Throws the errors as an ArgumentException
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("For future compatibility, specify the options instead. Example: ThrowErrors(options => { options.IsVerbose = true; })")]
    public void ThrowErrors(bool Verbose)
    {
        if (_messages.Count > 0)
        {
            var sb = AppendErrors();
            if (Verbose)
                throw new ArgumentException($"Errors: {sb.ToString()}, {_caller}.", Type);
            else
                throw new ArgumentException($"Errors: {sb.ToString()}.");
        }
    }

    /// <summary>
    /// Throws the errors as an ArgumentException
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    public void ThrowErrors(Action<IOptionsBuilder>? builder = null)
    {
        if (_messages.Count > 0)
        {
            var options = new OptionsBuilder(_messages, _caller, Type);
            if (builder is not null)
                builder(options);
            throw options.ThrowErrors();
        }
    }

    /// <summary>
    /// Returns the errors as a string
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("For future compatibility, specify the options instead. Example: ReturnErrors(options => { options.IsVerbose = true; })")]
    public string ReturnErrors(bool Verbose)
    {
        if (_messages.Count > 0)
        {
            var sb = AppendErrors();
            if (Verbose)
                return $"Errors: {sb.ToString()}, {_caller}. (Parameter '{Type}')";
            else
                return $"Errors: {sb.ToString()}.";
        }
        return String.Empty;
    }

    /// <summary>
    /// Returns the errors as a string
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    public string? ReturnErrors(Action<IOptionsBuilder>? builder = null)
    {
        if (_messages.Count > 0)
        {
            var options = new OptionsBuilder(_messages, _caller, Type);
            if (builder is not null)
                builder(options);
            return options.ReturnErrors();
        }
        return null;
    }

    private StringBuilder AppendErrors()
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
        return sb;
    }

    /// <summary>
    /// Throw only the first error.
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("For future compatibility, specify the options instead. Example: ThrowFirstError(options => { options.IsVerbose = true; })")]
    public void ThrowFirstError(bool Verbose)
    {
        if (_messages.Count > 0)
        {
            if (Verbose)
                throw new ArgumentException($"Errors: {_messages.First()}, {_caller}.", Type);
            else
                throw new ArgumentException($"Errors: {_messages.First()}.");
        }
    }

    /// <summary>
    /// Throw only the first error.
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    public void ThrowFirstError(Action<IOptionsBuilder>? builder = null)
    {
        if (_messages.Count > 0)
        {
            var options = new OptionsBuilder(_messages, _caller, Type);
            if (builder is not null)
                builder(options);
            throw options.ThrowFirstError();
        }
    }

    /// <summary>
    /// Return only the first error.
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    [Obsolete("For future compatibility, specify the options instead. Example: ReturnFirstError(options => { options.IsVerbose = true; })")]
    public string ReturnFirstError(bool Verbose)
    {
        if (_messages.Count > 0)
        {
            if (Verbose)
                return $"Errors: {_messages.First()}, {_caller}. (Parameter '{Type}')";
            else
                return $"Errors: {_messages.First()}.";
        }
        return String.Empty;
    }

    /// <summary>
    /// Return only the first error.
    /// </summary>
    /// <param name="Verbose">Include file name, line number, and parameter?</param>
    /// <exception cref="ArgumentException"></exception>
    public string? ReturnFirstError(Action<IOptionsBuilder>? builder = null)
    {
        if (_messages.Count > 0)
        {
            var options = new OptionsBuilder(_messages, _caller, Type);
            if (builder is not null)
                builder(options);

            return options.ReturnFirstError();
        }
        return null;
    }

    /// <summary>
    /// Check if it has errors
    /// </summary>
    /// <returns></returns>
    public bool HasErrors()
    {
        if (_messages.Count > 0)
            return true;

        return false;
    }

    /// <summary>
    /// Get the error cound
    /// </summary>
    /// <returns></returns>
    public int ErrorCount() => 
        _messages.Count;

    /// <summary>
    /// Check if the conditions are valid
    /// </summary>
    /// <returns></returns>
    public bool IsValid() => 
        _isValid;

    /// <summary>
    /// Adds an error to the list of errors.
    /// </summary>
    /// <param name="defaultMsg"></param>
    internal void ThrowError(string defaultMsg, string? customMsg = null)
    {
        _isValid = false;
        _ifValid = false;
        _messages.Add(customMsg is not null ? customMsg : defaultMsg);
    }

    /// <summary>
    /// Checks if the model is invalid
    /// </summary>
    /// <returns></returns>
    internal bool InvalidModel()
    {
        if (IsNull is true) { return true; }
        _ifValid = true;
        return false;
    }

    /// <summary>
    /// Checks if the previous if was invalid
    /// </summary>
    /// <returns></returns>
    internal bool InvalidIf()
    {
        if (!_ifValid || InvalidModel()) { return true; }
        return false;
    }

    /// <summary>
    /// Clears the evaluation state
    /// </summary>
    /// <returns></returns>
    public Check<T> Clear()
    {
        _isValid = true;
        _ifValid = true;
        _messages.Clear();
        return this;
    }

    /// <summary>
    /// Disposes the object
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing) { }
}
