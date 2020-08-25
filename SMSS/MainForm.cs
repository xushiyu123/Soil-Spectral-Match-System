using SMSS.Data;
using SMSS.Match_Form;
using SMSS.Windows;
using SMSS.Windows.Match_Form;
using SMSS.Windows.Windows_Refresh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SMSS
{
    public partial class MainForm : Form
    {
        #region 窗体初始化
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            timer1.Interval = 5000; timer1.Start();
            Welcome w = new Welcome(); w.ShowDialog();
            InputData id = new InputData();
            id.ReadConfigFile(Config.configflie);
            Initialize i = new Initialize();
            i.MainFormInitial();           
            ViewInitialization();          
        }

        private void ViewInitialization()
        {
            treeView1.Nodes[0].ImageIndex = 2; treeView1.Nodes[1].ImageIndex = 2;
            AddTree.AddNodes(MainFormContainers.ProfileID, treeView1, 1);
            AddTree.AddNodes(MainFormContainers.ukProfileID, treeView1, 0);
            GetView.GetGridView(MainFormContainers.SoilType, dataGridView2);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        #endregion

        #region 菜单栏
        private void 数据读取ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FileBrowser fb = new FileBrowser();
            fb.MoveFolder(FileBrowser.GetFolder(), Config.uksoilfolder);
            if (Config.uksoilfolder != "")
            {               
                Thread newthread = new Thread(new ThreadStart(readTestset));
                newthread.Start();
            }
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void 直方图匹配ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HistogramTrain ht = new HistogramTrain();
            ht.Show();
        }

        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPDF sp = new ShowPDF(AppDomain.CurrentDomain.BaseDirectory + "User book.pdf");
            sp.Show();
        }

        private void 光谱角匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpecAngleForm sf = new SpecAngleForm();
            sf.ShowDialog();
        }

        private void 波形相似度匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaveshapeForm form = new WaveshapeForm();
            form.ShowDialog();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            直方图显示ToolStripMenuItem_Click(sender, e);
        }

        private void 直方图匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistogramForm hf = new HistogramForm();
            hf.ShowDialog();
        }

        private void 光谱角匹配ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SATrain sat = new SATrain();
            sat.Show();
        }

        private void 波形相似度匹配ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WaveShapeTrain wst = new WaveShapeTrain();
            wst.Show();
        }

        private void 峰谷特征值匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WaveCharTrain wct = new WaveCharTrain();
            wct.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WavecharForm wf = new WavecharForm(MainFormContainers.Trainset, MainFormContainers.Testset);
            wf.Show();
        }

        private void 直方图显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHistogram sr = new ShowHistogram();
            sr.Show();
        }

        private void 光谱曲线显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCurve sl = new ShowCurve();
            sl.Show();
        }

        private void 归一化ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NormalForm nf = new NormalForm();
            nf.Show();
        }

        private void 除噪平滑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SmoothForm sf = new SmoothForm();
            sf.Show();
        }

        private void 一阶导ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DeriveForm df = new DeriveForm();
            df.Show();
        }

        private void 工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            工具栏ToolStripMenuItem.Checked = !工具栏ToolStripMenuItem.Checked;
            toolStrip1.Visible = 工具栏ToolStripMenuItem.Checked;
        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            状态栏ToolStripMenuItem.Checked = !状态栏ToolStripMenuItem.Checked;
            statusStrip1.Visible = 状态栏ToolStripMenuItem.Checked;
            progressBar1.Visible = 状态栏ToolStripMenuItem.Checked;
        }

        private void 目录树ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            目录树ToolStripMenuItem.Checked = !目录树ToolStripMenuItem.Checked;
            treeView1.Visible = 目录树ToolStripMenuItem.Checked;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPDF sp = new ShowPDF(AppDomain.CurrentDomain.BaseDirectory + "about us.pdf");
            sp.Show();
        }
        #endregion

        #region 工具栏
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            string[] SheetName = { "训练集", "测试集", "剖面类型" };
            Thread newthread = new Thread(new ThreadStart(RunTask)); newthread.Start();
            OutputData.ExportExcel("", MainFormContainers.Trainset, MainFormContainers.Testset, MainFormContainers.SoilType, SheetName);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            光谱曲线显示ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Mail m = new Mail();
            m.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            说明ToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// 目录树线程函数
        /// </summary>
        /// <param name="name"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Level > 0)
            {
                Waiting w = new Waiting(); w.Show();
                InputData id = new InputData();
                if (treeView1.SelectedNode.Parent.Text == "测试集合")
                {
                    string path = Config.uksoilfolder + "\\" + treeView1.SelectedNode.Text + ".csv";
                    MainFormContainers.Spectral = id.InputCSV(path);
                    Thread newthread = new Thread(new ParameterizedThreadStart(SelectTree1)); newthread.Start(treeView1.SelectedNode.Text);                                      
                    GetView.GetGridView(MainFormContainers.Spectral, dataGridView1);
                    tabControl1.SelectedTab = tabPage1;
                }
                else if (treeView1.SelectedNode.Parent.Text == "样本集合")
                {
                    string path = Config.soilfolder + "\\" + treeView1.SelectedNode.Text + ".csv";
                    MainFormContainers.Spectral = id.InputCSV(path);
                    Thread newthread = new Thread(new ParameterizedThreadStart(SelectTree2)); newthread.Start(treeView1.SelectedNode.Text);                                      
                    GetView.GetGridView(MainFormContainers.Spectral, dataGridView1);
                    tabControl1.SelectedTab = tabPage1;
                }
            }
        }

        void SelectTree1(object name)
        {
            string s = name.ToString();
            changeGrid1(MainFormContainers.Spectral);
            Waiting.IsWait = false;
        }

        void SelectTree2(object name)
        {
            changeGrid1(MainFormContainers.Spectral);
            Waiting.IsWait = false;
        }

        /// <summary>
        /// 清空目录树、表格，并且清空测试集数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            FreeContainer.FreeTreeNode(treeView1, 0);
            FreeContainer.FreeTable(MainFormContainers.Testset);
            FreeContainer.FreeGrid(dataGridView1);
            progressBar1.Value = 0;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            退出系统ToolStripMenuItem_Click(sender, e);
        }

        private void 归一化_Click(object sender, EventArgs e)
        {
            归一化ToolStripMenuItem_Click_1(sender, e);
        }

        private void 一阶导_Click(object sender, EventArgs e)
        {
            一阶导ToolStripMenuItem_Click_1(sender, e);
        }

        private void 波形相似度_Click(object sender, EventArgs e)
        {
            波形相似度匹配ToolStripMenuItem_Click(sender, e);
        }

        private void 光谱角_Click(object sender, EventArgs e)
        {
            光谱角匹配ToolStripMenuItem_Click(sender, e);
        }

        private void 直方图_Click(object sender, EventArgs e)
        {
            直方图匹配ToolStripMenuItem_Click(sender, e);
        }

        private void 峰谷特征值法_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1_Click(sender, e);
        }

        private void 平滑_Click(object sender, EventArgs e)
        {
            除噪平滑ToolStripMenuItem_Click(sender, e);
        }

        private void 配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm cf = new ConfigForm();
            cf.Show();
        }
        #endregion

        #region 线程内函数

        private void readTestset()
        {
            InputData id = new InputData();          
            if (MainFormContainers.ukProfileID.Count > 0)//清空样本编号列表
                MainFormContainers.ukProfileID.Clear();
            MainFormContainers.ukProfileID = id.ProfileList(Config.uksoilfolder);
            changeTree(MainFormContainers.ukProfileID, 0); MessageBox.Show("测试集合数据读取完成!");
        }

        private void readType()
        {
            FreeContainer.FreeTable(MainFormContainers.SoilType);
            InputData id = new InputData(); MainFormContainers.SoilType = id.InputCSV(Config.soilhabitat);
            changeGrid2(MainFormContainers.SoilType); MessageBox.Show("类型数据更换完成！");
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

        delegate void TreeDelegate(List<string> l, int index);

        void changeTree(List<string> l, int index)
        {
            if (treeView1.InvokeRequired)
            {
                TreeDelegate tree = new TreeDelegate(changeTree);
                treeView1.BeginInvoke(tree, new object[] { l, index });
            }
            else
            {
                if (treeView1.Nodes[index].Nodes.Count > 0)
                {
                    treeView1.Nodes[index].Nodes.Clear();
                }
                for (int i = 0; i < l.Count; i++)
                {
                    treeView1.Nodes[index].Nodes.Add(l[i]);
                    treeView1.Nodes[index].Nodes[i].ImageIndex = 1;
                }
                treeView1.Nodes[index].Expand();
            }
        }

        delegate void GridDelegate(DataTable dt);
        delegate void ChangeGrid();

        void changeGrid1(DataTable dt)
        {
            if (dataGridView1.InvokeRequired)
            {
                GridDelegate grid = new GridDelegate(changeGrid1);
                dataGridView1.BeginInvoke(grid, new object[] { dt });
            }
            else
            {
                GetView.GetGridView(dt, dataGridView1);
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
        #endregion

        #region 控件视图与大小
        /// <summary>
        /// 调整窗体大小变化时控件的大小与位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon1.Visible = true;
            }
            else
            {
                treeView1.Width = Convert.ToInt32(this.Width * 0.15);
                tabControl1.Width = Convert.ToInt32((this.Width - treeView1.Width) * 0.97);
                treeView1.Height = this.Height - menuStrip1.Height * Convert.ToInt32(menuStrip1.Visible) -
                    statusStrip1.Height * Convert.ToInt32(statusStrip1.Visible) - toolStrip1.Height * Convert.ToInt32(toolStrip1.Visible) - 45;
            }
        }
        private void treeView1_VisibleChanged(object sender, EventArgs e)
        {
            tabControl1.Width = this.Width - Convert.ToInt32(treeView1.Visible) * treeView1.Width - 10;
            tabControl1.Location = new Point((treeView1.Left + treeView1.Width) * Convert.ToInt32(treeView1.Visible), treeView1.Top);
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Width = Convert.ToInt32(tabControl1.Width - 15); dataGridView1.Height = Convert.ToInt32(tabControl1.Height - toolStrip2.Height - 30);
            dataGridView2.Width = Convert.ToInt32(tabControl1.Width - 15); dataGridView2.Height = Convert.ToInt32(tabControl1.Height - 30);
        }

        private void toolStrip1_VisibleChanged(object sender, EventArgs e)
        {
            treeView1.Location = new Point(0, toolStrip1.Top + toolStrip1.Height * Convert.ToInt32(toolStrip1.Visible) + 5);
            tabControl1.Location = new Point(treeView1.Left + treeView1.Width * Convert.ToInt32(treeView1.Visible), toolStrip1.Top + toolStrip1.Height * Convert.ToInt32(toolStrip1.Visible) + 5);
            treeView1.Height = this.Height - menuStrip1.Height * Convert.ToInt32(menuStrip1.Visible) -
                    statusStrip1.Height * Convert.ToInt32(statusStrip1.Visible) - toolStrip1.Height * Convert.ToInt32(toolStrip1.Visible) - 45;
            tabControl1.Height = treeView1.Height;
        }

        private void statusStrip1_VisibleChanged(object sender, EventArgs e)
        {
            treeView1.Height = this.Height - menuStrip1.Height * Convert.ToInt32(menuStrip1.Visible) -
                    statusStrip1.Height * Convert.ToInt32(statusStrip1.Visible) - toolStrip1.Height * Convert.ToInt32(toolStrip1.Visible) - 45;
            tabControl1.Height = treeView1.Height;
        }

        private void treeView1_SizeChanged(object sender, EventArgs e)
        {
            tabControl1.Height = treeView1.Height;
            tabControl1.Location = new Point((treeView1.Left + treeView1.Width) * Convert.ToInt32(treeView1.Visible), treeView1.Top);
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            toolStripLabel3.Text = "/" + GetView.MaxPage.ToString();
            toolStripLabel7.Text = GetView.MaxPage.ToString();
        }

        #endregion

        #region 定义托盘
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    显示系统界面ToolStripMenuItem.Enabled = false;
                    最小化到托盘ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    显示系统界面ToolStripMenuItem.Enabled = true;
                    最小化到托盘ToolStripMenuItem.Enabled = false;
                }

                if (this.ShowInTaskbar == true)
                {
                    隐藏图标ToolStripMenuItem.Enabled = true;
                    显示图标ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    隐藏图标ToolStripMenuItem.Enabled = false;
                    显示图标ToolStripMenuItem.Enabled = true;
                }
                this.notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void 显示系统界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            显示图标ToolStripMenuItem_Click(sender, e);
            this.WindowState = FormWindowState.Maximized;
        }

        private void 最小化到托盘ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            隐藏图标ToolStripMenuItem_Click(sender, e);
            this.WindowState = FormWindowState.Minimized;
        }

        private void 隐藏图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
        }

        private void 显示图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        #endregion

        #region 翻页工具条

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            GetView.Page = 1;
            toolStripLabel2.Text = "1";
            GetView.GetGridView(MainFormContainers.Spectral, dataGridView1);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(toolStripLabel2.Text) > 1)
            {
                int page = Convert.ToInt32(toolStripLabel2.Text);
                toolStripLabel2.Text = (page - 1).ToString(); toolStripTextBox1.Text = page.ToString();
                GetView.Page = page - 1;
                GetView.GetGridView(MainFormContainers.Spectral, dataGridView1);
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(toolStripLabel2.Text) < GetView.MaxPage)
            {
                int page = Convert.ToInt32(toolStripLabel2.Text);
                toolStripLabel2.Text = (page + 1).ToString(); toolStripTextBox1.Text = (page + 1).ToString();
                GetView.Page = page + 1;
                GetView.GetGridView(MainFormContainers.Spectral, dataGridView1);
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            GetView.Page = GetView.MaxPage;
            toolStripLabel2.Text = GetView.MaxPage.ToString();
            GetView.GetGridView(MainFormContainers.Spectral, dataGridView1);
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(toolStripTextBox1.Text) > 0 && Convert.ToInt32(toolStripTextBox1.Text) < GetView.MaxPage)
            {
                GetView.Page = Convert.ToInt32(toolStripTextBox1.Text);
                toolStripLabel2.Text = toolStripTextBox1.Text;
                GetView.GetGridView(MainFormContainers.Spectral, dataGridView1);
            }
            else
                MessageBox.Show("页码超出范围！");
        }
        #endregion

        #region 快捷键设置        

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.O) && e.Control)
                数据读取ToolStripMenuItem_Click_1(sender, e);
            if (e.KeyCode == Keys.Escape)
                退出系统ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.Z) && e.Alt)
                工具栏ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.X) && e.Alt)
                状态栏ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.C) && e.Alt)
                目录树ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.M) && e.Control)
                直方图显示ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.N) && e.Control)
                光谱曲线显示ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.G) && e.Control)
                归一化ToolStripMenuItem_Click_1(sender, e);
            if ((e.KeyCode == Keys.H) && e.Control)
                一阶导ToolStripMenuItem_Click_1(sender, e);
            if ((e.KeyCode == Keys.J) && e.Control)
                除噪平滑ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.U) && e.Control)
                说明ToolStripMenuItem_Click(sender, e);
            if ((e.KeyCode == Keys.I) && e.Control)
                说明ToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region 定时清理内存
        /// <summary>
        /// 每五秒清理一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            FreeContainer.ClearMemory();
        }

        #endregion

    }
}
