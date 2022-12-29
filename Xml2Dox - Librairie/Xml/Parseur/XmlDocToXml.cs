using System.Xml.Linq;

namespace Xml2Dox.Librairie;

/// <summary>
/// Class allowing to launch the process of documentation into a proper xml format
/// </summary>
internal class XmlDocToXml : IXmlDoc
{
    private XsltDocOptions Options = null!;
    private readonly Type xCommentType = typeof(XComment);
    private const char AT = '@';
    private const string CR = "\n";
    private const string ATNAME = "@NAME";
    private const string ATAUTHOR = "@AUTHOR";
    private const string ATDESCRIPTION = "@DESCRIPTION";
    private const string ATVERSION = "@VERSION";
    private const string ATREMARKS = "@REMARKS";
    private const string ATPARAM = "@PARAM";
    private const string VARIABLE = "VARIABLE";
    private const string OUTPUT = "OUTPUT";
    private const string TEMPLATE = "TEMPLATE";

    /// <inheritdoc/>
    public bool GenerateDocumentation(XsltDocOptions options)
    {
        Console.WriteLine("Xml documentations generation...");
        if (GenerateObject(options, out var doc))
        {
            Console.WriteLine("Seriliazation of xml documentation...");
            if (!FileHelper.TrySerializeXml(options.OutputPath, string.Concat(options.InputFileName, ".", options.Format), doc))
            {
                Console.WriteLine("An error occured during the save of the XML documentation file !");
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="doc"></param>
    /// <returns></returns>
    internal bool GenerateObject(XsltDocOptions options, out Documentations doc)
    {
        Options = options;
        doc = default!;
        XElement root = XDocument.Load(Options.Path).Root!;
        if (root is not null)
        {
            doc = new Documentations() { templates = new List<DocumentationsTemplate>(), variables = new List<DocumentationsVariable>() };
            foreach (var node in root.Nodes())
            {
                if (xCommentType.IsAssignableFrom(node.GetType()))
                {
                    var comment = node as XComment;
                    if (comment!.Value.Contains(AT))
                    {
                        TreatNode(ref doc, comment, (node.NextNode as XElement)!);
                    }
                }
            }
            doc.Templates = doc.templates.ToArray();
            doc.Variables = doc.variables.ToArray();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="commentNode"></param>
    /// <param name="node"></param>
    private void TreatNode(ref Documentations result, XComment commentNode, XElement node)
    {
        if (node is not null)
        {
            switch (node.Name.LocalName.ToUpper())
            {
                case VARIABLE: TreatVariableComment(ref result, commentNode, node); break;
                case OUTPUT: TreatOuptutComment(ref result, commentNode); break;
                case TEMPLATE: TreatTemplateComment(ref result, commentNode, node); break;
                default: break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="comment"></param>
    private void TreatOuptutComment(ref Documentations result, XComment comment)
    {
        var data = TreatCommon(ref result, comment, Path.GetFileName(Options.Path)).FirstOrDefault(d => d.ToUpper().StartsWith(ATAUTHOR));
        result.Authors = data?.Split(' ')[1].Split(" - ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="commentNode"></param>
    /// <param name="node"></param>
    private static void TreatVariableComment(ref Documentations result, XComment commentNode, XElement node)
    {
        var variable = new DocumentationsVariable();
        TreatCommon(ref variable, commentNode, node.Attributes().FirstOrDefault(a => a.Name == "name")?.Value!);
        variable.Value = node.Value;
        result.variables.Add(variable);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <param name="commentNode"></param>
    /// <param name="node"></param>
    private static void TreatTemplateComment(ref Documentations result, XComment commentNode, XElement node)
    {
        var template = new DocumentationsTemplate();
        var parametres = new List<DocumentationsTemplateParam>();
        var datas = TreatCommon(ref template, commentNode, node.Attributes().FirstOrDefault(a => a.Name == "name")?.Value!);
        foreach(var param in datas.Where(d => d.ToUpper().StartsWith(ATPARAM)).Select(d => d.Split(' ', 2)[1]))
        {
            var content = param.Split(" - ", StringSplitOptions.TrimEntries);
            parametres.Add(new() { Code = content[0], Description = content[1] });
        }
        template.Parametres = parametres;
        result.templates.Add(template);
    }

    private static string[] TreatCommon<T>(ref T result, XComment commentNode, string name = null!) where T : IXmlCommon
    {
        var datas = commentNode.Value.Split(CR, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var data in datas)
        {
            var list = data.Split(' ', 2);
            switch (list[0].ToUpper())
            {
                case ATNAME: result.Name = list[1].TrimStart(); break;
                case ATDESCRIPTION: result.Description = list[1].TrimStart(); break;
                case ATVERSION: result.Version = list[1].TrimStart(); break;
                case ATREMARKS: result.Remarks = list[1].TrimStart(); break;
                default: break;
            }
        }
        result.Name ??= name;
        return datas;
    }
}
