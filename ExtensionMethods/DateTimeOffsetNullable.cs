
using System.Collections;
using System.Collections.Generic;
/**
 * Author: Ryan A. Kueter
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */
namespace CheckValidators;

public static partial class CheckValidatorsExtensions
{
    /// <summary>
    /// Check if the date time is Utc
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfUtcTime(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value?.Offset == new TimeSpan(0, 0, 0))
        {
            data.ThrowError("The datetime format is Utc");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not Utc
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotUtcTime(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value?.Offset != new TimeSpan(0, 0, 0))
        {
            data.ThrowError("The datetime format is not Utc");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfDefault(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value == default)
        {
            data.ThrowError("The datetime is set to the default value");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotDefault(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value != default)
        {
            data.ThrowError("The datetime is not set to the default value");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is earlier than a date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfEarlierThan(this Check<DateTimeOffset?> data, DateTimeOffset dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTimeOffset.Compare((DateTimeOffset)data.Value!, dateTime) < 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is earlier than '{dateTime}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is later than a date
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfLaterThan(this Check<DateTimeOffset?> data, DateTimeOffset dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTimeOffset.Compare((DateTimeOffset)data.Value!, dateTime) > 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is later than '{dateTime}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is equal to
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfEqual(this Check<DateTimeOffset?> data, DateTimeOffset dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTimeOffset.Compare((DateTimeOffset)data.Value!, dateTime) is 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is equal to '{dateTime}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is equal to
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotEqual(this Check<DateTimeOffset?> data, DateTimeOffset dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTimeOffset.Compare((DateTimeOffset)data.Value!, dateTime) != 0)
        {
            data.ThrowError($"The datetime '{data.Value}' is not equal to '{dateTime}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is between two dates
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfBetween(this Check<DateTimeOffset?> data, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTimeOffset.Compare((DateTimeOffset)data.Value!, startTime) >= 0) && 
            (0 >= DateTimeOffset.Compare((DateTimeOffset)data.Value!, endTime)))
        {
            data.ThrowError($"The datetime '{data.Value}' is between '{startTime}' and '{endTime}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not between two dates
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotBetween(this Check<DateTimeOffset?> data, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Compare(value, startTime) < 0) || (0 < DateTimeOffset.Compare(value, endTime)))
        {
            data.ThrowError($"The datetime '{data.Value}' is not between '{startTime}' and '{endTime}'");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is on daylight savings time
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfDayLightSavingsTime(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (TimeZoneInfo.Local.IsDaylightSavingTime(value))
        {
            data.ThrowError($"The datetime '{data.Value}' is on daylight savings time");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not on daylight savings time
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotDayLightSavingsTime(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (!TimeZoneInfo.Local.IsDaylightSavingTime(value))
        {
            data.ThrowError($"The datetime '{data.Value}' is not on daylight savings time");
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
    public static Check<DateTimeOffset?> IfDaysOlderThan(this Check<DateTimeOffset?> data, double days)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalDays > days)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {days} days");
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
    public static Check<DateTimeOffset?> IfNotDaysOlderThan(this Check<DateTimeOffset?> data, double days)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalDays <= days)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {days} days");
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
    public static Check<DateTimeOffset?> IfMinutesOlderThan(this Check<DateTimeOffset?> data, double minutes)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalMinutes > minutes)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {minutes} minutes");
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
    public static Check<DateTimeOffset?> IfNotMinutesOlderThan(this Check<DateTimeOffset?> data, double minutes)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalMinutes <= minutes)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {minutes} minutes");
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
    public static Check<DateTimeOffset?> IfSecondsOlderThan(this Check<DateTimeOffset?> data, double seconds)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalSeconds > seconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {seconds} seconds");
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
    public static Check<DateTimeOffset?> IfNotSecondsOlderThan(this Check<DateTimeOffset?> data, double seconds)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalSeconds <= seconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {seconds} seconds");
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
    public static Check<DateTimeOffset?> IfMillisecondsOlderThan(this Check<DateTimeOffset?> data, double milliseconds)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalMilliseconds > milliseconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is older than {milliseconds} milliseconds");
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
    public static Check<DateTimeOffset?> IfNotMillisecondsOlderThan(this Check<DateTimeOffset?> data, double milliseconds)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if ((DateTimeOffset.Now - value).TotalMilliseconds <= milliseconds)
        {
            data.ThrowError($"The datetime '{data.Value}' is not older than {milliseconds} milliseconds");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Sunday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfSunday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is DayOfWeek.Sunday)
        {
            data.ThrowError($"The day of the week should not be Sunday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Sunday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotSunday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is not DayOfWeek.Sunday)
        {
            data.ThrowError($"The day of the week should be Sunday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Monday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfMonday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is DayOfWeek.Monday)
        {
            data.ThrowError($"The day of the week should not be Monday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Monday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotMonday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is not DayOfWeek.Monday)
        {
            data.ThrowError($"The day of the week should be Monday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Tuesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfTuesday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is DayOfWeek.Tuesday)
        {
            data.ThrowError($"The day of the week should not be Tuesday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Tuesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotTuesday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is not DayOfWeek.Tuesday)
        {
            data.ThrowError($"The day of the week should be Tuesday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Wednesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfWednesday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is DayOfWeek.Wednesday)
        {
            data.ThrowError($"The day of the week should not be Wednesday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Wednesday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotWednesday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is not DayOfWeek.Wednesday)
        {
            data.ThrowError($"The day of the week should be Wednesday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Thursday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfThursday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is DayOfWeek.Thursday)
        {
            data.ThrowError($"The day of the week should not be Thursday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Thursday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotThursday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is not DayOfWeek.Thursday)
        {
            data.ThrowError($"The day of the week should be Thursday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Friday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfFriday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is DayOfWeek.Friday)
        {
            data.ThrowError($"The day of the week should not be Friday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Friday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotFriday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is not DayOfWeek.Friday)
        {
            data.ThrowError($"The day of the week should be Friday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is Saturday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfSaturday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is DayOfWeek.Saturday)
        {
            data.ThrowError($"The day of the week should not be Saturday");
        }
        return data;
    }

    /// <summary>
    /// Check if the datetime is not Saturday
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTimeOffset?> IfNotSaturday(this Check<DateTimeOffset?> data)
    {
        if (data.InvalidModel()) { return data; }

        var value = (DateTimeOffset)data.Value!;
        if (value.DayOfWeek is not DayOfWeek.Saturday)
        {
            data.ThrowError($"The day of the week should be Saturday");
        }
        return data;
    }
}
