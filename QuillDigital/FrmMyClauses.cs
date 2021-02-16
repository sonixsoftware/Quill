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
    public partial class FrmMyClauses : Form
    {
        public string clientID;
        public string secret;
        WebServiceSoapClient servRef;
        DataTable clauses = null;
        string tagOneTemp = string.Empty;
        public FrmMyClauses(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            clientID = ClientID;
            secret = Secret;
            servRef = ServRef;
        }

        private void FrmMyClauses_Load(object sender, EventArgs e)
        {
            Loading ld = new Loading();
            ld.Show();
            clauses = servRef.GetClauses(clientID, secret, Globals.sqlCon, string.Empty);
            if (clauses.Rows.Count == 0 || clauses == null)
            {
                MessageBox.Show("No Clauses Found..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ld.Close();

                this.Close();
                return;
            }
            foreach (DataRow row in clauses.Rows)
            {
                string tagOne = row["TagOne"].ToString();
                clause.Items.Add(tagOne);
            }
            threshold.Text = "80";
            clause.SelectedIndex = 0;

            int thresholdOut = 1;
            while (thresholdOut < 101)
            {
                threshold.Items.Add(thresholdOut.ToString());
                thresholdOut++;
            }

            ld.Close();
        }

        private void clause_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loading ld = new Loading();
            ld.Show();
            string tagOne = clause.Text;
            foreach (DataRow row in clauses.Rows)
            {
                if (row["TagOne"].ToString().Equals(tagOne))
                {
                    tagOneTemp = row["TagOne"].ToString();
                    Tag1.Text = row["TagOne"].ToString();
                    Tag2.Text = row["TagTwo"].ToString();
                    Tag3.Text = row["TagThree"].ToString();
                    Tag4.Text = row["TagFour"].ToString();
                    Tag5.Text = row["TagFive"].ToString();
                    richTextBox1.Text = row["Letter"].ToString();
                    threshold.Text = row["P(A)"].ToString();

                }
            }
            ld.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult deleteClause = MessageBox.Show("Delete: " + Tag1.Text + "?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (deleteClause == DialogResult.Yes)
            {
                string delete = servRef.DeleteClause(Globals.sqlCon, clientID, secret, Tag1.Text);
                clause.Items.Remove(Tag1.Text);
                richTextBox1.Text = "";
                Tag1.Text = "";
                Tag2.Text = "";
                Tag3.Text = "";
                Tag4.Text = "";
                Tag5.Text = "";
                try
                {
                    clause.SelectedIndex = 0;
                    MessageBox.Show(Tag1.Text + " Deleted", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    //none to show
                    MessageBox.Show(Tag1.Text + " Deleted", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult updateClause = MessageBox.Show("Update Clause: " + Tag1.Text + "?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (updateClause == DialogResult.Yes)
            {
                string outPut = servRef.DeleteClause(Globals.sqlCon, clientID, secret, tagOneTemp);

                if (string.IsNullOrEmpty(richTextBox1.Text.Trim()))
                {
                    MessageBox.Show("Please enter a clause..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(Tag1.Text.Trim()))
                {
                    MessageBox.Show("Please enter at least Tag 1..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string thresholdOut = threshold.Text;
                if (string.IsNullOrEmpty(thresholdOut.Trim()))
                {
                    MessageBox.Show("Please enter a threshold..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Loading ld = new Loading();
                ld.Show();
                string clauseSend = servRef.CreateClause(Globals.sqlCon, clientID, secret, richTextBox1.Text.Trim(), Tag1.Text.Trim(), Tag2.Text.Trim(), Tag3.Text.Trim(), Tag4.Text.Trim(), Tag5.Text.Trim(), thresholdOut);
                int counter = 0;
                foreach (DataRow row in clauses.Rows)
                {
                    if (tagOneTemp.Equals(row["TagOne"].ToString()))
                    {
                        foreach (DataRow dgvrow in clauses.Rows)
                        {
                            string tag1 = dgvrow["TagOne"].ToString();
                            string tag2 = dgvrow["TagTwo"].ToString();
                            string tag3 = dgvrow["TagThree"].ToString();
                            string tag4 = dgvrow["TagFour"].ToString();
                            string tag5 = dgvrow["TagFive"].ToString();
                            string clauseTemp = dgvrow["Letter"].ToString();
                            string threshold = dgvrow["P(A)"].ToString();

                            clauses.Rows[counter]["TagOne"] = tag1;
                            clauses.Rows[counter]["TagTwo"] = tag2;
                            clauses.Rows[counter]["TagThree"] = tag3;
                            clauses.Rows[counter]["TagFour"] = tag4;
                            clauses.Rows[counter]["TagFive"] = tag5;
                            clauses.Rows[counter]["Letter"] = clauseTemp;
                            clauses.Rows[counter]["P(A)"] = threshold;

                            clause.Items.Remove(tagOneTemp);
                            clause.Items.Add(tag1);
                            clause.Text = tag1;


                        }
                    }
                    counter++;
                }
                MessageBox.Show(Tag1.Text + " Updated", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ld.Close();
            }
        }
    }
}
