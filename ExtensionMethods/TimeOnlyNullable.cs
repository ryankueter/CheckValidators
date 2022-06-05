/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the timeonly is default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfDefault(this Check<TimeOnly?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == default)
        {
            data.ThrowError($"The timeonly is set to the default value of {data.Value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the timeonly is not default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfNotDefault(this Check<TimeOnly?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != default)
        {
            data.ThrowError($"The timeonly '{data.Value}' is not set to the default value");
        }
        return data;
    }

    /// <summary>
    /// Check if the timeonly is the minimum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfMinValue(this Check<TimeOnly?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == TimeOnly.MinValue)
        {
            data.ThrowError($"The timeonly is set to the minimum value of {data.Value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the timeonly is not the minimum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfNotMinValue(this Check<TimeOnly?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != TimeOnly.MinValue)
        {
            data.ThrowError($"The timeonly '{data.Value}' is not set to the minimum value of {TimeOnly.MinValue}");
        }
        return data;
    }

    /// <summary>
    /// Check if the timeonly is the maximum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfMaxValue(this Check<TimeOnly?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == TimeOnly.MaxValue)
        {
            data.ThrowError($"The timeonly is set to the maximum value of {data.Value}");
        }
        return data;
    }

    /// <summary>
    /// Check if the timeonly is not the maximum value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfNotMaxValue(this Check<TimeOnly?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != TimeOnly.MaxValue)
        {
            data.ThrowError($"The timeonly '{data.Value}' is not set to the maximum value of {TimeOnly.MaxValue}");
        }
        return data;
    }

    /// <summary>
    /// Check if the timeonly between two times
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfBetween(this Check<TimeOnly?> data, TimeOnly startTime, TimeOnly endTime)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToBoolean(data.Value?.IsBetween(startTime, endTime)))
        {
            data.ThrowError($"The timeonly is between '{startTime}' and '{endTime}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the timeonly not between two times
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TimeOnly?> IfNotBetween(this Check<TimeOnly?> data, TimeOnly startTime, TimeOnly endTime)
    {
        if (data.InvalidModel()) { return data; }
        if (!Convert.ToBoolean(data.Value?.IsBetween(startTime, endTime)))
        {
            data.ThrowError($"The timeonly is not between '{startTime}' and '{endTime}'");
        }
        return data;
    }
}
