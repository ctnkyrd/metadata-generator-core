using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metadataGenerator
{
    class Parameters
    {
        static string paramters_file = ConfigurationManager.AppSettings["parameters"];

        dynamic data = JsonConvert.DeserializeObject(File.ReadAllText(paramters_file));

        public string p_metadataFolder{get;set;}
        public string p_postgresqlConnectionString {get;set;}
        public string p_topicCategory { get; set; }
        
        
        public JArray p_onlineResources { get; set; }

        public string cnnString()
        {
            p_postgresqlConnectionString = data.General.PostgresqlConnectionString;
            return p_postgresqlConnectionString;
        }

        public void generator()
        {
            p_metadataFolder = data.General.MetadataFolder;
            p_topicCategory = data.General.TopicCategory;
            p_onlineResources = data.General.OnlineResources;
        }
    }
}
