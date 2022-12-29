namespace Xml2Dox.Librairie;

/// <summary>
/// Class that contains methods in order to simplify string manipulation
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// Allow to get the content of a string that is present after a specific string
    /// </summary>
    /// <example>"Hello World".SubstringAfter("Hello") => "World"</example>
    /// <param name="str">The string to cut</param>
    /// <param name="input">The string to search</param>
    /// <returns></returns>
    public static string SubstringAfter(this string str, string input)
    {
        var index = str.IndexOf(input);
        if (index is not -1)
        {
            return str.Substring(index + str.Length);
        }
        return str;
    }

    /// <summary>
    /// Transform a string in order to respect the title case (first letter in upper only)
    /// </summary>
    /// <param name="str">The content to modify</param>
    /// <returns>The content in title case</returns>
    public static string ToTitleCase(this string str)
    {
        return $"{char.ToUpper(str[0])}{str[1..].ToLower()}";
    }
}
