using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace metadataGenerator
{
   

    class Parameters
    {
        public class Result
        {
            public bool status { get; set; }
            public string value { get; set; }
        }

        static string paramters_file = ConfigurationManager.AppSettings["parameters"];

        static Regex rFilter = new Regex(@"^({[Tt])@(.*)}$");

        dynamic data = JsonConvert.DeserializeObject(File.ReadAllText(paramters_file));

        //General Information
        public Result p_metadataFolder{get;set;}
        public string p_postgresqlConnectionString {get;set;}
        public string p_topicCategory { get; set; }
        public List<string> p_onlineResources { get; set; }

        //CatalogServer
        public bool p_save2Catalog { get; set; }
        public bool p_catalogOverwriteSameUUID { get; set; }
        public string p_catalogURL { get; set; }
        public string p_catalogUsername { get; set; }
        public string p_catalogPassword { get; set; }

        //Kurum Information
        public string p_kurumName { get; set; }
        public string p_organizationEmail { get; set; }
        //Record Base Information
        public List<Result> p_vt_keywords { get; set; }
        public string p_tableName { get; set; }
        public string p_tableCriteria { get; set; }
        public Result p_vt_guid { get; set; }
        public Result p_vt_metadataName { get; set; }
        public Result p_vt_responsibleMail { get; set; }
        public Result p_vt_abstract { get; set; }
        //BBOX
        public Result p_vt_bbox_west { get; set; }
        public Result p_vt_bbox_east { get; set; }
        public Result p_vt_bbox_north { get; set; }
        public Result p_vt_bbox_south { get; set; }

        public string p_useLimitation { get; set; }
        public string p_otherConstraints { get; set; }

        public string cnnString()
        {
            p_postgresqlConnectionString = data.General.PostgresqlConnectionString;
            return p_postgresqlConnectionString;
        }

        public void generator()
        {
            p_metadataFolder = getColumnName(data.General.MetadataFolder);
            p_topicCategory = data.General.TopicCategory;
            p_onlineResources = jArray2ListString(data.General.OnlineResources);
            p_vt_keywords = getColumnNamesMulti(data.Table.KeywordsColumns);
            p_tableName = data.Table.TableName;
            p_tableCriteria = data.Table.Criteria;
            p_vt_metadataName = getColumnName(data.Table.MetadataName);
            p_vt_abstract = getColumnName(data.Table.Abstract);

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

            //catalog
            p_save2Catalog = Convert.ToBoolean(data.CatalogServer.saveToCatalog);
            p_catalogOverwriteSameUUID = Convert.ToBoolean(data.CatalogServer.overwriteSameUUID);
            p_catalogURL = data.CatalogServer.url;
            p_catalogUsername = data.CatalogServer.username;
            p_catalogPassword = data.CatalogServer.password;
        }

        public Result getColumnName(JValue column)
        {
            string columName = column.ToString();
            Match match = rFilter.Match(columName);
            Result result = new Result();
            if (match.Success)
            {
                result.status = true;
                result.value = match.Groups[2].Value;
                return result;
            }
            else
            {
                result.status = false;
                result.value = column.ToString();
                return result;
            }
        }

        public List<Result> getColumnNamesMulti(JArray column)
        {
            List<Result> arrayList = new List<Result>();
            foreach (JToken i in column)
            {
                Result result = new Result();
                Match match = rFilter.Match(i.Value<string>("Name"));
                if (match.Success)
                {
                    result.status = true;
                    result.value = match.Groups[2].Value;
                    arrayList.Add(result);
                }
                else
                {
                    result.status = false;
                    result.value = i.Value<string>("Name");
                    arrayList.Add(result);
                }
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
    }
}
