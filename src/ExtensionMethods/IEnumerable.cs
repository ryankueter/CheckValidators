/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Checks if a collection is empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<IEnumerable<T>> IfEmpty<T>(this Check<IEnumerable<T>> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() == 0)
            {
                data.ThrowError("The list is empty", msg);
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if a collection is not empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<IEnumerable<T>> IfNotEmpty<T>(this Check<IEnumerable<T>> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() != 0)
            {
                data.ThrowError("The list is not empty", msg);
            }
        }
        catch { }
        return data;
    }


    /// <summary>
    /// Checks if a collection has a specified number of items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<IEnumerable<T>> IfCount<T>(this Check<IEnumerable<T>> data, int count, string? msg = null)
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
    /// Checks if a collection has a specified number of items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<IEnumerable<T>> IfNotCount<T>(this Check<IEnumerable<T>> data, int count, string? msg = null)
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
    /// Checks if a list has a count greater than the number specified.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<IEnumerable<T>> IfCountGreaterThan<T>(this Check<IEnumerable<T>> data, int count, string? msg = null)
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
    /// Checks if a list has a count less than the number specified.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<IEnumerable<T>> IfCountLessThan<T>(this Check<IEnumerable<T>> data, int count, string? msg = null)
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
