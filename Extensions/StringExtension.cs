using System.Text.RegularExpressions;
using DataServices.Models.Auth.Permissions;
using DataServices.Models.Auth.Users;

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

    public static string MaskString(this string str, bool full = false)
    {
        const string defaultString = "***";
        if (string.IsNullOrEmpty(str) || full) return defaultString;

        try
        {
            if (str.Contains("@")) return str.MaskEmail();
            if (str.Length <= 4 || str.ToLower() == "true" || str.ToLower() == "false") return defaultString;
            if (str.Length < 8)
            {
                var firstSubstring = str.Substring(0, 2);
                var lastSubstring = str.Substring(str.Length - 1, 1);
                var middleString = new string('*', str.Length - firstSubstring.Length - lastSubstring.Length);
                return firstSubstring + middleString + lastSubstring;
            }
            else
            {
                var firstSubstring = str.Substring(0, 3);
                var lastSubstring = str.Substring(str.Length - 2, 2);
                var middleString = new string('*', str.Length - firstSubstring.Length - lastSubstring.Length);
                return firstSubstring + middleString + lastSubstring;
            }
        }
        catch (Exception)
        {
            return defaultString;
        }
    }

    public static string MaskEmail(this string email)
    {
        var slitted = email.Split("@");
        if (slitted[0].Length < 5)
        {
            var pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{0}@)";
            return Regex.Replace(email, pattern, m => new string('*', m.Length));
        }
        else
        {
            var pattern = @"(?<=[\w]{2})[\w-\._\+%]*(?=[\w]{1}@)";
            return Regex.Replace(email, pattern, m => new string('*', m.Length));
        }
    }

    public static string GetByPermission(this string str, IBackOfficeUser user, PermissionResource resource, bool full = false)
    {
        if (user.CanView(resource))
        {
            return str;
        }

        return str.MaskString(full);
    }
    
    public static string MaskByBool(this string str, bool canView)
    {
        if (canView)
        {
            return str;
        }

        return str.MaskString();
    }
}