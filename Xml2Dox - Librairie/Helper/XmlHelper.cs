using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace Xml2Dox.Librairie.Helper;

internal static class XmlHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public static XslCompiledTransform CompilationXSL(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ApplicationException("Impossible to compile the xsl file ! Path is empty !");
        if (!File.Exists(path))
            return null!;
        else
        {
            XsltSettings settings = new()
            {
                EnableScript = true,
                EnableDocumentFunction = true
            };
            var xslTransform = new XslCompiledTransform(true);
            xslTransform.Load(path, settings, null);
            return xslTransform;
        }
    }

    public static void TransformOut(XslCompiledTransform xslTransform, Stream streamInfologic, XmlReader xmlreader)
    {
        var xWSetting = xslTransform.OutputSettings?.Clone() ?? new XmlWriterSettings();
        xWSetting.NewLineChars = Environment.NewLine;
        xWSetting.Indent = true;
        //xWSetting.Encoding = new UTF8Encoding(false, false);
        using (XmlWriter writer = XmlWriter.Create(streamInfologic, xWSetting))
        {
            xslTransform.Transform(xmlreader, writer);
        }
    }

    public static XDocument GetXml(XDocument xmlMessage, XslCompiledTransform xsl, string path, string filename)
    {
        using (var streamInfologic = new MemoryStream())
        {
            using (var xmlreader = xmlMessage.CreateReader())
            {
                TransformOut(xsl, streamInfologic, xmlreader);
                var file = Path.Combine(path, filename);
                using (var fileStream = File.Create(file))
                {
                    streamInfologic.Seek(0, SeekOrigin.Begin);
                    streamInfologic.CopyTo(fileStream);
                    fileStream.Close();
                }
                return XDocument.Load(file);
            }
        }
    }
}
