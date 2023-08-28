namespace DataServices.Extensions;

public static class StringExtension
{
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
    public static bool IsNotNullOrEmpty(this string str)
    {
        return !string.IsNullOrEmpty(str);
    }

    public static string Join(this string[] strArray, string separator = ",")
    {
        return string.Join(separator, strArray);
    }

    public static string Join(this IEnumerable<string> strArray, string separator = ",")
    {
        return string.Join(separator, strArray);
    }

    public static string ToBase64(this string plainText) 
    {
        var plainTextBytes = System.Text.Encoding.UTF32.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}