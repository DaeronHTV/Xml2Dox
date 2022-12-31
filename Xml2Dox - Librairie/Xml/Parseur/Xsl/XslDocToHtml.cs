namespace Xml2Dox.Librairie;

internal class XslDocToHtml: IXmlDoc
{
    private XsltDocOptions Options = null!;
    private const string xsltPath = "./Assets/XmlToHtml.xslt";

    /// <inheritdoc/>
    public bool GenerateDocumentation()
    {
        XslDocToXml parseur = new();
        if (!parseur.GenerateObject(out var doc))
        {
            return false;
        }
        return default;
    }

    public void Initialisation(XsltDocOptions options)
    {
        Options = options;
    }
}
