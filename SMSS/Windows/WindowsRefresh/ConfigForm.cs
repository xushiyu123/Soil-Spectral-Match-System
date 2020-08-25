using System;
using System.Windows.Forms;

using SMSS.Data;

namespace SMSS.Windows
{
    public partial class ConfigForm : Form
    {
        bool flag = false;
        public ConfigForm()
        {
            InitializeComponent();
            textBox1.Text = Config.workspace;
            textBox2.Text = Config.soilfolder;
            textBox4.Text = Config.uksoilfolder;
            textBox3.Text = Config.soilhabitat;
            textBox5.Text = Config.MinWavelen.ToString();
            textBox6.Text = Config.MaxWavelen.ToString();
            textBox7.Text = Config.SoilTrainFolder;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Config.workspace = FileBrowser.GetFolder();
            textBox1.Text = Config.workspace;
            flag = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                MessageBox.Show("配置信息已修改，请重启系统");
                OutputData od = new OutputData();
                od.SaveConfigFile(Config.configflie);
            }           
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.soilfolder = FileBrowser.GetFolder();
            textBox2.Text = Config.soilfolder;
            flag = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Config.uksoilfolder = FileBrowser.GetFolder();
            textBox4.Text = Config.uksoilfolder;
            flag = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Config.soilhabitat = FileBrowser.GetFile();
            textBox3.Text = Config.soilhabitat;
            flag = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            AuxiliaryFunc af1 = new AuxiliaryFunc();
            if (af1.TextIsNum(textBox5.Text))
            {
                Config.MinWavelen = Convert.ToInt32(textBox5.Text);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            AuxiliaryFunc af2 = new AuxiliaryFunc();
            if (af2.TextIsNum(textBox6.Text))
            {
                Config.MaxWavelen = Convert.ToInt32(textBox6.Text);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Config.SoilTrainFolder = FileBrowser.GetFolder();
            Config.TestFolder = Config.SoilTrainFolder + @"Test\";//测试文件夹
            Config.TrainFolder = Config.SoilTrainFolder + @"Train\";//样本文件夹
            Config.MatrixFolder = Config.SoilTrainFolder + @"Matrix\";//匹配结果矩阵文件夹
            textBox7.Text = Config.SoilTrainFolder;
            flag = true;
        }
    }
}
