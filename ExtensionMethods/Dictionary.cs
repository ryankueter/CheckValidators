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
    public static Check<Dictionary<TKey, TValue>?> IfEmpty<TKey, TValue>(this Check<Dictionary<TKey, TValue>?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (!data.Value.Any())
            {
                data.ThrowError(msg, "List is empty.");
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
    public static Check<Dictionary<TKey, TValue>?> IfNotEmpty<TKey, TValue>(this Check<Dictionary<TKey, TValue>?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Any())
            {
                data.ThrowError(msg, "List is not empty.");
            }
        }
        catch { }
        return data;
    }
}