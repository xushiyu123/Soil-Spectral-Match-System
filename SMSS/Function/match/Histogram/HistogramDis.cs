using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using SMSS.Windows.Match_Form;
using SMSS.Data;

namespace SMSS.Function.match.Histogram
{
    class HistogramDis
    {
        public static DataTable HMatrix = new DataTable();
        public static DataTable HProfile = new DataTable();

        //读取对应剖面直方图数据
        public bool ReadHistogram(string histpath,string profile,out List<double> l)
        {
            l = new List<double>();
            if (File.Exists(histpath))
            {
                string strline;
                StreamReader sr = new StreamReader(histpath, System.Text.Encoding.GetEncoding("gb2312"));
                while ((strline = sr.ReadLine()) != null)
                {
                    string[] aryline = strline.Split(new char[] { ',' });
                    //比对剖面id
                    if (aryline[0] == profile)
                    {
                        for (int i = 1; i < aryline.Length; i++)
                        {
                            l.Add(Convert.ToDouble(aryline[i]));                              
                        }
                    }
                }
                sr.Close();
            }
            return true;
        }

        //读取对应剖面的分类
        public string ReadType(int level,string profile)
        {
            string type = "";
            if (File.Exists(Config.soilhabitat))
            {
                string strline;
                StreamReader sr = new StreamReader(Config.soilhabitat, System.Text.Encoding.GetEncoding("gb2312"));
                while ((strline = sr.ReadLine()) != null)
                {
                    string[] aryline = strline.Split(new char[] { ',' });
                    //比对剖面id
                    if (aryline[0] == profile)
                    {
                        type = aryline[level + 1];
                    }
                }
                sr.Close();
            }
            return type;
        }

        public bool GetAnswer(List<string> Test, List<string> Train, int order, int level)
        {
            HistogramSet(Test, Train, order);
            MatrixAnalysis(HMatrix, level);
            AuxiliaryFunc.percent = 100;
            return true;
        }

        public bool HistogramSet(List<string> testset, List<string> trainset, int order)
        {
            if (testset.Count > 0)
            {               
                FreeContainer.FreeTable(HMatrix);
                for (int i = 0; i < trainset.Count + 1; i++)
                {
                    if (i == 0)
                    {
                        DataColumn dc = new DataColumn(" ");
                        HMatrix.Columns.Add(dc);
                    }
                    else
                    {
                        DataColumn dc = new DataColumn(trainset[i - 1]);
                        HMatrix.Columns.Add(dc);
                    }
                }
                for (int i = 0; i < testset.Count; i++)
                {
                    DataRow dr = HMatrix.NewRow();
                    dr[0] = testset[i];
                    List<double> l1 = new List<double>();
                    ReadHistogram(Config.uksoilhistogram, testset[i], out l1);
                    for (int j = 0; j < trainset.Count; j++)
                    {
                        AuxiliaryFunc.percent = Convert.ToInt32((i + 1) * (j + 1) / testset.Count / trainset.Count * 95);
                        List<double> l2 = new List<double>();
                        ReadHistogram(Config.soilhistogram, trainset[j], out l2);
                        switch (order)
                        {
                            case 0: dr[j + 1] = Distance_Fomula.Euclidean(l1, l2).ToString(); break;
                            case 1: dr[j + 1] = Distance_Fomula.X2(l1, l2).ToString(); break;
                            case 2: dr[j + 1] = Distance_Fomula.Dxy(l1, l2).ToString(); break;
                            default: break;
                        }
                    }
                    HMatrix.Rows.Add(dr);
                }
                return true;
            }
            else
            {
                MessageBox.Show("测试集为空！");
                return false;
            }
        }

        public bool MatrixAnalysis(DataTable dt, int level)
        {
            FreeContainer.FreeTable(HProfile);
            string[] column = { "剖面编号", "预测类型" };
            for (int i = 0; i < column.Length; i++)
            {
                DataColumn dc = new DataColumn(column[i]);
                HProfile.Columns.Add(dc);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = HProfile.NewRow();
                dr[0] = dt.Rows[i][0].ToString();
                double min = 2;
                int col = 1;
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (Convert.ToDouble(dt.Rows[i][j]) < min && Convert.ToDouble(dt.Rows[i][j]) != 0)
                    {
                        min = Convert.ToDouble(dt.Rows[i][j]);
                        col = j;
                    }
                }             
                dr[1] = ReadType(level, dt.Columns[col].ColumnName.ToString());
                HProfile.Rows.Add(dr);
            }
            return true;
        }
    }
}
