using DotNetCoreDecorators;

namespace DataServices.Extensions;

public static class NumericExtension
{
    public static DateTime EpochMilToDateTime(this long milliseconds )
    {
        return EpochMilToDateTime((ulong)milliseconds);
    }

    public static DateTime EpochMicToDateTime(this long milliseconds )
    {
        return EpochMicToDateTime((ulong)milliseconds);
    }

    public static DateTime EpochMilToDateTime(this ulong javaTimeStamp )
    {
        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddMilliseconds( javaTimeStamp ).ToLocalTime();
        return dateTime;
    }
    public static DateTime EpochMicToDateTime(this ulong microseconds )
    {
        return EpochMilToDateTime(microseconds/1000 );
    }

    public static long ToEpochMic(this DateTime dateTime )
    {
        return dateTime.UnixTime() * 1000 ;
    }
}