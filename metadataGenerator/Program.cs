using System;
using System.Collections.Generic;
using System.Data;
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
            List<string> keywords = Parameters.p_vt_keywords;
            List<string> onlineSources = Parameters.p_onlineResources;

            //get static values from configuration file
            string metaTableName = Parameters.p_tableName;
            string tableCriteria = Parameters.p_tableCriteria;
            string organizationEmail = Parameters.p_organizationEmail;
            string organizationName = Parameters.p_kurumName;
            string guidColumnName = Parameters.p_vt_guid;
            string bboxWest = Parameters.p_vt_bbox_west;
            string bboxEast = Parameters.p_vt_bbox_east;
            string bboxNorth = Parameters.p_vt_bbox_north;
            string bboxSouth = Parameters.p_vt_bbox_south;
            string resposibleEmail = Parameters.p_vt_responsibleMail;
            string recordName = Parameters.p_vt_metadataName;

            //new variables
            string useLimitation = Parameters.p_useLimitation;
            string otherConstraints = Parameters.p_otherConstraints;

            var spin = new ConsoleSpinner();

            Console.WriteLine(@"╔════╦╗─╔╦═══╦══╗╔═══╗╔═══╦═╗─╔╦════╦═══╦═══╦═══╦═══╦═══╦╗──╔╦═══╦═╗─╔╗
║╔╗╔╗║║─║║╔═╗║╔╗║║╔═╗║║╔══╣║╚╗║║╔╗╔╗║╔══╣╔═╗║╔═╗║╔═╗║╔═╗║╚╗╔╝║╔═╗║║╚╗║║
╚╝║║╚╣║─║║║─╚╣╚╝╚╣╚══╗║╚══╣╔╗╚╝╠╝║║╚╣╚══╣║─╚╣╚═╝║║─║║╚══╬╗╚╝╔╣║─║║╔╗╚╝║
──║║─║║─║║║─╔╣╔═╗╠══╗║║╔══╣║╚╗║║─║║─║╔══╣║╔═╣╔╗╔╣╚═╝╠══╗║╚╗╔╝║║─║║║╚╗║║
──║║─║╚═╝║╚═╝║╚═╝║╚═╝║║╚══╣║─║║║─║║─║╚══╣╚╩═║║║╚╣╔═╗║╚═╝║─║║─║╚═╝║║─║║║
──╚╝─╚═══╩═══╩═══╩═══╝╚═══╩╝─╚═╝─╚╝─╚═══╩═══╩╝╚═╩╝─╚╩═══╝─╚╝─╚═══╩╝─╚═╝
");

            Console.WriteLine("Metadata Generator PRO V1.0'a Hoşgeldiniz!, Devam etmek için lütfen Enter'a basınız basınız...");
            var key = Console.ReadLine();

            //Console.Write("Tamamlanıyor...."); 
            try //main code block
            {
                DataTable table = PsqlConnetion.getResults("SELECT * FROM " + metaTableName + " WHERE " + tableCriteria);
                int totalRows = table.Rows.Count;
                Logger.createLog(metaTableName + "\n\t" + tableCriteria + "\n\t" + totalRows + "- Veri Sayısı", "i");
                
                using (var progress = new ProgressBar())
                {
                    int rowCount = 0;
                    int totalRowCount = table.Rows.Count;
                    foreach (DataRow row in table.Rows)
                    {
                        //fetch data from dt by SQL column names
                        string rowId = row[guidColumnName].ToString();
                        string responsibleEmail = row[resposibleEmail].ToString();
                        string sit_adi = row[recordName].ToString();
                        string abstractOfRecord = sit_adi.ToString();

                        //bbox
                        string westBoundLongitude = row[bboxWest].ToString();
                        string eastBoundLongitude = row[bboxEast].ToString();
                        string southBoundLatitude = row[bboxSouth].ToString();
                        string northBoundLatitude = row[bboxNorth].ToString();

                        //createMetaData keywords
                        List<string> keywordsColumnNames = new List<string>();

                        foreach (string kw in (keywords))
                        {
                            if (row[kw].ToString() != "" || row[kw].ToString() != null)
                                keywordsColumnNames.Add(row[kw].ToString());
                        }

                        Metadata.createMetaData(rowId, responsibleEmail, sit_adi, abstractOfRecord, westBoundLongitude, eastBoundLongitude, southBoundLatitude, northBoundLatitude,
                                                keywordsColumnNames, organizationName, organizationEmail, metaDataFolder, topicCategory, onlineSources,
                                                useLimitation, otherConstraints);

                        //progress bar like spinning bar
                        //spin.Turn();
                        rowCount++;
                        progress.Report((double)rowCount / totalRowCount);
                        Thread.Sleep(20);

                    }
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
                Console.ReadLine();
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
                Thread.Sleep(1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }
    }
}
