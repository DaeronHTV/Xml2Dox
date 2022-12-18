namespace Xml2Dox.Librairie
{
    internal class FileHelper
    {
        public string SubstringAfter(string str, string input)
        {
            var index = input.IndexOf(str);
            if (index is not -1)
            {
                return input.Substring(index + str.Length);
            }
            return input;
        }
    }
}
