namespace XmlandDataSet
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnToDataSet = new System.Windows.Forms.Button();
            this.dGV = new System.Windows.Forms.DataGridView();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFormat = new System.Windows.Forms.Button();
            this.rtbXml = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnToDataSet
            // 
            this.btnToDataSet.Location = new System.Drawing.Point(125, 293);
            this.btnToDataSet.Name = "btnToDataSet";
            this.btnToDataSet.Size = new System.Drawing.Size(99, 35);
            this.btnToDataSet.TabIndex = 0;
            this.btnToDataSet.Text = "↑";
            this.btnToDataSet.UseVisualStyleBackColor = true;
            this.btnToDataSet.Click += new System.EventHandler(this.btnToDataSet_Click);
            // 
            // dGV
            // 
            this.dGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV.BackgroundColor = System.Drawing.Color.White;
            this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV.Location = new System.Drawing.Point(48, 26);
            this.dGV.Name = "dGV";
            this.dGV.RowTemplate.Height = 23;
            this.dGV.Size = new System.Drawing.Size(696, 203);
            this.dGV.TabIndex = 1;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(381, 293);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(99, 35);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "打开文件";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(190, 249);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(425, 21);
            this.txtFile.TabIndex = 3;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(516, 293);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(99, 35);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(125, 252);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(65, 12);
            this.lblFile.TabIndex = 5;
            this.lblFile.Text = "文件路径：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(46, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "DataSet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "(优先从路径加载文件)";
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(252, 293);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(99, 35);
            this.btnFormat.TabIndex = 8;
            this.btnFormat.Text = "格式化xml";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // rtbXml
            // 
            this.rtbXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbXml.Location = new System.Drawing.Point(50, 359);
            this.rtbXml.Name = "rtbXml";
            this.rtbXml.Size = new System.Drawing.Size(694, 219);
            this.rtbXml.TabIndex = 9;
            this.rtbXml.Text = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 638);
            this.Controls.Add(this.rtbXml);
            this.Controls.Add(this.btnFormat);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnToDataSet);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.dGV);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XmlToDataSet 工具";
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnToDataSet;
        private System.Windows.Forms.DataGridView dGV;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFormat;
        private System.Windows.Forms.RichTextBox rtbXml;
    }
}

