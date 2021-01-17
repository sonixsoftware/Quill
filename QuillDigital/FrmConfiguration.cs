﻿using QuillDigital.QuillWebServices;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;

namespace QuillDigital
{
    public partial class FrmConfiguration : Form
    {
        bool LoggedIn = false;
        bool TimeOut = false;
        WebServiceSoapClient servRef = new WebServiceSoapClient();
        Loading ld;
        string clientID;
        string secret;
        public FrmConfiguration()
        {
            InitializeComponent();
        }

        private void FrmConfiguration_Load(object sender, EventArgs e)
        {
            string clientID = GetConfiguration.GetConfigurationValueClientID();
            if (!string.IsNullOrEmpty(clientID.Trim()))
            {
                string[] clientidArr = clientID.Split('-');
                textBox1.Text = clientidArr[0];
                textBox2.Text = clientidArr[1];
                textBox3.Text = clientidArr[2];
                textBox4.Text = clientidArr[3];
                textBox5.Text = clientidArr[4];
            }
            ((BasicHttpBinding)servRef.Endpoint.Binding).MaxReceivedMessageSize = int.MaxValue;
            ((BasicHttpBinding)servRef.Endpoint.Binding).MaxBufferSize = int.MaxValue;
            foreach (OperationDescription op in servRef.Endpoint.Contract.Operations)
            {
                var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
            servRef.Endpoint.Binding.SendTimeout = new TimeSpan(0, 30, 0);
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                textBox1.Select();
            }
            else
            {
                maskedTextBox1.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ld.Show();
                ld.Activate();
            }
            catch
            {
                ld = new Loading();
                ld.Show();
                ld.Activate();
            }
            label4.Text = "Loading..Please Wait";
            BackgroundWorker login = new BackgroundWorker();
            login.DoWork += backgroundWorker1_DoWork;
            login.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            login.RunWorkerAsync();
           

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           

            clientID = string.Empty;
            secret = maskedTextBox1.Text.Trim();
            LoggedIn = false;

            if (tableLayoutPanel2.Visible == true)
            {
                string clientConfig1 = textBox1.Text.Replace("-", "").Trim().Replace(" ", "");
                string clientConfig2 = textBox2.Text.Replace("-", "").Trim().Replace(" ", "");
                string clientConfig3 = textBox3.Text.Replace("-", "").Trim().Replace(" ", "");
                string clientConfig4 = textBox4.Text.Replace("-", "").Trim().Replace(" ", "");
                string clientConfig5 = textBox5.Text.Replace("-", "").Trim().Replace(" ", "");

                clientID = clientConfig1 + "-" + clientConfig2 + "-" + clientConfig3 + "-" + clientConfig4 + "-" + clientConfig5;
                GetConfiguration.ConfigurationValueClientID(clientID);
            }
            else
            {
                clientID = GetConfiguration.GetConfigurationValueClientID();
            }

            string login = string.Empty;
            TimeOut = false;
            try
            {
                login = servRef.CheckKeyInfo(clientID, secret);
                
            }
            catch
            {
                TimeOut = true;
                return;
            }
            if (login.ToUpper().Trim().Equals("SUCCESS"))
            {

                LoggedIn = true;
            }
            else
            {
                LoggedIn = false;
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender,  RunWorkerCompletedEventArgs e)
        {
            if (TimeOut == true)
            {
                MessageBox.Show("Command Timeout.. Make sure you are connected to the internet and try again", "Command Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (LoggedIn == true)
            {
                label4.Text = "";
                maskedTextBox1.Text = "";
                ld.Close();
                FrmHome home = new FrmHome(clientID, secret, servRef);
                home.Show();
                this.Hide();

            }
            else
            {
                label4.Text = "";
                maskedTextBox1.Text = "";
                ld.Close();
                MessageBox.Show("Invalid Login. Please try again, or to update your credentials visit https://www.quilldigital.co.uk", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmConfiguration_Shown(object sender, EventArgs e)
        {
            ld = new Loading();
        }
    }
}