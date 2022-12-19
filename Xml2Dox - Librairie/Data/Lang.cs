using System.Globalization;

namespace Xml2Dox.Librairie;

public class Lang
{
    #region Attributs
    private static readonly object locker = new();
    private static Lang instance = null!;
    public string lang;
    #endregion

    #region Class Management
    private Lang() 
    {
        var cultureLang = CultureInfo.CurrentCulture.Name;
        lang = cultureLang.Contains("FR") ? "FR" : "EN";
    }

    public static Lang Instance
    {
        get
        {
            if(instance is null)
            {
                lock (locker)
                {
                    instance ??= new Lang();
                }
            }
            return instance;
        }
    }
    #endregion

    public string Language { get => lang; }
}
