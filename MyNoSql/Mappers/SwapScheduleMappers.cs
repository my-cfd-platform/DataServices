namespace DataServices.MyNoSql.Mappers;

public static class SwapScheduleMappers
{
        
    private static readonly Dictionary<char, DayOfWeek> MapperDayOfWeek = new Dictionary<char, DayOfWeek>
    {
        ['0'] = DayOfWeek.Sunday,
        ['1'] = DayOfWeek.Monday,
        ['2'] = DayOfWeek.Tuesday,
        ['3'] = DayOfWeek.Wednesday,
        ['4'] = DayOfWeek.Thursday,
        ['5'] = DayOfWeek.Friday,
        ['6'] = DayOfWeek.Saturday,
    };
        
        
    public static string SwapMomentToString(DayOfWeek dayOfWeek, TimeSpan time)
    {
        return (int) dayOfWeek + time.ToString("c").Substring(0,8);
    }

    public static DayOfWeek GetDayOfWeekFromRowKey(this string rowKey)
    {
        return MapperDayOfWeek[rowKey[0]];
    }
        
    public static string GetTimeFromRowKey(this string rowKey)
    {
        return rowKey.Substring(1, rowKey.Length-1);
    }
}