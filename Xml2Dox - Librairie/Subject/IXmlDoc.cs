namespace Xml2Dox.Librairie;

/// <summary>
/// Describe the method exposed on the different parseur that creates the documentation
/// </summary>
public interface IXmlDoc
{
    void Initialisation(XsltDocOptions options);

    /// <summary>
    /// Global method that generates the documentation in function of the options
    /// </summary>
    /// <param name="options">The options given by the user to generate the documentation</param>
    bool GenerateDocumentation();
}
