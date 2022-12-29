namespace Xml2Dox.Librairie;

internal class XslDocToHtml: IXmlDoc
{
    private const string xsltPath = "./Assets/XmlToHtml.xslt";

    /// <inheritdoc/>
    public bool GenerateDocumentation(XsltDocOptions options)
    {
        XslDocToXml parseur = new();
        if (!parseur.GenerateObject(options, out var doc))
        {
            return false;
        }
        return default;
    }
}
