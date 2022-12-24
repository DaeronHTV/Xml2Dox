namespace Xml2Dox;

/// <summary>
/// An interface to put in common the class Option and Parameter
/// generate by the xsd application to simplify the process of showing data
/// when the user uses the help command
/// </summary>
internal interface IParameters
{
    /// <summary>
    /// Name of the option or parameter
    /// </summary>
    internal string Name { get; }

    /// <summary>
    /// Description of the parameter or option
    /// </summary>
    internal string Value { get; }
}
