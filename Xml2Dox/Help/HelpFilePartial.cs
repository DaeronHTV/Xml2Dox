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
    internal static Help Instance
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
    /// Show the base list of command when the user execute the command Xml2Dox help
    /// </summary>
    internal void ShowCommandBaseList()
    {
        foreach(HelpCommand command in Commands)
        {
            Console.WriteLine($"{command.Name} : {command.Description}");
            Console.WriteLine($"Exemple : {command.Exemple}\n");
        }
        Console.WriteLine(Texts.FirstOrDefault(t => t.Name == "WantDetails")?.Value);
    }

    /// <summary>
    /// Generate all the documentation in detail for all the command
    /// </summary>
    internal void ShowAllCommandDetails()
    {
        foreach(HelpCommand command in Commands)
            ShowCommandDetails(command.Name);
    }

    /// <summary>
    /// Show the details of the specifics command when the user execute the commande Xml2Dox /h [commandName]
    /// </summary>
    /// <param name="commandName">The name of the command to show the help</param>
    internal void ShowCommandDetails(string commandName)
    {
        var command = Commands.FirstOrDefault(t => t.Name == commandName);
        if(command is null)
        {
            Console.WriteLine(Texts.FirstOrDefault(t => t.Name == "CommandNotFound")?.Value);
            return;
        }
        Console.WriteLine($"{command.Name} : {command.Description}");
        Console.WriteLine($"Exemple : {command.Exemple}");
        GenerateIParametersDoc(command.Parameters, "Parameters");
        GenerateIParametersDoc(command.Options, "Options");
    }

    /// <summary>
    /// Generate the list of parameters or options for a command
    /// </summary>
    /// <param name="list">List that contains the data</param>
    /// <param name="name">Name of the type data shown</param>
    private void GenerateIParametersDoc(IParameters[] list, string name)
    {
        if(list is not null && list.Length > 0)
        {
            Console.WriteLine($"\n{name}\n");
            foreach (IParameters objet in list)
                Console.WriteLine($"{objet.Name} : {objet.Value}");
        }
    }
}

/// <summary>
/// Class that defines the object Parameter in the XML file
/// </summary>
/// <remarks>
/// Jsut here to apply an interface to simplify the generation of the documentation
/// </remarks>
public partial class HelpCommandParameter : IParameters { }

/// <summary>
/// Class that defines the objet Option in the XML file
/// </summary>
/// <remarks>
/// Just here to apply an interface to simplify the generation of the documentation
/// </remarks>
public partial class HelpCommandOption : IParameters { }
