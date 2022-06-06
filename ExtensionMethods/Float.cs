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
    public static Check<float> IfNegative(this Check<float> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < 0)
        {
            data.ThrowError("The float is negative");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is positive
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<float> IfPositive(this Check<float> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > 0)
        {
            data.ThrowError("The float is positive");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<float> IfZero(this Check<float> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is 0)
        {
            data.ThrowError("The float is zero");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is not zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<float> IfNotZero(this Check<float> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is not 0)
        {
            data.ThrowError("The float is not zero");
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
    public static Check<float> IfGreaterThan(this Check<float> data, float value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > value)
        {
            data.ThrowError($"The float is greater than {value}");
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
    public static Check<float> IfLessThan(this Check<float> data, float value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > value)
        {
            data.ThrowError($"The float is less than {value}");
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
    public static Check<float> IfEquals(this Check<float> data, float value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == value)
        {
            data.ThrowError($"The float should not be {value}");
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
    public static Check<float> IfNotEquals(this Check<float> data, float value)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == value)
        {
            data.ThrowError($"The float should be {value}");
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
    public static Check<float> IfBetween(this Check<float> data, double startValue, double endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > startValue && data.Value < endValue)
        {
            data.ThrowError($"The float '{data.Value}' is between '{startValue}' and '{endValue}'");
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
    public static Check<float> IfNotBetween(this Check<float> data, double startValue, double endValue)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value <= startValue || data.Value >= endValue)
        {
            data.ThrowError($"The float '{data.Value}' is not between '{startValue}' and '{endValue}'");
        }
        return data;
    }
}
