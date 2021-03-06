﻿using QuillDigital.QuillWebServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuillDigital
{
    public partial class FrmFieldExtraction : Form
    {
        WebServiceSoapClient servRef;
        string clientid = string.Empty;
        string secret = string.Empty;
        public FrmFieldExtraction(string ClientID, string Secret, WebServiceSoapClient ServRef)
        {
            InitializeComponent();
            servRef = ServRef;
            clientid = ClientID;
            secret = Secret;
        }

        private void FrmFieldExtraction_Load(object sender, EventArgs e)
        {

            types.Items.Add("Number");
            types.Items.Add("Currency");
            types.Items.Add("Date");
            types.Items.Add("Percentage");
            types.Items.Add("Phone Number");
            types.Items.Add("Email");
            types.Items.Add("URL");
            types.Text = "Number";
            button3.Enabled = false;
            button4.Enabled = false;
            precisionIn.Enabled = false;
            richTextBox1.Enabled = false;
            findAll.Checked = true;
            int count = 1;
            while (count != 101)
            {
                precisionIn.Items.Add(count.ToString());
                count++;
            }
            precisionIn.Text = "80";
            count = 1;
            while (count != 5000)
            {
                pForward.Items.Add(count.ToString());
                count++;
            }
            count = 1;
            while (count != 5000)
            {
                pBack.Items.Add(count.ToString());
                count++;
            }
            pBack.Text = "250";
            pForward.Text = "250";
        }

        private void findAll_CheckedChanged(object sender, EventArgs e)
        {
            if (findAll.Checked == true)
            {
                button3.Enabled = false;
                button4.Enabled = false;
                button6.Enabled = false;
                pForward.Enabled = false;
                pBack.Enabled = false;
                precisionIn.Enabled = false;
                richTextBox1.Enabled = false;
                types.Items.Remove("Text (Generic)");
            }
            else
            {
                button3.Enabled = true;
                button4.Enabled = true;
                button6.Enabled = true;
                pForward.Enabled = true;
                pBack.Enabled = true;
                precisionIn.Enabled = true;
                richTextBox1.Enabled = true;
                types.Items.Add("Text (Generic)");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter a field name..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(regex.Checked == true){
                if(!types.Text.Trim().Equals("Text (Generic)"))
                {
                    MessageBox.Show("You cannot have a Custom Regex for any other Field Type than Text (Generic)", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            DialogResult addField = MessageBox.Show("Add field: " + textBox1.Text.Trim(), "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (addField == DialogResult.Yes)
            {
                string fieldName = textBox1.Text.Trim().Replace("'", "").ToUpper();
                bool findAllIn = findAll.Checked;
                string temp = "FALSE";
                string fieldType = types.Text.Trim();
                if (findAllIn == true)
                {
                    temp = "TRUE";
                }
                string depupeIN = "TRUE";
                if (dedup.Checked == false)
                {
                    depupeIN = "FALSE";
                }
                string currSymbolOnlyIN = "FALSE";
                if(currSymbol.Checked == true)
                {
                    currSymbolOnlyIN = "TRUE";
                }
                if (findAllIn == true)
                {
                    Loading ld = new Loading();
                    ld.Show();
                    string add = servRef.AddField(Globals.sqlCon, fieldName, fieldType, temp, clientid, secret);
                   
                    if (regex.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(strRegex.Text.Trim()))
                        {
                            servRef.UpdateFieldRegex(Globals.sqlCon, fieldName, strRegex.Text.Trim(), clientid, secret);
                        }
                    }
                    string depDupeOut = servRef.UpdateFieldDeDupe(Globals.sqlCon, fieldName, depupeIN, clientid, secret);
                    if (currSymbolOnlyIN.Equals("TRUE"))
                    {
                        string currSymbolOnly = servRef.UpdateFieldCurrencySymbolOnly(Globals.sqlCon, fieldName, currSymbolOnlyIN, clientid, secret);
                    }
                    ld.Close();
                    MessageBox.Show("Field: " + fieldName + " Added.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    if (dgvWords.Rows.Count == 1)
                    {
                        MessageBox.Show("Unless you use Find All, you must enter at least one word.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dgvPhrases.Rows.Count == 1)
                    {
                        MessageBox.Show("Unless you use Find All, you must enter at least one phrase.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Loading ld = new Loading();
                    ld.Show();
                    string add = servRef.AddField(Globals.sqlCon, fieldName, fieldType, temp, clientid, secret);
                    foreach (DataGridViewRow word in dgvWords.Rows)
                    {
                        if (word != null)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(word.Cells["Word"].Value.ToString().Trim()))
                                {
                                    string wordIN = word.Cells["Word"].Value.ToString().Trim();
                                    string forward = word.Cells["Forward"].Value.ToString().Trim();
                                    string backwards = word.Cells["Backward"].Value.ToString().Trim();
                                    string addWord = servRef.UpdateFieldWords(Globals.sqlCon, fieldName, wordIN, clientid, secret, forward, backwards);
                                }
                            }
                            catch
                            {
                                //skip as cannot be added
                            }
                        }
                    }
                    foreach (DataGridViewRow phrase in dgvPhrases.Rows)
                    {
                        if (phrase != null)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(phrase.Cells["Phrase"].Value.ToString().Trim()))
                                {
                                    string phraseIN = phrase.Cells["Phrase"].Value.ToString().Trim();
                                    string percision = phrase.Cells["Precision"].Value.ToString().Trim();
                                    string addPhrase = servRef.UpdateFieldPhrases(Globals.sqlCon, clientid, secret, fieldName, phraseIN, percision);
                                }
                            }
                            catch
                            {
                                //skip as cannot be added
                            }
                        }
                    }
                    if (regex.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(strRegex.Text.Trim()))
                        {
                            servRef.UpdateFieldRegex(Globals.sqlCon, fieldName, strRegex.Text.Trim(), clientid, secret);
                        }
                    }
                    string depDupeOut = servRef.UpdateFieldDeDupe(Globals.sqlCon, fieldName, depupeIN, clientid, secret);
                    if (currSymbol.Equals("TRUE"))
                    {
                        string currSymbolOnly = servRef.UpdateFieldCurrencySymbolOnly(Globals.sqlCon, fieldName, currSymbolOnlyIN, clientid, secret);
                    }
                    ld.Close();
                    MessageBox.Show("Field Added.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter a word in the text box to add.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string word = richTextBox1.Text.Trim();
            if (word.Contains(" "))
            {
                MessageBox.Show("A word cannot have any spaces..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            word = TrimPunctuation(word);
            word = Regex.Replace(word, @"[^\w\s]", "");
            string pforward = pForward.SelectedItem.ToString().Trim();
            string pback = pBack.SelectedItem.ToString().Trim();
            dgvWords.Rows.Add(word, pforward, pback);
            richTextBox1.Text = string.Empty;
        }

        static string TrimPunctuation(string value)
        {
            // Count start punctuation.
            int removeFromStart = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsPunctuation(value[i]))
                {
                    removeFromStart++;
                }
                else
                {
                    break;
                }
            }

            // Count end punctuation.
            int removeFromEnd = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (char.IsPunctuation(value[i]))
                {
                    removeFromEnd++;
                }
                else
                {
                    break;
                }
            }
            // No characters were punctuation.
            if (removeFromStart == 0 &&
                removeFromEnd == 0)
            {
                return value;
            }
            // All characters were punctuation.
            if (removeFromStart == value.Length &&
                removeFromEnd == value.Length)
            {
                return "";
            }
            // Substring.
            return value.Substring(removeFromStart,
                value.Length - removeFromEnd - removeFromStart);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text.Trim()))
            {
                MessageBox.Show("Please enter a Phrase in the text box to add.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string phrase = richTextBox1.Text.Trim();
            if (!phrase.Contains(" "))
            {
                MessageBox.Show("A phrase must be at least two words and have one space between each word..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            phrase = TrimPunctuation(phrase);
            phrase = Regex.Replace(phrase, @"[^\w\s]", "");
            string[] phrasetemp = phrase.Split(' ');
            if (phrasetemp.Length >= 2)
            {
                string percision = precisionIn.Text.Trim();
                dgvPhrases.Rows.Add(phrase, percision);
                richTextBox1.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("A phrase must be at least two words and have one space between each word..", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }



        private void types_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (types.Text.Equals("Currency"))
            {
                currSymbol.Visible = true;
                currSymbol.Checked = true;
            }
            else
            {
                currSymbol.Visible = false;
                currSymbol.Checked = false;
            }
        }

        private void pForward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvWords.Rows.Count > 1)
            {
                int counter = 0;
                string forward = pForward.Text.Trim();
                string back = pBack.Text.Trim();
                foreach (DataGridViewRow word in dgvWords.Rows)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(dgvWords.Rows[counter].Cells["Word"].Value.ToString().Trim()))
                        {
                            dgvWords.Rows[counter].Cells["Forward"].Value = forward;
                            dgvWords.Rows[counter].Cells["Backward"].Value = back;
                        }
                        counter++;
                    }
                    catch
                    {
                        //assume cant be updated
                    }
                }
            }
        }

        private void pBack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvWords.Rows.Count > 1)
            {
                int counter = 0;
                string forward = pForward.Text.Trim();
                string back = pBack.Text.Trim();
                foreach (DataGridViewRow word in dgvWords.Rows)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(dgvWords.Rows[counter].Cells["Word"].Value.ToString().Trim()))
                        {
                            dgvWords.Rows[counter].Cells["Forward"].Value = forward;
                            dgvWords.Rows[counter].Cells["Backward"].Value = back;
                        }
                        counter++;
                    }
                    catch
                    {
                        //assume cant be updated
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult deleteConfirm = MessageBox.Show("Delete Selected Rows?", "Quill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (deleteConfirm == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvWords.SelectedRows)
                {
                    dgvWords.Rows.RemoveAt(row.Index);
                }
                foreach (DataGridViewRow row in dgvPhrases.SelectedRows)
                {
                    dgvPhrases.Rows.RemoveAt(row.Index);
                }
            }
        }

        private void regex_CheckedChanged(object sender, EventArgs e)
        {
            if (regex.Checked == true)
            {
                strRegex.Visible = true;
            }
            else
            {
                strRegex.Visible = false;
                strRegex.Text = "";
            }
        }

        private void dedup_CheckedChanged(object sender, EventArgs e)
        {
            if (dedup.Checked == true)
            {
                MessageBox.Show("This will leave any duplicate entries in any fields that are searched for.", "Quill", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
