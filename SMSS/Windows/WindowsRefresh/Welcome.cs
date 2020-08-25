using System;
using System.Threading;
using System.Windows.Forms;

using SMSS.Data;

namespace SMSS.Windows
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
            Thread newthread = new Thread(new ThreadStart(RunTask));
            newthread.Start();
            this.timer1.Start();
            this.timer1.Interval = 3;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 500)
                this.Close();
            else if (progressBar1.Value >= 100 && progressBar1.Value < 300)
                label1.Text = "正在检查土壤样本类型数据……";
            else if (progressBar1.Value >= 300 && progressBar1.Value < 500)
                label1.Text = "正在初始化界面……";
        }

        delegate void ProgressDelegate(int total, int current);
        delegate void RunTaskDelegate(object seconds);
        void RunTask()
        {
            for (int i = 0; i < 500; i++)
            {
                Thread.Sleep(3);
                ShowProgress(500, (i + 1));
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

        private void Welcome_Load(object sender, EventArgs e)
        {
            
        }       

        private void Welcome_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timer1.Stop();
        }
    }
}
