using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
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

            //wms and wfs
            List<string> onlineSources = new List<string>();
            foreach (var i in Parameters.p_onlineResources)
            {
                onlineSources.Add(i.Value<string>("Name"));
            }

            List<string> keywords = new List<string>();
            foreach (var i in Parameters.p_keywords)
            {
                keywords.Add(i.Value<string>("Name"));
            }

            //get static values from app.config
            string metaTableName = Parameters.p_tableName;
            string tableCriteria = Parameters.p_tableCriteria;
            string organizationEmail = Parameters.p_organizationEmail;
            string organizationName = Parameters.p_kurumName;
            string guidColumnName = Parameters.p_guid;
            string bboxWest = Parameters.p_bbox_west;
            string bboxEast = Parameters.p_bbox_east;
            string bboxNorth = Parameters.p_bbox_north;
            string bboxSouth = Parameters.p_bbox_south;
            string resposibleEmail = Parameters.p_responsibleMail;
            string recordName = Parameters.p_metadataName;

            var spin = new ConsoleSpinner();
            Console.Write("Tamamlanıyor....");
            try //main code block
            {
                //DataTable table = SqlConnection.ShowDataInGridView("SELECT top 10 * FROM " + metaTableName + " WHERE " + tableCriteria);

                DataTable table = PsqlConnetion.getResults("SELECT * FROM " + metaTableName + " WHERE " + tableCriteria);


                int totalRows = table.Rows.Count;
                Logger.createLog(metaTableName + "\n\t" + tableCriteria + "\n\t" + totalRows + "- Veri Sayısı", "i");
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
