namespace Xml2Dox;

/// <summary>
/// Main class of the application
/// </summary>
public class Program
{
    static void Main(string[] args)
    {
        TextsText[] listText = Texts.Instance.Text;
        if (args.Length == 0)
        {
            Console.WriteLine(listText.FirstOrDefault(t => t.Name == TextConstHelper.NotEnoughArgument)?.Value);
            return;
        }
        try
        {
            switch (args[0])
            {
                case CommandConstHelper.Xsl:
                    {
                        if (!Proxy.Instance.GenerateDocumentation(new XsltDocOptions(args)))
                        {
                            //TODO
                        }
                        break;
                    }
                case CommandConstHelper.Help:
                    { 
                        CommandHelper.GetHelpFile(args);
                        break;
                    }
                case CommandConstHelper.Version:
                    {
                        Console.WriteLine(CommandConstHelper.Version);
                        break;
                    }
                case CommandConstHelper.Config:
                    { 
                        break;
                    }
                default:
                    Console.WriteLine(listText.FirstOrDefault(t => t.Name == TextConstHelper.CommandNotFound)?.Value);
                    Console.WriteLine(listText.FirstOrDefault(t => t.Name == TextConstHelper.WantDetails)?.Value);
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(listText.FirstOrDefault(t => t.Name == TextConstHelper.Exception)?.Value);
            Console.WriteLine(e);
        }
    }
}