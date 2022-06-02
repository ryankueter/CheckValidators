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
    public static Check<string> IfEmpty(this Check<string> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length is 0)
        {
            data.ThrowError("String is empty.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is whitespace
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfWhitespace(this Check<string> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.All(char.IsWhiteSpace))
        {
            data.ThrowError("String is whitepace.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is empty or whitespace
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfEmptyOrWhitespace(this Check<string> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Trim().Length is 0)
        {
            data.ThrowError("String is empty or whitespace.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is null or empty
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNullOrEmpty(this Check<string> data)
    {
        if (data.InvalidModel()) { return data; }
        if (string.IsNullOrEmpty(data.Value))
        {
            data.ThrowError("String is null or empty.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is null or whitespace
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNullOrWhitespace(this Check<string> data)
    {
        if (data.InvalidModel()) { return data; }
        if (string.IsNullOrWhiteSpace(data.Value))
        {
            data.ThrowError("String is null or whitespace.");
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
    public static Check<string> IfEquals(this Check<string> data, string compareString, StringComparison compareType = StringComparison.OrdinalIgnoreCase)
    {
        if (data.InvalidModel()) { return data; }
        if (string.Equals(data.Value, compareString, compareType))
        {
            data.ThrowError($"String should not be equal to '{compareString}' [StringComparison: '{compareType}'].");
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
    public static Check<string> IfNotEquals(this Check<string> data, string compareString, StringComparison compareType = StringComparison.InvariantCulture)
    {
        if (data.InvalidModel()) { return data; }
        if (!string.Equals(data.Value, compareString, compareType))
        {
            data.ThrowError($"String should be equal to '{compareString}' [StringComparison: '{compareType}'].");
        }
        return data;
    }

    /// <summary>
    /// Checks if the string is longer than the specified number of characters.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfLengthGreaterThan(this Check<string> data, int length)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length > length)
        {
            data.ThrowError($"String has exceeded the character limit of {length} characters.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the string is shorter than the specified number of characters.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfLengthLessThan(this Check<string> data, int length)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length < length)
        {
            data.ThrowError($"String does not meet the minimum character length of {length} characters.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the length of a string equals the parameter specified.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfLengthEquals(this Check<string> data, int length)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length == length)
        {
            data.ThrowError($"String length should not equal {length} characters.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the length of a string equals the parameter specified.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfLengthNotEquals(this Check<string> data, int length)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Length != length)
        {
            data.ThrowError($"String length should equal {length} characters.");
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
    public static Check<string> IfEndsWith(this Check<string> data, string ending, StringComparison compareType = StringComparison.InvariantCulture)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.EndsWith(ending, compareType))
        {
            data.ThrowError($"String should not end with '{ending}' [StringComparison: '{compareType}'].");
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
    public static Check<string> IfNotEndsWith(this Check<string> data, string ending, StringComparison compareType = StringComparison.InvariantCulture)
    {
        if (data.InvalidModel()) { return data; }
        if (!data.Value.EndsWith(ending, compareType))
        {
            data.ThrowError($"String does not end with '{ending}' [StringComparison: '{compareType}'].");
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
    public static Check<string> IfStartsWith(this Check<string> data, string beginning, StringComparison compareType = StringComparison.InvariantCulture)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.StartsWith(beginning, compareType))
        {
            data.ThrowError($"String should not start with '{beginning}' [StringComparison: '{compareType}'].");
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
    public static Check<string> IfNotStartsWith(this Check<string> data, string beginning, StringComparison compareType = StringComparison.InvariantCulture)
    {
        if (data.InvalidModel()) { return data; }
        if (!data.Value.StartsWith(beginning, compareType))
        {
            data.ThrowError($"String does not start with '{beginning}' [StringComparison: '{compareType}'].");
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
    public static Check<string> IfContains(this Check<string> data, string compare, StringComparison compareType = StringComparison.InvariantCulture)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Contains(compare, compareType))
        {
            data.ThrowError($"String should not contain '{compare}' [StringComparison: '{compareType}'].");
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
    public static Check<string> IfNotContains(this Check<string> data, string compare, StringComparison compareType = StringComparison.InvariantCulture)
    {
        if (data.InvalidModel()) { return data; }
        if (!data.Value.Contains(compare, compareType))
        {
            data.ThrowError($"String should contain '{compare}' [StringComparison: '{compareType}'].");
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
    public static Check<string> IfMatches(this Check<string> data, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
    {
        if (data.InvalidModel()) { return data; }
        var match = new Regex(pattern, options);
        if (match.IsMatch(data.Value))
        {
            data.ThrowError($"String should not match the regular expressions pattern '{pattern}'.");
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
    public static Check<string> IfNotMatches(this Check<string> data, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
    {
        if (data.InvalidModel()) { return data; }
        var match = new Regex(pattern, options);
        if (!match.IsMatch(data.Value))
        {
            data.ThrowError($"String should match the regular expressions pattern '{pattern}'.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotDate(this Check<string> data)
    {
        if (data.InvalidModel()) { return data; }
        DateTime d;
        if (!DateTime.TryParse(data.Value, out d))
        {
            data.ThrowError($"String {data.Value} is not a date.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not an integer
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string> IfNotInteger(this Check<string> data)
    {
        if (data.InvalidModel()) { return data; }
        int i;
        if (!Int32.TryParse(data.Value, out i))
        {
            data.ThrowError($"String {data.Value} is not an integer.");
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
    public static Check<string> IfNotEmail(this Check<string> data, string pattern = "", RegexOptions options = RegexOptions.IgnoreCase)
    {
        if (data.InvalidModel()) { return data; }

        // Regular Expressions: http://emailregex.com
        if (pattern is "")
            pattern = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        
        var match = new Regex(pattern, options);
        if (!match.IsMatch(data.Value))
        {
            data.ThrowError($"String '{data.Value}' is not an email address.");
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
    public static Check<string> IfNotURL(this Check<string> data, string pattern = "", RegexOptions options = RegexOptions.IgnoreCase)
    {
        if (data.InvalidModel()) { return data; }

        // Regular Expressions: http://emailregex.com
        if (pattern is "")
            pattern = @"^(http|https|file|mailto|ftp|ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";

        var match = new Regex(pattern, options);
        if (!match.IsMatch(data.Value))
        {
            data.ThrowError($"String {data.Value} is not a URL.");
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
        int numSpecial = 2)
    {
        if (data.InvalidModel()) { return data; }

        bool _valid = true;
        var msgs = new StringBuilder();
        msgs.Append($"The password does not contain: ");

        // Check the length.
        if (data.Value.Length < minLength)
        {
            _valid = false;
            msgs.Append($"A minimum of {minLength} characters. ");
        }

        // Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        var upper = new Regex("[A-Z]");
        if (upper.Matches(data.Value).Count < numUpper)
        {
            _valid = false;
            msgs.Append($"A minimum of {numUpper} upper-case characters. ");
        }

        var lower = new Regex("[a-z]");
        if (lower.Matches(data.Value).Count < numLower)
        {
            _valid = false;
            msgs.Append($"A minimum of {numLower} lower-case characters. ");
        }

        var number = new Regex("[0-9]");
        if (number.Matches(data.Value).Count < numNumbers)
        {
            _valid = false;
            msgs.Append($"A minimum of {numNumbers} numbers. ");
        }

        var special = new Regex("[^a-zA-Z0-9]");
        if (special.Matches(data.Value).Count < numSpecial)
        {
            _valid = false;
            msgs.Append($"A minimum of {numSpecial} special characters.");
        }

        if (_valid.Equals(false))
            data.ThrowError(msgs.ToString());

        return data;
    }
}
