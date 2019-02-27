using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace metadataGenerator
{
    class Parameters
    {
        static string paramters_file = ConfigurationManager.AppSettings["parameters"];

        static Regex rFilter = new Regex(@"^({[Tt])@(.*)}$");

        dynamic data = JsonConvert.DeserializeObject(File.ReadAllText(paramters_file));

        //General Information
        public string p_metadataFolder{get;set;}
        public string p_postgresqlConnectionString {get;set;}
        public string p_topicCategory { get; set; }
        public List<string> p_onlineResources { get; set; }
        //Kurum Information
        public string p_kurumName { get; set; }
        public string p_organizationEmail { get; set; }
        //Record Base Information
        public List<string> p_vt_keywords { get; set; }
        public string p_tableName { get; set; }
        public string p_tableCriteria { get; set; }
        public string p_vt_guid { get; set; }
        public string p_vt_metadataName { get; set; }
        public string p_vt_responsibleMail { get; set; }
        //BBOX
        public string p_vt_bbox_west { get; set; }
        public string p_vt_bbox_east { get; set; }
        public string p_vt_bbox_north { get; set; }
        public string p_vt_bbox_south { get; set; }

        public string p_useLimitation { get; set; }
        public string p_otherConstraints { get; set; }

        public string cnnString()
        {
            p_postgresqlConnectionString = data.General.PostgresqlConnectionString;
            return p_postgresqlConnectionString;
        }

        public void generator()
        {
            p_metadataFolder = data.General.MetadataFolder;
            p_topicCategory = data.General.TopicCategory;
            p_onlineResources = jArray2ListString(data.General.OnlineResources);
            p_vt_keywords = getColumnNamesMulti(data.Table.KeywordsColumns);
            p_tableName = data.Table.TableName;
            p_tableCriteria = data.Table.Criteria;
            p_vt_metadataName = getColumnName(data.Table.MetadataName);

            p_useLimitation = data.General.useLimitation;
            p_otherConstraints = data.General.otherConstraints;

            p_vt_guid = getColumnName(data.Table.GUID);

            p_vt_responsibleMail = getColumnName(data.Table.ResponsibleMail);
            p_vt_bbox_west = getColumnName(data.Table.BBOX.westLongitute);
            p_vt_bbox_east = getColumnName(data.Table.BBOX.eastLongitude);
            p_vt_bbox_north = getColumnName(data.Table.BBOX.northLatitude);
            p_vt_bbox_south = getColumnName(data.Table.BBOX.southLatidude);
            p_kurumName = data.Kurum.Name;
            p_organizationEmail = data.Kurum.OrganizationEmail;
        }

        public string getColumnName(JValue column)
        {
            string columName = column.ToString();
            Match match = rFilter.Match(columName);
            if (match.Success) return match.Groups[2].Value;
            else return "No Match";
        }

        public List<string> getColumnNamesMulti(JArray column)
        {
            List<string> arrayList = new List<string>();
            foreach (JToken i in column)
            {
                Match match = rFilter.Match(i.Value<string>("Name"));
                if (match.Success) arrayList.Add(match.Groups[2].Value);
            }
            return arrayList;
        }

        public List<string> jArray2ListString(JArray array)
        {
            List<string> arrayList = new List<string>();
            foreach (var i in array)
            {
                arrayList.Add(i.Value<string>("Name"));
            }
            return arrayList;
        }

        //public string getRowValue(DataRow row, string column)
        //{

        //}
    }
}
