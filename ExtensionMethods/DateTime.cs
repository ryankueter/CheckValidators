﻿/**
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
    public static Check<DateTime> IfUtcTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind == DateTimeKind.Utc)
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
    public static Check<DateTime> IfNotUtcTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind != DateTimeKind.Utc)
        {
            data.ThrowError("The datetime format is not Utc");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is local
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfLocalTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind == DateTimeKind.Local)
        {
            data.ThrowError("The datetime format is local");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is not local
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfNotLocalTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind != DateTimeKind.Local)
        {
            data.ThrowError("The datetime format is not local");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time format is unspecified
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfUnspecifiedTimeFormat(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.Kind == DateTimeKind.Unspecified)
        {
            data.ThrowError("The datetime format is unspecified");
        }
        return data;
    }

    /// <summary>
    /// Check if the date time is default
    /// </summary>
    /// <param name="data"></param>
    /// <param name="msg">Custom error message</param>
    /// <returns></returns>
    public static Check<DateTime> IfDefault(this Check<DateTime> data)
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
    public static Check<DateTime> IfNotDefault(this Check<DateTime> data)
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
    public static Check<DateTime> IfEarlierThan(this Check<DateTime> data, DateTime dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(data.Value, dateTime) < 0)
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
    public static Check<DateTime> IfLaterThan(this Check<DateTime> data, DateTime dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(data.Value, dateTime) > 0)
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
    public static Check<DateTime> IfEqual(this Check<DateTime> data, DateTime dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(data.Value, dateTime) is 0)
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
    public static Check<DateTime> IfNotEqual(this Check<DateTime> data, DateTime dateTime)
    {
        if (data.InvalidModel()) { return data; }
        if (DateTime.Compare(data.Value, dateTime) != 0)
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
    public static Check<DateTime> IfBetween(this Check<DateTime> data, DateTime startTime, DateTime endTime)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Compare(data.Value, startTime) >= 0) && (0 >= DateTime.Compare(data.Value, endTime)))
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
    public static Check<DateTime> IfNotBetween(this Check<DateTime> data, DateTime startTime, DateTime endTime)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Compare(data.Value, startTime) <= 0) || (0 <= DateTime.Compare(data.Value, endTime)))
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
    public static Check<DateTime> IfDayLightSavingsTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (TimeZoneInfo.Local.IsDaylightSavingTime(data.Value))
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
    public static Check<DateTime> IfNotDayLightSavingsTime(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (!TimeZoneInfo.Local.IsDaylightSavingTime(data.Value))
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
    public static Check<DateTime> IfDaysOlderThan(this Check<DateTime> data, double days)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalDays > days)
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
    public static Check<DateTime> IfNotDaysOlderThan(this Check<DateTime> data, double days)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalDays <= days)
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
    public static Check<DateTime> IfMinutesOlderThan(this Check<DateTime> data, double minutes)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalMinutes > minutes)
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
    public static Check<DateTime> IfNotMinutesOlderThan(this Check<DateTime> data, double minutes)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalMinutes <= minutes)
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
    public static Check<DateTime> IfSecondsOlderThan(this Check<DateTime> data, double seconds)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalSeconds > seconds)
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
    public static Check<DateTime> IfNotSecondsOlderThan(this Check<DateTime> data, double seconds)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalSeconds <= seconds)
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
    public static Check<DateTime> IfMillisecondsOlderThan(this Check<DateTime> data, double milliseconds)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalMilliseconds > milliseconds)
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
    public static Check<DateTime> IfNotMillisecondsOlderThan(this Check<DateTime> data, double milliseconds)
    {
        if (data.InvalidModel()) { return data; }
        if ((DateTime.Now - data.Value).TotalMilliseconds <= milliseconds)
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
    public static Check<DateTime> IfSunday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is DayOfWeek.Sunday)
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
    public static Check<DateTime> IfNotSunday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is not DayOfWeek.Sunday)
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
    public static Check<DateTime> IfMonday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is DayOfWeek.Monday)
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
    public static Check<DateTime> IfNotMonday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is not DayOfWeek.Monday)
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
    public static Check<DateTime> IfTuesday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is DayOfWeek.Tuesday)
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
    public static Check<DateTime> IfNotTuesday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is not DayOfWeek.Tuesday)
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
    public static Check<DateTime> IfWednesday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is DayOfWeek.Wednesday)
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
    public static Check<DateTime> IfNotWednesday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is not DayOfWeek.Wednesday)
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
    public static Check<DateTime> IfThursday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is DayOfWeek.Thursday)
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
    public static Check<DateTime> IfNotThursday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is not DayOfWeek.Thursday)
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
    public static Check<DateTime> IfFriday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is DayOfWeek.Friday)
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
    public static Check<DateTime> IfNotFriday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is not DayOfWeek.Friday)
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
    public static Check<DateTime> IfSaturday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is DayOfWeek.Saturday)
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
    public static Check<DateTime> IfNotSaturday(this Check<DateTime> data)
    {
        if (data.InvalidModel()) { return data; }
        if (data.Value.DayOfWeek is not DayOfWeek.Saturday)
        {
            data.ThrowError($"The day of the week should be Saturday");
        }
        return data;
    }
}
