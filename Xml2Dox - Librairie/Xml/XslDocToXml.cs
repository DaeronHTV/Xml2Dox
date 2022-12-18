using System.Xml.Linq;

namespace Xml2Dox.Librairie
{
    /// <summary>
    /// Classe permettant de lancer de processus de création d'une documentation 
    /// des feuilles xslt dans un format xml propriétaire
    /// </summary>
    public class XslDocToXml
    {
        /// <summary>
        /// Les options pour la génération de la documentation
        /// </summary>
        private XsltDocOptions Options;

        private List<string> ouputComment = new()
    {
        "@Name",
        "@Version",
        "@VersionS1",
        "@VersionS2",
        "@Historique",
    };

        public XslDocToXml(XsltDocOptions options)
        {
            Options = options;
        }

        public bool GenerateXml()
        {
            Console.WriteLine("Generation du xml dde documentations");
            var root = XDocument.Load(Options.Path).Root;
            if (root is not null)
            {
                var result = new Documentations();
                using (var enumerator = root.Nodes().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var element = enumerator.Current;
                        if (typeof(XComment).IsAssignableFrom(element.GetType()))
                        {
                            var comment = element as XComment;
                            if (comment!.Value.Contains('@'))
                            {
                                TreatNode(ref result, comment, (element.NextNode as XElement)!);
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void TreatNode(ref Documentations result, XComment comment, XElement element)
        {
            if (element is not null)
            {
                switch (element.Name.LocalName)
                {
                    case "element":
                        {
                            break;
                        }
                    case "output":
                        TreatOuptutComment(ref result, comment);
                        break;
                    case "template":
                        {
                            TreatTemplateComment(ref result, comment, element);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void TreatOuptutComment(ref Documentations result, XComment comment)
        {
            result.Name = Path.GetFileName(Options.Path);
            var datas = comment.Value.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            for (int i = 0; i < datas.Length; i++)
            {
                bool treat = false;
                foreach (var name in ouputComment)
                {

                    if (datas[i].StartsWith(name))
                    {
                        switch (name)
                        {
                            case "@Name":
                                {
                                    result.Name = datas[i][name.Length..].Trim();
                                    treat = true;
                                    continue;
                                }
                            case "@Version":
                                {
                                    result.Version = datas[i][name.Length..].Trim();
                                    treat = true;
                                    continue;
                                }
                            case "@VersionS1":
                                {
                                    var split = datas[i][name.Length..].Trim().Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                                    result.VersionS1 = new Version { Name = split?[0]!, Value = split?[1]! };
                                    treat = true;
                                    continue;
                                }
                            case "@VersionS2":
                                {
                                    var split = datas[i][name.Length..].Trim().Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                                    result.VersionS2 = new Version { Name = split?[0]!, Value = split?[1]! };
                                    treat = true;
                                    continue;
                                }
                            case "@Historique":
                                {
                                    i += 2;

                                    treat = true;
                                    continue;
                                }
                            default:
                                treat = true;
                                continue;
                        }
                    }
                    if (treat)
                        break;
                }
            }
            var index = Array.IndexOf(datas, XslConstHelper.VersionS1);
            if (index is not -1)
            {
                var test = datas[index].Substring(datas[index].IndexOf(' ')).Split('-', StringSplitOptions.TrimEntries);
                result.VersionS1 = new Version { Name = test?[0]!, Value = test?[1]! };
            }
            index = Array.IndexOf(datas, XslConstHelper.VersionS2);
            if (index is not -1)
            {
                result.VersionS2 = new Version { Name = "", Value = "" };
            }
            index = Array.IndexOf(datas, XslConstHelper.Historique);
            if (index is not -1)
            {

            }
        }

        private void TreatTemplateComment(ref Documentations result, XComment comment, XElement element)
        {
            var datas = comment.Value.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        }
    }
}
