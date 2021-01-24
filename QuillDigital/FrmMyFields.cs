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
    public partial class FrmMyFields : Form
    {
        WebServiceSoapClient servRef;
        string clientid = string.Empty;
        string secret = string.Empty;
        DataTable dtFields = null;
        public FrmMyFields(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            servRef = ServRef;
            clientid = ClientID;
            secret = Secret;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMyFields_Load(object sender, EventArgs e)
        {
            Loading ld = new Loading();
            ld.Show();
            types.Items.Add("Number");
            types.Items.Add("Currency");
            types.Items.Add("Date");
            types.Items.Add("Percentage");
            types.Items.Add("Phone Number");
            types.Items.Add("Email");
            types.Items.Add("URL");
            types.Text = "Number";
            int count = 1;
            while (count != 101)
            {
                precisionIn.Items.Add(count.ToString());
                count++;
            }
            precisionIn.Text = "80";
            count = 1;
            while (count != 5000)
            {
                pForward.Items.Add(count.ToString());
                count++;
            }
            count = 1;
            while (count != 5000)
            {
                pBack.Items.Add(count.ToString());
                count++;
            }
            pBack.Text = "250";
            pForward.Text = "250";
            string fields = servRef.GetFieldNames(Globals.sqlCon, clientid, secret);
            string[] fieldArr = fields.Split(',');
            foreach(string fieldIn in fieldArr)
            {
                Fields.Items.Add(fieldIn);
            }
           
            dtFields  = servRef.GetFields(Globals.sqlCon, clientid, secret);
            if(dtFields.Rows.Count == 0 || dtFields == null)
            {
                MessageBox.Show("No Fields set up yet.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            Fields.SelectedIndex = 0;
            ld.Close();

        }

        private void FindAll_CheckedChanged(object sender, EventArgs e)
        {

            if (findAll.Checked == true)
            {
               
                pForward.Enabled = false;
                pBack.Enabled = false;
                precisionIn.Enabled = false;
                richTextBox1.Enabled = false;
                types.Items.Remove("Text (Generic)");
            }
            else
            {
                
                pForward.Enabled = true;
                pBack.Enabled = true;
                precisionIn.Enabled = true;
                richTextBox1.Enabled = true;
                types.Items.Add("Text (Generic)");
            }
        }

        private void Fields_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loading ld = new Loading();
            ld.Show();

            string field = Fields.Text;
           //get data from master table
            foreach(DataRow words in dtFields.Rows)
            {
                if (field.ToUpper().Trim().Equals(words["Field_Name"].ToString().ToUpper().Trim()))
                {
                    if (words["Check_All"].ToString().Trim().Equals("1"))
                    {
                        findAll.Checked = true;
                        types.Text = words["Field_Type"].ToString().Trim();
                        pForward.Enabled = false;
                        pBack.Enabled = false;
                        precisionIn.Enabled = false;
                        richTextBox1.Text = "";
                        richTextBox1.Enabled = false;
                        button1.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        return;
                    }
                    else
                    {
                        
                    }
                }
               
            }


            ld.Close();
        }
    }
}
