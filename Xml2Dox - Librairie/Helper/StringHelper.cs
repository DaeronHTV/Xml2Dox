namespace Xml2Dox.Librairie;

public static class StringHelper
{
    public static string SubstringAfter(string str, string input)
    {
        var index = input.IndexOf(str);
        if (index is not -1)
        {
            return input.Substring(index + str.Length);
        }
        return input;
    }
}
