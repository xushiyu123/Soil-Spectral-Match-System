using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using RDotNet;
using SMSS.Data;
using SMSS.Windows.Windows_Refresh;

namespace SMSS.Windows
{
    public partial class WaveCharTrain : Form
    {
        List<string> TestList = new List<string>();
        List<string> TrainList = new List<string>();
        int times = 500;
        double rate = 0.75;
        int level = 2;
        int k = 10;

        public WaveCharTrain()
        {
            InitializeComponent();
            comboBox4.SelectedIndex = 2;
            SampleList(Config.TestFolder, checkedListBox1); SampleList(Config.TrainFolder, checkedListBox2);
            label5.Text = "共" + checkedListBox1.Items.Count.ToString() + "组";
            label6.Text = "共" + checkedListBox2.Items.Count.ToString() + "组";
            timer1.Start(); ;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = comboBox4.SelectedText;
            if (s.Length > 0)
            {
                s = s.Substring(0, s.Length - 1);
                rate = Convert.ToDouble(s) / 100;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AuxiliaryFunc af = new AuxiliaryFunc();
            if (af.TextIsNum(textBox1.Text))
            {
                times = Convert.ToInt32(textBox1.Text);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            AuxiliaryFunc af = new AuxiliaryFunc();
            if (af.TextIsNum(textBox2.Text))
            {
                k = Convert.ToInt32(textBox2.Text);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox1, checkedListBox1);
            GetView.AllSelect(checkBox1, checkedListBox2);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = comboBox3.SelectedIndex;
        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    checkedListBox2.SetItemChecked(i, true);
                else
                    checkedListBox2.SetItemChecked(i, false);
            }
        }

        private void checkedListBox2_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                    checkedListBox1.SetItemChecked(i, true);
                else
                    checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Config.MatrixFolder);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 进度条线程
        /// </summary>
        /// <param name="seconds"></param>
        /// 进程记录
        /// </summary>
        delegate void ProgressDelegate(int total, int current);
        delegate void RunTaskDelegate();
        void RunTask()
        {
            while (AuxiliaryFunc.percent <= 100)
            {
                Thread.Sleep(100);
                ShowProgress(100, Convert.ToInt32(AuxiliaryFunc.percent));
            }
        }
        /// <summary>
        /// 异步控制进度条显示
        /// </summary>
        /// <param name="total"></param>
        /// <param name="current"></param>
        void ShowProgress(int total, int current)
        {
            if (progressBar1.InvokeRequired)
            {
                ProgressDelegate Progress = new ProgressDelegate(ShowProgress);
                progressBar1.BeginInvoke(Progress, new object[] { total, current });
            }
            else
            {
                progressBar1.Maximum = total;
                progressBar1.Value = current;
            }
        }

        delegate void GridDelegate(DataTable dt);
        delegate void ChangeGrid();
        void changeGrid1(DataTable dt)
        {
            if (dataGridView2.InvokeRequired)
            {
                GridDelegate grid = new GridDelegate(changeGrid1);
                dataGridView2.BeginInvoke(grid, new object[] { dt });
            }
            else
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = dt;
            }
        }

        bool SampleList(string fold, CheckedListBox clb)
        {
            clb.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(fold);
            foreach (FileInfo file in di.GetFiles())
                clb.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            return true;
        }
    }
}
