using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.IO;

namespace metadataGenerator
{
    

    public partial class ParametersUI : Form
    {
        public ParametersUI()
        {
            InitializeComponent();
            generator();
        }
        static string paramters_file = ConfigurationManager.AppSettings["parameters"];
        dynamic data = JsonConvert.DeserializeObject(File.ReadAllText(paramters_file));
        

        public void generator()
        {
            p_metadataFolder.Text = data.General.MetadataFolder;
            p_postgresqlConnectionString.Text = data.General.PostgresqlConnectionString;
            p_topicCategory.Text = data.General.TopicCategory;

            List<string> onlineSources = jArray2ListString(data.General.OnlineResources);
            p_onlineResources.DataSource = onlineSources;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.General.MetadataFolder = p_metadataFolder.Text;
            data.General.PostgresqlConnectionString = p_postgresqlConnectionString.Text;
            data.General.TopicCategory = p_topicCategory.Text;

            generator();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(paramters_file, output);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            
           
        }
    }
}
