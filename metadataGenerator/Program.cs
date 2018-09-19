using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Data;

namespace metadataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionSQL SqlConnection = new ConnectionSQL();
            try
            {
                createMetaData();
                //DataTable data = SqlConnection.ShowDataInGridView("SELECT * FROM ANIT WHERE GEO_DURUM = 1") as DataTable;
                //foreach
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

          
            void createMetaData()
            {
                try
                {
                    //from db variables
                    string fileName = "1_KA_SIT";
                    string personalEmail = "arda.cetinkaya@netcad.com.tr";

                    //organization static variables
                    string organizationName = "Kültür ve Turizm Bakanlığı";
                    string organizationEmail = "yasingulbay@gmail.com";

                    //calculated variables
                    string metadataDate = DateTime.Now.ToString("yyyy-MM-dd");

                    //definition of all namespaces existing in the metadata document
                    XNamespace gmd = "http://www.isotc211.org/2005/gmd";
                    XNamespace gco = "http://www.isotc211.org/2005/gco";
                    XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
                    XNamespace gml = "http://www.opengis.net/gml";
                    XNamespace xlink = "http://www.w3.org/1999/xlink";
                    XNamespace schemaLocation = "http://www.isotc211.org/2005/gmd http://schemas.opengis.net/iso/19139/20060504/gmd/gmd.xsd";

                    XDocument xdoc = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement(gmd + "MD_Metadata",
                            new XAttribute(XNamespace.Xmlns + "gmd", gmd),
                            new XAttribute(XNamespace.Xmlns + "gco", gco),
                            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                            new XAttribute(XNamespace.Xmlns + "gml", gml),
                            new XAttribute(XNamespace.Xmlns + "xlink", xlink),
                            new XAttribute(xsi + "schemaLocation", schemaLocation),
                            new XElement(gmd+"fileIdentifier",
                                new XElement(gmd+"CharacterString", fileName+".xml")
                                ),
                            new XElement(gmd+"wfsRole", 
                                new XElement(gco+"CharacterString")
                                ),
                            new XElement(gmd + "organizationLogoUrl",
                                new XElement(gco + "CharacterString")
                                ),
                            new XElement(gmd + "wfsCatalog",
                                new XElement(gco + "CharacterString")
                                ),
                            new XElement(gmd + "language",
                                new XElement(gmd + "LanguageCode",
                                    new XAttribute("codeList", "http://www.loc.gov/standards/iso639-2/"), 
                                    new XAttribute("codeListValue", "tur"), "tur")
                                ),
                            new XElement(gmd + "characterSet",
                                new XElement(gmd + "MD_CharacterSetCode",
                                    new XAttribute("codeSpace", "ISOTC211/19115"),
                                    new XAttribute("codeList", "http://www.isotc211.org/2005/resources/codelist/gmxCodelists.xml#MD_CharacterSetCode"),
                                    new XAttribute("codeListValue", "MD_CharacterSetCode_utf8"), "MD_CharacterSetCode_utf8")
                                ),
                            new XElement(gmd + "hierarchyLevel",
                                new XElement(gmd + "MD_ScopeCode",
                                    new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#MD_ScopeCode"),
                                    new XAttribute("codeListValue", "dataset"), "Veri Seti")
                                ),
                            new XElement(gmd + "contact",
                                new XElement(gmd + "CI_ResponsibleParty",
                                    new XElement(gmd + "organisationName", 
                                        new XElement(gco + "CharacterString", organizationName),
                                    new XElement(gmd + "contactInfo",
                                        new XElement(gmd + "CI_Contact",
                                            new XElement(gmd + "address",
                                                new XElement(gmd + "CI_Address",
                                                    new XElement(gmd + "electronicMailAddress", new XElement(gco + "CharacterString", organizationEmail)),
                                                    new XElement(gmd + "electronicMailAddress", new XElement(gco + "CharacterString", personalEmail))
                                                )
                                            )
                                        )
                                    ),
                                    new XElement(gmd + "role",
                                        new XElement(gmd + "CI_RoleCole",
                                            new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#CI_RoleCode"),
                                            new XAttribute("codeListValue", "pointOfContact"),
                                            "pointOfContact"
                                        )
                                    )

                                )
                            )
                        ),
                            new XElement(gmd+"dateStamp",
                                new XElement(gco+"Date", metadataDate)
                            ), 
                            new XElement(gmd + "metadataStandardName",
                                new XElement(gco+ "CharacterString" , "ISO19115"
                                )
                            )


                            ));
                    xdoc.Save("GENERATEDXML\\"+fileName+".xml");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

    }
}
