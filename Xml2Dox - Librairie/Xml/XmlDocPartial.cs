namespace Xml2Dox.Librairie
{
    public partial class Documentations
    {
        IEnumerable<DocumentationsHistorique> EHistoriques
        {
            get => Historiques;
            set => Historiques = value.ToArray();
        }

        IEnumerable<DocumentationsVariable> EVariables
        {
            get => Variables;
            set => Variables = value.ToArray();
        }

        IEnumerable<DocumentationsTemplate> ETemplates
        {
            get => Templates;
            set => Templates = value.ToArray();
        }
    }

    public partial class DocumentationsTemplate
    {
        IEnumerable<DocumentationsTemplateParam> Parametres
        {
            get => Params;
            set => Params = value.ToArray();
        }
    }
}
