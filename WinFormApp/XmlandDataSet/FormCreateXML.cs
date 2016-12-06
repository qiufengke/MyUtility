using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Utils;
using XmlAndDataSet.Properties;

namespace XmlAndDataSet
{
    public partial class FormCreateXml : Form
    {
        public FormCreateXml()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlUtil.CreateXml(txtFileName.Text, txtFormat.Text);
            using (
                var sr = new StreamReader(txtFileName.Text,
                    txtFileName.Text.EndsWith(".txt") ? Encoding.Default : Encoding.UTF8))
            {
                rtbResult.Text = Resources.dataMsg + Path.GetFullPath(txtFileName.Text) + @"\n" + sr.ReadToEnd();
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            rtbResult.Text = "";
            var xml = XmlUtil.CreateXml(txtFormat.Text);
            rtbResult.Text = xml;
        }
    }
}