using Xml2Dox.Librairie;

namespace Xml2Dox;

public partial class Help
{
    private static readonly object locker = new object();
    private static Help instance = null!;

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
                        if(FileHelper.TryDezerializeXml($"./Assets/HelpFile/HelpFile_{Lang.Instance.Language}.xml", out instance))
                        {
                            Console.WriteLine("Impossible to load the help file");
                        }
                    }
                }
            }
            return instance;
        }
    }
}
