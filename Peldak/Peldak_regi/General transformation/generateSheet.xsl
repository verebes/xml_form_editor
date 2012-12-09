<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />
	<xsl:template match="/">	
		<xsl:element name="Document">
			<xsl:element name="source">proba.xml</xsl:element>
			<xsl:element name="Form">
				<xsl:attribute name="Name">Generated page</xsl:attribute>
				<xsl:for-each select="/*"	>	
					<xsl:call-template name="control">	
						<xsl:with-param name="path"><xsl:value-of select="concat('/', name()) "/></xsl:with-param>
					</xsl:call-template>			
				</xsl:for-each>				
			</xsl:element>
		</xsl:element>
	</xsl:template>
	
	
	<xsl:template name="control">
		<xsl:param name="path" select="'/'"/>
		<xsl:param name="level"  select="1"/>


	<xsl:variable name="linecntattr">
		<xsl:value-of select="count(preceding::* | ancestor::*)  + count((preceding::* | ancestor::*)/@* )" />		
	</xsl:variable>
	
	<xsl:variable name="linecnt">
		<xsl:value-of select="count(preceding::* | ancestor::*) " />		
	</xsl:variable>

	<xsl:variable name="linecnt2">
		<xsl:value-of select="count( preceding::*[count(*) = 0] ) * 2 " />		
	</xsl:variable>
			
		<xsl:choose>
			<xsl:when test="count(*) =0 ">
				<xsl:element name="Control">			
					<xsl:attribute name="Type">StaticLabel</xsl:attribute>
					<xsl:attribute name="X"><xsl:value-of select="$level * 20"/></xsl:attribute>
					<xsl:attribute name="Y"><xsl:value-of select="$linecnt * 25"/></xsl:attribute>
					<xsl:attribute name="Width">500</xsl:attribute>
					<xsl:attribute name="Height">20</xsl:attribute>
					<xsl:element name="Text"><xsl:value-of select="$path" /></xsl:element>
				</xsl:element>			
				<xsl:element name="Control">
					<xsl:attribute name="Type">XMLTextBox</xsl:attribute>
					<xsl:attribute name="X"><xsl:value-of select="$level * 20"/></xsl:attribute>
					<xsl:attribute name="Y"><xsl:value-of select="($linecnt+1)* 25"/></xsl:attribute>
					<xsl:attribute name="Width">500</xsl:attribute>
					<xsl:attribute name="Height">20</xsl:attribute>
					<xsl:element name="SourceDocument">proba.xml</xsl:element>
					<xsl:element name="XPath"><xsl:value-of select="$path" /></xsl:element>		
				 </xsl:element>				
			</xsl:when>
			<xsl:otherwise>
			</xsl:otherwise>
		</xsl:choose>

		<xsl:for-each select="*"	>	
		
			<xsl:variable name="name"><xsl:value-of select="name()"/></xsl:variable>
			<xsl:variable name="index"><xsl:value-of select="count((preceding-sibling::*[name()=$name]))+1 "/></xsl:variable>
			<xsl:variable name="indextext">
				<xsl:choose>
					<xsl:when test="count(../*[name()=$name]) =1">
						<xsl:text></xsl:text>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="concat('[', $index, ']')" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>		
		
			<xsl:call-template name="control">	
				<xsl:with-param name="path" select="concat($path, '/' , name(), $indextext)" />
				<xsl:with-param name="level" select="$level+1" />
			</xsl:call-template>
		</xsl:for-each> 
	</xsl:template>
		


		
</xsl:stylesheet>
