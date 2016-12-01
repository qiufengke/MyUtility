using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using NPOI.HSSF.UserModel;

namespace XmlandDataSet
{
    public partial class FormMain : Form
    {
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
                ds.ReadXml(txtFile.Text);
                dGV.DataSource = ds.Tables[ds.Tables.Count - 1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                var sr = new StreamReader(txtFile.Text, oFD.FileName.EndsWith(".txt") ? Encoding.Default : Encoding.UTF8);
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
                ExportToExcel(ds, sfd);
            }
        }

        /// <summary>
        /// npoi 导出
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="sfd"></param>
        private static void ExportToExcel(DataTable ds, SaveFileDialog sfd)
        {
            var rowCount = ds.Rows.Count;
            var columnCount = ds.Columns.Count;
            var fileName = sfd.FileName;

            if (columnCount > 0 && rowCount > 0)
            {
                var wb = new HSSFWorkbook();
                var sheet = wb.CreateSheet("表1");
                var header = sheet.CreateRow(0);
                for (var i = 0; i < columnCount; i++)
                {
                    header.CreateCell(i).SetCellValue(ds.Columns[i].ColumnName);
                }
                for (var i = 0; i < rowCount; i++)
                {
                    var tr = sheet.CreateRow(i + 1);
                    for (var j = 0; j < columnCount; j++)
                    {
                        var cellValue = 0;
                        if (int.TryParse(ds.Rows[i][j].ToString(), out cellValue))
                        {
                            tr.CreateCell(j).SetCellValue(cellValue);
                        }
                        else
                        {
                            tr.CreateCell(j).SetCellValue(ds.Rows[i][j].ToString());
                        }
                    }
                }

                //var path = System.AppDomain.CurrentDomain.BaseDirectory;
                //var fileName = path + ds.TableName + ".xls";
                using (var file = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    wb.Write(file);
                    file.Close();
                }
            }
        }

        private static string FormatXml(string xml)
        {
            var xd = new XmlDocument();
            xd.LoadXml(xml);
            var sw = new StringWriter();
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 2
                };
                xd.WriteTo(xtw);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return xml;
            }
            finally
            {
                if (xtw != null) xtw.Close();
            }
            return sw.ToString();
        }

        /// <summary>
        /// 格式化xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFormat_Click(object sender, EventArgs e)
        {
            rtbXml.Text = FormatXml(rtbXml.Text);
        }
    }
}