namespace Xml2Dox.Librairie;

public class DataHelper
{
    #region Attributs
    private static object locker = new object();
    private static DataHelper instance = null!;
    private About about;
    #endregion

    #region Class Management
    private DataHelper() 
    {
        InitAbout();
    }

    private void InitAbout()
    {
        if (FileHelper.TryDezerializeXml("./Assets/About.xml", out about))
        {
            about = new About()
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

    public static DataHelper Instance
    {
        get
        {
            if(instance is null)
            {
                lock (locker)
                {
                    if(instance is null)
                    {
                        instance = new DataHelper();
                    }
                }
            }
            return instance;
        }
    }
    #endregion

    public About About { get => about; }
}
