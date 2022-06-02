﻿/**
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
    public static Check<T[]> IfEmpty<T>(this Check<T[]> data)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (!data.Value.Any())
            {
                data.ThrowError("The array is empty.");
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
    public static Check<T[]> IfNotEmpty<T>(this Check<T[]> data)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Any())
            {
                data.ThrowError("The array is not empty.");
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if an array has a specified number of records
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<T[]> IfCount<T>(this Check<T[]> data, int count)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() == count)
            {
                data.ThrowError($"The item count should not be {count}.");
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if an array has a specified number of records
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<T[]> IfNotCount<T>(this Check<T[]> data, int count)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() != count)
            {
                data.ThrowError($"The item count is not {count}.");
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if an array has a record count greater than the number specified.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<T[]> IfCountGreaterThan<T>(this Check<T[]> data, int count)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() > count)
            {
                data.ThrowError($"The item count is greater than {count}.");
            }
        }
        catch { }
        return data;
    }

    /// <summary>
    /// Checks if an array has a record count less than the number specified.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count">The record count</param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<T[]> IfCountLessThan<T>(this Check<T[]> data, int count)
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Count() < count)
            {
                data.ThrowError($"The item count is less than {count}.");
            }
        }
        catch { }
        return data;
    }
}