namespace Xml2Dox.Librairie
{
    /// <summary>
    /// Classe qui va contenir les options pour la génération des documentations
    /// pour les feuilles de styles
    /// </summary>
    public class XsltDocOptions
    {
        #region CONSTANTES
        private const string extOptions = "/E";
        private const string outputOptions = "/O";
        #endregion

        /// <summary>
        /// Le chemin vers le dossier ou le fichier à prendre pour la génération
        /// de la documentation
        /// </summary>
        public string Path { get; private init; }

        /// <summary>
        /// Indique si le chemin est un dossier ou non
        /// </summary>
        /// <remarks>Optionnel par defaut false, mais on vérifie quand même par sécurité</remarks>
        public bool IsFolder { get; private set; }

        /// <summary>
        /// Le chemin vers lequel déposer le fichier de documentations
        /// </summary>
        /// <remarks>Paramètre optionnel, si non présent alors le dossier ouput sera le dossier input</remarks>
        public string OutputPath { get; private init; }

        /// <summary>
        /// Indique si on doit rechercher dans les sous-dossiers du dossier donné
        /// pour récupérer les fichiers
        /// </summary>
        /// <remarks>Optionnel, par défaut false</remarks>
        public string IncludeSubDirectory { get; private init; }

        /// <summary>
        /// La liste des extensions de fichiers à traiter par le processus de
        /// documentations
        /// </summary>
        public ISet<string> Extensions { get; private init; }

        public XsltDocOptions(string[] args)
        {
            if (args.Length is not 2)
            {
                Path = null!;
            }
            else
            {
                Path = args[1];
                var index = Array.IndexOf(args, outputOptions);
                OutputPath = index is not -1 ? args[index + 1] : Path;

            }
        }

        /// <summary>
        /// Verifie la configuration donnée par l'utilisateur pour voir si il est possible
        /// d'executer le processus
        /// </summary>
        /// <returns>True si la configuration donnée est correcte, False sinon</returns>
        internal bool CheckConfig()
        {
            var IsFolder = File.Exists(Path);
            if (!IsFolder && !Directory.Exists(Path))
            {
                Console.WriteLine("Le chemin vers le fichier/dossier a traiter n'est definie ou est mal forme !");
                return false;
            }
            if (OutputPath != Path && !File.Exists(OutputPath) && !Directory.Exists(OutputPath))
            { //Si le ouputPath est différent du path seulement
                Console.WriteLine("Le chemin vers le dossier OutPut n'est definie ou est mal forme !");
                return false;
            }
            return true;
        }
    }
}
