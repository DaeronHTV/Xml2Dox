namespace Xml2Dox.Librairie;

/// <summary>
/// Class that contains the options for the generation of stylesheet documentation
/// </summary>
public class XsltDocOptions
{
    private static readonly TextsText[] listText = Texts.Instance.Text;

    #region Options Property
    /// <summary>
    /// The path to the folder or the file for the generation
    /// </summary>
    public string Path { get; private init; }

    /// <summary>
    /// Indicate if the path describe a folder or not
    /// </summary>
    /// <remarks>(Optional) By default False, but we verify anyway by security</remarks>
    public bool IsFolder { get; private set; }

    /// <summary>
    /// The output path 
    /// </summary>
    /// <remarks>Optional, if not given the output folder will be the input folder</remarks>
    public string OutputPath { get; private init; }

    /// <summary>
    /// Indicate if we need to include the sub folder in the research of file to documentate
    /// </summary>
    /// <remarks>Optional, by default false</remarks>
    public string IncludeSubDirectory { get; private init; }

    /// <summary>
    /// The list of extensions to take in charge for researching the file
    /// </summary>
    public ISet<string> Extensions { get; private init; }

    /// <summary>
    /// Give the format to generate for the documentation
    /// </summary>
    public string Format { get; private init; }
    #endregion

    /// <summary>
    /// The constructor used by the console application to give the parameters
    /// </summary>
    /// <param name="args">The list of argument given by the user</param>
    public XsltDocOptions(string[] args)
    {
        if (args.Length < 2)
        {
            throw new ArgumentException(listText.FirstOrDefault(t => t.Name == TextConstHelper.XsltOptionsNotEnoughArgument)?.Value);
        }
        Path = args[1];
        IsFolder = File.Exists(Path);
        var index = Array.IndexOf(args, OptionsConstHelper.OutputPath);
        OutputPath = index is not -1 ? args[index++] : Path;
        index = Array.IndexOf(args, OptionsConstHelper.GiveFormatDoc);
        Format = index is not -1 ? args[index++] : "Xml";

    }

    /// <summary>
    /// Verifie la configuration donnée par l'utilisateur pour voir si il est possible
    /// d'executer le processus
    /// </summary>
    /// <returns>True si la configuration donnée est correcte, False sinon</returns>
    internal bool CheckConfig()
    {
        if (!IsFolder && !Directory.Exists(Path))
        {
            Console.WriteLine(listText.FirstOrDefault(t => t.Name == TextConstHelper.WrongInputPath)?.Value);
            return false;
        }
        if (OutputPath != Path && !Directory.Exists(OutputPath))
        {
            Console.WriteLine(listText.FirstOrDefault(t => t.Name == TextConstHelper.WrongOutputPath)?.Value);
            return false;
        }
        return true;
    }
}
