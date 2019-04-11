using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Linq;
using System.Windows.Forms;

namespace metadataGenerator
{
    class Program
    {
        public static void createFolder(string folderPath)
        {
            Logger Logger = new Logger();
            try
            {
                string path = folderPath;
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {

                Logger.createLog(e.Message.ToString(), "e");
            }

        }


        static void Main(string[] args)
        {
            
            //update parameters
            //Application.Run(new ParametersUI());
            


            //create necessary classes
            ConnectionSQL SqlConnection = new ConnectionSQL();
            ConnectionPostgreSQL PsqlConnetion = new ConnectionPostgreSQL();
            Logger Logger = new Logger();
            Metadata Metadata = new Metadata();
            Parameters Parameters = new Parameters();
            Parameters.generator();


            

            //check katalog link
            if (Parameters.p_save2Catalog)
            {
                string hostUri = Parameters.p_catalogURL;
                UriBuilder catalogUri = new UriBuilder(hostUri);
                Logger.createLog("Checking catalog url adress", "i");
                bool pingResult = pingHost(catalogUri.Host, catalogUri.Port);
                if (pingResult)
                {
                    Logger.createLog("Catalog url adress and port ok", "s");
                }
                else
                {
                    Console.WriteLine("katalog servis adres ve portuna erişilemiyor!! " + hostUri);
                    Logger.createLog("Check catalog adress and port not accessible!: " + hostUri, "e");
                    MessageBox.Show("IP/Port hatalı program kapatılacak!");
                    Environment.Exit(0);
                }
            }
            

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

            Console.WriteLine(@"╔════╦╗─╔╦═══╦══╗╔═══╗╔═══╦═╗─╔╦════╦═══╦═══╦═══╦═══╦═══╦╗──╔╦═══╦═╗─╔╗
║╔╗╔╗║║─║║╔═╗║╔╗║║╔═╗║║╔══╣║╚╗║║╔╗╔╗║╔══╣╔═╗║╔═╗║╔═╗║╔═╗║╚╗╔╝║╔═╗║║╚╗║║
╚╝║║╚╣║─║║║─╚╣╚╝╚╣╚══╗║╚══╣╔╗╚╝╠╝║║╚╣╚══╣║─╚╣╚═╝║║─║║╚══╬╗╚╝╔╣║─║║╔╗╚╝║
──║║─║║─║║║─╔╣╔═╗╠══╗║║╔══╣║╚╗║║─║║─║╔══╣║╔═╣╔╗╔╣╚═╝╠══╗║╚╗╔╝║║─║║║╚╗║║
──║║─║╚═╝║╚═╝║╚═╝║╚═╝║║╚══╣║─║║║─║║─║╚══╣╚╩═║║║╚╣╔═╗║╚═╝║─║║─║╚═╝║║─║║║
──╚╝─╚═══╩═══╩═══╩═══╝╚═══╩╝─╚═╝─╚╝─╚═══╩═══╩╝╚═╩╝─╚╩═══╝─╚╝─╚═══╩╝─╚═╝
");
            int rowCount = 0;
            int recordsSavedToCatalog = 0;
            try //main code block
            {
                DataTable table = PsqlConnetion.getResults("SELECT * FROM " + metaTableName + " WHERE " + tableCriteria);
                int totalRows = table.Rows.Count;
                
                Logger.createLog(metaTableName + "\n\t" + tableCriteria + "\n\t" + totalRows + "- Veri Sayısı", "i");
                Console.WriteLine("Metadata Generator PRO V1.0'a Hoşgeldiniz!");
                Console.WriteLine(new string('.', 100));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("SQL ==> SELECT * FROM " + metaTableName + " WHERE " + tableCriteria);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Oluşturulacak metaveri sayısı: "+totalRows+" devam edilsin [E/h]:"+Environment.NewLine);
                ConsoleKeyInfo key = Console.ReadKey();
                string[] choices = { "E", "e", "H", "h" };
                Console.ForegroundColor = ConsoleColor.White;
                if (!choices.Contains(key.KeyChar.ToString()))
                {
                    Console.Write("\b");
                    Console.WriteLine("Hatalı seçim!");
                    Logger.createLog("Hatalı seçim", "w");
                }
                else if (key.KeyChar == 'e' || key.KeyChar == 'E')
                {
                    Console.Write("\b");
                    Logger.createLog("E/e seçildi", "i");
                    Logger.createLog("Metaveri Oluşturma Başlatıldı", "i");
                    createFolder(metaDataFolder);
                    using (var progress = new ProgressBar())
                    {
                        
                        foreach (DataRow row in table.Rows)
                        {
                            //fetch data from dt by SQL column names
                            string guid = row[guidColumnName].ToString();
                            string responsibleEmail = row[resposibleEmail].ToString();
                            string sit_adi = row[recordName].ToString();
                            string abstractOfRecord = sit_adi.ToString();

                            //bbox format
                            string westBoundLongitude = row[bboxWest].ToString();
                            string eastBoundLongitude = row[bboxEast].ToString();
                            string southBoundLatitude = row[bboxSouth].ToString();
                            string northBoundLatitude = row[bboxNorth].ToString();

                            //createMetaData keywords
                            List<string> keywordsColumnNames = new List<string>();

                            //populate keywork columns
                            foreach (string kw in (keywords))
                            {
                                if (row[kw].ToString() != "" || row[kw].ToString() != null)
                                    keywordsColumnNames.Add(row[kw].ToString());
                            }

                            //create xml metadata
                            XDocument metadata = Metadata.createMetaData(guid, responsibleEmail, sit_adi, abstractOfRecord, westBoundLongitude, eastBoundLongitude, southBoundLatitude, northBoundLatitude,
                                                    keywordsColumnNames, organizationName, organizationEmail, metaDataFolder, topicCategory, onlineSources,
                                                    useLimitation, otherConstraints);

                            

                            int insertedRecord = 0;

                            //if save2catalog parameter is marked as true at parameters
                            if (Parameters.p_save2Catalog)
                            {
                                
                                //check metadata if exists at catalog delete it
                                Metadata.getRecordById(guid, Parameters.p_catalogURL, Parameters.p_catalogUsername, Parameters.p_catalogPassword, Parameters.p_catalogOverwriteSameUUID);

                                //insert new metadata to the catalog
                                insertedRecord = Metadata.insertMetadata(metadata, Parameters.p_catalogURL,
                                                            Parameters.p_catalogUsername, Parameters.p_catalogPassword);
                            }

                            rowCount++;
                            recordsSavedToCatalog += insertedRecord;
                            progress.Report((double)rowCount / totalRows);

                        }
                    }
                }
                else
                {
                    Console.Write("\b");
                    Console.WriteLine("İşlem iptal edildi!");
                    Logger.createLog("İşlem iptal edildi", "w");
                }

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message.ToString());
                Logger.createLog(e.Message.ToString(), "e");
            }
            finally
            {
                string result = "Metaveri Oluşturma İşlemi " + rowCount + " adet metaveri dosyası oluşturularak tamamlandı"+
                                Environment.NewLine+"Katalog Servise Kayıt Sayısı: "+recordsSavedToCatalog;
                Logger.createLog(result, "i");
                Console.WriteLine(result);
                MessageBox.Show(result);
                Environment.Exit(0);
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

        public static bool pingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException)
            {
                Console.WriteLine("Error pinging catalog host:'" + hostUri + ":" + portNumber.ToString() + "'");
                return false;
            }
        }
    }
}
