using System.Xml;
using System.Xml.Serialization;

namespace Xml2Dox.Librairie;

public static class FileHelper
{
    public static bool TryDezerializeXml<T>(string path, out T contentObject)
    {
        contentObject = default!;
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using(var reader = XmlReader.Create(stream))
                {
                    if(serializer.CanDeserialize(reader))
                    {
                        contentObject = (T)serializer.Deserialize(reader)!;
                        return true;
                    }
                    return false;
                }
            }
        }
        catch
        {
            return false;
        }
    }
}
