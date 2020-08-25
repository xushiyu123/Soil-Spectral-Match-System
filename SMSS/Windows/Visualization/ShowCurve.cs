using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SMSS.Data;

namespace SMSS.Windows
{
    public partial class ShowCurve : Form
    {
        List<int> x = new List<int>();
        Color color = new Color();
        DataTable dt = new DataTable();
        int count = 0;
        public ShowCurve()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (count <= 10)
            {
                chart1.Series.Add(comboBox2.SelectedItem.ToString()+ "-" + (comboBox3.SelectedIndex + 1).ToString());
                chart1.Series[count].ChartType = SeriesChartType.Spline;
                chart1.Series[count].Color = color;
                x.Clear();
                chart1.Series[count].Points.DataBindXY(x, choose());
                count++;
            }
            else
            {
                MessageBox.Show("曲线数量过多或者该曲线已存在！");
                return;
            }
        }

        private List<double> choose()
        {
            List<double> l = new List<double>();
            for (int j = 3; j < dt.Columns.Count; j++)
            {
                l.Add(Convert.ToDouble(dt.Rows[comboBox3.SelectedIndex + 1][j]));
                x.Add(j + Config.MinWavelen - 1);
            }
            return l;
        }

    private void button2_Click(object sender, EventArgs e)
    {
        Dispose();
        Close();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        chart1.Series.Clear();
        count = 0;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        OutputData.saveChart("土壤光谱曲线", chart1);
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        comboBox2.Items.Clear();
        if (comboBox1.SelectedIndex == 0)
        {
            for (int i = 0; i < MainFormContainers.ProfileID.Count; i++)
                comboBox2.Items.Add(MainFormContainers.ProfileID[i]);
        }
        else
        {
            for (int i = 0; i < MainFormContainers.ukProfileID.Count; i++)
                comboBox2.Items.Add(MainFormContainers.ukProfileID[i]);
        }
        comboBox2.SelectedIndex = 0;
    }

    /// <summary>
    /// 下拉框2，选项更改触发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        FreeContainer.FreeTable(dt);
        InputData id = new InputData();
        string fold;
        if (comboBox1.SelectedIndex == 0)
            fold = Config.soilfolder;
        else
            fold = Config.uksoilfolder;
        dt = id.InputCSV(fold + @"\" + comboBox2.SelectedItem.ToString() + ".csv");
        comboBox3.Items.Clear();
        for (int i = 1; i <= dt.Rows.Count; i++)
            comboBox3.Items.Add(i.ToString() + "nm");
        comboBox3.SelectedIndex = 0;
    }

    private void button5_Click(object sender, EventArgs e)
    {
        ColorDialog colorDialog1 = new ColorDialog();
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            color = colorDialog1.Color;
            button5.BackColor = color;
        }
    }
}
}
