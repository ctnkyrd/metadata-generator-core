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

        //General Information
        public string p_metadataFolder{get;set;}
        public string p_postgresqlConnectionString {get;set;}
        public string p_topicCategory { get; set; }
        public JArray p_onlineResources { get; set; }

        //Kurum Information
        public string p_kurumName { get; set; }
        public string p_organizationEmail { get; set; }

        //Record Base Information
        public JArray p_keywords { get; set; }
        public string p_tableName { get; set; }
        public string p_tableCriteria { get; set; }
        public string p_guid { get; set; }
        public string p_metadataName { get; set; }
        public string p_responsibleMail { get; set; }
            //BBOX
        public string p_bbox_west { get; set; }
        public string p_bbox_east { get; set; }
        public string p_bbox_north { get; set; }
        public string p_bbox_south { get; set; }


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
            p_keywords = data.Table.KeywordsColumns;
            p_tableName = data.Table.TableName;
            p_tableCriteria = data.Table.Criteria;
            p_metadataName = data.Table.MetadataName;
            p_guid = data.Table.GUID;
            p_responsibleMail = data.Table.ResponsibleMail;

            p_bbox_west = data.Table.BBOX.westLongitute;
            p_bbox_east = data.Table.BBOX.eastLongitude;
            p_bbox_north = data.Table.BBOX.northLatitude;
            p_bbox_south = data.Table.BBOX.southLatidude;


            p_kurumName = data.Kurum.Name;
            p_organizationEmail = data.Kurum.OrganizationEmail;
        }
    }
}
