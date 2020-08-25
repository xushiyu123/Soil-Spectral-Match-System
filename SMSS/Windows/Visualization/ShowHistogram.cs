using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SMSS.Data;

namespace SMSS.Windows
{
    public partial class ShowHistogram : Form
    {
        List<double> x = new List<double>();
        List<double> y = new List<double>();
        int groups = 10;
        int count = 0;
        Color color = new Color();
        public ShowHistogram()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            count = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            OutputData.saveChart("土壤光谱直方图", chart1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AuxiliaryFunc af = new AuxiliaryFunc();
            if (af.TextIsNum(textBox1.Text))
            {
                groups = Convert.ToInt32(textBox1.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (count <= 10)
            {
                chart1.Series.Add(comboBox2.SelectedItem.ToString());
                chart1.Series[count].ChartType = SeriesChartType.Column;
                chart1.Series[count].Label = "#VAL";
                chart1.Series[count].Color = color;
                chart1.Series[count].IsValueShownAsLabel = true;
                chart1.Series[count].CustomProperties = "LabelStyle=Top";
                x.Clear();
                y.Clear();
                InputData id = new InputData();
                string fold;
                if (comboBox1.SelectedIndex == 0)
                    fold = Config.soilfolder;
                else
                    fold = Config.uksoilfolder;
                DataTable dt = id.InputCSV(fold + @"\" + comboBox2.SelectedItem.ToString() + ".csv");
                Histogram(dt,groups,out y,out x);
                for (int i = 0; i < y.Count; i++)
                    y[i] = Math.Round(y[i], 3);
                for (int i = 0; i < x.Count; i++)
                    x[i] = Math.Round(x[i], 3);
                chart1.Series[count].Points.DataBindXY(x, y);
                count++;
            }
            else
            {
                MessageBox.Show("直方图数量过多！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog colordialog1 = new ColorDialog();
            if (colordialog1.ShowDialog() == DialogResult.OK)
            {
                color = colordialog1.Color;
                button5.BackColor = color;
            }
        }

        private bool Histogram(DataTable dt,int groups,out List<double> l1,out List<double> l2)
        {
            l1 = new List<double>();
            l2 = new List<double>();
            double max = 0; double min = 100;

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                for (int j = 3; j < dt.Columns.Count; j++)
                {
                    if (Convert.ToDouble(dt.Rows[i][j]) > max)
                        max = Convert.ToDouble(dt.Rows[i][j]);
                    if (Convert.ToDouble(dt.Rows[i][j]) < min)
                        min = Convert.ToDouble(dt.Rows[i][j]);
                }
            }
            int[] t = new int[groups];
            for (int i = 0; i < t.Length; i++)
                t[i] = 0;
            double intervals = Convert.ToDouble(max - min) / Convert.ToDouble(groups - 1);
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                for (int j = 3; j < dt.Columns.Count; j++)
                {
                    t[(int)((Convert.ToDouble(dt.Rows[i][j]) - min) / intervals)]++;
                }
            }
            for (int i = 0; i < t.Length; i++)
                l1.Add(Convert.ToDouble(t[i]) / (Convert.ToDouble(dt.Rows.Count) * Convert.ToDouble(dt.Columns.Count - 3)));
            for (int i = 0; i < t.Length; i++)
                l2.Add(min + i * intervals);
            return true;
        }

    }
}
