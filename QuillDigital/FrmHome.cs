using PInvoke;
using QuillDigital.QuillWebServices;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;

namespace QuillDigital
{
    public partial class FrmHome : Form
    {
        BackgroundWorker main = new BackgroundWorker();
        bool cancelMain = false;
        public string translationlang = string.Empty;
        public string clientID;
        public string secret;
        WebServiceSoapClient servRef;
        bool lineRemoval = false;
        string strLineRemoval = string.Empty;
        bool wordWarning = false;

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
            DataTable languageTable = null;
            try
            {
                languageTable = servRef.GetLanguages(clientID, secret);
            }
            catch (Exception exe)
            {
                if (exe.ToString().Contains("Document Limit Reached"))
                {
                    MessageBox.Show("Document Limit Reached. You must purchase a license to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    System.Windows.Forms.Application.Exit();
                    return;
                }
            }


            languages.Items.Add("NONE");
            foreach (DataRow langRow in languageTable.Rows)
            {
                languages.Items.Add(langRow[0].ToString());
            }
            languages.Text = "NONE";

            string pages = servRef.GetPagesLeft(clientID, secret);
            label8.Text = "Pages left: " + pages;
            extractFields.Checked = true;
            savePath.Text = GetConfiguration.GetConfigurationValueSaveLocation();
            Globals.ocrType = GetConfiguration.GetConfigurationValueOCR();
            Globals.meta = GetConfiguration.GetConfigurationValueMeta();
            Globals.dpi = GetConfiguration.GetConfigurationValueDPI();
            string refreshDate = servRef.GetRefreshDate(clientID, secret);
            label5.Text = refreshDate;
            ld.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (System.Windows.Forms.Application.OpenForms[i].Name == "FrmConfiguration")
                {
                    System.Windows.Forms.Application.OpenForms[i].Show();
                }
                else
                {
                    System.Windows.Forms.Application.OpenForms[i].Close();
                }

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (fileType.Checked == false)
            {
                FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath.Text = openFileDialog.SelectedPath;
                    Globals.files = null;
                }
            }
            else
            {
                OpenFileDialog openFolderDialog = new OpenFileDialog();
                openFolderDialog.Filter = "All files|*.*";
                openFolderDialog.Multiselect = true;
                if (openFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Globals.files = openFolderDialog.FileNames;
                    button2.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(folderPath.Text) && fileType.Checked == false)
            {

                MessageBox.Show("Please provide a folder path for the files you wish to run.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            else
            {
                DialogResult run = MessageBox.Show("Run Quill?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (run == DialogResult.Yes)
                {
                    if (fileType.Checked == true && Globals.files == null)
                    {
                        MessageBox.Show("Please choose some files..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    try
                    {
                        File.WriteAllText(Path.Combine(savePath.Text.Trim(), "#QUILL#_OUTPUTS.txt"), "");
                        File.Delete(Path.Combine(savePath.Text.Trim(), "#QUILL#_OUTPUTS.txt"));
                    }
                    catch
                    {
                        MessageBox.Show("Unable to write to the Save directory.. Please choose a folder that you have write access to..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrEmpty(savePath.Text.Trim()))
                    {
                        MessageBox.Show("Please enter a Report directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                    if (string.IsNullOrEmpty(folderPath.Text.Trim()) && fileType.Checked == false)
                    {
                        MessageBox.Show("Please enter a Run directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                    if (savePath.Text.Trim().ToUpper().Equals(folderPath.Text.ToUpper().Trim()))
                    {
                        MessageBox.Show("The Run Folder and Report Folder are the same. You need to create a Report Folder or browse to a different location", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        button3.Enabled = false;
                        button2.Enabled = true;
                        button5.Enabled = true;
                        button9.Enabled = true;
                        button7.Enabled = true;
                        folderPath.ReadOnly = false;
                        button11.Enabled = true;
                        button12.Enabled = true;
                        savePath.ReadOnly = false;
                        languages.Enabled = true;
                        button12.PerformClick();

                        return;

                    }
                    cancelMain = false;
                    translationlang = languages.Text.Trim();
                    languages.Enabled = false;
                    button5.Enabled = false;
                    button9.Enabled = false;
                    button3.Enabled = false;
                    button7.Enabled = false;
                    button12.Enabled = false;
                    savePath.ReadOnly = true;
                    folderPath.ReadOnly = true;
                    fileType.Enabled = false;
                    button2.Enabled = false;
                    button11.Enabled = false;
                    progressBar.Visible = true;
                    button3.Enabled = true;
                    if (extractFields.Checked == true)
                    {
                        if (GetConfiguration.GetConfigurationValueFields().Equals(string.Empty))
                        {
                            DialogResult extract = MessageBox.Show("You have not selected any fields to extract.. Click Fields To Extract to choose saved fields, or OK to continue with no extraction.", "Quill", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (DialogResult.Cancel == extract)
                            {
                                button3.Enabled = false;
                                button2.Enabled = true;
                                button7.Enabled = true;
                                button5.Enabled = true;
                                button9.Enabled = true;
                                if (fileType.Checked == false)
                                {
                                    folderPath.ReadOnly = false;
                                }
                                button11.Enabled = true;
                                button12.Enabled = true;
                                savePath.ReadOnly = false;
                                languages.Enabled = true;
                                fileType.Enabled = true;
                                button6.PerformClick();
                                return;
                            }
                            else
                            {
                                extractFields.Checked = false;
                                button2.Enabled = false;
                                button3.Enabled = true;
                                button5.Enabled = false;
                                button9.Enabled = false;
                                button12.Enabled = false;
                                languages.Enabled = false;
                                button7.Enabled = false;
                                savePath.ReadOnly = true;
                                fileType.Enabled = false;
                            }
                        }
                    }
                    if (clauses.Checked == true)
                    {
                        if (GetConfiguration.GetConfigurationValueClauses().Equals(string.Empty))
                        {
                            DialogResult extract = MessageBox.Show("You have not selected any clauses to extract.. Click Clauses To Extract to choose saved clauses, or OK to continue with no extraction.", "Quill", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (DialogResult.Cancel == extract)
                            {
                                button3.Enabled = false;
                                button5.Enabled = true;
                                button7.Enabled = true;
                                button9.Enabled = true;
                                button2.Enabled = true;
                                if (fileType.Checked == false)
                                {
                                    folderPath.ReadOnly = false;
                                }
                                button11.Enabled = true;
                                button12.Enabled = true;
                                savePath.ReadOnly = false;
                                languages.Enabled = true;
                                fileType.Enabled = true;
                                button10.PerformClick();
                                return;
                            }
                            else
                            {
                                clauses.Checked = false;
                                button2.Enabled = false;
                                button7.Enabled = false;
                                button3.Enabled = true;
                                button5.Enabled = false;
                                button9.Enabled = false;
                                button12.Enabled = false;
                                savePath.ReadOnly = true;
                                fileType.Enabled = false;
                            }
                        }
                    }
                    main = new BackgroundWorker();
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

        private delegate void UpdatePagesTextDelegate(string message);
        public void PagesLabel(string Message)
        {
            label8.Text = "setting...";

            label8.Text = Message;
        }
        private delegate void UpdateStatusProgressDelegate(int percent);
        public void Progress(int percent)
        {
            progressBar.Value = percent;
        }
        static bool isNotLockFile(string n)
        {
            return (!Path.GetFileName(n).StartsWith("~$"));
        }
        private void Main_Run_DoWork(object sender, DoWorkEventArgs e)
        {

            #region Set Up Run
            DataTable finishedRun = new DataTable();
            finishedRun.Columns.Add("Digitised Text");
            finishedRun.Columns.Add("Translated Text");
            finishedRun.Columns.Add("Fields Found");
            finishedRun.Columns.Add("Clauses Found");

            Globals.dpi = GetConfiguration.GetConfigurationValueDPI();
            Globals.meta = GetConfiguration.GetConfigurationValueMeta();
            Globals.ocrType = GetConfiguration.GetConfigurationValueOCR();
            strLineRemoval = GetConfiguration.GetConfigurationValueGrayScale();
            if (Globals.ocrType.Equals("Microsoft Cloud"))
            {
                Globals.ocrType = "1";
            }
            else if (Globals.ocrType.Equals("Google Cloud"))
            {
                Globals.ocrType = "2";
            }
            else
            {
                Globals.ocrType = "0";
            }

            //Make sure thread does not send PC to sleep
            Kernel32.SetThreadExecutionState(Kernel32.EXECUTION_STATE.ES_CONTINUOUS |
                                           Kernel32.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                           Kernel32.EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            string[] docList = null;
            if (fileType.Checked == false)
            {
                try
                {
                    docList = Directory.GetFiles(folderPath.Text.Trim(), "*.*");
                }
                catch
                {
                    MessageBox.Show("Unable to find Run directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }
            else
            {
                if (Globals.files != null)
                {
                    docList = Globals.files;
                }
                else
                {
                    MessageBox.Show("Please choose some files..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }
            docList = Array.FindAll(docList, isNotLockFile);
            if (!Directory.Exists(savePath.Text.Trim()))
            {
                MessageBox.Show("Unable to find Save directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (docList.Length <= 0)
            {
                MessageBox.Show("Unable to find any files in the Run directory..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            //Prepare Run
            string prepare = servRef.PrepareRun(clientID, secret, Globals.sqlCon);
            if (!prepare.ToUpper().Equals("SUCCESS"))
            {
                MessageBox.Show("Oops. Something went wrong.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
            }
            #endregion
            string lastFile = docList[docList.Length - 1];
            foreach (string file in docList)
            {



                UpdateStatusProgressDelegate UpdateProgress = Progress;
                UpdateProgressTextDelegate UpdateText = ProgressLabel;
                UpdateStatusTextDelegate UpdateStatus = StatusLabel;
                UpdatePagesTextDelegate UpdatePages = PagesLabel;
                bool convertedFile = false;
                string runFile = file;
                string tempFile = file;
                bool corruptFile = false;
                if (Path.GetExtension(file).ToUpper().Trim().Equals(".DOCX"))
                {
                    Invoke(UpdateStatus, "Resaving Word Document as PDF");
                    try
                    {
                        //check for mixed media
                        var wordApplication = new Microsoft.Office.Interop.Word.Application();
                        wordApplication.Visible = false;

                        var document = wordApplication.Documents.Open(file, false, true);
                        Guid strGuid = Guid.NewGuid();
                        //convert to pdf
                        string tempPath = Path.GetDirectoryName(file);
                        string tempFileName = Path.GetFileNameWithoutExtension(file);
                        document.ExportAsFixedFormat(System.IO.Path.Combine(tempPath, strGuid + ".pdf"), WdExportFormat.wdExportFormatPDF);
                        runFile = System.IO.Path.Combine(tempPath, strGuid + ".pdf");
                        convertedFile = true;

                        // Close word
                        wordApplication.Quit();
                        document = null;
                        wordApplication = null;
                        GC.Collect();

                    }
                    catch
                    {
                        if (wordWarning == false)
                        {
                            MessageBox.Show("Without Microsoft Word on this machine we will only be able to use the native text these types of document(.DOCX) document. To digitise any pictures in the document, please install Microsoft Word. Alternatively convert it to a .pdf and rerun Quill.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            wordWarning = true;
                        }
                    }
                }

                #region Loop Files
                if (cancelMain == true)
                {

                    break;
                }

                string fileName = Path.GetFileName(runFile);
                Invoke(UpdateProgress, 0);
                if (!string.IsNullOrEmpty(tempFile))
                {
                    Invoke(UpdateText, "Working on file: " + Path.GetFileName(tempFile));
                }
                else
                {
                    Invoke(UpdateText, "Working on file: " + fileName);
                }

                Invoke(UpdateStatus, "Transmitting File..");
                #region Send File
                //Convert to Bytes
                FileInfo fi = new FileInfo(runFile);
                long numBytes = fi.Length;
                FileStream fs = new FileStream(runFile, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] fileArray = br.ReadBytes((int)numBytes);
                br.Close();
                fs.Close();
                fs.Dispose();
                Invoke(UpdateStatus, "Transmitting");
                //Transmit File
                Invoke(UpdateProgress, 10);
                if (cancelMain == true)
                {

                    return;
                }
                string transmit = servRef.SaveClientFile(fileArray, runFile, clientID, secret);
                bool invalidTypeContinue = false;
                if (!transmit.ToUpper().Equals("SUCCESS"))
                {
                   
                   DialogResult type = MessageBox.Show("Oops. Quill Can't convert: "+ runFile +" Would you like to continue?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (DialogResult.No == type)
                    {
                        cancelMain = true;
                        break;
                    }
                    else
                    {
                        invalidTypeContinue = true;
                    }
                }
                if (cancelMain == true)
                {

                    break;
                }
                #endregion
                if (invalidTypeContinue == false)
                {
                    #region Get File ID
                    Invoke(UpdateStatus, "Get File ID");
                    string fileID = servRef.GetFileID(fileName, clientID, secret);
                    Invoke(UpdateProgress, 20);
                    #endregion

                    #region Check file type
                    Invoke(UpdateStatus, "Native Check..");
                    string native = servRef.NativeTextCheck(fileName, Globals.sqlCon, false, clientID, secret, fileID, Globals.meta);
                    if (native.Contains("QuillException: Document Limit Reached"))
                    {
                        MessageBox.Show("Document Limit Reached. You must purchase a license to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Invoke(UpdateProgress, 30);
                    //Digitise
                    string fullText = string.Empty;
                    #endregion
                    #region Digitise
                    if (native.ToUpper().Equals("TRUE"))
                    {
                        if (cancelMain == true)
                        {

                            break;
                        }
                        Invoke(UpdateStatus, "Getting Text..");
                        fullText = servRef.GetFullTextByID(fileID, clientID, secret);
                        if (fullText.Contains("QuillException: Document Limit Reached"))
                        {
                            MessageBox.Show("Document Limit Reached. You must purchase a license to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        fullText = Regex.Replace(fullText, @"(\r\n){2,}", Environment.NewLine);

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
                            string digitise = servRef.Digitise(fileName, fileID, clientID, secret, Globals.sqlCon, Globals.ocrType, strLineRemoval, Globals.dpi, "0");
                            if (digitise.Contains("QuillException: Document limit reached"))
                            {
                                MessageBox.Show("Document Limit Reached. You must purchase a license or more pages to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (digitise.Contains("File Corrupt- unable to convert"))
                            {
                                corruptFile = true;
                            }

                        }
                        catch (Exception ex)
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
                        if (corruptFile == false)
                        {
                            fullText = servRef.GetFullTextByID(fileID, clientID, secret);
                            if (fullText.Contains("QuillException: Document Limit Reached"))
                            {
                                MessageBox.Show("Document Limit Reached. You must purchase a license to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            fullText = Regex.Replace(fullText, @"(\r\n){2,}", Environment.NewLine);
                        }
                        else
                        {
                            fullText = "File Corrupt - unable to convert";
                        }


                    }
                    string clausesFound = string.Empty;
                    string fields = string.Empty;
                    string translated = string.Empty;
                    #endregion
                    if (corruptFile == false)
                    {

                        Invoke(UpdateProgress, 50);
                        #region Translate
                        //Translate
                        if (!translationlang.ToUpper().Trim().Equals("NONE"))
                        {
                            Invoke(UpdateProgress, 60);
                            Invoke(UpdateStatus, "Translating..");
                            if (cancelMain == true)
                            {

                                break;
                            }
                            translated = servRef.TranslateByFileID(clientID, secret, Globals.sqlCon, fileID, translationlang);
                            if (translated.Contains("QuillException: Document Limit Reached"))
                            {
                                MessageBox.Show("Document Limit Reached. You must purchase a license to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            translated = Regex.Replace(translated, @"(\r\n){2,}", Environment.NewLine);


                        }
                        #endregion

                        //extract fields
                        #region Extract Fields
                        if (extractFields.Checked == true)
                        {

                            if (cancelMain == true)
                            {

                                break;
                            }



                            Invoke(UpdateProgress, 70);
                            Invoke(UpdateStatus, "Extracting Fields..");



                            if (cancelMain == true)
                            {

                                break;
                            }
                            fields = servRef.ExtractFieldsByFileID(fileID, fileName, clientID, secret, Globals.sqlCon, "0", GetConfiguration.GetConfigurationValueFields(), "0");
                            if (fields.Contains("QuillException: Document Limit Reached"))
                            {
                                MessageBox.Show("Document Limit Reached. You must purchase a license to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            fields = Regex.Replace(fields, @"(\r\n){1,}", Environment.NewLine);



                        }
                        #endregion
                        if (cancelMain == true)
                        {

                            break;
                        }

                        #region Check Clauses
                        if (clauses.Checked == true)
                        {
                            Invoke(UpdateProgress, 80);
                            Invoke(UpdateStatus, "Extracting Clauses..");
                            clausesFound = servRef.CheckForClausesByFileID(clientID, secret, Globals.sqlCon, fileID, fileName, GetConfiguration.GetConfigurationValueClauses());
                            if (clausesFound.Contains("QuillException: Document Limit Reached"))
                            {
                                MessageBox.Show("Document Limit Reached. You must purchase a license to continue, please visit www.QuillDigital.co.uk", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            DataTable dtclausesFound = servRef.GetFoundClausesByID(clientID, secret, Globals.sqlCon, fileID);
                            if (dtclausesFound.Rows.Count <= 0)
                            {
                                clausesFound = "No Clauses Found.";
                            }
                            else
                            {
                                clausesFound = string.Empty;
                                foreach (DataRow row in dtclausesFound.Rows)
                                {
                                    string tagOne = row["TagOne"].ToString();
                                    if (string.IsNullOrEmpty(tagOne))
                                    {
                                        tagOne = "Tag One not found..";
                                    }
                                    string tagTwo = row["TagTwo"].ToString();
                                    if (string.IsNullOrEmpty(tagTwo))
                                    {
                                        tagTwo = "Tag Two not found..";
                                    }
                                    string tagThree = row["TagThree"].ToString();
                                    if (string.IsNullOrEmpty(tagThree))
                                    {
                                        tagThree = "Tag Three not found..";
                                    }
                                    string tagFour = row["TagFour"].ToString();
                                    if (string.IsNullOrEmpty(tagFour))
                                    {
                                        tagFour = "Tag Four not found..";
                                    }
                                    string tagFive = row["TagFive"].ToString();
                                    if (string.IsNullOrEmpty(tagFive))
                                    {
                                        tagFive = "Tag Five not found..";
                                    }
                                    string clauseFound = row["ClauseFound"].ToString();
                                    string probablility = row["Probablility"].ToString();
                                    clausesFound = clausesFound + Environment.NewLine + tagOne + Environment.NewLine + tagTwo + Environment.NewLine + tagThree + Environment.NewLine + tagFour
                                        + Environment.NewLine + tagFive + Environment.NewLine + "Levenstein Distance: " + probablility + Environment.NewLine + Environment.NewLine;
                                }
                            }
                            clausesFound = Regex.Replace(clausesFound, @"(\r\n){2,}", Environment.NewLine);
                            //need to extract clauses
                        }

                        #endregion
                    }


                    Invoke(UpdateProgress, 90);
                    Invoke(UpdateStatus, "Writing Report..");
                    #region Write Report

                    string xmlDoc = string.Empty;
                    string xmlReport = servRef.GetReportByID(clientID, secret, fileID);

                    if (string.IsNullOrEmpty(tempFile))
                    {
                        xmlDoc = Path.GetFileNameWithoutExtension(file) + ".xml";
                        System.IO.File.WriteAllText(Path.Combine(savePath.Text, xmlDoc), xmlReport);

                    }
                    else
                    {
                        xmlDoc= Path.GetFileNameWithoutExtension(tempFile) + ".xml";
                        System.IO.File.WriteAllText(Path.Combine(savePath.Text, xmlDoc), xmlReport);

                    }
                    if (convertedFile == true)
                    {
                        File.Delete(runFile);
                    }
                    if (file.ToUpper().Trim().Equals(lastFile.ToUpper().Trim()) | tempFile.ToUpper().Trim().Equals(lastFile.ToUpper().Trim()))
                    {
                        break;
                    }
                    #endregion
                    #endregion
                }
                string pages = servRef.GetPagesLeft(clientID, secret);
                Invoke(UpdatePages, "Pages left: " + pages);

            }

            return;
        }

      
        private void Main_Run_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                button7.Enabled = true;
                button3.Enabled = false;
                button5.Enabled = true;
                button9.Enabled = true;
                button2.Enabled = false;
                folderPath.ReadOnly = false;
                button11.Enabled = true;
                button12.Enabled = true;
                savePath.ReadOnly = false;
                fileType.Enabled = true;
                fileType.Checked = false;
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

                Invoke(UpdateProgress, 0);
                string pages = servRef.GetPagesLeft(clientID, secret);
                label8.Text = "Pages left: " + pages;
                languages.Enabled = true;
                progressBar.Visible = false;
                folderPath.Text = "";
                //reset away mode
                Kernel32.SetThreadExecutionState(Kernel32.EXECUTION_STATE.None |
                                               Kernel32.EXECUTION_STATE.None |
                                               Kernel32.EXECUTION_STATE.None);
                MessageBox.Show("Run Complete.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                //assume worker closed
                MessageBox.Show("Oops. Something went wrong.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            if (DialogResult.Yes == stop)
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
            FrmFieldExtraction extraction = new FrmFieldExtraction(clientID, secret, servRef);
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
            FrmSettings settings = new FrmSettings(clientID, secret, servRef);
            settings.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmClauseFinder clauseFinder = new FrmClauseFinder(clientID, secret, servRef);
            clauseFinder.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmMyClauses myClauses = new FrmMyClauses(clientID, secret, servRef);
            myClauses.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmClausesToExtract clausesToExtract = new FrmClausesToExtract(clientID, secret, servRef);
            clausesToExtract.ShowDialog();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                savePath.Text = openFileDialog.SelectedPath;
                GetConfiguration.ConfigurationValueSaveLocation(openFileDialog.SelectedPath);
            }
        }



        private void button13_Click(object sender, EventArgs e)
        {


            FrmReport report = new FrmReport(GetConfiguration.GetConfigurationValueSaveLocation());
            report.ShowDialog();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.quilldigital.co.uk/Login");
        }

        private void fileType_CheckedChanged(object sender, EventArgs e)
        {
            if (fileType.Checked == true)
            {
                button11.Text = "Choose Files";
                folderPath.Text = "";
                folderPath.ReadOnly = true;
            }
            else
            {
                button11.Text = "Open Folder";
                folderPath.Text = "";
                folderPath.ReadOnly = false;
            }
        }
    }
}
