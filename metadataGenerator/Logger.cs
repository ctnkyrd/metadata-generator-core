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
        public static string logFilePath = "Log";
        public static Dictionary<string, string> logTypes = new Dictionary<string, string> {
            {"i", "[INFORMATION]"},
            {"s", "[SUCCESS]"},
            {"w", "[WARNING]"},
            {"e", "[ERROR]"}
        };

        
        public void makeLogDir(string folderPath)
        {
            try
            {
                string path = folderPath;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {

                createLog(e.Message.ToString(), "e");
            }

        }

        public void createLog(string logText, string logType)
        {
            makeLogDir(logFilePath);
            try
            {
                string logFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string text = DateTime.Now.ToString("[yyyyMMdd HH:mm:ss.fff]") + logTypes[logType] + "- "+ logText + Environment.NewLine;
                File.AppendAllText(logFilePath + "\\" + logFileName, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                //nothing continue
            }
        }

    }
}
