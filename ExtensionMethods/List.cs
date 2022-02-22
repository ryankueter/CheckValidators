namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Checks if an list is emply
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<List<T>?> IfEmpty<T>(this Check<List<T>?> data, string msg = "")
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
    /// Checks if an list is not empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="msg">The custom error</param>
    /// <returns></returns>
    public static Check<List<T>?> IfNotEmpty<T>(this Check<List<T>?> data, string msg = "")
    {
        if (data.InvalidModel()) { return data; }
        try
        {
            if (data.Value.Any())
            {
                data.ThrowError(msg, "The list is empty.");
            }
        }
        catch { }
        return data;
    }
}
