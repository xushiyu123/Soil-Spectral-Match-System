using System;
using System.Data;
using System.Windows.Forms;

namespace SMSS.Function.match
{
    class SpecAngel
    {
        /// <summary>
        /// 读入样本库与土壤库，循环计算SA，
        /// 并循环比较得出最小SA，得出最匹配土壤种类
        /// </summary>
        /// <param name="Dtb"></param>
        /// <param name="Stb"></param>
        /// <returns></returns>
        public double SAcal(DataTable test, DataTable sample)
        {
            double angle = 0;//储存光谱角值
            if (test.Rows.Count > 0)
            {
                int rows = Math.Min(test.Rows.Count, sample.Rows.Count);
                rows = rows - 2;
                int columns = test.Columns.Count - 3;
                double t = 0, r = 0, tr = 0;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        tr = tr + Convert.ToDouble(test.Rows[i + 2][j + 3]) * Convert.ToDouble(sample.Rows[i + 2][j + 3]);
                        t = t + Convert.ToDouble(sample.Rows[i + 2][j + 3]) * Convert.ToDouble(sample.Rows[i + 2][j + 3]);
                        r = r + Convert.ToDouble(test.Rows[i + 2][j + 3]) * Convert.ToDouble(test.Rows[i + 2][j + 3]);
                    }
                }
                angle = Math.Acos(tr / ((Math.Sqrt(t) * Math.Sqrt(r))));
            }
            else
                MessageBox.Show("测试集为空！");
            return angle;
        }
    }
}
