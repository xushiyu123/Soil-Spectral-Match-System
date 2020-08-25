using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.IO;
using SMSS.Data;

namespace SMSS.Function.match
{
    class MatchAccuracy
    {
        public DataTable Matrix = new DataTable();//距离矩阵
        public DataTable Profile = new DataTable();//一次训练结果
        public DataTable MMatrix = new DataTable();//混淆矩阵
        public DataTable AMatrix = new DataTable();//一次精确度矩阵
        public DataTable Accuracy = new DataTable();//剖面精确度表
        public DataTable BriefAccu = new DataTable();//总精确度表    
        /// <summary>
        /// 非特征训练
        /// </summary>
        /// <param name="Test">测试集</param>
        /// <param name="Train">训练集</param>
        /// <param name="levels">分类级别</param>
        /// <param name="order">方法指令</param>
        /// <returns></returns>
        public bool GetAnswer(List<string> Test, List<string> Train, int levels, int order)
        {
            FreeContainer.FreeTable(BriefAccu);
            string[] column = { "实验序号", "总正确率" };
            for (int i = 0; i < column.Length; i++)
            {
                DataColumn dc = new DataColumn(column[i]);
                BriefAccu.Columns.Add(dc);
            }
            for (int i = 0; i < Test.Count && i < Train.Count; i++)
            {
                List<string> TestSample = new List<string>();
                List<string> TrainSample = new List<string>();
                GetSamplelist(Test[i], out TestSample);//从测试集文件中取出剖面ID
                GetSamplelist(Train[i], out TrainSample);//从训练集文件中取出剖面ID
                NormalMatrix(TestSample, TrainSample, order);//计算距离矩阵
                AnswerAnalysis(Matrix, levels);//计算匹配剖面
                AccuracyAnalysis(Profile, i + 1);//计算匹配剖面类型
                MixMatrix(Profile);//计算混淆矩阵
                MixAccuracy(MMatrix);//计算该次实验的总精确度
                OutputData od = new OutputData();//输出该次实验结果
                od.ExportToCsv(Config.MatrixFolder, "实验" + (i + 1).ToString() + "混淆矩阵", MMatrix);
                od.ExportToCsv(Config.MatrixFolder, "实验" + (i + 1).ToString() + "匹配结果", AMatrix);
                AuxiliaryFunc.percent += 90 / Train.Count;
            }
            return true;
        }
        /// <summary>
        /// 直方图训练方法
        /// </summary>
        /// <param name="Test">测试集</param>
        /// <param name="Train">训练集</param>
        /// <param name="levels">分类级别</param>
        /// <param name="equation">距离函数指令</param>
        /// <returns></returns>
        public bool GetHistAnswer(List<string> Test, List<string> Train, int levels,int equation)
        {
            FreeContainer.FreeTable(BriefAccu);
            string[] column = { "实验序号", "总正确率" };
            for (int i = 0; i < column.Length; i++)
            {
                DataColumn dc = new DataColumn(column[i]);
                BriefAccu.Columns.Add(dc);
            }
            for (int i = 0; i < Test.Count && i<Train.Count; i++)
            {
                List<string> TestSample = new List<string>();
                List<string> TrainSample = new List<string>();
                GetSamplelist(Test[i], out TestSample);//从测试集文件中取出剖面ID
                GetSamplelist(Train[i], out TrainSample);//从训练集文件中取出剖面ID
                HistogramMatrix(TestSample, TrainSample, equation);//计算距离矩阵
                AnswerAnalysis(Matrix, levels);//计算匹配剖面
                AccuracyAnalysis(Profile, i + 1);//计算匹配剖面类型
                MixMatrix(Profile);//计算混淆矩阵
                MixAccuracy(MMatrix);//计算该次实验的总精确度
                OutputData od = new OutputData();//输出该次实验结果
                od.ExportToCsv(Config.MatrixFolder, "实验" + (i + 1).ToString() + "混淆矩阵", MMatrix);
                od.ExportToCsv(Config.MatrixFolder, "实验" + (i + 1).ToString() + "匹配结果", AMatrix);
                AuxiliaryFunc.percent += 90 / Train.Count;
            }
            return true;
        }
        /// <summary>
        /// 获取文件中的剖面ID列表
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="l">土壤编号列表</param>
        /// <returns></returns>
        bool GetSamplelist(string filename, out List<string> l)
        {
            l = new List<string>(); l.Clear();
            string strline; bool Flag = true;
            StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default);
            while ((strline = sr.ReadLine()) != null)
            {
                string[] aryline = strline.Split(new char[] { ',' });        //给datatable加上列名                    
                if (Flag) Flag = false;
                else
                    l.Add(aryline[1]);
            }
            sr.Close();
            return true;
        }
        /// <summary>
        /// 计算匹配矩阵
        /// </summary>
        /// <param name="dt">匹配矩阵表格</param>
        /// <param name="levels">分类级别</param>
        /// <returns></returns>
        public bool AnswerAnalysis(DataTable dt, int levels)
        {
            FreeContainer.FreeTable(Profile);
            string[] column = { "土壤编号", "预测类型", "真实类型", "是否匹配" };
            for (int i = 0; i < column.Length; i++)
            {
                DataColumn dc = new DataColumn(column[i]);
                Profile.Columns.Add(dc);
            }

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                DataRow dr = Profile.NewRow();
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
                dr[1] = ReadType(levels, dt.Columns[col].ColumnName.ToString());
                dr[2] = ReadType(levels, dt.Rows[i][0].ToString());
                if (dr[1].ToString() == dr[2].ToString())
                    dr[3] = "是";
                else
                    dr[3] = "否";
                Profile.Rows.Add(dr);
            }
            return true;
        }
       /// <summary>
       /// 从类型文件中获取类别
       /// </summary>
       /// <param name="level">分类级别</param>
       /// <param name="profile">剖面名称</param>
       /// <returns></returns>
        static string ReadType(int level, string profile)
        {
            profile = profile.Substring(1, profile.Length - 2);
            string type = "";
            StreamReader sr = new StreamReader(Config.soilhabitat, System.Text.Encoding.Default);
            string strline;
            while ((strline = sr.ReadLine()) != null)
            {
                string[] aryline = strline.Split(new char[] { ',' });        //给datatable加上列名                    
                if (aryline[0] == profile)
                    type = aryline[level + 1];
            }
            sr.Close();
            if (type == "")
                type = "未知";
            return type;
        }
        /// <summary>
        /// 计算非特征值距离矩阵
        /// </summary>
        /// <param name="testset">测试集剖面</param>
        /// <param name="trainset">训练集剖面</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool NormalMatrix(List<string> testset, List<string> trainset, int order)
        {
            FreeContainer.FreeTable(Matrix);
            SpecAngel sa = new SpecAngel();
            WaveShape ws = new WaveShape();
            InputData id = new InputData();
            for (int i = 0; i < trainset.Count + 1; i++)
            {
                if (i == 0)
                {
                    DataColumn dc = new DataColumn();
                    Matrix.Columns.Add(dc);
                }
                else
                {
                    DataColumn dc = new DataColumn(trainset[i - 1]);
                    Matrix.Columns.Add(dc);
                }
            }
            for (int i = 0; i < testset.Count; i++)
            {
                DataRow dr = Matrix.NewRow();
                dr[0] = testset[i];
                DataTable dt1 = id.InputCSV(Config.uksoilfolder + "\\" + testset[i].Substring(1, testset[i].Length - 2) + ".csv");
                for (int j = 0; j < trainset.Count; j++)
                {
                    DataTable dt2 = id.InputCSV(Config.soilfolder + "\\" + trainset[j].Substring(1, trainset[j].Length - 2) + ".csv");
                    if (dt1.Rows.Count > 0 && dt2.Rows.Count > 0)//防止剖面缺失
                    {
                        switch (order)
                        {
                            case 0: dr[j + 1] = sa.SAcal(dt1, dt2).ToString(); break;
                            case 1: dr[j + 1] = ws.WaveCal(dt1, dt2).ToString(); break;
                            default: break;
                        }
                    }
                    else
                        dr[j + 1] = 9999.ToString();//9999表示剖面缺失

                }
                Matrix.Rows.Add(dr);
            }
            return true;
        }
        /// <summary>
        /// 计算直方图距离矩阵
        /// </summary>
        /// <param name="testset"></param>
        /// <param name="trainset"></param>
        /// <param name="groups"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool HistogramMatrix(List<string> testset, List<string> trainset, int order)
        {
            FreeContainer.FreeTable(Matrix);
            for (int i = 0; i < trainset.Count + 1; i++)
            {
                if (i == 0)
                {
                    DataColumn dc = new DataColumn();
                    Matrix.Columns.Add(dc);
                }
                else
                {
                    DataColumn dc = new DataColumn(trainset[i - 1]);
                    Matrix.Columns.Add(dc);
                }
            }
            for (int i = 0; i < testset.Count; i++)
            {
                DataRow dr = Matrix.NewRow();
                dr[0] = testset[i];
                List<double> l1 = new List<double>();
                ReadHistogram(Config.soilhistogram, testset[i], out l1);
                for (int j = 0; j < trainset.Count; j++)
                {
                    List<double> l2 = new List<double>();
                    ReadHistogram(Config.soilhistogram, trainset[j], out l2);
                    if (l1.Count > 0 && l2.Count > 0)//防止剖面缺失
                    {
                        switch (order)
                        {
                            case 0: dr[j + 1] = Distance_Fomula.Euclidean(l1, l2).ToString(); break;
                            case 1: dr[j + 1] = Distance_Fomula.X2(l1, l2).ToString(); break;
                            case 2: dr[j + 1] = Distance_Fomula.Dxy(l1, l2).ToString(); break;
                            default: break;
                        }
                    }
                    else
                        dr[j + 1] = 9999.ToString();//9999表示剖面缺失

                }
                Matrix.Rows.Add(dr);
            }
            return true;
        }
        /// <summary>
        /// 读取直方图数据
        /// </summary>
        /// <param name="HistigramPath">直方图文件路径</param>
        /// <param name="profile">剖面编号</param>
        /// <param name="l">土壤编号列表</param>
        /// <returns></returns>
        public bool ReadHistogram(string HistigramPath, string profile, out List<double> l)
        {
            profile = profile.Substring(1, profile.Length - 2);
            l = new List<double>();
            l.Clear();
            StreamReader sr = new StreamReader(Config.soilhistogram, System.Text.Encoding.Default);
            string strline;
            while ((strline = sr.ReadLine()) != null)
            {
                string[] aryline = strline.Split(new char[] { ',' });        //给datatable加上列名                    
                if (aryline[0] == profile)
                {
                    for (int i = 1; i < aryline.Length; i++)
                    {
                        l.Add(Convert.ToDouble(aryline[i]));
                    }
                }
            }
            sr.Close();
            return true;
        }
        /// <summary>
        /// 计算正确率
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool AccuracyAnalysis(DataTable dt, int num)
        {
            FreeContainer.FreeTable(Accuracy);
            string[] column = { "实验序号", "正确率" };
            for (int i = 0; i < column.Length; i++)
            {
                DataColumn dc = new DataColumn(column[i]);
                Accuracy.Columns.Add(dc);
            }
            int sum = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][3].ToString() == "是")
                    sum++;
            }
            double degree = Convert.ToDouble(sum) / Convert.ToDouble(dt.Rows.Count);
            DataRow dr = Accuracy.NewRow();
            DataRow dr0 = BriefAccu.NewRow();
            dr[0] = "实验" + num.ToString(); dr[1] = (degree * 100).ToString("f2") + "%";
            dr0[0] = "实验" + num.ToString(); dr0[1] = (degree * 100).ToString("f2") + "%";
            Accuracy.Rows.Add(dr);
            BriefAccu.Rows.Add(dr0);
            return true;
        }
        /// <summary>
        /// 根据匹配结果计算混淆矩阵
        /// </summary>
        /// <param name="dt">匹配结果</param>
        /// <returns></returns>
        bool MixMatrix(DataTable dt)
        {
            FreeContainer.FreeTable(MMatrix);
            Hashtable h1 = new Hashtable();//存储预测类型
            Hashtable h2 = new Hashtable();//存储真实类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!h1.ContainsKey(dt.Rows[i][1].ToString()))
                    h1.Add(dt.Rows[i][1].ToString(), h1.Count + 1);//预测
                if (!h2.ContainsKey(dt.Rows[i][2].ToString()))
                    h2.Add(dt.Rows[i][2].ToString(), h2.Count + 1);//真实
            }
            MMatrix.Columns.Add(new DataColumn(" "));
            foreach (string key in h1.Keys)
                MMatrix.Columns.Add(new DataColumn(key, typeof(int)));
            foreach (string key in h2.Keys)
            {
                DataRow dr = MMatrix.NewRow();
                dr[0] = key;
                for (int j = 1; j < MMatrix.Columns.Count; j++)
                    dr[j] = 0;
                MMatrix.Rows.Add(dr);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int row = Convert.ToInt32(h2[dt.Rows[i][2].ToString()]);
                int col = Convert.ToInt32(h1[dt.Rows[i][1].ToString()]);
                MMatrix.Rows[row - 1][col] = Convert.ToInt32(MMatrix.Rows[row - 1][col]) + 1;
            }
            return true;
        }
        /// <summary>
        /// 计算各类型土壤分类精确度
        /// </summary>
        /// <param name="mix">混淆矩阵</param>
        /// <returns></returns>
        bool MixAccuracy(DataTable mix)
        {
            FreeContainer.FreeTable(AMatrix);
            AMatrix.Columns.Add(new DataColumn("  ", typeof(string))); //添加列
            AMatrix.Columns.Add(new DataColumn("用户精度", typeof(string)));
            AMatrix.Columns.Add(new DataColumn("错分率", typeof(string)));
            for (int i = 0; i < mix.Rows.Count; i++)//遍历混淆矩阵计算各类型精确度
            {
                DataRow dr = AMatrix.NewRow();
                dr[0] = mix.Rows[i][0].ToString();
                int sum = 0, num = 0;
                double ua = 0, cm = 0;
                for (int j = 1; j < mix.Columns.Count; j++)
                {
                    sum += Convert.ToInt32(mix.Rows[i][j]);
                    if (mix.Columns[j].ToString() == mix.Rows[i][0].ToString())
                        num = Convert.ToInt32(mix.Rows[i][j]);
                }
                ua = Convert.ToDouble(num) / Convert.ToDouble(sum);
                cm = 1.0 - ua;
                dr[1] = (ua * 100).ToString("f2") + "%";
                dr[2] = (cm * 100).ToString("f2") + "%";
                AMatrix.Rows.Add(dr);
            }
            return true;
        }
    }
}
