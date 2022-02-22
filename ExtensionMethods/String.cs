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
    /// Checks if the value is empty or whitespace
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string?> IfEmptyOrWhitespace(this Check<string?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Trim().Length is 0)
        {
            data.ThrowError(msg, "String is empty or whitespace.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not a date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string?> IfNotDate(this Check<string?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        DateTime d;
        if (DateTime.TryParse(data.Value, out d) is false)
        {
            data.ThrowError(msg, "String is not a date.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not an integer
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string?> IfNotInteger(this Check<string?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        int i;
        if (Int32.TryParse(data.Value, out i) is false)
        {
            data.ThrowError(msg, "String is not an integer.");
        }
        return data;
    }

    /// <summary>
    /// Checks if the value is not an email
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string?> IfNotEmail(this Check<string?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        var match = new Regex(@"\\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z", RegexOptions.IgnoreCase);
        if (match.IsMatch(data.Value) is false)
        {
            data.ThrowError(msg, "String is not an email address.");
        }
        return data;
    }


    /// <summary>
    /// Checks if the value is not a url
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<string?> IfNotURL(this Check<string?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        var match = new Regex(@"^(gn|http|https|file|mailto|ftp)(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\;\(\)\?\,\'\/\\\+&=:%\$#_]*)?", RegexOptions.IgnoreCase);
        if (match.IsMatch(data.Value) is false)
        {
            data.ThrowError(msg, "String is not a URL.");
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
    public static Check<string?> IfNotPassword(this Check<string?> data, string msg = "", int minLength = 8, int numUpper = 2, int numLower = 2, int numNumbers = 2, int numSpecial = 2)
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
            data.ThrowError(msg, msgs.ToString());

        return data;
    }
}
