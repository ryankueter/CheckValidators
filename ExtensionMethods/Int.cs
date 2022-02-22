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
    public static Check<int> IfNegative(this Check<int> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < 0)
        {
            data.ThrowError(msg, "The number is negative.");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is negative
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<int?> IfNegative(this Check<int?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value < 0)
        {
            data.ThrowError(msg, "The number is negative.");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is positive
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<int> IfPositive(this Check<int> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > 0)
        {
            data.ThrowError(msg, "The number is positive.");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is positive
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<int?> IfPositive(this Check<int?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value > 0)
        {
            data.ThrowError(msg, "The number is positive.");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<int> IfZero(this Check<int> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is 0)
        {
            data.ThrowError(msg, "The number is zero.");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<int?> IfZero(this Check<int?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is 0)
        {
            data.ThrowError(msg, "The number is zero.");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is not zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<int> IfNotZero(this Check<int> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is not 0)
        {
            data.ThrowError(msg, "The number is not zero.");
        }
        return data;
    }

    /// <summary>
    /// Check if the number is not zero
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<int?> IfNotZero(this Check<int?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is not 0)
        {
            data.ThrowError(msg, "The number is not zero.");
        }
        return data;
    }
}
