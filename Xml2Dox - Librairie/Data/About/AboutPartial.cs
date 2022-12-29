namespace Xml2Dox.Librairie;

public partial class About
{
    private static readonly object locker = new();
    private static About instance = null!;

    public static About Instance
    {
        get
        {
            if(instance is null)
            {
                lock(locker)
                {
                    if(instance is null)
                    {
                        if (!FileHelper.TryDezerializeXml("./Assets/About.xml", out instance))
                        {
                            instance = new About()
                            {
                                Name = ConstHelper.ApplicationName,
                                Description = new AboutText[] {
                                    new AboutText()
                                    {
                                        Value = ConstHelper.DefaultDescription,
                                        Lang = "EN"
                                    }
                                }
                            };
                        }
                    }
                }
            }
            return instance;
        }
    }
}
