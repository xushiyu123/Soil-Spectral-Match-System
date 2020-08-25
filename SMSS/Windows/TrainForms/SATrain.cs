using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using RDotNet;
using SMSS.Data;
using SMSS.Windows.Windows_Refresh;
using SMSS.Function.match;

namespace SMSS.Windows
{
    public partial class SATrain : Form
    {
        List<string> TestList = new List<string>();
        List<string> TrainList = new List<string>();
        int times = 500;
        double rate = 0.75;
        int level = 2;

        public SATrain()
        {
            InitializeComponent();
            comboBox4.SelectedIndex = 2;
            SampleList(Config.TestFolder, checkedListBox1); SampleList(Config.TrainFolder, checkedListBox2);
            label5.Text = "共" + checkedListBox1.Items.Count.ToString() + "组";
            label6.Text = "共" + checkedListBox2.Items.Count.ToString() + "组";
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Thread AllocateThread = new Thread(new ThreadStart(Allocate));
            Thread processthread = new Thread(new ThreadStart(RunTask));
            AllocateThread.Start();
            processthread.Start();
        }

        void Allocate()
        {
            FileBrowser fb = new FileBrowser();
            fb.DeleteFiles(Config.TestFolder);//清空文件夹
            fb.DeleteFiles(Config.TrainFolder);
            fb.DeleteFiles(Config.MatrixFolder);
            OutputData od = new OutputData();
            od.WriteR(Config.SamplingR, times, rate);
            AuxiliaryFunc.percent = 10;
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Initialize();
            engine.Evaluate("source(\"" + Config.SamplingR.Replace("\\", "/") + "\")");
            MessageBox.Show("测试集分配完成!");
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
                timer1.Stop();
            else
            {
                FileBrowser fb = new FileBrowser();
                AuxiliaryFunc.percent = 10 + (Convert.ToDouble(fb.FilesCount(Config.TrainFolder)) / Convert.ToDouble(times) * 100);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            AuxiliaryFunc.percent = 10;
            AuxiliaryFunc af = new AuxiliaryFunc();
            af.FileList(out TestList, Config.TestFolder, checkedListBox1);
            af.FileList(out TrainList, Config.TrainFolder, checkedListBox2);            
            Thread newthread = new Thread(new ThreadStart(Function));
            Thread processthread = new Thread(new ThreadStart(RunTask));
            newthread.Start();
            processthread.Start();
        }

        void Function()
        {
            MatchAccuracy ma = new MatchAccuracy();
            ma.GetAnswer(TestList, TrainList, level, 0);
            changeGrid1(ma.BriefAccu);
            MessageBox.Show("计算完成");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Config.MatrixFolder);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox1, checkedListBox1);
            GetView.AllSelect(checkBox1, checkedListBox2);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = comboBox3.SelectedIndex;
        }
    }
}
