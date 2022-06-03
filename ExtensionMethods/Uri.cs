/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the Uri scheme is correct.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="scheme">The Uri scheme</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfScheme(this Check<Uri> data, string scheme)
    {
        if (data.InvalidModel()) { return data; }
        if (string.Equals(data.Value.Scheme, scheme, StringComparison.OrdinalIgnoreCase))
        {
            data.ThrowError($"Uri scheme should not be '{scheme}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri scheme is not correct.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="scheme">The Uri scheme</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfNotScheme(this Check<Uri> data, string scheme)
    {
        if (data.InvalidModel()) { return data; }
        if (!string.Equals(data.Value.Scheme, scheme, StringComparison.OrdinalIgnoreCase))
        {
            data.ThrowError($"Uri scheme is not '{scheme}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri scheme is absolute.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfAbsoluteUri(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.IsAbsoluteUri)
        {
            data.ThrowError($"Uri is absolute, consider changing it to relative");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri scheme is relative.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfRelativeUri(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (!data.Value.IsAbsoluteUri)
        {
            data.ThrowError($"Uri is relative, consider changing it to absolute");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri port is correct.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="port">The Uri port</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfUriPort(this Check<Uri> data, int port)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Port == port)
        {
            data.ThrowError($"Uri port should not be {port}");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri port is incorrect.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="port">The Uri port</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfNotUriPort(this Check<Uri> data, int port)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Port != port)
        {
            data.ThrowError($"Uri port should be {port}");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri is a file
    /// </summary>
    /// <param name="data"></param>
    /// <param name="port">The Uri port</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfFile(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.IsFile)
        {
            data.ThrowError($"Uri is a file path");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri is not a file
    /// </summary>
    /// <param name="data"></param>
    /// <param name="port">The Uri port</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfNotFile(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (!data.Value.IsFile)
        {
            data.ThrowError($"Uri is not a file");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri is a UNC path
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfUnc(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.IsUnc)
        {
            data.ThrowError($"Uri is a UNC path");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri is not a UNC path
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfNotUnc(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (!data.Value.IsUnc)
        {
            data.ThrowError($"Uri is not a UNC path");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri is a loopback
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfLoopback(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.IsLoopback)
        {
            data.ThrowError($"Uri is the loopback address");
        }
        return data;
    }

    /// <summary>
    /// Check if the Uri is not a loopback
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<Uri> IfNotLoopback(this Check<Uri> data)
    {
        if (data.InvalidModel()) { return data; }
        if (!data.Value.IsLoopback)
        {
            data.ThrowError($"Uri is not the loopback address");
        }
        return data;
    }
}
