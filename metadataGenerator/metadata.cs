using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace metadataGenerator
{
    class Metadata
    {

        Logger Logger = new Logger();
        public void createMetaData(string oid, string responsibleEmail, string sit_adi, string genel_tanim, string westBoundLongitude,
                                string eastBoundLongitude, string southBoundLatitude, string northBoundLatitude, List<string> keywords, string organizationName, 
                                string organizationEmail, string metaDataFolder)
        {
            try
            {
                //from db variables
                string fileName = oid;
                string dataResponsibleEmail = responsibleEmail;


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
                        new XElement(gmd + "fileIdentifier",
                            new XElement(gmd + "CharacterString", fileName + ".xml")
                            ),
                        new XElement(gmd + "wfsRole",
                            new XElement(gco + "CharacterString")
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
                                                new XElement(gmd + "electronicMailAddress", new XElement(gco + "CharacterString", dataResponsibleEmail))
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
                        new XElement(gmd + "dateStamp",
                            new XElement(gco + "Date", metadataDate)
                        ),
                        new XElement(gmd + "metadataStandardName",
                            new XElement(gco + "CharacterString", "ISO19115"
                            )
                        ),
                         new XElement(gmd + "metadataStandardVersion",
                            new XElement(gco + "CharacterString", "2003/Cor.1:2006"
                            )
                        ),
                        new XElement(gmd + "identificationInfo",
                            new XElement(gmd + "MD_DataIdentification",
                                new XElement(gmd + "citation",
                                    new XElement(gmd + "CI_Citation",

                                        // name of the unit
                                        new XElement(gmd + "title", new XElement(gco + "CharacterString", sit_adi)),

                                        new XElement(gmd + "date",
                                            new XElement(gmd + "CI_Date",
                                                new XElement(gmd + "date", new XElement(gco + "Date")), //date must be added
                                                new XElement(gmd + "dateType",
                                                    new XElement(gmd + "CI_DateTypeCode",
                                                        new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#CI_DateTypeCode"),
                                                        new XAttribute("codeListValue", "publication"), "publication"
                                                    )
                                                )
                                            )
                                        ),
                                          new XElement(gmd + "date",
                                            new XElement(gmd + "CI_Date",
                                                new XElement(gmd + "date", new XElement(gco + "Date")), //date must be added
                                                new XElement(gmd + "dateType",
                                                    new XElement(gmd + "CI_DateTypeCode",
                                                        new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#CI_DateTypeCode"),
                                                        new XAttribute("codeListValue", "creation"), "creation"
                                                    )
                                                )
                                            )
                                        ),
                                          new XElement(gmd + "date",
                                            new XElement(gmd + "CI_Date",
                                                new XElement(gmd + "date", new XElement(gco + "Date")), //date must be added
                                                new XElement(gmd + "dateType",
                                                    new XElement(gmd + "CI_DateTypeCode",
                                                        new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#CI_DateTypeCode"),
                                                        new XAttribute("codeListValue", "revision"), "revision"
                                                    )
                                                )
                                            )
                                        ),
                                        new XElement(gmd + "identifier",
                                            new XElement(gmd + "RS_Identifier",
                                                new XElement(gmd + "code",
                                                    new XElement(gco + "CharacterString")
                                                ),
                                                new XElement(gmd + "codeSpace",
                                                    new XElement(gco + "CharacterString")
                                                )
                                            )
                                        )

                                    )
                                ),

                                // abstract of the unit
                                new XElement(gmd + "abstract",
                                    new XElement(gco + "CharacterString", genel_tanim)
                                ),
                                new XElement(gmd + "pointOfContact",
                                    new XElement(gmd + "CI_ResponsibleParty",
                                        new XElement(gmd + "organisationName", new XElement(gco + "CharacterString", organizationName)),
                                        new XElement(gmd + "contactInfo",
                                            new XElement(gmd + "CI_Contact",
                                                new XElement(gmd + "address",
                                                    new XElement(gmd + "CI_Address",
                                                        new XElement(gmd + "electronicMailAddress", new XElement(gco + "CharacterString", organizationEmail)),
                                                        new XElement(gmd + "electronicMailAddress", new XElement(gco + "CharacterString", responsibleEmail))
                                                    )
                                                )
                                            )
                                        ),
                                        new XElement(gmd + "role",
                                            new XElement(gmd + "CI_RoleCode",
                                                new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#CI_RoleCode"),
                                                new XAttribute("codeListValue", "author"), "author"
                                            )
                                        )

                                    )
                                ),

                                // need to be arraged for multivalues keywords
                                new XElement(gmd + "descriptiveKeywords",
                                    new XElement(gmd + "MD_Keywords",
                                        from kw in keywords
                                        select new XElement(gmd + "keyword", new XElement(gco + "CharacterString", kw)),
                                            new XElement(gmd + "thesaurusName",
                                            new XElement(gmd + "CI_Citation",
                                                new XElement(gmd + "title", new XElement(gco + "CharacterString")),
                                                new XElement(gmd + "date",
                                                    new XElement(gmd + "CI_Date",
                                                        new XElement(gmd + "date", new XElement(gco + "Date")),
                                                        new XElement(gmd + "dateType",
                                                            new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#CI_DateTypeCode"),
                                                            new XAttribute("codeListValue", "creation"), "creation"
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                ),

                                // text content may be revised
                                new XElement(gmd + "resourceConstraints",
                                    new XElement(gmd + "MD_Constraints",
                                        new XElement(gmd + "useLimitation", new XElement(gco + "CharacterString",
                                        "Bilgi Amaçlıdır. Resmi İşlemlerde Kullanılamaz. Veriler çoğaltılarak hiçbir şekilde üçüncü şahıslara kullandırılamaz ve yayınlanamaz. Verilerin amaç dışında ve ticari amaçla kullanıldığının tespit edilmesi halinde ilgililer hakkında hukuki işlemler başlatılacaktır.")
                                        )
                                    )
                                ),

                                // text content may be revised
                                new XElement(gmd + "resourceConstraints",
                                    new XElement(gmd + "MD_LegalConstraints",
                                        new XElement(gmd + "accessConstraints",
                                            new XElement(gmd + "MD_RestrictionCode",
                                                new XAttribute("codeListValue", "otherRestrictions"), "otherRestrictions",
                                                new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#MD_RestrictionCode")
                                            )
                                        ),
                                        new XElement(gmd + "otherConstraints",
                                            new XElement(gco + "CharacterString",
                                            "Bilgi Amaçlıdır. Resmi İşlemlerde Kullanılamaz. Veriler çoğaltılarak hiçbir şekilde üçüncü şahıslara kullandırılamaz ve yayınlanamaz. Verilerin amaç dışında ve ticari amaçla kullanıldığının tespit edilmesi halinde ilgililer hakkında hukuki işlemler başlatılacaktır.")
                                        )
                                    )
                                ),
                                new XElement(gmd + "language",
                                    new XElement(gmd + "LanguageCode",
                                        new XAttribute("codeList", "http://www.loc.gov/standards/iso639-2/"),
                                        new XAttribute("codeListValue", "tur"), "tur")
                                ),

                                // needed to be decide topic category
                                new XElement(gmd + "topicCategory",
                                    new XElement(gmd + "MD_TopicCategoryCode", "SIT") //topic may be involved to app.config
                                ),

                                new XElement(gmd + "extent",
                                    new XElement(gmd + "EX_Extent",
                                        new XElement(gmd + "geographicElement",
                                            new XElement(gmd + "EX_GeographicBoundingBox",
                                                new XElement(gmd + "westBoundLongitude", new XElement(gco + "Decimal", westBoundLongitude)),
                                                new XElement(gmd + "eastBoundLongitude", new XElement(gco + "Decimal", eastBoundLongitude)),
                                                new XElement(gmd + "southBoundLongitude", new XElement(gco + "Decimal", southBoundLatitude)),
                                                new XElement(gmd + "northBoundLongitude", new XElement(gco + "Decimal", northBoundLatitude))
                                            )
                                        ),
                                                new XElement(gmd + "temporalElement",
                                                    new XElement(gmd + "EX_TemporalExtent",
                                                        new XElement(gmd + "extent",
                                                            new XElement(gml + "TimePeriod",
                                                                new XAttribute(gml + "id", "IDc1161bd1-d59a-4641-b0a5-c60fff77476b"), //id must be corrected
                                                                new XAttribute(xsi + "type", "gml:TimePeriodType"),
                                                                new XElement(gml + "beginPosition"), //beginning time must be added
                                                                new XElement(gml + "endPosition") //ending time must be added
                                                            )
                                                        )
                                                    )
                                                )


                                     )
                                )

                            )
                        ),
                        new XElement(gmd + "distributionInfo",
                            new XElement(gmd + "MD_Distribution",
                                new XElement(gmd + "distributionFormat",
                                    new XElement(gmd + "MD_Format",
                                        new XElement(gmd + "name", new XElement(gco + "CharacterString")),  //name must be added
                                        new XElement(gmd + "version", new XElement(gco + "CharacterString"))    //version must be adde
                                    )
                                ),
                                new XElement(gmd + "transferOptions",
                                    new XElement(gmd + "MD_DigitalTransferOptions",
                                        new XElement(gmd + "onLine",
                                            new XElement(gmd + "CI_OnlineResource",
                                                new XElement(gmd + "linkage",
                                                    new XElement(gmd + "URL")   //link must be added
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        ),
                        new XElement(gmd + "dataQualityInfo",
                            new XElement(gmd + "DQ_DataQuality",
                                new XElement(gmd + "scope",
                                    new XElement(gmd + "DQ_Scope",
                                        new XElement(gmd + "level",
                                            new XElement(gmd + "MD_ScopeCode",
                                                new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#MD_ScopeCode"),
                                                new XAttribute("codeListValue", "dataset"), "dataset" //what if it's not dataset
                                            )
                                        )
                                    )
                                ),
                                new XElement(gmd + "report",
                                    new XElement(gmd + "DQ_DomainConsistency",
                                        new XAttribute(xsi + "type", "gmd:DQ_DomainConsistency_Type"),
                                        new XElement(gmd + "result",
                                           new XElement(gmd + "DQ_ConformanceResult",
                                               new XAttribute(xsi + "type", "gmd:DQ_ConformanceResult"),
                                               new XElement(gmd + "specification",
                                                   new XElement(gmd + "CI_Citation",
                                                       new XElement(gmd + "title", new XElement(gco + "CharacterString")),
                                                       new XElement(gmd + "date",
                                                           new XElement(gmd + "CI_Date",
                                                               new XElement(gmd + "date", new XElement(gco + "Date")),
                                                                   new XElement(gmd + "dateType",
                                                                        new XElement(gmd + "CI_DateTypeCode",
                                                                            new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/codelist/ML_gmxCodelists.xml#CI_DateTypeCode"),
                                                                            new XAttribute("codeListValue", "publication"), "publication"
                                                                        )
                                                                    )
                                                           )
                                                       )
                                                   )
                                               ),
                                               new XElement(gmd + "explanation", new XElement(gco + "CharacterString", "See the referenced specification")),
                                               new XElement(gmd + "pass", new XElement(gco + "Boolean", "true"))
                                           )
                                        )
                                    )
                                ),
                                new XElement(gmd + "lineage",
                                    new XElement(gmd + "LI_Lineage",
                                        new XElement(gmd + "statement", new XElement(gco + "CharacterString") //
                                        )
                                    )
                                )
                            )
                        )


                    )
                );

                xdoc.Save(metaDataFolder + "\\" + fileName + ".xml");
            }
            catch (Exception e)
            {
                Logger.createLog(e.Message.ToString(), "e");
            }

        }


    }

}
    }
}
