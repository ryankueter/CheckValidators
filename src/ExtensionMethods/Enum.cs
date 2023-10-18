/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the enum does not contain a value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TValue> IfContainsValue<TValue>(this Check<TValue> data, string enumvalue, string? msg = null) where TValue : struct, Enum
    {
        if (data.InvalidModel()) { return data; }
        if (Enum.TryParse(enumvalue, out TValue e))
        {
            data.ThrowError($"The enum contains the value '{enumvalue}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the enum does not contain a value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TValue> IfNotContainsValue<TValue>(this Check<TValue> data, string enumvalue, string? msg = null) where TValue : struct, Enum
    {
        if (data.InvalidModel()) { return data; }
        if (!Enum.TryParse(enumvalue, out TValue e))
        {
            data.ThrowError($"The enum does not contain the value '{enumvalue}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the enum contains a value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TValue> IfDefined<TValue>(this Check<TValue> data, int enumvalue, string? msg = null) where TValue : struct, Enum
    {
        if (data.InvalidModel()) { return data; }
        if (Enum.IsDefined(typeof(TValue), enumvalue))
        {
            data.ThrowError($"The enum should not define a value for index {enumvalue}", msg);
        }
        return data;
    }
    public static Check<TValue> IfDefined<TValue>(this Check<TValue> data, TValue enumvalue, string? msg = null) where TValue : struct, Enum
    {
        if (data.InvalidModel()) { return data; }
        if (Enum.IsDefined(typeof(TValue), enumvalue))
        {
            data.ThrowError($"The enum should not define a value '{enumvalue}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the enum does not contain a value
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<TValue> IfNotDefined<TValue>(this Check<TValue> data, int enumvalue, string? msg = null) where TValue : struct, Enum
    {
        if (data.InvalidModel()) { return data; }
        if (!Enum.IsDefined(typeof(TValue), enumvalue))
        {
            data.ThrowError($"The enum does not define a value for index {enumvalue}", msg);
        }
        return data;
    }
    public static Check<TValue> IfNotDefined<TValue>(this Check<TValue> data, TValue enumvalue, string? msg = null) where TValue : struct, Enum
    {
        if (data.InvalidModel()) { return data; }
        if (!Enum.IsDefined(typeof(TValue), enumvalue))
        {
            data.ThrowError($"The enum does not define the value '{enumvalue}'", msg);
        }
        return data;
    }
}
