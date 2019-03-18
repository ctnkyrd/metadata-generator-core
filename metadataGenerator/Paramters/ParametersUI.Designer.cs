namespace metadataGenerator
{
    partial class ParametersUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button saveToFile;
            this.p_metadataFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.p_postgresqlConnectionString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.p_topicCategory = new System.Windows.Forms.TextBox();
            this.p_onlineResources = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            saveToFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // p_metadataFolder
            // 
            this.p_metadataFolder.Location = new System.Drawing.Point(160, 19);
            this.p_metadataFolder.Name = "p_metadataFolder";
            this.p_metadataFolder.Size = new System.Drawing.Size(432, 21);
            this.p_metadataFolder.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Klasör";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.p_onlineResources);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.p_topicCategory);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.p_metadataFolder);
            this.groupBox1.Controls.Add(saveToFile);
            this.groupBox1.Controls.Add(this.p_postgresqlConnectionString);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1092, 376);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametre Ayarları";
            // 
            // saveToFile
            // 
            saveToFile.Location = new System.Drawing.Point(1011, 347);
            saveToFile.Name = "saveToFile";
            saveToFile.Size = new System.Drawing.Size(75, 23);
            saveToFile.TabIndex = 0;
            saveToFile.Text = "Kaydet";
            saveToFile.UseVisualStyleBackColor = true;
            saveToFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "PostgreSQL Bağlantısı";
            // 
            // p_postgresqlConnectionString
            // 
            this.p_postgresqlConnectionString.Location = new System.Drawing.Point(160, 45);
            this.p_postgresqlConnectionString.Name = "p_postgresqlConnectionString";
            this.p_postgresqlConnectionString.Size = new System.Drawing.Size(432, 21);
            this.p_postgresqlConnectionString.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Topic Kategorisi";
            // 
            // p_topicCategory
            // 
            this.p_topicCategory.Location = new System.Drawing.Point(160, 72);
            this.p_topicCategory.Name = "p_topicCategory";
            this.p_topicCategory.Size = new System.Drawing.Size(432, 21);
            this.p_topicCategory.TabIndex = 5;
            // 
            // p_onlineResources
            // 
            this.p_onlineResources.FormattingEnabled = true;
            this.p_onlineResources.Location = new System.Drawing.Point(160, 99);
            this.p_onlineResources.Name = "p_onlineResources";
            this.p_onlineResources.Size = new System.Drawing.Size(432, 43);
            this.p_onlineResources.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "WMS/WFS Linkleri";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Yeni Ekle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ParametersUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 403);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ParametersUI";
            this.Text = "Parametre Ayarları";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox p_metadataFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox p_postgresqlConnectionString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox p_topicCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox p_onlineResources;
        private System.Windows.Forms.Button button1;
    }
}