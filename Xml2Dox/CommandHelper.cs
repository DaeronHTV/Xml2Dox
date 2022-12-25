namespace Xml2Dox;

internal static class CommandHelper
{
    /// <summary>
    /// Show the result of the command help in function of the options and parameters given
    /// </summary>
    /// <param name="args">Arguments list given by the user when calling the application</param>
    internal static void GetHelpFile(string[] args)
    {
        if (args.Length == 1)
            Help.Instance.ShowCommandBaseList();
        else if (args[1] is OptionsConstHelper.HelpDetails)
            Help.Instance.ShowAllCommandDetails();
        else
            Help.Instance.ShowCommandDetails(args[1]);
    }
}
