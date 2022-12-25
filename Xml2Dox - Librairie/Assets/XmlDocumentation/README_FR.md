Ce document contient la liste des types possibles pour la documentation des feuilles de style xsl

- Les élements commun pour toutes les balises
	+ @Description : Une rapide description de l'objet en cours de documentation
	+ @Version : (Optionel) Version actuelle de l'objet
	+ @Remarks : (Optionel)Permet d'ajouter des informations complémentaires si nécessaire

- Pour la feuille de style globale (xsl:stylesheet)
	+ @Name : (Optionel) Nom de la feuille de style. Si non présent on prendra le nom de la feuille de style (XXXX.xsl)
	+ @Author : (Optionel) Permet d'indiquer la personne à la base de la création ou la dernière à avoir modifier

- Pour les templates
	+ @Param : Donne des informations sur un paramètre de la template (Code - Description)
	+ @Return : Indique ce que retourne la template si elle retourne une information
	+ @DateDerModif : (Optionel) Permet d'indiquer la date de dernière modification
	+ @Author : (Optionel) Permet d'indiquer la personne à la base de la création ou la dernière à avoir modifier
	+ Remarque : Le nom de la template sera récupéré via l'attribut name de la balise

- Pour les variables globals
	+ Aucun element supplémentaires à part ceux commun avec les autres balises

- Pour les autres balises (exemple foo:XXX, etc...)
	+ Actuellement rien n'est prévu pour ces balises. Suivant leur utilité on pourra se permettre de rajouter des possibilités
	pour ces dernières

Remarque : Les variables contenus à l'intérieur d'une template ne sont pas encore pris en compte dans la documentation et ne le 
sera probablement jamais.

Remarque : Il est possible de mettre le nom des élements (@XXX) dans le format souhaité (Upper, lower, CamelCase), tous seront traités
de la même manière.