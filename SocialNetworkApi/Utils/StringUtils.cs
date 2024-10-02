using System.Reflection;
using System.Text.RegularExpressions;

namespace ExpenseTrackerGrupo2.Utils;

public static class StringUtils
{
    public static string ToSnakeCase(string input)
    {
        return Regex.Replace(input, "(?<!^)[A-Z]", "_$0").ToLower();
    }

    public static string GetColumnNames<T>()
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return string.Join(", ", properties.Select(p => StringUtils.ToSnakeCase(p.Name)));
    }

    public static string GetColumnParameters<T>()
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return string.Join(", ", properties.Select(p => "@" + StringUtils.ToSnakeCase(p.Name)));
    }

    public static string GetSetClause<T>()
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return string.Join(", ", properties.Select(p => $"{StringUtils.ToSnakeCase(p.Name)} = @{StringUtils.ToSnakeCase(p.Name)}"));
    }

}
