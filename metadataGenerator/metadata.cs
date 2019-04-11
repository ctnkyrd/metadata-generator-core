using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net.Http;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.IO;

namespace metadataGenerator
{
    class Metadata
    {
        //definition of all namespaces existing in the metadata document
        XNamespace gmd = "http://www.isotc211.org/2005/gmd";
        XNamespace dc = "http://purl.org/dc/elements/1.1/";
        XNamespace srv = "http://www.isotc211.org/2005/srv";
        XNamespace gco = "http://www.isotc211.org/2005/gco";
        XNamespace ogc = "http://www.opengis.net/ogc";
        XNamespace csw = "http://www.opengis.net/cat/csw/2.0.2";
        XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        XNamespace gml = "http://www.opengis.net/gml";
        XNamespace xlink = "http://www.w3.org/1999/xlink";
        XNamespace schemaLocation = "http://www.isotc211.org/2005/gmd http://schemas.opengis.net/iso/19139/20060504/gmd/gmd.xsd";



        private static readonly HttpClient client = new HttpClient();
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

        public int insertMetadata(XDocument metadata, string url, string username, string password)
        {
            try
            {
                XDocument postData = metadata;
                XDocument transactionInsert = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement(csw + "Transaction",
                            new XAttribute("service", "CSW"),
                            new XAttribute("version", "2.0.2"),
                            new XElement(csw + "Insert", postData.Root)
                            )
                        );

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/srv/eng/csw-publication");
                byte[] bytes;
                bytes = Encoding.UTF8.GetBytes(transactionInsert.ToString());
                request.ContentType = "application/xml; encoding='utf-8'";
                request.ContentLength = bytes.Length;
                request.Method = "POST";

                //authentication
                string uname = username;
                string pass = password;
                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(uname + ":" + pass));
                request.Headers.Add("Authorization", "Basic " + encoded);

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XDocument insertResponse = XDocument.Parse(responseStr);
                    int insertedCount = 0;
                    try
                    {
                        insertedCount = Convert.ToInt16(insertResponse.Descendants(csw + "totalInserted").First().Value);
                    }
                    catch (Exception e)
                    {
                        Logger.createLog(e.Message.ToString() + Environment.NewLine + insertResponse.ToString(), "e");
                    }

                    return insertedCount;
                }
                else {
                    Logger.createLog("InsertMetadata HTTP Status:"+response.StatusCode, "e");
                    return 0;
                } 
            }
            catch (Exception e)
            {
                Logger.createLog("Transaction Insert: "+e.Message.ToString(), "e");
                return 0;
            }
            
        }

        public int getRecordById(string identifier, string url, string username, string password, bool overwriteSameUUID)
        {
            string guid = identifier;
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/srv/eng/csw-publication?request=GetRecordById&service=CSW&version=2.0.2&elementSetName=full&id="+guid+".xml");
                request.ContentType = "application/xml; encoding='utf-8'";
                request.Method = "GET";

                //authentication
                string uname = username;
                string pass = password;
                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(uname + ":" + pass));
                request.Headers.Add("Authorization", "Basic " + encoded);

                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XDocument updateResponse = XDocument.Parse(responseStr);
                    string foundMD = "";

                    if (updateResponse.Root.Value != "")
                    {
                        try
                        {
                            foundMD = updateResponse.Descendants(dc + "identifier").First().Value;

                            if (foundMD.Length > 10)
                            {
                                int a = deleteMetadata(url, foundMD, uname, password);
                                return a;
                            }
                            else return 0;
                        }
                        catch (Exception)
                        {
                            Logger.createLog("Incorrect Response String Deletemetadata", "e");
                            return 0;
                        }
                    }
                    else return 0;
                   
                }
                else
                {
                    Logger.createLog("HTTP Status:" + response.StatusCode, "e");
                    return -1;
                }
            }
            catch (Exception e)
            {
                Logger.createLog("GetRecordById: " + e.Message.ToString(), "e");
                return -1;
            }

        }

        public int deleteMetadata(string url, string identifier, string username, string password)
        {
            string guid = identifier;

            try
            {
                
                XDocument transactionDelete = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement(csw + "Transaction",
                        new XAttribute(XNamespace.Xmlns + "csw", csw),
                        new XAttribute(XNamespace.Xmlns + "ogc", ogc),
                            new XAttribute("service", "CSW"),
                            new XAttribute("version", "2.0.2"),
                            new XElement(csw + "Delete", 
                                new XElement(csw+ "Constraint", new XAttribute("version", "1.1.0"),
                                    new XElement(ogc+"Filter",
                                        new XElement(ogc+ "PropertyIsEqualTo",
                                            new XElement(ogc+ "PropertyName", "identifier"),
                                                new XElement(ogc+ "Literal", guid)))
                                ))
                            )
                        );

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/srv/eng/csw-publication");
                byte[] bytes;
                bytes = Encoding.UTF8.GetBytes(transactionDelete.ToString());
                request.ContentType = "application/xml; encoding='utf-8'";
                request.ContentLength = bytes.Length;
                request.Method = "POST";

                //authentication
                string uname = username;
                string pass = password;
                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(uname + ":" + pass));
                request.Headers.Add("Authorization", "Basic " + encoded);

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    XDocument deleteResponse = XDocument.Parse(responseStr);
                    int insertedCount = 0;
                    try
                    {
                        insertedCount = Convert.ToInt16(deleteResponse.Descendants(csw + "totalInserted").First().Value);
                    }
                    catch (Exception e)
                    {
                        Logger.createLog(e.Message.ToString() + Environment.NewLine + deleteResponse.ToString(), "e");
                    }

                    return insertedCount;
                }
                else {
                    Logger.createLog("Deletemetadata HTTP Status:" + response.StatusCode, "e");
                    return 0;
                };
            }
            catch (Exception e)
            {
                Logger.createLog("Transaction Insert: " + e.Message.ToString(), "e");
                return 0;
            }

        }

        

    }

}