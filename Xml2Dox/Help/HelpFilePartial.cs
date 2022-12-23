using Xml2Dox.Librairie;

namespace Xml2Dox;

/// <summary>
/// Class that describe the XML file that contains the content shown by the help command
/// </summary>
public partial class Help
{
    private static readonly object locker = new();
    private static Help instance = null!;

    /// <summary>
    /// Get the singleton instance of the help object
    /// </summary>
    public static Help Instance
    {
        get
        {
            if(instance is null)
            {
                lock(locker)
                {
                    if(instance is null)
                    {
                        if(!FileHelper.TryDezerializeXml($"./Assets/HelpFile/HelpFile_{Lang.Instance.Language}.xml", out instance))
                            Console.WriteLine("Impossible to load the help file");
                    }
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// Show the base list of command when the user execute the command Xml2Dox /h
    /// </summary>
    public void ShowCommandBaseList()
    {
        foreach(HelpCommand command in Commands)
        {
            Console.WriteLine($"{command.Name} : {command.Description}");
            Console.WriteLine($"Exemple : {command.Exemple}\n");
        }
        Console.WriteLine(Texts.FirstOrDefault(t => t.Name == "WantDetails")?.Value);
    }

    public void ShowAllCommandDetails()
    {
        foreach(HelpCommand command in Commands)
        {
            ShowCommandDetails(command.Name);
            Console.WriteLine("\n----------------------------------------------------------------\n");
        }
    }

    /// <summary>
    /// Show the details of the specifics command when the user execute the commande Xml2Dox /h [commandName]
    /// </summary>
    /// <param name="commandName">The name of the command to show the help</param>
    public void ShowCommandDetails(string commandName)
    {
        var command = Commands.FirstOrDefault(t => t.Name == commandName);
        if(command is null)
        {
            Console.WriteLine(Texts.FirstOrDefault(t => t.Name == "CommandNotFound")?.Value);
            return;
        }
        Console.WriteLine($"{command.Name} : {command.Description}");
        Console.WriteLine($"Exemple : {command.Exemple}");
        if(command.Parameters is not null && command.Parameters.Length > 0)
        {
            Console.WriteLine("\n"+nameof(command.Parameters).ToUpper()+"\n");
            foreach (IParameters parameter in command.Parameters)
                Console.WriteLine($"[{parameter.Name}] : {parameter.Value}");
        }
        if(command.Options is not null && command.Options.Length > 0)
        {
            Console.WriteLine("\n"+nameof(command.Options).ToUpper()+ "\n");
            foreach (IParameters option in command.Options)
                Console.WriteLine($"{option.Name} : {option.Value}");
        }
    }
}

public partial class HelpCommandParameter : IParameters { }

public partial class HelpCommandOption : IParameters { }
