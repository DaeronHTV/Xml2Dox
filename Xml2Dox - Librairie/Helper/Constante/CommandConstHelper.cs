namespace Xml2Dox;

/// <summary>
/// Class that contains all the name of the command for switch in the main program
/// </summary>
public static class CommandConstHelper
{
    public const string Help = "help";
    public const string Xsl = "xsl";
    public const string Version = "version";
    public const string Config = "config";
}

/// <summary>
/// Class that contains all the options possible for the different command
/// </summary>
public static class OptionsConstHelper
{
    public const string HelpDetails = "/D";
    public const string IncludeSubFile = "/S";
    public const string GiveFormatDoc = "/F";
    public const string OutputPath = "/O";
}