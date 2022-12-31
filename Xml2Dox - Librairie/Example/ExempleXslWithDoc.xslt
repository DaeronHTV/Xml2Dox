<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!-- 
@Name XML documentation into HTML documentation
@Description A stylesheet that allows to transform an xml into html
@Remarks 
@Version Version of the file if you want 
@Author DaeronHTV
-->
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<!-- 
	@Description principale template de la feuille de style permettant de générer le documentation souhaitée
	@Remarks Ici on peut rajouter des remarques spéciales si nécessaire
	@Version You can give a version to the template if you want ot keeep an history of modification for each of them
	-->
	<xsl:template match="Books">
		<xsl:element name="Authors">
			
		</xsl:element>
	</xsl:template>

	<!--
		@description Permet de determiner si le service interop doit rejeter le message reçu
		si il existe un champ obligatoire dans le message qui n'est pas present
		@remarks les champs obligatoire sont specifies dans un tableau de foo.
		Il s'agit d'une template recursive !!
		@return True si un champ est bien manquant, False sinon
	-->
	<xsl:template name="GetNoAction">
		<xsl:param name="indice" select="'0'"/>
		<xsl:param name="result" select="'False'"/>
		<xsl:variable name="nbRejet" select="count(document('')/xsl:stylesheet/*)"/>
		<!--<xsl:value-of select="concat('Nb Rejet(s) : ', $nbRejet)"/>-->
		<xsl:choose>
			<xsl:when test="$result = 'True' or $indice = $nbRejet">
				<xsl:value-of select="$result"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="node" select="document('')/xsl:stylesheet/*"/>
				<!--<xsl:value-of select="$node/@Segment"/>-->
				<xsl:call-template name="GetNoAction">
					<xsl:with-param name="indice" select="$indice+1"/>
					<xsl:with-param name="result">
						<xsl:call-template name="SMissFieldReception">
							<xsl:with-param name="Segments" select="./*[contains(name(), $node/@Segment)]/*"/>
							<xsl:with-param name="Field" select="$node/@Field"/>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<!--
		@description Cette Template genere un boolean que nous utilisons pour detecter ou non si
		un champ obligatoire pour l'integration est manquant ou non.
		@remarks Separe des templates principales car communes a plusieurs templates et pour 
		faciliter le debug en cas de probleme
		@param Segments - La nodelist du segment souhaite
		@param Field - Le champ a rechercher
		@return True si le champ est manquant, False sinon
	-->
	<xsl:template name="SMissFieldReception">
		<xsl:param name="Segments"/>
		<xsl:param name="Field"/>
		<!--<xsl:value-of select="$Segments"/>-->
		<xsl:variable name="found">
			<xsl:for-each select="$Segments">
				<!-- Exemple : ./*[contains(name(), 'QPD')]/* donne la liste des QPD dans le QPDs-->
				<xsl:variable name="fields" select="./*[contains(name(), $Field)]/*[node()]"/>
				<!--<xsl:value-of select="count($fields)"/>-->
				<xsl:choose>
					<xsl:when test="count($fields) = '0'">True</xsl:when>
					<xsl:when test="count($fields) != count(./*[contains(name(), $Field)]/*)">True</xsl:when>
					<xsl:otherwise>False</xsl:otherwise>
				</xsl:choose>
			</xsl:for-each>
		</xsl:variable>
		<xsl:value-of select="$found"/>
	</xsl:template>
</xsl:stylesheet>