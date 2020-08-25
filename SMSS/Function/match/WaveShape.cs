using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMSS.Function.match
{
    /// <summary>
    /// 波形相似度计算
    /// </summary>
    public class WaveShape
    {
        public double WaveCal(DataTable test, DataTable sample)
        {
            double wsm = 0;//存储波形相似度
            if (test.Rows.Count > 0)
            {
                int rows = Math.Min(test.Rows.Count, sample.Rows.Count);                
                rows = rows - 2;
                int columns = test.Columns.Count - 3;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        wsm += Convert.ToDouble(sample.Rows[i + 2][j + 3]) / Convert.ToDouble(test.Rows[i + 2][j + 3]);
                    }
                }
                wsm = Math.Abs(wsm / (rows * columns));
            }
            else
                MessageBox.Show("测试集为空！");
            return wsm;
        }
    }
}
