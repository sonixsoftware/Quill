namespace QuillDigital
{
    partial class FrmMyFields
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMyFields));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.Fields = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.findAll = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.strRegex = new System.Windows.Forms.RichTextBox();
            this.regex = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.dedupe = new System.Windows.Forms.CheckBox();
            this.currSymbol = new System.Windows.Forms.CheckBox();
            this.dgvWords = new System.Windows.Forms.DataGridView();
            this.Word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Backward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPhrases = new System.Windows.Forms.DataGridView();
            this.Phrase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.types = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.precisionIn = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pBack = new System.Windows.Forms.ComboBox();
            this.pForward = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhrases)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.75F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvWords, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvPhrases, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.80711F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.19289F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.findAll, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel9, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel10, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.06011F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.93989F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(412, 222);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.64922F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.35078F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.Fields, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(200, 33);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Field";
            // 
            // Fields
            // 
            this.Fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Fields.FormattingEnabled = true;
            this.Fields.Location = new System.Drawing.Point(76, 3);
            this.Fields.Name = "Fields";
            this.Fields.Size = new System.Drawing.Size(121, 21);
            this.Fields.TabIndex = 1;
            this.Fields.SelectedIndexChanged += new System.EventHandler(this.Fields_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Words";
            // 
            // findAll
            // 
            this.findAll.AutoSize = true;
            this.findAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findAll.Location = new System.Drawing.Point(224, 3);
            this.findAll.Name = "findAll";
            this.findAll.Size = new System.Drawing.Size(185, 33);
            this.findAll.TabIndex = 3;
            this.findAll.Text = "Find All";
            this.findAll.UseVisualStyleBackColor = true;
            this.findAll.CheckedChanged += new System.EventHandler(this.FindAll_CheckedChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(224, 42);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(185, 146);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.19418F));
            this.tableLayoutPanel9.Controls.Add(this.strRegex, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.regex, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 42);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(215, 146);
            this.tableLayoutPanel9.TabIndex = 5;
            // 
            // strRegex
            // 
            this.strRegex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strRegex.Location = new System.Drawing.Point(3, 28);
            this.strRegex.Name = "strRegex";
            this.strRegex.Size = new System.Drawing.Size(209, 115);
            this.strRegex.TabIndex = 1;
            this.strRegex.Text = "";
            this.strRegex.Visible = false;
            // 
            // regex
            // 
            this.regex.AutoSize = true;
            this.regex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regex.Location = new System.Drawing.Point(3, 3);
            this.regex.Name = "regex";
            this.regex.Size = new System.Drawing.Size(209, 19);
            this.regex.TabIndex = 0;
            this.regex.Text = "Regex";
            this.regex.UseVisualStyleBackColor = true;
            this.regex.CheckedChanged += new System.EventHandler(this.regex_CheckedChanged);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.72973F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.27027F));
            this.tableLayoutPanel10.Controls.Add(this.dedupe, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.currSymbol, 1, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(224, 194);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(185, 25);
            this.tableLayoutPanel10.TabIndex = 6;
            // 
            // dedupe
            // 
            this.dedupe.AutoSize = true;
            this.dedupe.Location = new System.Drawing.Point(3, 3);
            this.dedupe.Name = "dedupe";
            this.dedupe.Size = new System.Drawing.Size(85, 17);
            this.dedupe.TabIndex = 6;
            this.dedupe.Text = "DeDuplicate";
            this.dedupe.UseVisualStyleBackColor = true;
            // 
            // currSymbol
            // 
            this.currSymbol.AutoSize = true;
            this.currSymbol.Location = new System.Drawing.Point(94, 3);
            this.currSymbol.Name = "currSymbol";
            this.currSymbol.Size = new System.Drawing.Size(60, 17);
            this.currSymbol.TabIndex = 7;
            this.currSymbol.Text = "Symbol";
            this.currSymbol.UseVisualStyleBackColor = true;
            this.currSymbol.Visible = false;
            // 
            // dgvWords
            // 
            this.dgvWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Word,
            this.Forward,
            this.Backward});
            this.dgvWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWords.Location = new System.Drawing.Point(3, 231);
            this.dgvWords.Name = "dgvWords";
            this.dgvWords.Size = new System.Drawing.Size(412, 216);
            this.dgvWords.TabIndex = 1;
            // 
            // Word
            // 
            this.Word.HeaderText = "Word";
            this.Word.Name = "Word";
            this.Word.ReadOnly = true;
            // 
            // Forward
            // 
            this.Forward.HeaderText = "Forward";
            this.Forward.Name = "Forward";
            this.Forward.ReadOnly = true;
            // 
            // Backward
            // 
            this.Backward.HeaderText = "Backward";
            this.Backward.Name = "Backward";
            this.Backward.ReadOnly = true;
            // 
            // dgvPhrases
            // 
            this.dgvPhrases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhrases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Phrase,
            this.Precision});
            this.dgvPhrases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhrases.Location = new System.Drawing.Point(421, 231);
            this.dgvPhrases.Name = "dgvPhrases";
            this.dgvPhrases.Size = new System.Drawing.Size(376, 216);
            this.dgvPhrases.TabIndex = 2;
            // 
            // Phrase
            // 
            this.Phrase.HeaderText = "Phrase";
            this.Phrase.Name = "Phrase";
            this.Phrase.ReadOnly = true;
            // 
            // Precision
            // 
            this.Precision.HeaderText = "Precision";
            this.Precision.Name = "Precision";
            this.Precision.ReadOnly = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(421, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.61468F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.38532F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(376, 222);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Phrases";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackgroundImage = global::QuillDigital.Properties.Resources.quill_digital_logo_alizarin_crimson_white1;
            this.tableLayoutPanel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.17278F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.82722F));
            this.tableLayoutPanel5.Controls.Add(this.types, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.precisionIn, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.pBack, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.pForward, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.44444F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(182, 188);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // types
            // 
            this.types.Dock = System.Windows.Forms.DockStyle.Top;
            this.types.FormattingEnabled = true;
            this.types.Location = new System.Drawing.Point(70, 3);
            this.types.Name = "types";
            this.types.Size = new System.Drawing.Size(109, 21);
            this.types.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Type";
            // 
            // precisionIn
            // 
            this.precisionIn.Dock = System.Windows.Forms.DockStyle.Top;
            this.precisionIn.FormattingEnabled = true;
            this.precisionIn.Location = new System.Drawing.Point(70, 156);
            this.precisionIn.Name = "precisionIn";
            this.precisionIn.Size = new System.Drawing.Size(109, 21);
            this.precisionIn.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 35);
            this.label4.TabIndex = 1;
            this.label4.Text = "Precision";
            // 
            // pBack
            // 
            this.pBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBack.FormattingEnabled = true;
            this.pBack.Location = new System.Drawing.Point(70, 129);
            this.pBack.Name = "pBack";
            this.pBack.Size = new System.Drawing.Size(109, 21);
            this.pBack.TabIndex = 2;
            this.pBack.SelectedIndexChanged += new System.EventHandler(this.pBack_SelectedIndexChanged);
            // 
            // pForward
            // 
            this.pForward.Dock = System.Windows.Forms.DockStyle.Top;
            this.pForward.FormattingEnabled = true;
            this.pForward.Location = new System.Drawing.Point(70, 99);
            this.pForward.Name = "pForward";
            this.pForward.Size = new System.Drawing.Size(109, 21);
            this.pForward.TabIndex = 3;
            this.pForward.SelectedIndexChanged += new System.EventHandler(this.pForward_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "Back";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Forward";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.424084F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.57591F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(191, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.3913F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.6087F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(182, 188);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.button4, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.button3, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(20, 82);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.55752F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.44248F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(159, 103);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(82, 54);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 46);
            this.button4.TabIndex = 3;
            this.button4.Text = "Delete Field";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(3, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 46);
            this.button3.TabIndex = 2;
            this.button3.Text = "Delete Row";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(82, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 45);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add Phrase";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Word";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.54762F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.45238F));
            this.tableLayoutPanel8.Controls.Add(this.button5, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.button6, 1, 1);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(20, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(159, 71);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.Location = new System.Drawing.Point(92, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(64, 29);
            this.button5.TabIndex = 0;
            this.button5.Text = "Close";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button6.Location = new System.Drawing.Point(92, 38);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(64, 30);
            this.button6.TabIndex = 1;
            this.button6.Text = "Save";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // FrmMyFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMyFields";
            this.Text = "My Fields";
            this.Load += new System.EventHandler(this.FrmMyFields_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhrases)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvWords;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Fields;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox findAll;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dgvPhrases;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ComboBox precisionIn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox pBack;
        private System.Windows.Forms.ComboBox pForward;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox types;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word;
        private System.Windows.Forms.DataGridViewTextBoxColumn Forward;
        private System.Windows.Forms.DataGridViewTextBoxColumn Backward;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phrase;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precision;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.CheckBox regex;
        private System.Windows.Forms.RichTextBox strRegex;
        private System.Windows.Forms.CheckBox dedupe;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.CheckBox currSymbol;
    }
}