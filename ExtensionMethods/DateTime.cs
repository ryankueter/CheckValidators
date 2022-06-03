/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the date time is Utc
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfUtcTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind == DateTimeKind.Utc)
        {
            data.ThrowError("The datetime format is Utc");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not Utc
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfNotUtcTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind != DateTimeKind.Utc)
        {
            data.ThrowError("The datetime format is not Utc");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is local
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfLocalTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind == DateTimeKind.Local)
        {
            data.ThrowError("The datetime format is local");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not local
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfNotLocalTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind != DateTimeKind.Local)
        {
            data.ThrowError("The datetime format is not local");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time format is unspecified
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfUnspecifiedTimeFormat(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind == DateTimeKind.Unspecified)
        {
            data.ThrowError("The datetime format is unspecified");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfDefault(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == default)
        {
            data.ThrowError("The datetime is set to the default value");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfNotDefault(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != default)
        {
            data.ThrowError("The datetime is not set to the default value");
        }
        return data;
    }
}
