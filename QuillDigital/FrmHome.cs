using QuillDigital.QuillWebServices;
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

namespace QuillDigital
{
    public partial class FrmHome : Form
    {
        public string clientID;
        public string secret;
        WebServiceSoapClient servRef;
        string meta = string.Empty;
        string dpi = string.Empty;
        bool lineRemoval = false;
        string strLineRemoval = string.Empty;
        string ocrType = string.Empty;

        public FrmHome(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            clientID = ClientID;
            secret = Secret;
            servRef = ServRef;

        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            DataTable languageTable = servRef.GetLanguages(clientID, secret);
            languages.Items.Add("NONE");
            foreach (DataRow langRow in languageTable.Rows)
            {
                languages.Items.Add(langRow[0].ToString());
            }
            languages.Text = "NONE";
            DPI.Items.Add("150");
            DPI.Items.Add("300");
            DPI.Text = "300";

            int count = 5000;
            while (count > 0)
            {
                metatoll.Items.Add(count.ToString());
                count--;
            }
            metatoll.Text = "5000";

            ocrtype.Items.Add("Microsoft Cloud");
            ocrtype.Items.Add("Google Cloud");
            ocrtype.Items.Add("Quill Cloud");
            ocrtype.Text = "Microsoft Cloud";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name == "FrmConfiguration")
                {
                    Application.OpenForms[i].Show();
                }
                else
                {
                    Application.OpenForms[i].Close();
                }

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath.Text = openFileDialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(folderPath.Text))
            {
                DialogResult run = MessageBox.Show("Run Quill?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (run == DialogResult.Yes)
                {
                    meta = metatoll.Text.Trim();
                    dpi = DPI.Text.Trim();
                    lineRemoval = grayscale.Checked;
                    if(lineRemoval == true)
                    {
                        strLineRemoval = "1";
                    }
                    else
                    {
                        strLineRemoval = "0";
                    }
                    ocrType = ocrtype.Text.Trim();
                    if(ocrType.Equals("Microsoft Cloud"))
                    {
                        ocrType = "1";
                    }
                    else if (ocrType.Equals("Google Cloud"))
                    {
                        ocrType = "2";
                    }
                    else
                    {
                        ocrType = "0";
                    }
                    BackgroundWorker main = new BackgroundWorker();
                    main.DoWork += Main_Run_DoWork;
                    main.RunWorkerCompleted += Main_Run_RunWorkerCompleted;
                    main.RunWorkerAsync();
                }
                else
                {
                    return;
                }


            }
            else
            {
                MessageBox.Show("Please provide a folder path for the files you wish to run.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private delegate void UpdateProgressTextDelegate(string message);
        public void ProgressLabel(string Message)
        {
            message.Text = "setting...";

            message.Text = Message;
        }
        private delegate void UpdateStatusTextDelegate(string message);
        public void StatusLabel(string Message)
        {
            status.Text = "setting...";

            status.Text = Message;
        }
        private void Main_Run_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] docList = Directory.GetFiles(folderPath.Text.Trim(), "*.*");

            //Prepare Run
            string prepare = servRef.PrepareRun(clientID, secret, Globals.sqlCon);
            if (!prepare.ToUpper().Equals("SUCCESS"))
            {
                MessageBox.Show("Oops. Something went wrong.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (string file in docList)
            {
                UpdateProgressTextDelegate UpdateText = ProgressLabel;
                UpdateStatusTextDelegate UpdateStatus = StatusLabel;
                string fileName = Path.GetFileName(file);
                Invoke(UpdateText, "Working on file: " + fileName);

                Invoke(UpdateStatus, "Transmitting File..");

                //Convert to Bytes
                FileInfo fi = new FileInfo(file);
                long numBytes = fi.Length;
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] fileArray = br.ReadBytes((int)numBytes);
                br.Close();
                fs.Close();
                fs.Dispose();
                Invoke(UpdateStatus, "Getting File ID");
                //Transmit File
                string transmit = servRef.SaveClientFile(fileArray, file, clientID, secret);
                if (!transmit.ToUpper().Equals("SUCCESS"))
                {
                    MessageBox.Show("Oops. Something went wrong." + Environment.NewLine + transmit, "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                string fileID = servRef.GetFileID(fileName, clientID, secret);

                //Get Native
                Invoke(UpdateStatus, "Native Check..");
                string native = servRef.NativeTextCheck(fileName, Globals.sqlCon, false, clientID, secret, fileID, meta);

                //Digitise
                string fullText = string.Empty;

                if (native.ToUpper().Equals("TRUE"))
                {
                    Invoke(UpdateStatus, "Getting Text..");
                    fullText = servRef.GetFullTextByID(fileID, clientID, secret);
                }
                else
                {
                    Invoke(UpdateStatus, "Digitising..");
                    try
                    {
                        string digitise = servRef.Digitise(fileName, fileID, clientID, secret, Globals.sqlCon, ocrType, strLineRemoval, dpi, "1");
                        MessageBox.Show(digitise);
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

               


            }
        }
        private void Main_Run_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void grayscale_CheckedChanged(object sender, EventArgs e)
        {
            if (grayscale.Checked == true)
            {
                MessageBox.Show("Grayscaling will add significant time to processing.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

            }
        }
    }
}
