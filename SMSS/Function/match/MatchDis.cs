using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using SMSS.Data;

namespace SMSS.Function.match
{
    class MatchDis
    {
        public static DataTable HMatrix = new DataTable();//储存距离矩阵
        public static DataTable HProfile = new DataTable();//储存匹配结果

        /// <summary>
        /// 获取匹配结果
        /// </summary>
        /// <param name="Test"></param>
        /// <param name="Train"></param>
        /// <param name="order"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool MatchAnswer(List<string> Test, List<string> Train, int order, int level)
        {
            MatrixCal(Test, Train, order);
            MatrixAnalysis(HMatrix, level);
            AuxiliaryFunc.percent = 100;
            return true;
        }

        public bool MatrixCal(List<string> testset, List<string> trainset,int order)
        {
            if (testset.Count > 0)
            {
                FreeContainer.FreeTable(HMatrix);
                SpecAngel sa = new SpecAngel();
                WaveShape ws = new WaveShape();
                InputData id = new InputData();
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
                    DataTable dt1 = id.InputCSV(Config.uksoilfolder + @"\" + testset[i] + ".csv");
                    for (int j = 0; j < trainset.Count; j++)
                    {
                        AuxiliaryFunc.percent = Convert.ToInt32((i + 1) * (j + 1) / testset.Count / trainset.Count * 95);
                        DataTable dt2 = id.InputCSV(Config.soilfolder + @"\" + trainset[i] + ".csv");
                        switch (order)
                        {
                            case 0: dr[j + 1] = sa.SAcal(dt1, dt2).ToString(); break;
                            case 1: dr[j + 1] = ws.WaveCal(dt1,dt2).ToString(); break;
                            default: break;
                        }
                    }
                    AuxiliaryFunc.percent += Convert.ToDouble(i) / Convert.ToDouble(testset.Count) * 90;
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

        //读取对应剖面的分类
        public string ReadType(int level, string profile)
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


        /// <summary>
        /// 通过类型判断结果计算混淆矩阵
        /// </summary>
        /// <param name="dt">类型判断矩阵</param>
        /// <returns></returns>
        DataTable ConfusionMatrix(DataTable dt)
        {
            DataTable cm = new DataTable();//存储混淆矩阵
            Hashtable h1 = new Hashtable();//存储预测类型
            Hashtable h2 = new Hashtable();//存储真实类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!h1.ContainsKey(dt.Rows[i][1].ToString()))
                    h1.Add(dt.Rows[i][1].ToString(), h1.Count + 1);//预测
                if (!h2.ContainsKey(dt.Rows[i][2].ToString()))
                    h2.Add(dt.Rows[i][2].ToString(), h2.Count + 1);//真实
            }
            cm.Columns.Add(new DataColumn(" "));
            foreach (string key in h1.Keys)//根据类型构建混淆矩结构
                cm.Columns.Add(new DataColumn(key, typeof(int)));
            foreach (string key in h2.Keys)
            {
                DataRow dr = cm.NewRow();
                dr[0] = key;
                for (int j = 1; j < cm.Columns.Count; j++)
                    dr[j] = 0;
                cm.Rows.Add(dr);
            }
            for (int i = 0; i < dt.Rows.Count; i++)//填充混淆矩阵
            {
                int row = Convert.ToInt32(h2[dt.Rows[i][2].ToString()]);
                int col = Convert.ToInt32(h1[dt.Rows[i][1].ToString()]);
                cm.Rows[row - 1][col] = Convert.ToInt32(cm.Rows[row - 1][col]) + 1;
            }
            return cm;
        }

        /// <summary>
        /// 分析距离矩阵
        /// </summary>      
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
                double min = 999;
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
