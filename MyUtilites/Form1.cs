using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUtilites
{
    public partial class MainForm : Form
    {
        Dictionary<string, double> metrica;

        public MainForm()
        {
            InitializeComponent();
            counter = new Counter();
            r = new Random();

            metrica = new Dictionary<string, double>();
            metrica.Add("mm", 1);
            metrica.Add("cm", 10);
            metrica.Add("dm", 100);
            metrica.Add("m",  1000);
            metrica.Add("km", 1000000);
            metrica.Add("mile", 1609344);
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Some utilies", "About");
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Counter counter;

        private void btnPlus_Click(object sender, EventArgs e)
        {
            counter.Plus();
            lblCounter.Text = counter.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            counter.Minus();
            lblCounter.Text = counter.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            counter.Reset();
            lblCounter.Text = counter.ToString();
        }

        Random r;

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int number = r.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
            lblNumber.Text = number.ToString();

            int i = 0;
            if (cbRandom.Checked){
                while (tbRandom.Text.IndexOf(number.ToString()) != -1)
                {
                    number = r.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
                    i++;
                    if (i > Convert.ToInt32(numericUpDown2.Value) - Convert.ToInt32(numericUpDown1.Value))
                        break;
                }
                if (i <= Convert.ToInt32(numericUpDown2.Value) - Convert.ToInt32(numericUpDown1.Value))
                    tbRandom.AppendText(number + "\n");
            }
            else
                tbRandom.AppendText(number + "\n");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbRandom.Text);
        }

        private void tsmiInsertDate_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortDateString() + "\n");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortTimeString() + "\n");
        }

        private void tsmiInsertDataAndTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortDateString() + "\n" + DateTime.Now.ToShortTimeString() + "\n");
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNotepad.SaveFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Troubles with saving", "Error");
            }
        }

        void LoadNotepad()
        {
            try
            {
                rtbNotepad.LoadFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Troubles with loading", "Error");
            }
        }

        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            LoadNotepad();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadNotepad();
            clbPassword.SetItemChecked(0, true);
        }

        char[] specialSymb = new char []{ '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '?' };

        private void btnCreatePassword_Click(object sender, EventArgs e)
        {
            if (clbPassword.CheckedItems.Count == 0) return;

            string password = "";
            for(int i = 0; i < nudPassLength.Value; i++)
            {
                int n = r.Next(0, clbPassword.CheckedItems.Count);
                string s = clbPassword.CheckedItems[n].ToString();

                switch(s)
                {
                    case "Digits": password += r.Next(10);
                        break;
                    case "Capital letters": password += Convert.ToChar(r.Next(65, 88));
                        break;
                    case "String letters": password += Convert.ToChar(r.Next(97, 122));
                        break;
                    case "Special symbols": password += specialSymb[r.Next(specialSymb.Length)];
                        break;
                }
            }//for

            tbPassword.Text = password;
            Clipboard.SetText(password);
        }
        
        private void btnConvert_Click(object sender, EventArgs e)
        {
            double m1 = metrica[cbFrom.Text];
            double m2 = metrica[cbTo.Text];
            double n = Convert.ToDouble(tbFrom.Text);
            tbTo.Text = (n * m1 / m2).ToString();
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string tmp = cbFrom.Text;
            cbFrom.Text = cbTo.Text;
            cbTo.Text = tmp;
        }

        private void cbMetrica_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetrica.Text)
            {
                case "length":

                    metrica.Clear();
                    metrica.Add("mm", 1);
                    metrica.Add("cm", 10);
                    metrica.Add("dm", 100);
                    metrica.Add("m", 1000);
                    metrica.Add("km", 1000000);
                    metrica.Add("mile", 1609344);

                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("mm");
                    cbFrom.Items.Add("dm");
                    cbFrom.Items.Add("cm");
                    cbFrom.Items.Add("m");
                    cbFrom.Items.Add("km");
                    cbFrom.Items.Add("mile");

                    cbTo.Items.Clear();
                    cbTo.Items.Add("mm");
                    cbTo.Items.Add("dm");
                    cbTo.Items.Add("cm");
                    cbTo.Items.Add("m");
                    cbTo.Items.Add("km");
                    cbTo.Items.Add("mile");

                    cbFrom.Text = "mm";
                    cbTo.Text = "mm";

                    break;

                case "weight":

                    metrica.Clear();
                    metrica.Add("g", 1);
                    metrica.Add("kg", 1000);
                    metrica.Add("t", 1000000);
                    metrica.Add("lb", 453.6);
                    metrica.Add("oz", 283);

                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("g");
                    cbFrom.Items.Add("kg");
                    cbFrom.Items.Add("t");
                    cbFrom.Items.Add("lb");
                    cbFrom.Items.Add("oz");

                    cbTo.Items.Clear();
                    cbTo.Items.Add("g");
                    cbTo.Items.Add("kg");
                    cbTo.Items.Add("t");
                    cbTo.Items.Add("lb");
                    cbTo.Items.Add("oz");

                    cbFrom.Text = "g";
                    cbTo.Text = "g";

                    break;

                default:
                    break;
            }
        }
    }
}
