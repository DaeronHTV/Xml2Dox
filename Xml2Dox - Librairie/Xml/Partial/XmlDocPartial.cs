namespace Xml2Dox.Librairie;

public partial class Documentations: IXmlCommon
{
    internal List<DocumentationsTemplate> templates;
    internal List<DocumentationsVariable> variables;
}

public partial class DocumentationsTemplate : IXmlCommon
{
    internal IList<DocumentationsTemplateParam> Parametres
    {
        set => Params = value.ToArray();
    }
}

public partial class DocumentationsVariable : IXmlCommon { }