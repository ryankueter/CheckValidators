﻿/**
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
    public static Check<long?> IfNegative(this Check<long?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < 0)
        {
            data.ThrowError("The number is negative", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the number is positive
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<long?> IfPositive(this Check<long?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > 0)
        {
            data.ThrowError("The number is positive", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the number is zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<long?> IfZero(this Check<long?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is 0)
        {
            data.ThrowError("The number is zero", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the number is not zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<long?> IfNotZero(this Check<long?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is not 0)
        {
            data.ThrowError("The number is not zero", msg);
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
    public static Check<long?> IfGreaterThan(this Check<long?> data, long value, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > value)
        {
            data.ThrowError($"The number is greater than {value}", msg);
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
    public static Check<long?> IfLessThan(this Check<long?> data, long value, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < value)
        {
            data.ThrowError($"The number is less than {value}", msg);
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
    public static Check<long?> IfEquals(this Check<long?> data, long value, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == value)
        {
            data.ThrowError($"The number should not be {value}", msg);
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
    public static Check<long?> IfNotEquals(this Check<long?> data, long value, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != value)
        {
            data.ThrowError($"The number should be {value}", msg);
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
    public static Check<long?> IfBetween(this Check<long?> data, long startValue, long endValue, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > startValue && data.Value < endValue)
        {
            data.ThrowError($"The number '{data.Value}' is between '{startValue}' and '{endValue}'", msg);
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
    public static Check<long?> IfNotBetween(this Check<long?> data, long startValue, long endValue, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < startValue || data.Value > endValue)
        {
            data.ThrowError($"The number '{data.Value}' is not between '{startValue}' and '{endValue}'", msg);
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
    public static Check<long?> IfBetweenOrEqual(this Check<long?> data, long startValue, long endValue, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value >= startValue && data.Value <= endValue)
        {
            data.ThrowError($"The number '{data.Value}' is between or equal to '{startValue}' and '{endValue}'", msg);
        }
        return data;
    }
}