/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the decimal is negative
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfNegative(this Check<decimal> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < 0)
        {
            data.ThrowError("The decimal is negative");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is positive
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfPositive(this Check<decimal> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > 0)
        {
            data.ThrowError("The decimal is positive");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfZero(this Check<decimal> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is 0)
        {
            data.ThrowError("The decimal is zero");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is not zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfNotZero(this Check<decimal> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is not 0)
        {
            data.ThrowError("The decimal is not zero");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is greater than a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfGreaterThan(this Check<decimal> data, decimal value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > value)
        {
            data.ThrowError($"The decimal is greater than {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is greater than a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfLessThan(this Check<decimal> data, decimal value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < value)
        {
            data.ThrowError($"The decimal is less than {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal equals a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfEquals(this Check<decimal> data, decimal value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == value)
        {
            data.ThrowError($"The decimal should not be {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is does not equal a specified value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfNotEquals(this Check<decimal> data, decimal value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != value)
        {
            data.ThrowError($"The decimal should be {value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is between two values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfBetween(this Check<decimal> data, decimal startValue, decimal endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > startValue && data.Value < endValue)
        {
            data.ThrowError($"The decimal '{data.Value}' is between '{startValue}' and '{endValue}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is not between two values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfNotBetween(this Check<decimal> data, decimal startValue, decimal endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < startValue || data.Value > endValue)
        {
            data.ThrowError($"The decimal '{data.Value}' is not between '{startValue}' and '{endValue}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the decimal is between or equal to two values
    /// </summary>
    /// <param name="data"></param>
    /// <param name="value">The number you are comparing</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<decimal> IfBetweenOrEquals(this Check<decimal> data, decimal startValue, decimal endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value >= startValue && data.Value <= endValue)
        {
            data.ThrowError($"The decimal '{data.Value}' is between or equal to '{startValue}' and '{endValue}'");
        }
        return data;
    }
}
