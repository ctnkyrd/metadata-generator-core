using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace metadataGenerator
{
    class Metadata
    {

        Logger Logger = new Logger();
        public XDocument createMetaData(string oid, string responsibleEmail, string metadataName, string genel_tanim, string westBoundLongitude,
                                string eastBoundLongitude, string southBoundLatitude, string northBoundLatitude, List<string> keywords, string organizationName,
                                string organizationEmail, string metaDataFolder, string topicCategory, List<string> onlineResources,
                                //new variables
                                string useLimitation, string otherConstraints
                                )
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
                XNamespace srv = "http://www.isotc211.org/2005/srv";
                XNamespace gco = "http://www.isotc211.org/2005/gco";
                XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
                XNamespace gml = "http://www.opengis.net/gml";
                XNamespace xlink = "http://www.w3.org/1999/xlink";
                XNamespace schemaLocation = "http://www.isotc211.org/2005/gmd http://schemas.opengis.net/iso/19139/20060504/gmd/gmd.xsd";

                XDocument xdoc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement(gmd + "MD_Metadata",
                        new XAttribute(XNamespace.Xmlns + "gmd", gmd),
                        new XAttribute(XNamespace.Xmlns + "srv", srv),
                        new XAttribute(XNamespace.Xmlns + "gco", gco),
                        new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                        new XAttribute(XNamespace.Xmlns + "gml", gml),
                        new XAttribute(XNamespace.Xmlns + "xlink", xlink),
                        new XAttribute(xsi + "schemaLocation", schemaLocation),
                        new XElement(gmd + "fileIdentifier",
                            new XElement(gco + "CharacterString", fileName + ".xml")
                            ),
                        //new XElement(gmd + "wfsRole",
                        //    new XElement(gco + "CharacterString")
                        //    ),
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
                                new XAttribute("codeListValue", "service"), "Veri Servisi")
                            ),
                        new XElement(gmd + "contact",
                            new XElement(gmd + "CI_ResponsibleParty",
                                new XElement(gmd + "organisationName",
                                    new XElement(gco + "CharacterString", organizationName)),
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
                            new XElement(srv + "SV_ServiceIdentification",
                                new XElement(gmd + "citation",
                                    new XElement(gmd + "CI_Citation",

                                        // name of the unit
                                        new XElement(gmd + "title", new XElement(gco + "CharacterString", metadataName)),

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

                                //keywords
                                from kw in keywords
                                select new XElement(gmd + "descriptiveKeywords",
                                    new XElement(gmd + "MD_Keywords",
                                         new XElement(gmd + "keyword", new XElement(gco + "CharacterString", kw)),
                                        new XElement(gmd + "thesaurusName",
                                            new XElement(gmd + "CI_Citation",
                                                new XElement(gmd + "title", new XElement(gco + "CharacterString", kw)),
                                                new XElement(gmd + "date",
                                                    new XElement(gmd + "CI_Date",
                                                        new XElement(gmd + "date", new XElement(gco + "Date", metadataDate)),
                                                        new XElement(gmd + "dateType",
                                                            new XElement(gmd + "CI_DateTypeCode",
                                                            new XAttribute("codeList", "http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/Codelist/ML_gmxCodelists.xml#CI_DateTypeCode"),
                                                            new XAttribute("codeListValue", "revision"), "revision"
                                                            )
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
                                        useLimitation
                                            )
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
                                            otherConstraints
                                            )
                                        )
                                    )
                                ),
                                new XElement(srv + "serviceType",
                                    new XElement(gco + "LocalName", " Konumsal ")),
                                       new XElement(gmd + "language",
                                    new XElement(gmd + "LanguageCode",
                                        new XAttribute("codeList", "http://www.loc.gov/standards/iso639-2/"),
                                        new XAttribute("codeListValue", "tur"), "tur")
                                ),

                                // needed to be decide topic category
                                new XElement(gmd + "topicCategory",
                                    new XElement(gmd + "MD_TopicCategoryCode", topicCategory) //topic may be involved to app.config
                                ),

                                new XElement(srv + "extent",
                                    new XElement(gmd + "EX_Extent",
                                        new XElement(gmd + "geographicElement",
                                            new XElement(gmd + "EX_GeographicBoundingBox",
                                                new XElement(gmd + "westBoundLongitude", new XElement(gco + "Decimal", westBoundLongitude)),
                                                new XElement(gmd + "eastBoundLongitude", new XElement(gco + "Decimal", eastBoundLongitude)),
                                                new XElement(gmd + "southBoundLatitude", new XElement(gco + "Decimal", southBoundLatitude)),
                                                new XElement(gmd + "northBoundLatitude", new XElement(gco + "Decimal", northBoundLatitude))
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
                                        new XElement(gmd + "name", new XElement(gco + "CharacterString", "unknown")),  //name must be added
                                        new XElement(gmd + "version", new XElement(gco + "CharacterString", "unknown"))    //version must be adde
                                    )
                                ),
                                new XElement(gmd + "transferOptions",
                                    new XElement(gmd + "MD_DigitalTransferOptions",
                                      //wms wfs servis ekleme
                                      new XElement(gmd + "onLine",
                                         from os in onlineResources
                                         select new XElement(gmd + "CI_OnlineResource",
                                             new XElement(gmd + "linkage",
                                                 new XElement(gmd + "URL", os)
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
                                                new XAttribute("publication", "publication"), "publication" //what if it's not dataset
                                            )
                                        )
                                    )
                                ),
                                new XElement(gmd + "report",
                                    new XElement(gmd + "DQ_DomainConsistency",
                                        new XAttribute(xsi + "type", "gmd:DQ_DomainConsistency_Type"),
                                        new XElement(gmd + "result",
                                           new XElement(gmd + "DQ_ConformanceResult",
                                               new XAttribute(xsi + "type", "gmd:DQ_ConformanceResult_Type"),
                                               new XElement(gmd + "specification",
                                                   new XElement(gmd + "CI_Citation",
                                                       new XElement(gmd + "title", new XElement(gco + "CharacterString", topicCategory)),
                                                       new XElement(gmd + "date",
                                                           new XElement(gmd + "CI_Date",
                                                               new XElement(gmd + "date", new XElement(gco + "Date"), metadataDate),
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
                                        new XElement(gmd + "statement", new XElement(gco + "CharacterString")
                                        )
                                    )
                                )
                            )
                        )


                    )
                );
                xdoc.Save(metaDataFolder + "\\" + fileName + ".xml");
                return xdoc;
            }
            catch (Exception e)
            {
                Logger.createLog(e.Message.ToString(), "e");

                XDocument erroX = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement("Error",
                            new XElement("CharacterString", e.Message.ToString())
                )
                    );

                return erroX;
            }

        }

        public void insertMetadata(XDocument metadata)
        {

        }

    }

}