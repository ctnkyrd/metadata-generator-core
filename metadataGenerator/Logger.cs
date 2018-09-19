using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace metadataGenerator
{
    class Logger
    {
        public static string logFilePath = "GENERATEDXML\\Logs";
        string text, path;

        public void makeLogDir(string folderPath)
        {
            try
            {
                string path = folderPath;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("Log Klasörü Oluşturuldu");
                }
                else
                {
                    Console.WriteLine("Log Klasörü Mevcut");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

        public void createLog(string logText)
        {
            makeLogDir(logFilePath);
            try
            {
                string logFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string text = DateTime.Now.ToString("[yyyyMMdd HH:mm:ss.fff]") +"- "+ logText + Environment.NewLine;
                File.AppendAllText(logFilePath + "\\" + logFileName, text);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
