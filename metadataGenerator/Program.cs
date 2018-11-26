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
            ConnectionPostgreSQL PsqlConnetion = new ConnectionPostgreSQL();
            Logger Logger = new Logger();
            Metadata Metadata = new Metadata();
            Parameters Parameters = new Parameters();
            Parameters.generator();

            

            Logger.createLog("Metaveri Oluşturma Başlatıldı", "i");

            string metaDataFolder = Parameters.p_metadataFolder;
            string topicCategory = Parameters.p_topicCategory;

            //wms and wfs
            List<string> onlineSources = new List<string>();
            foreach (var i in Parameters.p_onlineResources)
            {
                onlineSources.Add(i.Value<string>("Name"));
            }

            //get static values from app.config
            string metaTableName = ConfigurationManager.AppSettings["metaTableName"];
            string tableCriteria = ConfigurationManager.AppSettings["tableCriteria"];
            string organizationEmail = ConfigurationManager.AppSettings["organizationEmail"];
            string organizationName = ConfigurationManager.AppSettings["organizationName"];
            string guidColumnName = ConfigurationManager.AppSettings["guidColumn"];

            var spin = new ConsoleSpinner();
            Console.Write("Tamamlanıyor....");
            try //main code block
            {
                //DataTable table = SqlConnection.ShowDataInGridView("SELECT top 10 * FROM " + metaTableName + " WHERE " + tableCriteria);

                DataTable table = PsqlConnetion.getResults("SELECT * FROM " + metaTableName + " WHERE " + tableCriteria+ " limit 100");


                int totalRows = table.Rows.Count;
                Logger.createLog(metaTableName + "\n\t" + tableCriteria + "\n\t" + totalRows + "- Veri Sayısı", "i");
                foreach (DataRow row in table.Rows)
                {
                    //fetch data from dt by SQL column names
                    string rowId = row[guidColumnName].ToString();
                    string responsibleEmail = row["USER_MODIFY_N"].ToString() + "@kultur.gov.tr";
                    string sit_adi = row["ADI"].ToString();
                    string genel_tanim = row["GENEL_TANIM"].ToString();


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

                    //createMetaData keywords
                    List<string> keywordsColumnNames = new List<string>();
                    
                    foreach (string kw in (ConfigurationManager.AppSettings["keywords"].Split(new char[] { ';' })))
                    {
                        if (row[kw].ToString() != "" || row[kw].ToString() != null)
                            keywordsColumnNames.Add(row[kw].ToString());
                    }

                    //online resources wms,wfs
                    //List<string> onlineSources = new List<string>();
                    //foreach (string os in (ConfigurationManager.AppSettings["onlineSources"].Split(new char[] { ';' })))
                    //{
                    //    onlineSources.Add(os);
                    //}

                    Metadata.createMetaData(rowId, responsibleEmail, sit_adi, genel_tanim, westBoundLongitude, eastBoundLongitude, southBoundLatitude, northBoundLatitude,
                                            keywordsColumnNames, organizationName, organizationEmail, metaDataFolder, topicCategory, onlineSources);
                    //for visual satisfaction :)
                    spin.Turn();

                }

            }
            catch (Exception e)
            {
                Logger.createLog(e.Message.ToString(), "e");
            }
            finally
            {
                Console.Write("\r Tamamlandı!");
                Logger.createLog("İşlem Başarıyla Tamamlandı", "s");
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
}
