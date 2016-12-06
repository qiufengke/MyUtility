using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using XmlAndDataSet;

namespace XmlandDataSet
{
    public partial class FormMain : Form
    {
        private static FormCreateXml _instance;

        public static FormCreateXml Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FormCreateXml();
                else if (_instance.IsDisposed)
                    _instance = new FormCreateXml();
                return _instance;
            }
        }

        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// XML to DataSet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToDataSet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbXml.Text)) return;

            try
            {
                var ds = new DataSet();
                if (string.IsNullOrEmpty(txtFile.Text))
                {
                    var sr = new StringReader(rtbXml.Text);
                    ds.ReadXml(sr);
                }
                else
                {
                    ds.ReadXml(txtFile.Text);
                }

                dGV.DataSource = ds.Tables[ds.Tables.Count - 1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            var oFD = new OpenFileDialog
            {
                Title = @"打开文件",
                ShowHelp = true,
                Filter = @"xml文件|*.xml|文本文件|*.txt",
                FilterIndex = 1,
                RestoreDirectory = false,
                InitialDirectory = "c:\\",
                Multiselect = false
            };

            if (oFD.ShowDialog() != DialogResult.OK) return;
            txtFile.Text = oFD.FileName;

            try
            {
                var ds = new DataSet();
                //从路径读取文件
                ds.ReadXml(oFD.FileName);
                dGV.DataSource = ds.Tables[ds.Tables.Count - 1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                var sr = new StreamReader(oFD.FileName, oFD.FileName.EndsWith(".txt") ? Encoding.Default : Encoding.UTF8);
                rtbXml.Text = sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            var ds = dGV.DataSource as DataTable;
            if (ds == null) return;

            var sfd = new SaveFileDialog
            {
                Filter = @"xls files(*.xls)|*.xls",
                FileName = ds.TableName,
                AddExtension = true,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FormUtil.ExportToExcel(ds, sfd);
            }
        }

        /// <summary>
        /// 格式化xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFormat_Click(object sender, EventArgs e)
        {
            rtbXml.Text = FormUtil.FormatXml(rtbXml.Text);
        }

        /// <summary>
        /// 弹出XML生成框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiCreateXml_Click(object sender, EventArgs e)
        {
            Instance.Show();
            Instance.Focus();
        }
    }
}