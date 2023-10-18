/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the dateonly is default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateOnly?> IfDefault(this Check<DateOnly?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == default)
        {
            data.ThrowError($"The dateonly is set to the default value.", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the dateonly is not default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateOnly?> IfNotDefault(this Check<DateOnly?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != default)
        {
            data.ThrowError($"The dateonly '{data.Value}' is not set to the default value", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the dateonly is the minimum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateOnly?> IfMinValue(this Check<DateOnly?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == DateOnly.MinValue)
        {
            data.ThrowError($"The dateonly is set to the minimum value of {data.Value}", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the dateonly is not the minimum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateOnly?> IfNotMinValue(this Check<DateOnly?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != DateOnly.MinValue)
        {
            data.ThrowError($"The dateonly '{data.Value}' is not set to the minimum value of {DateOnly.MinValue}", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the dateonly is the maximum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateOnly?> IfMaxValue(this Check<DateOnly?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == DateOnly.MaxValue)
        {
            data.ThrowError($"The dateonly is set to the maximum value of {data.Value}", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the dateonly is not the maximum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateOnly?> IfNotMaxValue(this Check<DateOnly?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != DateOnly.MaxValue)
        {
            data.ThrowError($"The dateonly '{data.Value}' is not set to the maximum value of {DateOnly.MaxValue}", msg);
        }
        return data;
    }
}
