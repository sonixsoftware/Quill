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
    public partial class FrmChangeSecret : Form
    {
        public string clientID;
        public string secret;
        WebServiceSoapClient servRef;
        public FrmChangeSecret(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            clientID = ClientID;
            secret = Secret;
            servRef = ServRef;
        }

        private void FrmChangeSecret_Load(object sender, EventArgs e)
        {
            label1.Text = "Client ID: "+clientID;
            label5.Text = secret;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.quilldigital.co.uk/Login");
        }
    }
}
