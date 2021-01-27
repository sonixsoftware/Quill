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
    public partial class FrmClausesToExtract : Form
    {

        WebServiceSoapClient servRef;
        string clientid = string.Empty;
        string secret = string.Empty;
        DataTable clauses = null;

        public FrmClausesToExtract(string ClientID, string Secret, WebServiceSoapClient ServRef)
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

        private void FrmClausesToExtract_Load(object sender, EventArgs e)
        {
            Loading ld = new Loading();
            ld.Show();
            clauses = servRef.GetClauses(clientid, secret, Globals.sqlCon, string.Empty);
            if(clauses.Rows.Count <=0 || clauses == null)
            {
                MessageBox.Show("No Clauses to show..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ld.Close();

                this.Close();
                return;
            }
            foreach(DataRow row in clauses.Rows)
            {
                ClausesToExtract.Items.Add(row["TagOne"].ToString());
            }

            ld.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < ClausesToExtract.Items.Count; i++)
                {
                    ClausesToExtract.SetItemChecked(i, true);
                }
            }
            else
            {

                for (int i = 0; i < ClausesToExtract.Items.Count; i++)
                {
                    ClausesToExtract.SetItemChecked(i, false);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string checkedFields = string.Empty;
            for (int i = 0; i <= (ClausesToExtract.Items.Count - 1); i++)
            {
                if (ClausesToExtract.GetItemChecked(i))
                {
                    checkedFields = checkedFields + ClausesToExtract.Items[i].ToString() + ",";
                }
            }
            checkedFields = checkedFields.TrimStart(',').TrimEnd(',');
            Globals.clausesToExtract = checkedFields;
            this.Close();
        }
    }
}
