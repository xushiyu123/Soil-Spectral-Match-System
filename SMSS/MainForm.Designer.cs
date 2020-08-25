namespace SMSS
{
    partial class MainForm
    {

        #region Windows 窗体设计器生成的代码


        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试集数据读取ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 说明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem 工具栏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 状态栏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 目录树ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示系统界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出系统ToolStripMenuItem1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("测试集合");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("样本集合");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试集数据读取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.退出系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具栏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.状态栏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.目录树ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.归一化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一阶导ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.除噪平滑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.可视化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直方图显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.光谱曲线显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.光谱角匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直方图匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.波形相似度匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.方法训练ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.光谱角匹配ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.直方图匹配ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.峰谷特征值匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.波形相似度匹配ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平滑数据ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.一阶导数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.归一化数据ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.更改类型数据路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改训练集数据路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.更改工作空间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel18 = new System.Windows.Forms.ToolStripLabel();
            this.平滑 = new System.Windows.Forms.ToolStripButton();
            this.归一化 = new System.Windows.Forms.ToolStripButton();
            this.一阶导 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.波形相似度 = new System.Windows.Forms.ToolStripButton();
            this.光谱角 = new System.Windows.Forms.ToolStripButton();
            this.直方图 = new System.Windows.Forms.ToolStripButton();
            this.峰谷特征值法 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示系统界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最小化到托盘ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示图标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏图标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.预处理ToolStripMenuItem,
            this.可视化ToolStripMenuItem,
            this.匹配ToolStripMenuItem,
            this.方法训练ToolStripMenuItem,
            this.配置ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(778, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.测试集数据读取ToolStripMenuItem,
            this.toolStripSeparator3,
            this.退出系统ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 测试集数据读取ToolStripMenuItem
            // 
            this.测试集数据读取ToolStripMenuItem.Name = "测试集数据读取ToolStripMenuItem";
            this.测试集数据读取ToolStripMenuItem.Size = new System.Drawing.Size(223, 24);
            this.测试集数据读取ToolStripMenuItem.Text = "数据读取     Ctrl+O";
            this.测试集数据读取ToolStripMenuItem.Click += new System.EventHandler(this.数据读取ToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(220, 6);
            // 
            // 退出系统ToolStripMenuItem
            // 
            this.退出系统ToolStripMenuItem.Name = "退出系统ToolStripMenuItem";
            this.退出系统ToolStripMenuItem.Size = new System.Drawing.Size(223, 24);
            this.退出系统ToolStripMenuItem.Text = "退出系统                ESC";
            this.退出系统ToolStripMenuItem.Click += new System.EventHandler(this.退出系统ToolStripMenuItem_Click);
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具栏ToolStripMenuItem,
            this.状态栏ToolStripMenuItem,
            this.目录树ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.视图ToolStripMenuItem.Text = "视图";
            // 
            // 工具栏ToolStripMenuItem
            // 
            this.工具栏ToolStripMenuItem.Checked = true;
            this.工具栏ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.工具栏ToolStripMenuItem.Name = "工具栏ToolStripMenuItem";
            this.工具栏ToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.工具栏ToolStripMenuItem.Text = "工具栏           Alt+Z";
            this.工具栏ToolStripMenuItem.Click += new System.EventHandler(this.工具栏ToolStripMenuItem_Click);
            // 
            // 状态栏ToolStripMenuItem
            // 
            this.状态栏ToolStripMenuItem.Checked = true;
            this.状态栏ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.状态栏ToolStripMenuItem.Name = "状态栏ToolStripMenuItem";
            this.状态栏ToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.状态栏ToolStripMenuItem.Text = "状态栏           Alt+X";
            this.状态栏ToolStripMenuItem.Click += new System.EventHandler(this.状态栏ToolStripMenuItem_Click);
            // 
            // 目录树ToolStripMenuItem
            // 
            this.目录树ToolStripMenuItem.Checked = true;
            this.目录树ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.目录树ToolStripMenuItem.Name = "目录树ToolStripMenuItem";
            this.目录树ToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.目录树ToolStripMenuItem.Text = "数据树           Alt+C";
            this.目录树ToolStripMenuItem.Click += new System.EventHandler(this.目录树ToolStripMenuItem_Click);
            // 
            // 预处理ToolStripMenuItem
            // 
            this.预处理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.归一化ToolStripMenuItem,
            this.一阶导ToolStripMenuItem,
            this.除噪平滑ToolStripMenuItem});
            this.预处理ToolStripMenuItem.Name = "预处理ToolStripMenuItem";
            this.预处理ToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.预处理ToolStripMenuItem.Text = "数据处理";
            // 
            // 归一化ToolStripMenuItem
            // 
            this.归一化ToolStripMenuItem.Name = "归一化ToolStripMenuItem";
            this.归一化ToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.归一化ToolStripMenuItem.Text = "归一化          Ctrl+G";
            this.归一化ToolStripMenuItem.Click += new System.EventHandler(this.归一化ToolStripMenuItem_Click_1);
            // 
            // 一阶导ToolStripMenuItem
            // 
            this.一阶导ToolStripMenuItem.Name = "一阶导ToolStripMenuItem";
            this.一阶导ToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.一阶导ToolStripMenuItem.Text = "一阶导          Ctrl+H";
            this.一阶导ToolStripMenuItem.Click += new System.EventHandler(this.一阶导ToolStripMenuItem_Click_1);
            // 
            // 除噪平滑ToolStripMenuItem
            // 
            this.除噪平滑ToolStripMenuItem.Name = "除噪平滑ToolStripMenuItem";
            this.除噪平滑ToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.除噪平滑ToolStripMenuItem.Text = "除噪平滑       Ctrl+J";
            this.除噪平滑ToolStripMenuItem.Click += new System.EventHandler(this.除噪平滑ToolStripMenuItem_Click);
            // 
            // 可视化ToolStripMenuItem
            // 
            this.可视化ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直方图显示ToolStripMenuItem,
            this.光谱曲线显示ToolStripMenuItem});
            this.可视化ToolStripMenuItem.Name = "可视化ToolStripMenuItem";
            this.可视化ToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.可视化ToolStripMenuItem.Text = "可视化";
            // 
            // 直方图显示ToolStripMenuItem
            // 
            this.直方图显示ToolStripMenuItem.Name = "直方图显示ToolStripMenuItem";
            this.直方图显示ToolStripMenuItem.Size = new System.Drawing.Size(243, 24);
            this.直方图显示ToolStripMenuItem.Text = "直方图显示            Ctrl+M";
            this.直方图显示ToolStripMenuItem.Click += new System.EventHandler(this.直方图显示ToolStripMenuItem_Click);
            // 
            // 光谱曲线显示ToolStripMenuItem
            // 
            this.光谱曲线显示ToolStripMenuItem.Name = "光谱曲线显示ToolStripMenuItem";
            this.光谱曲线显示ToolStripMenuItem.Size = new System.Drawing.Size(243, 24);
            this.光谱曲线显示ToolStripMenuItem.Text = "光谱曲线显示         Ctrl+N";
            this.光谱曲线显示ToolStripMenuItem.Click += new System.EventHandler(this.光谱曲线显示ToolStripMenuItem_Click);
            // 
            // 匹配ToolStripMenuItem
            // 
            this.匹配ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.光谱角匹配ToolStripMenuItem,
            this.直方图匹配ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.波形相似度匹配ToolStripMenuItem});
            this.匹配ToolStripMenuItem.Name = "匹配ToolStripMenuItem";
            this.匹配ToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.匹配ToolStripMenuItem.Text = "匹配识别";
            // 
            // 光谱角匹配ToolStripMenuItem
            // 
            this.光谱角匹配ToolStripMenuItem.Name = "光谱角匹配ToolStripMenuItem";
            this.光谱角匹配ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.光谱角匹配ToolStripMenuItem.Text = "光谱角匹配";
            this.光谱角匹配ToolStripMenuItem.Click += new System.EventHandler(this.光谱角匹配ToolStripMenuItem_Click);
            // 
            // 直方图匹配ToolStripMenuItem
            // 
            this.直方图匹配ToolStripMenuItem.Name = "直方图匹配ToolStripMenuItem";
            this.直方图匹配ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.直方图匹配ToolStripMenuItem.Text = "直方图匹配";
            this.直方图匹配ToolStripMenuItem.Click += new System.EventHandler(this.直方图匹配ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(176, 24);
            this.toolStripMenuItem1.Text = "峰谷特征匹配";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 波形相似度匹配ToolStripMenuItem
            // 
            this.波形相似度匹配ToolStripMenuItem.Name = "波形相似度匹配ToolStripMenuItem";
            this.波形相似度匹配ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.波形相似度匹配ToolStripMenuItem.Text = "波形相似度匹配";
            this.波形相似度匹配ToolStripMenuItem.Click += new System.EventHandler(this.波形相似度匹配ToolStripMenuItem_Click);
            // 
            // 方法训练ToolStripMenuItem
            // 
            this.方法训练ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.光谱角匹配ToolStripMenuItem1,
            this.直方图匹配ToolStripMenuItem1,
            this.峰谷特征值匹配ToolStripMenuItem,
            this.波形相似度匹配ToolStripMenuItem1});
            this.方法训练ToolStripMenuItem.Name = "方法训练ToolStripMenuItem";
            this.方法训练ToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.方法训练ToolStripMenuItem.Text = "方法训练";
            // 
            // 光谱角匹配ToolStripMenuItem1
            // 
            this.光谱角匹配ToolStripMenuItem1.Name = "光谱角匹配ToolStripMenuItem1";
            this.光谱角匹配ToolStripMenuItem1.Size = new System.Drawing.Size(176, 24);
            this.光谱角匹配ToolStripMenuItem1.Text = "光谱角匹配";
            this.光谱角匹配ToolStripMenuItem1.Click += new System.EventHandler(this.光谱角匹配ToolStripMenuItem1_Click);
            // 
            // 直方图匹配ToolStripMenuItem1
            // 
            this.直方图匹配ToolStripMenuItem1.Name = "直方图匹配ToolStripMenuItem1";
            this.直方图匹配ToolStripMenuItem1.Size = new System.Drawing.Size(176, 24);
            this.直方图匹配ToolStripMenuItem1.Text = "直方图匹配";
            this.直方图匹配ToolStripMenuItem1.Click += new System.EventHandler(this.直方图匹配ToolStripMenuItem1_Click);
            // 
            // 峰谷特征值匹配ToolStripMenuItem
            // 
            this.峰谷特征值匹配ToolStripMenuItem.Name = "峰谷特征值匹配ToolStripMenuItem";
            this.峰谷特征值匹配ToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
            this.峰谷特征值匹配ToolStripMenuItem.Text = "峰谷特征值匹配";
            this.峰谷特征值匹配ToolStripMenuItem.Click += new System.EventHandler(this.峰谷特征值匹配ToolStripMenuItem_Click);
            // 
            // 波形相似度匹配ToolStripMenuItem1
            // 
            this.波形相似度匹配ToolStripMenuItem1.Name = "波形相似度匹配ToolStripMenuItem1";
            this.波形相似度匹配ToolStripMenuItem1.Size = new System.Drawing.Size(176, 24);
            this.波形相似度匹配ToolStripMenuItem1.Text = "波形相似度匹配";
            this.波形相似度匹配ToolStripMenuItem1.Click += new System.EventHandler(this.波形相似度匹配ToolStripMenuItem1_Click);
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.配置ToolStripMenuItem.Text = "配置";
            this.配置ToolStripMenuItem.Click += new System.EventHandler(this.配置ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.说明ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 说明ToolStripMenuItem
            // 
            this.说明ToolStripMenuItem.Name = "说明ToolStripMenuItem";
            this.说明ToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.说明ToolStripMenuItem.Text = "说明       Ctrl+I";
            this.说明ToolStripMenuItem.Click += new System.EventHandler(this.说明ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(177, 24);
            this.关于ToolStripMenuItem.Text = "关于       Ctrl+U";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 平滑数据ToolStripMenuItem1
            // 
            this.平滑数据ToolStripMenuItem1.Name = "平滑数据ToolStripMenuItem1";
            this.平滑数据ToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // 一阶导数据ToolStripMenuItem
            // 
            this.一阶导数据ToolStripMenuItem.Name = "一阶导数据ToolStripMenuItem";
            this.一阶导数据ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 归一化数据ToolStripMenuItem1
            // 
            this.归一化数据ToolStripMenuItem1.Name = "归一化数据ToolStripMenuItem1";
            this.归一化数据ToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // 更改类型数据路径ToolStripMenuItem
            // 
            this.更改类型数据路径ToolStripMenuItem.Name = "更改类型数据路径ToolStripMenuItem";
            this.更改类型数据路径ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 更改训练集数据路径ToolStripMenuItem
            // 
            this.更改训练集数据路径ToolStripMenuItem.Name = "更改训练集数据路径ToolStripMenuItem";
            this.更改训练集数据路径ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(268, 6);
            // 
            // 更改工作空间ToolStripMenuItem
            // 
            this.更改工作空间ToolStripMenuItem.Name = "更改工作空间ToolStripMenuItem";
            this.更改工作空间ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripButton8,
            this.toolStripSeparator10,
            this.toolStripButton7,
            this.toolStripLabel18,
            this.平滑,
            this.归一化,
            this.一阶导,
            this.toolStripSeparator22,
            this.toolStripLabel1,
            this.波形相似度,
            this.光谱角,
            this.直方图,
            this.峰谷特征值法,
            this.toolStripSeparator23,
            this.toolStripButton4,
            this.toolStripButton6,
            this.toolStripButton5,
            this.toolStripSeparator9});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(778, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.VisibleChanged += new System.EventHandler(this.toolStrip1_VisibleChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(44, 42);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "曲线图";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(44, 42);
            this.toolStripButton8.Text = "toolStripButton8";
            this.toolStripButton8.ToolTipText = "直方图";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton7.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(44, 42);
            this.toolStripButton7.Text = "toolStripButton7";
            this.toolStripButton7.ToolTipText = "退出系统";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripLabel18
            // 
            this.toolStripLabel18.Name = "toolStripLabel18";
            this.toolStripLabel18.Size = new System.Drawing.Size(44, 42);
            this.toolStripLabel18.Text = "预处理";
            // 
            // 平滑
            // 
            this.平滑.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.平滑.Image = ((System.Drawing.Image)(resources.GetObject("平滑.Image")));
            this.平滑.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.平滑.Name = "平滑";
            this.平滑.Size = new System.Drawing.Size(44, 42);
            this.平滑.Text = "平滑";
            this.平滑.Click += new System.EventHandler(this.平滑_Click);
            // 
            // 归一化
            // 
            this.归一化.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.归一化.Image = ((System.Drawing.Image)(resources.GetObject("归一化.Image")));
            this.归一化.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.归一化.Name = "归一化";
            this.归一化.Size = new System.Drawing.Size(44, 42);
            this.归一化.Text = "归一化";
            this.归一化.Click += new System.EventHandler(this.归一化_Click);
            // 
            // 一阶导
            // 
            this.一阶导.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.一阶导.Image = ((System.Drawing.Image)(resources.GetObject("一阶导.Image")));
            this.一阶导.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.一阶导.Name = "一阶导";
            this.一阶导.Size = new System.Drawing.Size(44, 42);
            this.一阶导.Text = "一阶导";
            this.一阶导.Click += new System.EventHandler(this.一阶导_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.White;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 42);
            this.toolStripLabel1.Text = "匹配";
            // 
            // 波形相似度
            // 
            this.波形相似度.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.波形相似度.Image = ((System.Drawing.Image)(resources.GetObject("波形相似度.Image")));
            this.波形相似度.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.波形相似度.Name = "波形相似度";
            this.波形相似度.Size = new System.Drawing.Size(44, 42);
            this.波形相似度.Text = "波形相似度匹配";
            this.波形相似度.Click += new System.EventHandler(this.波形相似度_Click);
            // 
            // 光谱角
            // 
            this.光谱角.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.光谱角.Image = ((System.Drawing.Image)(resources.GetObject("光谱角.Image")));
            this.光谱角.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.光谱角.Name = "光谱角";
            this.光谱角.Size = new System.Drawing.Size(44, 42);
            this.光谱角.Text = "光谱角匹配";
            this.光谱角.Click += new System.EventHandler(this.光谱角_Click);
            // 
            // 直方图
            // 
            this.直方图.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.直方图.Image = ((System.Drawing.Image)(resources.GetObject("直方图.Image")));
            this.直方图.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.直方图.Name = "直方图";
            this.直方图.Size = new System.Drawing.Size(44, 42);
            this.直方图.Text = "直方图匹配";
            this.直方图.Click += new System.EventHandler(this.直方图_Click);
            // 
            // 峰谷特征值法
            // 
            this.峰谷特征值法.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.峰谷特征值法.Image = ((System.Drawing.Image)(resources.GetObject("峰谷特征值法.Image")));
            this.峰谷特征值法.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.峰谷特征值法.Name = "峰谷特征值法";
            this.峰谷特征值法.Size = new System.Drawing.Size(44, 42);
            this.峰谷特征值法.Text = "峰谷特征值匹配";
            this.峰谷特征值法.Click += new System.EventHandler(this.峰谷特征值法_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(44, 42);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "邮件";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(44, 42);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.ToolTipText = "清空容器";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(44, 42);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "帮助";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 45);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 501);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(778, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.VisibleChanged += new System.EventHandler(this.statusStrip1_VisibleChanged);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(107, 17);
            this.toolStripStatusLabel1.Text = "2018/1/29 星期一";
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 76);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.NodeFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode1.Text = "测试集合";
            treeNode2.Name = "节点1";
            treeNode2.NodeFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode2.Text = "样本集合";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeView1.SelectedImageIndex = 1;
            this.treeView1.Size = new System.Drawing.Size(167, 422);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.SizeChanged += new System.EventHandler(this.treeView1_SizeChanged);
            this.treeView1.VisibleChanged += new System.EventHandler(this.treeView1_VisibleChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "图标1.JPG");
            this.imageList1.Images.SetKeyName(1, "图标2.JPG");
            this.imageList1.Images.SetKeyName(2, "图层.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(173, 76);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(605, 422);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SizeChanged += new System.EventHandler(this.tabControl1_SizeChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip2);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(597, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "土壤光谱";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.toolStripButton2,
            this.toolStripButton10,
            this.toolStripLabel2,
            this.toolStripLabel3,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripSeparator12,
            this.toolStripLabel4,
            this.toolStripTextBox1,
            this.toolStripLabel5,
            this.toolStripSeparator13,
            this.toolStripSeparator15,
            this.toolStripLabel8,
            this.toolStripLabel7,
            this.toolStripLabel6,
            this.toolStripSeparator14});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(591, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "跳转至首页";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton10.Text = "上一页";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(15, 24);
            this.toolStripLabel2.Text = "1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(20, 24);
            this.toolStripLabel3.Text = "/1";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton11.Text = "下一页";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton12.Text = "跳转至尾页";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(56, 24);
            this.toolStripLabel4.Text = "跳转至第";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(20, 27);
            this.toolStripTextBox1.Text = "1";
            this.toolStripTextBox1.ToolTipText = "跳转页码";
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(20, 24);
            this.toolStripLabel5.Text = "页";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(20, 24);
            this.toolStripLabel8.Text = "页";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(15, 24);
            this.toolStripLabel7.Text = "1";
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(20, 24);
            this.toolStripLabel6.Text = "共";
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 27);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(0, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(597, 359);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(597, 396);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "剖面信息";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.Location = new System.Drawing.Point(4, 4);
            this.dataGridView2.Name = "dataGridView2";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(574, 389);
            this.dataGridView2.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(525, 504);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(241, 19);
            this.progressBar1.TabIndex = 5;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "土壤光谱匹配系统";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示系统界面ToolStripMenuItem,
            this.最小化到托盘ToolStripMenuItem,
            this.显示图标ToolStripMenuItem,
            this.隐藏图标ToolStripMenuItem,
            this.退出系统ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 114);
            // 
            // 显示系统界面ToolStripMenuItem
            // 
            this.显示系统界面ToolStripMenuItem.Name = "显示系统界面ToolStripMenuItem";
            this.显示系统界面ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示系统界面ToolStripMenuItem.Text = "显示系统界面";
            this.显示系统界面ToolStripMenuItem.Click += new System.EventHandler(this.显示系统界面ToolStripMenuItem_Click);
            // 
            // 最小化到托盘ToolStripMenuItem
            // 
            this.最小化到托盘ToolStripMenuItem.Name = "最小化到托盘ToolStripMenuItem";
            this.最小化到托盘ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.最小化到托盘ToolStripMenuItem.Text = "最小化到托盘";
            this.最小化到托盘ToolStripMenuItem.Click += new System.EventHandler(this.最小化到托盘ToolStripMenuItem_Click);
            // 
            // 显示图标ToolStripMenuItem
            // 
            this.显示图标ToolStripMenuItem.Name = "显示图标ToolStripMenuItem";
            this.显示图标ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示图标ToolStripMenuItem.Text = "显示图标";
            this.显示图标ToolStripMenuItem.Click += new System.EventHandler(this.显示图标ToolStripMenuItem_Click);
            // 
            // 隐藏图标ToolStripMenuItem
            // 
            this.隐藏图标ToolStripMenuItem.Name = "隐藏图标ToolStripMenuItem";
            this.隐藏图标ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.隐藏图标ToolStripMenuItem.Text = "隐藏图标";
            this.隐藏图标ToolStripMenuItem.Click += new System.EventHandler(this.隐藏图标ToolStripMenuItem_Click);
            // 
            // 退出系统ToolStripMenuItem1
            // 
            this.退出系统ToolStripMenuItem1.Name = "退出系统ToolStripMenuItem1";
            this.退出系统ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.退出系统ToolStripMenuItem1.Text = "退出系统";
            this.退出系统ToolStripMenuItem1.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(778, 523);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基于光谱的土壤剖面识别系统 V1.0";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStripMenuItem 最小化到托盘ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 隐藏图标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示图标ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton 平滑;
        private System.Windows.Forms.ToolStripButton 归一化;
        private System.Windows.Forms.ToolStripButton 一阶导;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripButton 波形相似度;
        private System.Windows.Forms.ToolStripButton 光谱角;
        private System.Windows.Forms.ToolStripButton 直方图;
        private System.Windows.Forms.ToolStripButton 峰谷特征值法;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改类型数据路径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改训练集数据路径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem 更改工作空间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel18;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem 平滑数据ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 一阶导数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 归一化数据ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 匹配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 光谱角匹配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方图匹配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 波形相似度匹配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 可视化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方图显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 光谱曲线显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 归一化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一阶导ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 除噪平滑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStripMenuItem 方法训练ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 光谱角匹配ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 直方图匹配ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 峰谷特征值匹配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 波形相似度匹配ToolStripMenuItem1;
    }
}

