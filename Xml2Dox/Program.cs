using Xml2Dox.Librairie;

namespace Xml2Dox;

/// <summary>
/// Main class of the application
/// </summary>
internal class Program
{ 
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Not enough arguments to launch the application");
        }
        else
        {
            switch (args[0].Substring(1, args[0].Length-1))
            {
                case nameof(Actions.xsl):
                    { //Launch the generation of a xsl documentation
                        var parseur = new XslDocToXml(new XsltDocOptions(args));
                        if (parseur.GenerateXml())
                        {

                        }
                        break;
                    }
                case nameof(Actions.h):
                    { //Show help file for the commands
                        GetHelpFile();
                        break;
                    }
                case nameof(Actions.v):
                    { //Show the version of the application
                        break;
                    }
                case nameof(Actions.c):
                    { //Generate documentation in function of a config file
                        break;
                    }
                default:
                    //TODO Use a xml file specific with the langage of the user
                    Console.WriteLine("Actions non connues par l'application");
                    Console.WriteLine("Tapez la commande Xml2Dox /h pour avoir une description complete des actions possibles.");
                    break;
            }
        }
    }

    static void GetHelpFile()
    {
        
    }
}