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
    public partial class FrmFieldsToExtract : Form
    {
        WebServiceSoapClient servRef;
        string clientid = string.Empty;
        string secret = string.Empty;
        
        public FrmFieldsToExtract(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            servRef = ServRef;
            clientid = ClientID;
            secret = Secret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmFieldsToExtract_Load(object sender, EventArgs e)
        {
            Loading ld = new Loading();
            ld.Show();
            string fields = servRef.GetFieldNames(Globals.sqlCon, clientid, secret);
            if(fields.Length == 0)
            {
                MessageBox.Show("No Fields to show..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ld.Close();
                
                this.Close();
                return;
            }
            string[] fieldArr = fields.Split(',');
            if (fields.Length == 1)
            {
                if (fieldArr[0].ToString().Trim().Equals("0"))
                {
                    MessageBox.Show("No Fields to show..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ld.Close();

                    this.Close();
                    return;
                }
            }
           

            foreach(string field in fieldArr)
            {
                FieldsToExtract.Items.Add(field);
            }
           
            if (!string.IsNullOrEmpty(GetConfiguration.GetConfigurationValueFields()))
            {
                string[] split = GetConfiguration.GetConfigurationValueFields().Split(',');
                foreach(string field in split)
                {
                   
                    for (int i = 0; i < FieldsToExtract.Items.Count; i++)
                    {
                        string strItem = FieldsToExtract.Items[i].ToString();
                        if (strItem.Equals(field))
                        {
                            FieldsToExtract.SetItemChecked(i, true);
                        }
                        
                    }
                }
            }
            ld.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                for (int i = 0; i < FieldsToExtract.Items.Count; i++)
                {
                    FieldsToExtract.SetItemChecked(i, true);
                }
            }
            else
            {

                for (int i = 0; i < FieldsToExtract.Items.Count; i++)
                {
                    FieldsToExtract.SetItemChecked(i, false);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string checkedFields = string.Empty;
            for (int i = 0; i <= (FieldsToExtract.Items.Count - 1); i++)
            {
                if (FieldsToExtract.GetItemChecked(i))
                {
                    checkedFields = checkedFields + FieldsToExtract.Items[i].ToString()+",";
                }
            }
            checkedFields  = checkedFields.TrimStart(',').TrimEnd(',');
            GetConfiguration.ConfigurationValueFields(checkedFields);
            this.Close();
        }
    }
}
