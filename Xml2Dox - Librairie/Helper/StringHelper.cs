namespace Xml2Dox.Librairie;

public static class StringHelper
{
    public static string SubstringAfter(this string str, string input)
    {
        var index = str.IndexOf(input);
        if (index is not -1)
        {
            return str.Substring(index + str.Length);
        }
        return str;
    }

    public static string ToTitleCase(this string str)
    {
        return $"{char.ToUpper(str[0])}{str[1..].ToLower()}";
    }
}
