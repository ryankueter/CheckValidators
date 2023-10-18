/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Checks if a dictionary is emply
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static Check<Dictionary<TKey, TValue>> IfEmpty<TKey, TValue>(this Check<Dictionary<TKey, TValue>> data, string? msg = null) where TKey : notnull
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() == 0)
            {
                data.ThrowError("Dictionary is empty", msg);
            }
        }
        catch { }
        return data;
    }
    /// <summary>
    /// Checks if a dictionary is not emply
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static Check<Dictionary<TKey, TValue>> IfNotEmpty<TKey, TValue>(this Check<Dictionary<TKey, TValue>> data, string? msg = null) where TKey : notnull
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() != 0)
            {
                data.ThrowError("Dictionary is not empty", msg);
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if a dictionary has a specified number of records
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<Dictionary<TKey, TValue>> IfCount<TKey, TValue>(this Check<Dictionary<TKey, TValue>> data, int count, string? msg = null) where TKey : notnull
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() == count)
            {
                data.ThrowError($"The item count should not be {count}", msg);
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if a dictionary has a specified number of records
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<Dictionary<TKey, TValue>> IfNotCount<TKey, TValue>(this Check<Dictionary<TKey, TValue>> data, int count, string? msg = null) where TKey : notnull
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() != count)
            {
                data.ThrowError($"The item count is not {count}", msg);
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if a dictionary has a record count greater than the number specified.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<Dictionary<TKey, TValue>> IfCountGreaterThan<TKey, TValue>(this Check<Dictionary<TKey, TValue>> data, int count, string? msg = null) where TKey : notnull
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() > count)
            {
                data.ThrowError($"The item count is greater than {count}", msg);
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if a dictionary has a record count less than the number specified.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<Dictionary<TKey, TValue>> IfCountLessThan<TKey, TValue>(this Check<Dictionary<TKey, TValue>> data, int count, string? msg = null) where TKey : notnull
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() < count)
            {
                data.ThrowError($"The item count is less than {count}", msg);
            }
        }
        catch { }
        return data;
    }
}