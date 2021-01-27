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
    public partial class FrmClauseFinder : Form
    {
        public string clientID;
        public string secret;
        WebServiceSoapClient servRef;
        public FrmClauseFinder(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            clientID = ClientID;
            secret = Secret;
            servRef = ServRef;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            string clauseSend = servRef.CreateClause(Globals.sqlCon,clientID,secret,richTextBox1.Text.Trim(),Tag1.Text.Trim(), Tag2.Text.Trim(), Tag3.Text.Trim(), Tag4.Text.Trim(), Tag5.Text.Trim(),thresholdOut);
            ld.Close();
            MessageBox.Show("Clasue: " + Tag1.Text + " has been added.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void FrmClauseFinder_Load(object sender, EventArgs e)
        {
            int thresholdOut = 1;
            while(thresholdOut < 101)
            {
                threshold.Items.Add(thresholdOut.ToString());
                thresholdOut++;
            }
            threshold.Text = "80";
        }
    }
}
