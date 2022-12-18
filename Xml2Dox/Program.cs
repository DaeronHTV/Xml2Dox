using Xml2Dox.Librairie;

namespace Xml2Dox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Pas assez d'argument pour lancer l'application");
            }
            else
            {
                switch (args[0])
                {
                    case nameof(Actions.xsl):
                        {
                            var parseur = new XslDocToXml(new XsltDocOptions(args));
                            if (parseur.GenerateXml())
                            {

                            }
                            break;
                        }
                    case nameof(Actions.h):
                        {
                            break;
                        }
                    case nameof(Actions.v):
                        {
                            break;
                        }
                    case nameof(Actions.c):
                        {
                            break;
                        }
                    case nameof(Actions.g):
                        { //Genere tout
                            break;
                        }
                    default:
                        Console.WriteLine("Actions non connues par l'application");
                        break;
                }
            }
        }
    }
}