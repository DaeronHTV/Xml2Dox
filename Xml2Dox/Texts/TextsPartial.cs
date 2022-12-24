using System;
using System.Collections.Generic;
using Xml2Dox.Librairie;

namespace Xml2Dox
{
    /// <summary>
    /// Class aut-generated in order to get the text shown by the application in order to be 
    /// </summary>
    public partial class Texts
    {
        private static readonly object locker = new();
        private static Texts instance = null!;

        /// <summary>
        /// 
        /// </summary>
        public static Texts Instance
        {
            get
            {
                if(instance is null)
                {
                    lock (locker)
                    {
                        if(instance is null)
                        {
                            if (!FileHelper.TryDezerializeXml($"./Assets/Text/Text_{Lang.Instance.Language}.xml", out instance))
                                Console.WriteLine("Impossible to load the text file");
                        }
                    }
                }
                return instance;
            }
        }
    }
}
