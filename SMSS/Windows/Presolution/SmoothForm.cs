using System;
using System.Threading;
using System.Windows.Forms;
using SMSS.Data;
using SMSS.Function.PreSolve;

namespace SMSS.Windows
{
    public partial class SmoothForm : Form
    {
        string foldname = "";
        string newfoldname = "";

        public SmoothForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread newthread1 = new Thread(new ThreadStart(smooth)); AuxiliaryFunc.percent = 0;
            Thread newthread2 = new Thread(new ThreadStart(RunTask));
            newthread1.Start();
            newthread2.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foldname = FileBrowser.GetFolder();
            textBox1.Text = foldname;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newfoldname = FileBrowser.GetFolder();
            textBox2.Text = newfoldname;
        }

        private void smooth()
        {
            Presolution pre = new Presolution();
            AuxiliaryFunc.percent = 0;
            pre.DoPresolve(foldname, newfoldname, "S11");
            AuxiliaryFunc.percent = 100; MessageBox.Show("平滑计算完成！");
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
            while (AuxiliaryFunc.percent < 100)
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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
