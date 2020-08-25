using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using SMSS.Data;
using SMSS.Windows.Windows_Refresh;
using SMSS.Function.match.Histogram;

namespace SMSS.Windows.Match_Form
{
    public partial class HistogramForm : Form
    {
        public static int groups = 10;
        public static int level = 2;
        public static int order = 0;
        public static List<string> ProfileList = new List<string>();
        public static List<string> ukProfileList = new List<string>();

        public HistogramForm()
        {
            InitializeComponent();
            GetView.ListBox(MainFormContainers.ProfileID, checkedListBox2);
            GetView.ListBox(MainFormContainers.ukProfileID, checkedListBox1);
            label5.Text = "共" + MainFormContainers.ukProfileID.Count.ToString() + "个";
            label6.Text = "共" + MainFormContainers.ProfileID.Count.ToString() + "个";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread newthread = new Thread(new ThreadStart(WriteHistogram));
            Thread process = new Thread(new ThreadStart(RunTask));
            newthread.Start();
            process.Start();
        }

        void WriteHistogram()
        {
            AuxiliaryFunc.percent = 0;
            HistCalculate hc = new HistCalculate();
            hc.HistCharacter(Config.soilfolder, Config.soilhistogram, groups);
            hc.HistCharacter(Config.uksoilfolder, Config.uksoilhistogram, groups);
            AuxiliaryFunc.percent = 100;
            MessageBox.Show("计算完成");
        }

        void Match()
        {
            GetView.ProfileList(out ProfileList, checkedListBox2);
            GetView.ProfileList(out ukProfileList, checkedListBox1);
            HistogramDis hd = new HistogramDis(); 
            hd.GetAnswer(ukProfileList, ProfileList, order, level);
            changeGrid1(HistogramDis.HMatrix);changeGrid2(HistogramDis.HProfile);
            AuxiliaryFunc.percent = 100;
            MessageBox.Show("执行完毕");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groups = Convert.ToInt32(comboBox1.Items[comboBox1.SelectedIndex]);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox1, checkedListBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AuxiliaryFunc.percent = 0;
            Thread newthread = new Thread(new ThreadStart(Match));
            Thread process = new Thread(new ThreadStart(RunTask));
            process.Start();
            newthread.Start();
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = comboBox3.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            order = comboBox2.SelectedIndex;
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

        private void button4_Click(object sender, EventArgs e)
        {
            string[] sheets = { "实验信息","距离矩阵", "匹配结果"};
            OutputData.ExportExcel("实验结果",Parameter.BriefInformation("直方图匹配") ,HistogramDis.HMatrix, HistogramDis.HProfile, sheets);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox2, checkedListBox2);
        }
    }
}
