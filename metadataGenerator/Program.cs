﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Threading;

namespace metadataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //create necessary classes
            ConnectionSQL SqlConnection = new ConnectionSQL();
            Logger logger = new Logger();

            logger.createLog("Metaveri Oluşturma Başlatıldı", "i");
            //get static values from app.config
            string metaTableName = ConfigurationManager.AppSettings["metaTableName"];
            string tableCriteria = ConfigurationManager.AppSettings["tableCriteria"];
            string organizationEmail = ConfigurationManager.AppSettings["organizationEmail"];
            string organizationName = ConfigurationManager.AppSettings["organizationName"];
            string metaDataFolder = ConfigurationManager.AppSettings["metaDataFolder"];

            var spin = new ConsoleSpinner();
            Console.Write("Tamamlanıyor....");
            try //main code block
            {
                DataTable table =  SqlConnection.ShowDataInGridView("SELECT top 50 * FROM "+metaTableName+" WHERE "+tableCriteria);
                int totalRows = table.Rows.Count;
                logger.createLog(metaTableName + "\n\t" + tableCriteria + "\n\t" + totalRows + "- Veri Sayısı", "i");
                foreach (DataRow row in table.Rows)
                {
                    //fetch data from dt by SQL column names
                    string rowId = row["OBJECTID"].ToString();
                    string responsibleEmail = row["USER_MODIFY_N"].ToString();
                    //bounding box conversions
                    BoundingBox Bbox = new BoundingBox(
                        Convert.ToDouble(row["CLLY"]), 
                        Convert.ToDouble(row["CURY"]), 
                        Convert.ToDouble(row["CLLX"]), 
                        Convert.ToDouble(row["CURY"])
                        );
                    string westBoundLongitude = Bbox.WBL.ToString();
                    string eastBoundLongitude = Bbox.EBL.ToString();
                    string southBoundLatitude = Bbox.SBL.ToString();
                    string northBoundLatitude = Bbox.NBL.ToString();

                    createMetaData(rowId, responsibleEmail);
                    spin.Turn();
                }

            }
            catch (Exception e)
            {
                logger.createLog(e.Message.ToString(), "e");
            }
            finally
            {
                Console.Write("\r Tamamlandı!");
                logger.createLog("İşlem Başarıyla Tamamlandı", "s");
            }
          
            void createMetaData(string oid, string responsibleEmail)
            {
                try
                {
                    //from db variables
                    string fileName = oid + "_KA_SIT";
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
                            new XElement(gmd+"dateStamp",
                                new XElement(gco+"Date", metadataDate)
                            ), 
                            new XElement(gmd + "metadataStandardName",
                                new XElement(gco+ "CharacterString" , "ISO19115"
                                )
                            )


                            ));
                    xdoc.Save(metaDataFolder+"\\"+fileName+".xml");
                }
                catch (Exception e)
                {
                    logger.createLog(e.Message.ToString(), "e");
                }

            }

               
    }

    }
        //bbox class
        class BoundingBox
    {
        public double WBL { get; set; }
        public double EBL { get; set; }
        public double SBL { get; set; }
        public double NBL { get; set; }

        public BoundingBox(double westBoundLongitude, double eastBoundLongitude, double southBoundLatitude, double northBoundLatitude)
        {
            WBL = westBoundLongitude;
            EBL = eastBoundLongitude;
            SBL = southBoundLatitude;
            NBL = northBoundLatitude;
        }
    }


    //for console spinner
    public class ConsoleSpinner
    {
        int counter;

        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("/"); counter = 0; break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Thread.Sleep(100);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}
