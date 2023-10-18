/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the date time is default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNull(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value is null)
        {
            data.ThrowError("The datetime is null", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is Utc
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfUtcTime(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        DateTime result = default;
        if (data.Value.HasValue)
            result = Convert.ToDateTime(data.Value);

        if (result.Kind == DateTimeKind.Utc)
        {
            data.ThrowError("The datetime format is Utc", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not Utc
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotUtcTime(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        DateTime result = default;
        if (data.Value.HasValue)
            result = Convert.ToDateTime(data.Value);

        if (result.Kind != DateTimeKind.Utc)
        {
            data.ThrowError("The datetime format is not Utc", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is local
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfLocalTime(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        DateTime result = default;
        if (data.Value.HasValue)
            result = Convert.ToDateTime(data.Value);

        if (result.Kind == DateTimeKind.Local)
        {
            data.ThrowError("The datetime format is local", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not local
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotLocalTime(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        DateTime result = default;
        if (data.Value.HasValue)
            result = Convert.ToDateTime(data.Value);

        if (result.Kind != DateTimeKind.Local)
        {
            data.ThrowError("The datetime format is not local", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the date time format is unspecified
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfUnspecifiedTimeFormat(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }

        DateTime result = default;
        if (data.Value.HasValue)
            result = Convert.ToDateTime(data.Value);

        if (result.Kind == DateTimeKind.Unspecified)
        {
            data.ThrowError("The datetime format is unspecified", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfDefault(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == default)
        {
            data.ThrowError("The datetime is set to the default value", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotDefault(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != default)
        {
            data.ThrowError("The datetime is not set to the default value", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is earlier than a date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfEarlierThan(this Check<DateTime?> data, DateTime dateTime, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(Convert.ToDateTime(data.Value), dateTime) < 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is earlier than '{dateTime}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is later than a date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfLaterThan(this Check<DateTime?> data, DateTime dateTime, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(Convert.ToDateTime(data.Value), dateTime) > 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is later than '{dateTime}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is equal to
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfEqual(this Check<DateTime?> data, DateTime dateTime, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(Convert.ToDateTime(data.Value), dateTime) is 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is equal to '{dateTime}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not equal to
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotEqual(this Check<DateTime?> data, DateTime dateTime, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(Convert.ToDateTime(data.Value), dateTime) != 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is not equal to '{dateTime}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is between two dates
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfBetween(this Check<DateTime?> data, DateTime startTime, DateTime endTime, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Compare(Convert.ToDateTime(data.Value), startTime) >= 0) && (0 >= DateTime.Compare(Convert.ToDateTime(data.Value), endTime)))
        {
            data.ThrowError($"The datetime '{data.Value}' is between '{startTime}' and '{endTime}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not between two dates
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotBetween(this Check<DateTime?> data, DateTime startTime, DateTime endTime, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Compare(Convert.ToDateTime(data.Value), startTime) < 0) || (0 < DateTime.Compare(Convert.ToDateTime(data.Value), endTime)))
        {
            data.ThrowError($"The datetime '{data.Value}' is not between '{startTime}' and '{endTime}'", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is on daylight savings time
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfDayLightSavingsTime(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (TimeZoneInfo.Local.IsDaylightSavingTime(Convert.ToDateTime(data.Value)))
        {
            data.ThrowError($"The datetime '{data.Value}' is on daylight savings time", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not on daylight savings time
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotDayLightSavingsTime(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (!TimeZoneInfo.Local.IsDaylightSavingTime(Convert.ToDateTime(data.Value)))
        {
            data.ThrowError($"The datetime '{data.Value}' is not on daylight savings time", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is older than a number of days
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of days</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfDaysOlderThan(this Check<DateTime?> data, double days, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalDays > days)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {days} days", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not older than a number of days
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of days</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotDaysOlderThan(this Check<DateTime?> data, double days, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalDays <= days)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {days} days", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is older than a number of minutes
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of minutes</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfMinutesOlderThan(this Check<DateTime?> data, double minutes, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalMinutes > minutes)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {minutes} minutes", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not older than a number of minutes
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of minutes</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotMinutesOlderThan(this Check<DateTime?> data, double minutes, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalMinutes <= minutes)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {minutes} minutes", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is older than a number of seconds
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of seconds</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfSecondsOlderThan(this Check<DateTime?> data, double seconds, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalSeconds > seconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {seconds} seconds", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not older than a number of seconds
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of seconds</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotSecondsOlderThan(this Check<DateTime?> data, double seconds, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalSeconds <= seconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {seconds} seconds", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is older than a number of milliseconds
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of milliseconds</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfMillisecondsOlderThan(this Check<DateTime?> data, double milliseconds, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalMilliseconds > milliseconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {milliseconds} milliseconds", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not older than a number of milliseconds
    /// </summary>
    /// <param name="data"></param>
    /// <param name="days">The number of milliseconds</param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotMillisecondsOlderThan(this Check<DateTime?> data, double milliseconds, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - Convert.ToDateTime(data.Value)).TotalMilliseconds <= milliseconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {milliseconds} milliseconds", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Sunday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfSunday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is DayOfWeek.Sunday)
        {
            data.ThrowError($"The day of the week should not be Sunday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Sunday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotSunday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is not DayOfWeek.Sunday)
        {
            data.ThrowError($"The day of the week should be Sunday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Monday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfMonday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is DayOfWeek.Monday)
        {
            data.ThrowError($"The day of the week should not be Monday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Monday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotMonday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is not DayOfWeek.Monday)
        {
            data.ThrowError($"The day of the week should be Monday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Tuesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfTuesday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is DayOfWeek.Tuesday)
        {
            data.ThrowError($"The day of the week should not be Tuesday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Tuesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotTuesday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is not DayOfWeek.Tuesday)
        {
            data.ThrowError($"The day of the week should be Tuesday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Wednesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfWednesday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is DayOfWeek.Wednesday)
        {
            data.ThrowError($"The day of the week should not be Wednesday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Wednesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotWednesday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is not DayOfWeek.Wednesday)
        {
            data.ThrowError($"The day of the week should be Wednesday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Thursday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfThursday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is DayOfWeek.Thursday)
        {
            data.ThrowError($"The day of the week should not be Thursday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Thursday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotThursday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is not DayOfWeek.Thursday)
        {
            data.ThrowError($"The day of the week should be Thursday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Friday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfFriday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is DayOfWeek.Friday)
        {
            data.ThrowError($"The day of the week should not be Friday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Friday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotFriday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is not DayOfWeek.Friday)
        {
            data.ThrowError($"The day of the week should be Friday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Saturday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfSaturday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is DayOfWeek.Saturday)
        {
            data.ThrowError($"The day of the week should not be Saturday", msg);
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Saturday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime?> IfNotSaturday(this Check<DateTime?> data, string? msg = null)
    {
        if (data.InvalidModel()) { return data; }
        if (Convert.ToDateTime(data.Value).DayOfWeek is not DayOfWeek.Saturday)
        {
            data.ThrowError($"The day of the week should be Saturday", msg);
        }
        return data;
    }
}
