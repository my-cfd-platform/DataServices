using DotNetCoreDecorators;

namespace DataServices.Extensions;

public static class DateTimeExtension
{

    public static long UnixTime(this DateTime? dateTime )
    {
        return dateTime?.UnixTime() ?? DateTime.UnixEpoch.UnixTime();
    }

    public static DateTime EpochMicToDateTime(this long microseconds )
    {
        return EpochMilToDateTime(microseconds/1000 );
    }

    public static DateTime EpochMilToDateTime(this long microseconds )
    {
        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddMilliseconds( microseconds ).ToLocalTime();
        return dateTime;
    }

    public static DateTime EpochMilToDateTime(this ulong microseconds )
    {
        return EpochMilToDateTime((long)microseconds);
    }

    public static DateTime EpochMicToDateTime(this ulong microseconds )
    {
        return EpochMicToDateTime((long)microseconds);
    }

    public static long ToEpochMic(this DateTime dateTime )
    {
        return dateTime.UnixTime() * 1000 ;
    }

    public static long ToEpochMic(this DateTime? dateTime )
    {
        return dateTime is null ? 0 : dateTime.UnixTime() * 1000;
    }

    public static string ToFormattedString(this DateTime src)
    {
        return src.ToString("g");
    }
}