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
    public static Check<double?> IfNegative(this Check<double?> data)
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
    public static Check<double?> IfPositive(this Check<double?> data)
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
    public static Check<double?> IfZero(this Check<double?> data)
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
    public static Check<double?> IfNotZero(this Check<double?> data)
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
    public static Check<double?> IfGreaterThan(this Check<double?> data, double value)
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
    public static Check<double?> IfLessThan(this Check<double?> data, double value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < value)
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
    public static Check<double?> IfEquals(this Check<double?> data, double value)
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
    public static Check<double?> IfNotEquals(this Check<double?> data, double value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != value)
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
    public static Check<double?> IfBetween(this Check<double?> data, double startValue, double endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value >= startValue && data.Value <= endValue)
        {
            data.ThrowError($"The number '{data.Value}' is between '{startValue}' and '{endValue}'");
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
    public static Check<double?> IfNotBetween(this Check<double?> data, double startValue, double endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < startValue || data.Value > endValue)
        {
            data.ThrowError($"The number '{data.Value}' is not between '{startValue}' and '{endValue}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is between or equal to two values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<double?> IfBetweenOrEqual(this Check<double?> data, double startValue, double endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value >= startValue && data.Value <= endValue)
        {
            data.ThrowError($"The number '{data.Value}' is between or equal to '{startValue}' and '{endValue}'");
        }
        return data;
    }
}
