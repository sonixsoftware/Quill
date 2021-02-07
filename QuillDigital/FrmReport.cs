using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QuillDigital
{
    public partial class FrmReport : Form
    {
        public string savePath;

        public FrmReport(string SavePath)
        {
            InitializeComponent();
            savePath = SavePath;
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            string[] docList = null;
            try
            {
                docList = Directory.GetFiles(savePath, "*.xml");
            }
            catch
            {
                MessageBox.Show("Unable to find any report files in the Report directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (docList.Length <= 0)
            {
                MessageBox.Show("Unable to find any report files in the Report directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            foreach (string file in docList)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                files.Items.Add(fileName);
            }
            files.SelectedIndex = 0;

        }

        private void files_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string xmlPath = Path.Combine(savePath, files.Text + ".xml");
                XmlDataDocument xmldoc = new XmlDataDocument();
                XmlNodeList xmlnode;
                int i = 0;
                string digitisedText = string.Empty;
                string translatedText = string.Empty;
                string language = string.Empty;
                FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                xmlnode = xmldoc.GetElementsByTagName("Run");
                for (i = 0; i <= xmlnode.Count - 1; i++)
                {
                    xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                    digitisedText = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                    translatedText = xmlnode[i].ChildNodes.Item(1).InnerText.Trim();
                    DigitisedText.Text = digitisedText;
                    TranslatedText.Text = translatedText;
                    Fields.Text = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                    Clauses.Text = xmlnode[i].ChildNodes.Item(3).InnerText.Trim();
                    label3.Text = "Language: "+xmlnode[i].ChildNodes.Item(6).InnerText.Trim();
                    string type = Path.GetExtension(xmlnode[i].ChildNodes.Item(4).InnerText.Trim());
                    label8.Text = "File Extension: " + type + " Date Run: " + xmlnode[i].ChildNodes.Item(5).InnerText.Trim();

                }
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Unable to load report..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult deleteAll = MessageBox.Show("Are you sure you want to delete: "+files.Text+"? You will be unable to view this file again.", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(deleteAll == DialogResult.Yes)
            {
                string fileName = files.Text;
               string[] docList = Directory.GetFiles(savePath, "*.xml");
                foreach(string delete in docList)
                {
                    if (Path.GetFileName(delete).Equals(fileName + ".xml"))
                    {
                        File.Delete(delete);
                    }
                }
                files.Items.Remove(fileName);
                try
                {
                    files.SelectedIndex = 0;
                }
                catch
                {
                    this.Close();
                }

            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Are you sure you want to delete all report files? This cannot be undone..", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(delete == DialogResult.Yes){
                foreach(string item in files.Items)
                {
                    string deleteItem = item + ".xml";
                    string pathDelete = Path.Combine(savePath, deleteItem);
                    File.Delete(pathDelete);
                }
                MessageBox.Show("All report files deleted.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
