/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Checks if an array is emply
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<T[]> IfEmpty<T>(this Check<T[]> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (!data.Value.Any())
            {
                data.ThrowError(msg, "The list is empty.");
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if an array is not empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<T[]> IfNotEmpty<T>(this Check<T[]> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Any())
            {
                data.ThrowError(msg, "The list is not empty.");
            }
        }
        catch { }
        return data;
    }
}