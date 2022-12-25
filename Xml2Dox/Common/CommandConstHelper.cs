namespace Xml2Dox;

/// <summary>
/// Class that contains all the name of the command for switch in the main program
/// </summary>
internal static class CommandConstHelper
{
    internal const string Help = "help";
    internal const string Xsl = "xsl";
    internal const string Version = "version";
    internal const string Config = "config";
}

/// <summary>
/// Class that contains all the options possible for the different command
/// </summary>
internal static class OptionsConstHelper
{
    internal const string HelpDetails = "/D";
    internal const string IncludeSubFile = "/S";
    internal const string GiveFormatDoc = "/F";
    internal const string OutputPath = "/O";
}