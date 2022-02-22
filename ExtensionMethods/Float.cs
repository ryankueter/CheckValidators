namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the number is negative
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<float> IfNegative(this Check<float> data, string msg = "")
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
    public static Check<float?> IfNegative(this Check<float?> data, string msg = "")
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
    public static Check<float> IfPositive(this Check<float> data, string msg = "")
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
    public static Check<float?> IfPositive(this Check<float?> data, string msg = "")
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
    public static Check<float> IfZero(this Check<float> data, string msg = "")
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
    public static Check<float?> IfZero(this Check<float?> data, string msg = "")
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
    public static Check<float> IfNotZero(this Check<float> data, string msg = "")
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
    public static Check<float?> IfNotZero(this Check<float?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is not 0)
        {
            data.ThrowError(msg, "The number is not zero.");
        }
        return data;
    }
}
