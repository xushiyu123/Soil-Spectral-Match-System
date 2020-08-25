using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using SMSS.Windows.Windows_Refresh;
using SMSS.Data;
using SMSS.Function.match;

namespace SMSS.Match_Form
{
    public partial class WaveshapeForm : Form
    {
        public static int groups = 10;
        public static int level = 2;
        public static List<string> ProfileList = new List<string>();
        public static List<string> ukProfileList = new List<string>();

        public WaveshapeForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 2;
            GetView.ListBox(MainFormContainers.ProfileID, checkedListBox2);
            GetView.ListBox(MainFormContainers.ukProfileID, checkedListBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FreeContainer.FreeGrid(dataGridView1);
            FreeContainer.FreeGrid(dataGridView2);
            level = Convert.ToInt32(comboBox1.SelectedIndex + 1);
            Thread newthread0 = new Thread(new ThreadStart(RunTask));
            Thread newthread = new Thread(new ThreadStart(match));
            newthread0.Start(); newthread.Start();
        }

        private void match()
        {
            AuxiliaryFunc.percent = 0;
            GetView.ProfileList(out ProfileList, checkedListBox2);
            GetView.ProfileList(out ukProfileList, checkedListBox1);
            MatchDis md = new MatchDis();
            md.MatchAnswer(ukProfileList, ProfileList, 1, level);
            changeGrid1(MatchDis.HMatrix); changeGrid2(MatchDis.HProfile);
            AuxiliaryFunc.percent = 100;
            MessageBox.Show("执行完毕");
        }

         /// <summary>
        /// 进度条线程
        /// </summary>
        /// <param name="seconds"></param>
        ///         /// <summary>
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
        /// <summary>
        ///更改表格视图 
        /// </summary>
        /// <param name="dt"></param>
        delegate void GridDelegate(DataTable dt);
        delegate void ChangeGrid();

        void changeGrid1(DataTable dt)
        {
            if (dataGridView2.InvokeRequired)
            {
                GridDelegate grid = new GridDelegate(changeGrid1);
                dataGridView1.BeginInvoke(grid, new object[] { dt });
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
        }

        void changeGrid2(DataTable dt)
        {
            if (dataGridView2.InvokeRequired)
            {
                GridDelegate grid = new GridDelegate(changeGrid2);
                dataGridView2.BeginInvoke(grid, new object[] { dt });
            }
            else
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AuxiliaryFunc.percent = 0;
            string[] SheetName = { "基础信息", "混淆矩阵", "剖面匹配结果" };
            Thread newthread = new Thread(new ThreadStart(RunTask)); newthread.Start();
            OutputData.ExportExcel("波形相似度匹配", Parameter.BriefInformation("波形相似度匹配"), MatchDis.HMatrix, MatchDis.HProfile, SheetName);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = comboBox1.SelectedIndex;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox1, checkedListBox1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox2, checkedListBox2);
        }
    }
}
