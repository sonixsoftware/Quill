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
    public partial class FrmNLP : Form
    {
        public FrmNLP()
        {
            InitializeComponent();
        }

        private void FrmNLP_Load(object sender, EventArgs e)
        {
           
            NLPList.Items.Add("tokenize");          
            NLPList.Items.Add("ssplit");
            NLPList.Items.Add("POS");
            NLPList.Items.Add("lemma");
            NLPList.Items.Add("regexner");
            NLPList.Items.Add("parse");
            NLPList.Items.Add("natlog");
            NLPList.Items.Add("entitylink");
            NLPList.Items.Add("kbp");
            NLPList.Items.Add("relation");
            NLPList.Items.Add("quote");
            NLPList.Items.Add("ner");
            if (Globals.nlpAnns != null)
            {
                foreach (string anns in Globals.nlpAnns)
                {

                    for (int i = 0; i < NLPList.Items.Count; i++)
                    {
                        string strItem = NLPList.Items[i].ToString();
                        if (strItem.Equals(anns))
                        {
                            NLPList.SetItemChecked(i, true);
                        }

                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult save = MessageBox.Show("NLP can take a long time to process, especially on larger documents. Continue?", "Quill NLP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (save == DialogResult.Yes)
            {
                string nlpFields = string.Empty;
                for (int i = 0; i <= (NLPList.Items.Count - 1); i++)
                {
                    if (NLPList.GetItemChecked(i))
                    {
                        nlpFields = nlpFields + NLPList.Items[i].ToString() + ",";
                    }
                }
                if (nlpFields.Length > 1)
                {
                    Globals.nlpAnns = nlpFields.Split(',');
                }
                else
                {
                    Globals.nlpAnns = null;
                }
                MessageBox.Show("Saved NLP data for this run.", "Quill NLP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void NLPList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < NLPList.Items.Count; ++ix)
            {
                if (ix != e.Index) NLPList.SetItemChecked(ix, false);
            }

        }
    }
}
