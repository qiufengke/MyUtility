using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using NPOI.HSSF.UserModel;

namespace XmlAndDataSet
{
    public class FormUtil
    {
        public static string FormatXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "";
            }
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
        /// npoi 导出
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="sfd"></param>
        public static void ExportToExcel(DataTable ds, SaveFileDialog sfd)
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
    }
}
