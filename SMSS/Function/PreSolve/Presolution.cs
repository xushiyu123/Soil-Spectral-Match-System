using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System;
using SMSS.Data;

namespace SMSS.Function.PreSolve
{
    class Presolution
    {
        public static int MaxThreshold = 1;
        public static int MinThreshold = 0;

        public bool DoPresolve(string foldname, string newfolder, string order)
        {
            if (foldname != null && newfolder != null)
            {
                AuxiliaryFunc.percent = 0;         //为主界面清空进度
                List<string> l = new List<string>();
                DirectoryInfo di = new DirectoryInfo(foldname);
                foreach (FileInfo file in di.GetFiles())
                    l.Add(Path.GetFileNameWithoutExtension(file.Name));
                for (int i = 0; i < l.Count; i++)
                {
                    DataTable dt = new DataTable();     //创建临时表容器
                    InputData id = new InputData();
                    dt = id.InputCSV(foldname + "\\" + l[i] + ".csv");
                    switch (order)                       //执行相应的变换
                    {
                        case "D": Derive(dt); break;
                        case "N": Normal(dt); break;
                        case "S11": Plot(dt); break;
                        case "S5": Plot5(dt); break;
                        default: break;
                    }
                    string[] col = new string[3];
                    for (int k = 0; k < col.Length; k++)
                        col[k] = dt.Columns[i].ColumnName;
                    OutputData od = new OutputData();
                    od.ExportToCsv(newfolder, l[i], dt);//将处理数据存至新文夹
                    AuxiliaryFunc.percent += 80 / Convert.ToDouble(l.Count);//更新进度条
                }
            }
            return true;
        }

        /// <summary>
        /// 求一阶导
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Derive(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    for (int j = 3; j < dt.Columns.Count - 1; j++)     //由于数据间隔是1nm，所以计算相邻差值即可
                    {
                        dt.Rows[i][j] = (Convert.ToDouble(dt.Rows[i][j + 1]) - Convert.ToDouble(dt.Rows[i][j])).ToString();
                    }
                    dt.Rows[i][dt.Columns.Count - 1] = "0";
                }
                return true;
            }
            else
            {
                MessageBox.Show("数据集为空，一阶导失败！");
                return false;
            }
        }

        public bool Normal(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    double max = 0;
                    for (int j = 3; j < dt.Columns.Count; j++)     //获取最大值
                    {
                        if (Convert.ToDouble(dt.Rows[i][j]) > max)
                            max = Convert.ToDouble(dt.Rows[i][j]);
                    }
                    if (max <= 0)
                    {
                        MessageBox.Show("数值异常，行数为" + (i + 1).ToString());
                        return false;
                    }
                    for (int j = 3; j < dt.Columns.Count; j++)     //循环归一化
                    {
                        dt.Rows[i][j] = (Convert.ToDouble(dt.Rows[i][j]) / max * MaxThreshold + MinThreshold).ToString();
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("数据集为空，归一化失败！");
                return false;
            }
        }
               /// <summary>
        /// 将光谱曲线做平滑处理，采取11点平均法
        /// </summary>
        public bool Plot(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {                  
                    for (int j = 3; j < dt.Columns.Count - 11; j++)     //由于数据间隔是1nm，所以计算相邻差值即可
                    {
                        double sum11 = 0.0;
                        for (int k = 0; k < 11; k++)
                            sum11 += Convert.ToDouble(dt.Rows[i][j + k]);
                        dt.Rows[i][j] = (sum11 / 11.0).ToString();
                    }
                }
                MessageBox.Show("平滑完成！");
                return true;
            }

            else {
                MessageBox.Show("数据集为空，平滑失败！");
                return false;
            }
        }

        /// <summary>
        /// 将光谱曲线做平滑处理，采取5点平均法
        /// </summary>
        public bool Plot5(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    double col3 = Convert.ToDouble(dt.Rows[i][3]);
                    double col4 = Convert.ToDouble(dt.Rows[i][4]);
                    dt.Rows[i][3] = (3 * col3 + 2 * col4 + Convert.ToDouble(dt.Rows[i][5]) - Convert.ToDouble(dt.Rows[i][6])) / 5;
                    dt.Rows[i][4] = (4 * col3 + 3 * col4 + 2 * Convert.ToDouble(dt.Rows[i][5]) + Convert.ToDouble(dt.Rows[i][6])) / 10;
                    for (int j = 5; j < dt.Columns.Count - 2; j++)     //移动平均
                    {
                        double j1 = Convert.ToDouble(dt.Rows[i][j - 2]); double j2 = Convert.ToDouble(dt.Rows[i][j - 1]);
                        dt.Rows[i][j] = (j1 + j2 + Convert.ToDouble(dt.Rows[i][j]) + Convert.ToDouble(dt.Rows[i][j + 1]) + Convert.ToDouble(dt.Rows[i][j + 2])) / 5.0;
                    }
                    dt.Rows[i][dt.Columns.Count - 2] = (4 * Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 1]) + 3 * Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 2])
                        + 2 * Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 3]) + Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 4])) / 10;
                    dt.Rows[i][dt.Columns.Count - 1] = (3 * Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 1]) 
                        + 2 * Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 2]) + Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 3]) - Convert.ToDouble(dt.Rows[i][dt.Columns.Count - 5])) / 5;
                }
                return true;
            }
            else
            {
                MessageBox.Show("数据缺失！");
                return false;
            }
        }
    }
}
