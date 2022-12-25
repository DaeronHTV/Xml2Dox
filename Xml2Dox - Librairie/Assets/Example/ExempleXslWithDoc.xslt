<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<!-- 
@Name XML documentation into HTML documentation
@Description A stylesheet that allows to transform an xml into html
@Version 1.0.0
@Author DaeronHTV
========================================================== -->
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>

	<xsl:template match="ITEST_ACKQUERY_MEDIPATH">
		<xsl:element name="GAPEntrant">
			<xsl:element name="Parametres">
				<xsl:attribute name="Gap">
					<xsl:value-of select="name()"/>
				</xsl:attribute>
				<xsl:element name="Parametre">
					<xsl:attribute name="name">REJETSINOACTION</xsl:attribute>
					<xsl:value-of select="'O'"/>
				</xsl:element>
			</xsl:element>
			<Support Name="OML_O21" Indice="1" Format="HL7">
				<xsl:attribute name="Version">
					<xsl:value-of select="MSHs/MSH/MSH.12s/MSH.12/MSH.12.1"/>
				</xsl:attribute>
				<xsl:attribute name="Valeur">
					<xsl:value-of select="MSHs/MSH/MSH.10s/MSH.10/MSH.10.1"/>
				</xsl:attribute>
				<Type>MLLP</Type>
				<DateHeure>
					<xsl:call-template name="formatdatehl7">
						<xsl:with-param name="maDate" select="./MSHs/MSH/MSH.7s/MSH.7/MSH.7.1"/>
					</xsl:call-template>
				</DateHeure>
				<DateHeureReception>
					<xsl:value-of select="@Maintenant"/>
				</DateHeureReception>
				<Files>
					<File>
						<Type>DONNEES</Type>
						<FileName>
							<xsl:value-of select="@FileName"/>
						</FileName>
						<PathName>
							<xsl:value-of select="@FilePath"/>
						</PathName>
						<FileCreateTime>
							<xsl:value-of select="@FileCreateTime"/>
						</FileCreateTime>
						<FileWriteTime>
							<xsl:value-of select="@FileWriteTime"/>
						</FileWriteTime>
					</File>
				</Files>
				<Infos>
					<Info Name="EMETTEUR" Indice="1" Type="STR">
						<xsl:attribute name="Code">
							<xsl:value-of select="MSHs/MSH/MSH.3s/MSH.3/MSH.3.1"/>
						</xsl:attribute>
						<xsl:value-of select="MSHs/MSH/MSH.4s/MSH.4/MSH.4.1"/>
					</Info>
					<Info Name="RECEPTEUR" Indice="2" Type="STR">
						<xsl:attribute name="Code">
							<xsl:value-of select="MSHs/MSH/MSH.5s/MSH.5/MSH.5.1"/>
						</xsl:attribute>
						<xsl:value-of select="MSHs/MSH/MSH.6s/MSH.6/MSH.6.1"/>
					</Info>
					<Info Name="DATE" Indice="3" Type="DAH" Format="yyyyMMddHHmmss">
						<xsl:call-template name="getLongDateHl7">
							<xsl:with-param name="dateHl7" select="./MSHs/MSH/MSH.7s/MSH.7/MSH.7.1"/>
						</xsl:call-template>
					</Info>
				</Infos>
			</Support>
			<Messages>
				<xsl:element name="Message">
					<xsl:attribute name="Name">
						<xsl:value-of select="''"/>
					</xsl:attribute>
					<xsl:attribute name="Indice">1</xsl:attribute>
					<xsl:attribute name="Valeur">
						<xsl:value-of select="./QPDs/QPD/QPD.3s/QPD.3/QPD.3.2"/>
					</xsl:attribute>
					<xsl:variable name="IsNoAction">
						<xsl:call-template name="GetNoAction"/>
					</xsl:variable>
					<!--<xsl:value-of select="concat('Valeur du boolean : ', $IsNoAction)"/>-->
					<xsl:choose>
						<xsl:when test="$IsNoAction = 'True'">
							<Actions>
								<Action Name="IGNORE" Indice="1">O</Action>
							</Actions>
						</xsl:when>
						<xsl:otherwise>
							<Actions>
								<Action Name="ACKQUERY" Indice="5" Parametre="">O</Action>
							</Actions>
							<Entites>
								<Entite Name="ACKQUERY" Indice="8" Multiple="N">
									<AckVariables>
										<AckVariable Name="NUDDEEXT" Format="">
											<xsl:value-of select="./QPDs/QPD/QPD.3s/QPD.3/QPD.3.2"/>
										</AckVariable>
									</AckVariables>
								</Entite>
							</Entites>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:element>
			</Messages>
		</xsl:element>
	</xsl:template>

	<!--
		@description Permet de determiner si le service interop doit rejeter le message reçu
		si il existe un champ obligatoire dans le message HL7 qui n'est pas present
		@remark les champs obligatoire sont specifies dans un tableau de foo.
		Il s'agit d'une template recursive !!
		@return True si un champ est bien manquant, False sinon
	-->
	<xsl:template name="GetNoAction">
		<xsl:param name="indice" select="'0'"/>
		<xsl:param name="result" select="'False'"/>
		<xsl:variable name="nbRejet" select="count(document('')/xsl:stylesheet/foo:rejects/foo:reject)"/>
		<!--<xsl:value-of select="concat('Nb Rejet(s) : ', $nbRejet)"/>-->
		<xsl:choose>
			<xsl:when test="$result = 'True' or $indice = $nbRejet">
				<xsl:value-of select="$result"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:variable name="node" select="document('')/xsl:stylesheet/foo:rejects/*[position() = $indice+1]"/>
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

	<!--
		@description Transforme une date sous le format YYYYMMDDHHmmss HL7 vers un nouveau
		format
		@return La date sous le format HL7 YYYY-MM-DDTHH:mm:ssZ
	-->
	<xsl:template name="formatdatehl7">
		<xsl:param name="maDate"/>
		<xsl:variable name="dateLg" select="concat($maDate, '000000')"/>
		<xsl:variable name="D" select="substring($dateLg,7,2)"/>
		<xsl:variable name="M" select="substring($dateLg,5,2)"/>
		<xsl:variable name="Y" select="substring($dateLg,1,4)"/>
		<xsl:variable name="h" select="substring($dateLg,9,2)"/>
		<xsl:variable name="m" select="substring($dateLg,11,2)"/>
		<xsl:variable name="s" select="substring($dateLg,13,2)"/>
		<xsl:value-of select="concat($Y, '-', $M, '-', $D, 'T', $h, ':', $m, ':', $s,'Z')"/>
	</xsl:template>

	<xsl:template name="getLongDateHl7">
		<xsl:param name="dateHl7"/>
		<xsl:variable name="lg">
			<xsl:value-of select="string-length($dateHl7)"/>
		</xsl:variable>
		<xsl:choose>
			<xsl:when test="$lg = 14 or $lg = 0">
				<xsl:value-of select="$dateHl7"/>
			</xsl:when>
			<xsl:when test="$lg > 14">
				<xsl:value-of select="substring($dateHl7, 1, 14)"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="concat($dateHl7, substring('000000', 1, 14 - $lg))"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>