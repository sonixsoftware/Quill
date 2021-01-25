using PInvoke;
using QuillDigital.QuillWebServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuillDigital
{
    public partial class FrmHome : Form
    {
       
        bool cancelMain = false;
        public BackgroundWorker main = new BackgroundWorker();
        public string translationlang = string.Empty;
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
            Loading ld = new Loading();
            ld.Show();
            ld.Activate();
           

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
            string pages = servRef.GetPagesLeft(clientID, secret);
            label7.Text = "Pages left: " + pages;
            extractFields.Checked = true;
            ld.Close();
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
                    cancelMain = false;
                    translationlang = languages.Text.Trim();
                    languages.Enabled = false;
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
                    button2.Enabled = false;
                    progressBar.Visible = true;
                    button3.Enabled = true;

                    main.DoWork += Main_Run_DoWork;
                    main.RunWorkerCompleted += Main_Run_RunWorkerCompleted;
                    main.WorkerSupportsCancellation = true;
                    main.WorkerReportsProgress = true;
                   
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
        private delegate void UpdateStatusProgressDelegate(int percent);
        public void Progress(int percent)
        {
            progressBar.Value = percent;
        }
        private void Main_Run_DoWork(object sender, DoWorkEventArgs e)
        {
           
            //Make sure thread does not send PC to sleep
            Kernel32.SetThreadExecutionState(Kernel32.EXECUTION_STATE.ES_CONTINUOUS |
                                           Kernel32.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                           Kernel32.EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            string[] docList = null;
            try
            {
                docList = Directory.GetFiles(folderPath.Text.Trim(), "*.*");
            }
            catch
            {
                MessageBox.Show("Unable to find directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
            }

            //Prepare Run
            string prepare = servRef.PrepareRun(clientID, secret, Globals.sqlCon);
            if (!prepare.ToUpper().Equals("SUCCESS"))
            {
                MessageBox.Show("Oops. Something went wrong.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            foreach (string file in docList)
            {
                if (cancelMain == true)
                {
                   
                    break;
                }
                UpdateStatusProgressDelegate UpdateProgress = Progress;
                UpdateProgressTextDelegate UpdateText = ProgressLabel;
                UpdateStatusTextDelegate UpdateStatus = StatusLabel;
                string fileName = Path.GetFileName(file);
                Invoke(UpdateProgress, 0);
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
                Invoke(UpdateProgress, 10);
                if (cancelMain == true)
                {
                    
                    return;
                }
                string transmit = servRef.SaveClientFile(fileArray, file, clientID, secret);
                if (!transmit.ToUpper().Equals("SUCCESS"))
                {
                    MessageBox.Show("Oops. Something went wrong.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cancelMain = true;
                    break;
                }
                if (cancelMain == true)
                {
                    
                    break;
                }
                string fileID = servRef.GetFileID(fileName, clientID, secret);
                Invoke(UpdateProgress, 20);
                //Get Native
                Invoke(UpdateStatus, "Native Check..");
                string native = servRef.NativeTextCheck(fileName, Globals.sqlCon, false, clientID, secret, fileID, meta);
                Invoke(UpdateProgress, 30);
                //Digitise
                string fullText = string.Empty;

                if (native.ToUpper().Equals("TRUE"))
                {
                    if (cancelMain == true)
                    {
                        
                        break;
                    }
                    Invoke(UpdateStatus, "Getting Text..");
                    fullText = servRef.GetFullTextByID(fileID, clientID, secret);
                    Invoke(UpdateProgress, 40);
                }
                else
                {
                    Invoke(UpdateStatus, "Digitising..");
                    try
                    {
                        if (cancelMain == true)
                        {
                            
                            break;
                        }
                        Invoke(UpdateProgress, 40);
                        string digitise = servRef.Digitise(fileName, fileID, clientID, secret, Globals.sqlCon, ocrType, strLineRemoval, dpi, "0");
                        
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Oops. Something went wrong.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       
                        break;
                    }
                    Invoke(UpdateProgress, 50);
                    Invoke(UpdateStatus, "Getting Text..");
                    if (cancelMain == true)
                    {
                       
                        break;
                    }
                    fullText = servRef.GetFullTextByID(fileID, clientID, secret);
                   
                }
                string translated = string.Empty;
                Invoke(UpdateProgress, 50);
                //Translate
                if (!translationlang.ToUpper().Trim().Equals("NONE"))
                {
                    Invoke(UpdateProgress, 60);
                    Invoke(UpdateStatus, "Translating..");
                    if (cancelMain == true)
                    {
                        
                        break;
                    }
                    translated = servRef.Translate(clientID, secret, Globals.sqlCon, fullText, translationlang);

                }
                string fields = string.Empty;
                //extract fields
               if(extractFields.Checked == true)
                {
                    Invoke(UpdateProgress, 60);
                    Invoke(UpdateStatus, "Getting Field Names..");
                    if (cancelMain == true)
                    {
                        
                        break;
                    }
                   
                    //No Fields set up
                    if (fields.Equals("0"))
                    {
                        Invoke(UpdateProgress, 70);
                        Invoke(UpdateStatus, "No Field Names..");
                    }
                    else
                    {
                        Invoke(UpdateProgress, 70);
                        if (cancelMain == true)
                        {
                           
                            break;
                        }
                        fields = servRef.ExtractFieldsByFileID(fileID, fileName, clientID, secret, Globals.sqlCon, "0", Globals.fieldsToExtract, "0");
                    }
                   
                }
                if (cancelMain == true)
                {
                    
                    break;
                }
                string clausesFound = string.Empty;
                string tags = string.Empty;
                if (clauses.Checked == true)
                {
                    DataTable clauses = servRef.GetClauses(clientID, secret, Globals.sqlCon, string.Empty);
                    if (clauses.Rows.Count > 0)
                    {
                        foreach(DataRow row in clauses.Rows)
                        {
                            string tagOne = row["TagOne"].ToString();
                            tags = tags + ","+tagOne;
                           
                        }
                    }
                    tags = tags.TrimStart(',').TrimEnd(',');
                    clausesFound = servRef.CheckForClausesByFileID(clientID, secret, Globals.sqlCon, fileID, fileName, tags);
                }
            }
        }
        private void Main_Run_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button3.Enabled = false;
            UpdateProgressTextDelegate UpdateText = ProgressLabel;
            UpdateStatusTextDelegate UpdateStatus = StatusLabel;
            UpdateStatusProgressDelegate UpdateProgress = Progress;
            if (main.IsBusy)
            {
                Invoke(UpdateText, "Stopping..");
                Invoke(UpdateStatus, "Stopping..");
            }
            Invoke(UpdateText, "Complete.");
            Invoke(UpdateStatus, "");
            button2.Enabled = true;
            Invoke(UpdateProgress, 0);
            string pages = servRef.GetPagesLeft(clientID, secret);
            label7.Text = "Pages left: " + pages;
            languages.Enabled = true;
            progressBar.Visible = false;
            
            
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

        private void folderPath_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(folderPath.Text.Trim()))
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            DialogResult stop = MessageBox.Show("Stop Quill?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(DialogResult.Yes == stop)
            {
                button3.Enabled = false;
                try
                {
                    
                    string stopOps = servRef.AbortOperation(clientID, secret);
                }
                catch
                {
                    //this is OK - if fails, abort has been logged
                }
                
                UpdateProgressTextDelegate UpdateText = ProgressLabel;
                UpdateStatusTextDelegate UpdateStatus = StatusLabel;
                UpdateStatusProgressDelegate UpdateProgress = Progress;
                
                Invoke(UpdateText, "Stopping...");
                Invoke(UpdateStatus, "Stopping...");
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmFieldExtraction extraction = new FrmFieldExtraction(clientID,secret,servRef);
            extraction.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmMyFields myFields = new FrmMyFields(clientID, secret, servRef);
            myFields.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmFieldsToExtract fieldstoextract = new FrmFieldsToExtract(clientID, secret, servRef);
            fieldstoextract.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmChangeSecret secretChange = new FrmChangeSecret(clientID, secret, servRef);
            secretChange.ShowDialog();
        }
    }
}
