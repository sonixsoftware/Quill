using QuillDigital.QuillWebServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuillDigital
{
    public partial class FrmSettings : Form
    {

        public string clientID;
        public string secret;
        WebServiceSoapClient servRef;
        public FrmSettings(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            clientID = ClientID;
            secret = Secret;
            servRef = ServRef;
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            ignoreMeta.Checked = true;
            richTextBox1.Text = "Client ID: " + clientID;
            label3.Text = secret;
            DPI.Items.Add("150");
            DPI.Items.Add("300");
            DPI.Text = GetConfiguration.GetConfigurationValueDPI();

            int count = 5000;
            while (count > 0)
            {
                metatoll.Items.Add(count.ToString());
                count--;
            }
            metatoll.Text = GetConfiguration.GetConfigurationValueMeta();

            ocrtype.Items.Add("Microsoft Cloud");
            ocrtype.Items.Add("Google Cloud");
            ocrtype.Items.Add("Quill Cloud");
            ocrtype.Text = GetConfiguration.GetConfigurationValueOCR();
            string grayScale = GetConfiguration.GetConfigurationValueGrayScale();

            if (grayScale.Trim().Equals("1"))
            {
                grayscale.Checked = true;
            }
            else
            {
                grayscale.Checked = false;
            }
        }

        private void grayscale_CheckedChanged(object sender, EventArgs e)
        {
            if (grayscale.Checked == true)
            {
                MessageBox.Show("Grayscaling will add significant time to processing.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dpi = DPI.Text.Trim();
            string ocr = ocrtype.Text.Trim();
            string meta = metatoll.Text.Trim();
           

            DialogResult save = MessageBox.Show("Save settings?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(save == DialogResult.Yes)
            {
                GetConfiguration.ConfigurationValueDPI(dpi);
                GetConfiguration.ConfigurationValueMeta(meta);
                GetConfiguration.ConfigurationValueOCR(ocr);
                if(grayscale.Checked == true)
                {
                    GetConfiguration.ConfigurationValueGrayScale("1");
                }
                else
                {
                    GetConfiguration.ConfigurationValueGrayScale("0");
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.quilldigital.co.uk/Login");
            
        }

        private void ignoreMeta_CheckedChanged(object sender, EventArgs e)
        {
            if(ignoreMeta.Checked == true)
            {
                Globals.ignoreMeta = "TRUE";
            }
            else
            {
                Globals.ignoreMeta = "FALSE";
            }
        }
    }
}
