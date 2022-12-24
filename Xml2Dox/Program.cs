namespace Xml2Dox;

/// <summary>
/// Main class of the application
/// </summary>
internal class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(Texts.Instance.Text.FirstOrDefault(t => t.Name == TextConstHelper.NotEnoughArgument)?.Value);
        }
        else
        {
            try
            {
                switch (args[0])
                {
                    case CommandConstHelper.Xsl:
                        { //Launch the generation of a xsl documentation
                            var parseur = new XslDocToXml(new XsltDocOptions(args));
                            if (parseur.GenerateXml())
                            {

                            }
                            break;
                        }
                    case CommandConstHelper.Help:
                        { //Show help file for the commands
                            GetHelpFile(args);
                            break;
                        }
                    case CommandConstHelper.Version:
                        {
                            Console.WriteLine(CommandConstHelper.Version);
                            break;
                        }
                    case CommandConstHelper.Config:
                        { //Generate documentation in function of a config file
                            break;
                        }
                    default:
                        Console.WriteLine(Texts.Instance.Text.FirstOrDefault(t => t.Name == TextConstHelper.CommandNotFound)?.Value);
                        Console.WriteLine(Texts.Instance.Text.FirstOrDefault(t => t.Name == TextConstHelper.WantDetails)?.Value);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Texts.Instance.Text.FirstOrDefault(t => t.Name == TextConstHelper.Exception)?.Value);
                Console.WriteLine(e);
            }
        }
    }

    /// <summary>
    /// Show the result of the command help in function of the options and parameters given
    /// </summary>
    /// <param name="args">Arguments list given by the user when calling the application</param>
    static void GetHelpFile(string[] args)
    {
        if (args.Length == 1)
            Help.Instance.ShowCommandBaseList();
        else if (args[1] is "/D")
            Help.Instance.ShowAllCommandDetails();
        else
            Help.Instance.ShowCommandDetails(args[1]);
    }
}