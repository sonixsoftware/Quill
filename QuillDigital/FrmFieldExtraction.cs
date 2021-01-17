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
    public partial class FrmFieldExtraction : Form
    {
        WebServiceSoapClient servRef;
        string clientid = string.Empty;
        string secret = string.Empty;
        public FrmFieldExtraction(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            servRef = ServRef;
            clientid = ClientID;
            secret = Secret;
        }

        private void FrmFieldExtraction_Load(object sender, EventArgs e)
        {
            
            types.Items.Add("Number");
            types.Items.Add("Currency");
            types.Items.Add("Date");
            types.Items.Add("Percentage");
            types.Items.Add("Phone Number");
            types.Items.Add("Email");
            types.Items.Add("URL");
            types.Text = "Number";
            button3.Enabled = false;
            button4.Enabled = false;
            precisionIn.Enabled = false;
            richTextBox1.Enabled = false;
            findAll.Checked = true;
            int count = 0;
            while (count != 101)
            {
                precisionIn.Items.Add(count.ToString());
                count++;
            }
            precisionIn.Text = "80";
        }

        private void findAll_CheckedChanged(object sender, EventArgs e)
        {
            if (findAll.Checked == true)
            {
                button3.Enabled = false;
                button4.Enabled = false;
                precisionIn.Enabled = false;
                richTextBox1.Enabled = false;
                types.Items.Remove("Text (Generic)");
            }
            else
            {
                button3.Enabled = true;
                button4.Enabled = true;
                precisionIn.Enabled = true;
                richTextBox1.Enabled = true;
                types.Items.Add("Text (Generic)");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter a field name..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string fieldName = textBox1.Text.Trim().Replace("'","").ToUpper();
            bool findAllIn = findAll.Checked;
            string temp = "OFF";
            string fieldType = types.Text.Trim();
            if (findAllIn == true)
            {
                temp = "ON";
            }
            if (findAllIn == true)
            {
                Loading ld = new Loading();
                ld.Show();
                string add = servRef.AddField(Globals.sqlCon, fieldName, fieldType, temp, clientid, secret);
                ld.Close();
                MessageBox.Show("Field: " + fieldName + " Added.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
