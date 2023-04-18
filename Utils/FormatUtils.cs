//using DealingAdmin.Abstractions.Models;

namespace DataServices.Utils;

public static class FormatUtils
{
    public const string DateTimeCommonFormat = "yyyy-MM-dd HH:mm:ss";
        
    public const string FullDateTimeWithMsFormat = "ddd, dd MMM yyy HH:mm:ss.fff";
        
    public const string ImagePngDataPrefix = "data:image/png;base64, ";
        
    public const string ImageSvgDataPrefix = "data:image/svg+xml;base64, ";

    public static string TimeSpanToString(TimeSpan ts)
    {
        // ReSharper disable UseStringInterpolation
        return string.Format("{0} {1} {2} {3}",
            ts.Days > 0 ? string.Format("{0:0} day{1} ", ts.Days, ts.Days == 1 ? string.Empty : "s") : string.Empty,
            ts.Hours > 0 ? string.Format("{0:0}h", ts.Hours) : string.Empty,
            ts.Minutes > 0 ? string.Format("{0:0}m ", ts.Minutes) : string.Empty,
            ts.Seconds > 0 ? string.Format("{0:0}s", ts.Seconds) : string.Empty);
        // ReSharper restore UseStringInterpolation
    }

    public static string DateTimeFormat(DateTime dt)
    {
        return dt.ToString(DateTimeCommonFormat);
    }

    public static string DateTimeNamedWithMsFormat(DateTime dt)
    {
        return dt.ToString(FullDateTimeWithMsFormat);
    }
}