using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml2Dox
{
    internal enum Actions
    {
        /// <summary>
        /// On génère la documentation d'une feuille de style
        /// </summary>
        xsl = 0,
        /// <summary>
        /// Permet d'indiquer la demande de documentations de l'application
        /// </summary>
        h = 1,
        /// <summary>
        /// Permet de lancer un processus avec une configuration
        /// </summary>
        c = 2,
        /// <summary>
        /// Version de l'application
        /// </summary>
        v = 3,
        /// <summary>
        /// 
        /// </summary>
        g = 4
    }
}
