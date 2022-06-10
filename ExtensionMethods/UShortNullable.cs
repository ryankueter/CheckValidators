/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the number is negative
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfNegative(this Check<ushort?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < 0)
        {
            data.ThrowError("The number is negative");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is positive
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfPositive(this Check<ushort?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > 0)
        {
            data.ThrowError("The number is positive");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfZero(this Check<ushort?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is 0)
        {
            data.ThrowError("The number is zero");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is not zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfNotZero(this Check<ushort?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is not 0)
        {
            data.ThrowError("The number is not zero");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is greater than a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfGreaterThan(this Check<ushort?> data, ushort value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > value)
        {
            data.ThrowError($"The number is greater than {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is greater than a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfLessThan(this Check<ushort?> data, ushort value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > value)
        {
            data.ThrowError($"The number is less than {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the number equals a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfEquals(this Check<ushort?> data, ushort value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == value)
        {
            data.ThrowError($"The number should not be {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is does not equal a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfNotEquals(this Check<ushort?> data, ushort value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == value)
        {
            data.ThrowError($"The number should be {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is between two values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfBetween(this Check<ushort?> data, ushort startValue, ushort endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > startValue && data.Value < endValue)
        {
            data.ThrowError($"The number '{data.Value}' is between '{startValue}' and '{endValue}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is between or equual to two values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfBetweenOrEqual(this Check<ushort?> data, ushort startValue, ushort endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value >= startValue && data.Value <= endValue)
        {
            data.ThrowError($"The number '{data.Value}' is between or equal to '{startValue}' and '{endValue}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is not between two values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<ushort?> IfNotBetween(this Check<ushort?> data, ushort startValue, ushort endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < startValue || data.Value > endValue)
        {
            data.ThrowError($"The number '{data.Value}' is not between '{startValue}' and '{endValue}'");
        }
        return data;
    }
}
