using System.Xml;
using System.Xml.Serialization;

namespace Xml2Dox.Librairie;

/// <summary>
/// 
/// </summary>
public static class FileHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="contentObject"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="outputPath"></param>
    /// <param name="fileName"></param>
    /// <param name="contentObject"></param>
    /// <returns></returns>
    public static bool TrySerializeXml<T>(string outputPath, string fileName, T contentObject)
    {
        if(contentObject is null || !Directory.Exists(outputPath))
        {
            return false;
        }
        try
        {
            var serializer = new XmlSerializer(typeof(T));
            var file = File.Create(Path.Combine(outputPath, fileName));
            serializer.Serialize(file, contentObject);
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occured during the serialization of XMl file !");
            Console.WriteLine(ex.ToString());
            return false;
        }
        return true;
    }
}
