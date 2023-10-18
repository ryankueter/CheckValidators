/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
using System.Text;
using System.Text.RegularExpressions;

namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{    
    /// <summary>
    /// Checks if the value is empty
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfEmpty(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length is 0)
        {
            data.ThrowError("String is empty");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is whitespace
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfWhitespace(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.All(char.IsWhiteSpace))
        {
            data.ThrowError("String is whitepace");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is empty or whitespace
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfEmptyOrWhitespace(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Trim().Length is 0)
        {
            data.ThrowError("String is empty or whitespace");
        }
        return data;
    }

    /// <summary>
    /// Checks if the string is equal to another string
    /// </summary>
    /// <param name="data"></param>
    /// <param name="compareString">The string you want to compare</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfEquals(this Check<string> data, string compareString, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (compareType is null)
            compareType = StringComparison.OrdinalIgnoreCase;

        if (string.Equals(data.Value, compareString, (StringComparison)compareType))
        {
            data.ThrowError($"String should not be equal to '{compareString}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if the string is not equal to another string
    /// </summary>
    /// <param name="data"></param>
    /// <param name="compareString">The string you want to compare</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfNotEquals(this Check<string> data, string compareString, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (compareType is null)
            compareType = StringComparison.InvariantCulture;

        if (!string.Equals(data.Value, compareString, (StringComparison)compareType))
        {
            data.ThrowError($"String should be equal to '{compareString}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if the string is longer than the specified number of characters.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfLengthGreaterThan(this Check<string> data, int length, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length > length)
        {
            data.ThrowError($"String has exceeded the character limit of {length} characters");
        }
        return data;
    }

    /// <summary>
    /// Checks if the string is shorter than the specified number of characters.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfLengthLessThan(this Check<string> data, int length, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length < length)
        {
            data.ThrowError($"String does not meet the minimum character length of {length} characters");
        }
        return data;
    }

    /// <summary>
    /// Checks if the length of a string equals the parameter specified.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfLengthEquals(this Check<string> data, int length, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length == length)
        {
            data.ThrowError($"String length should not equal {length} characters");
        }
        return data;
    }

    /// <summary>
    /// Checks if the length of a string equals the parameter specified.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotLengthEquals(this Check<string> data, int length, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length != length)
        {
            data.ThrowError($"String length should equal {length} characters");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string ends with the specified characters
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ending">The characters you are checking for.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfEndsWith(this Check<string> data, string ending, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (compareType is null)
            compareType = StringComparison.InvariantCulture;

        if (data.Value.EndsWith(ending, (StringComparison)compareType))
        {
            data.ThrowError($"String should not end with '{ending}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string does not end with the specified characters
    /// </summary>
    /// <param name="data"></param>
    /// <param name="ending">The characters you are checking for.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfNotEndsWith(this Check<string> data, string ending, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (compareType is null)
            compareType = StringComparison.InvariantCulture;

        if (!data.Value.EndsWith(ending, (StringComparison)compareType))
        {
            data.ThrowError($"String does not end with '{ending}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string starts with the specified characters
    /// </summary>
    /// <param name="data"></param>
    /// <param name="beginning">The characters you are checking for.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfStartsWith(this Check<string> data, string beginning, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (compareType is null)
            compareType = StringComparison.InvariantCulture;

        if (data.Value.StartsWith(beginning, (StringComparison)compareType))
        {
            data.ThrowError($"String should not start with '{beginning}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string does not start with the specified characters
    /// </summary>
    /// <param name="data"></param>
    /// <param name="beginning">The characters you are checking for.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfNotStartsWith(this Check<string> data, string beginning, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (compareType is null)
            compareType = StringComparison.InvariantCulture;

        if (!data.Value.StartsWith(beginning, (StringComparison)compareType))
        {
            data.ThrowError($"String does not start with '{beginning}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string contains specified characters
    /// </summary>
    /// <param name="data"></param>
    /// <param name="compare">The string you are comparing.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfContains(this Check<string> data, string compare, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        
        if (compareType is null)
            compareType = StringComparison.InvariantCulture;

        if (data.Value.Contains(compare, (StringComparison)compareType))
        {
            data.ThrowError($"String should not contain '{compare}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string does not contain specified characters
    /// </summary>
    /// <param name="data"></param>
    /// <param name="compare">The string you are comparing.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="compareType">A parameter to supply the StringComparison options.</param>
    /// <returns></returns>
    public static Check<string> IfNotContains(this Check<string> data, string compare, StringComparison? compareType = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (compareType is null)
            compareType = StringComparison.InvariantCulture;

        if (!data.Value.Contains(compare, (StringComparison)compareType))
        {
            data.ThrowError($"String should contain '{compare}' [StringComparison: '{compareType}']");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string matches a regular expressions pattern
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pattern">The regular expressions pattern.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="options">The regular expressions options.</param>
    /// <returns></returns>
    public static Check<string> IfMatches(this Check<string> data, string pattern, RegexOptions? options = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (options is null)
            options = RegexOptions.IgnoreCase;

        var match = new Regex(pattern, (RegexOptions)options);
        if (match.IsMatch(data.Value))
        {
            data.ThrowError($"String should not match the regular expressions pattern '{pattern}'");
        }
        return data;
    }

    /// <summary>
    /// Checks if a string matches a regular expressions pattern
    /// </summary>
    /// <param name="data"></param>
    /// <param name="pattern">The regular expressions pattern.</param>
    /// <param name="msg">Custom error message</param>
    /// <param name="options">The regular expressions options.</param>
    /// <returns></returns>
    public static Check<string> IfNotMatches(this Check<string> data, string pattern, RegexOptions? options = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        if (options is null)
            options = RegexOptions.IgnoreCase;

        var match = new Regex(pattern, (RegexOptions)options);
        if (!match.IsMatch(data.Value))
        {
            data.ThrowError($"String should match the regular expressions pattern '{pattern}'");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotDate(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        DateTime d;
        if (!DateTime.TryParse(data.Value, out d))
        {
            data.ThrowError($"String {data.Value} is not a date");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not an Int16
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotInt16(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        Int16 i;
        if (!Int16.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not an Int16");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not an integer
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotInteger(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        int i;
        if (!Int32.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not an integer");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not an Int64
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotInt64(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        Int64 i;
        if (!Int64.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not an Int64");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a TimeOnly
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotTimeOnly(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        TimeOnly i;
        if (!TimeOnly.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not a TimeOnly");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a DateOnly
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotDateOnly(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        DateOnly i;
        if (!DateOnly.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not a DateOnly");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a double
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotDouble(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        double i;
        if (!Double.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not a double");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a float
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotFloat(this Check<string> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        float i;
        if (!float.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not a float");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not an email
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <param name="pattern">A custom regular expression pattern for validating the email.</param>
    /// <param name="options">Regular expressions options.</param>
    /// <returns></returns>
    public static Check<string> IfNotEmail(this Check<string> data, string? pattern = null, RegexOptions? options = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        // Regular Expressions: http://emailregex.com
        if (pattern is null)
            pattern = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        if (options is null)
            options = RegexOptions.IgnoreCase;

        var match = new Regex(pattern, (RegexOptions)options);
        if (!match.IsMatch(data.Value))
        {
            data.ThrowError($"String '{data.Value}' is not an email address");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a url
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <param name="pattern">A custom regular expression pattern for validating the email.</param>
    /// <param name="options">Regular expressions options.</param>
    /// <returns></returns>
    public static Check<string> IfNotURL(this Check<string> data, string? pattern = null, RegexOptions? options = null, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        // Regular Expressions: http://emailregex.com
        if (pattern is null)
            pattern = @"^(http|https|file|mailto|ftp|ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";

        if (options is null)
            options = RegexOptions.IgnoreCase;

        var match = new Regex(pattern, (RegexOptions)options);
        if (!match.IsMatch(data.Value))
        {
            data.ThrowError($"String {data.Value} is not a URL");
        }
        return data;
    }

    /// <summary>
    /// Determines if a password is sufficiently complex.
    /// 
    /// Example Source:
    /// https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/strings/walkthrough-validating-that-passwords-are-complex
    /// </summary>
    /// <param name="minLength">Minimum number of password characters.</param>
    /// <param name="numUpper">Minimum number of uppercase characters.</param>
    /// <param name="numLower">Minimum number of lowercase characters.</param>
    /// <param name="numNumbers">Minimum number of numeric characters.</param>
    /// <param name="numSpecial">Minimum number of special characters.</param>
    /// <returns>True if the password is sufficiently complex.</returns>
    public static Check<string> IfNotValidPassword(this Check<string> data, 
        int minLength = 8, 
        int numUpper = 2, 
        int numLower = 2, 
        int numNumbers = 2, 
        int numSpecial = 2, 
        string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        bool _valid = true;
        var msgs = new StringBuilder();
        
        // Check the length.
        if (data.Value.Length < minLength)
        {
            _valid = false;
            if (msgs.Length == 0)
                msgs.Append($"{minLength} characters");
            else
                msgs.Append($"; {minLength} characters");
        }

        // Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        var upper = new Regex("[A-Z]");
        if (upper.Matches(data.Value).Count < numUpper)
        {
            _valid = false;
            if (msgs.Length == 0)
                msgs.Append($"{numUpper} upper-case characters");
            else
                msgs.Append($"; {numUpper} upper-case characters");
        }

        var lower = new Regex("[a-z]");
        if (lower.Matches(data.Value).Count < numLower)
        {
            _valid = false;
            if (msgs.Length == 0)
                msgs.Append($"{numLower} lower-case characters");
            else
                msgs.Append($"; {numLower} lower-case characters");
        }

        var number = new Regex("[0-9]");
        if (number.Matches(data.Value).Count < numNumbers)
        {
            _valid = false;
            if (msgs.Length == 0)
                msgs.Append($"{numNumbers} numbers");
            else
                msgs.Append($"; {numNumbers} numbers");
        }

        var special = new Regex("[^a-zA-Z0-9]");
        if (special.Matches(data.Value).Count < numSpecial)
        {
            _valid = false;
            if (msgs.Length == 0)
                msgs.Append($"{numSpecial} special characters");
            else
                msgs.Append($"; {numSpecial} special characters");
        }

        if (_valid.Equals(false))
            data.ThrowError($"The password requires the following: {msgs.ToString()}", msg);

        return data;
    }
}
