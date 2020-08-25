using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using RDotNet;
using SMSS.Data;
using SMSS.Function.PreSolve;
using SMSS.Windows.Windows_Refresh;

namespace SMSS.Windows.Match_Form
{
    public partial class WavecharForm : Form
    {
        public static int k = 10;
        public static int level = 2;
        public WavecharForm(DataTable dt, DataTable et)
        {
            Raw = dt.Copy();
            Example = et.Copy();
            ExampleAfter = et.Copy();
            RawAfter = dt.Copy();
            pRaw = new double[Raw.Rows.Count, 8];
            pRawAfter = new double[Raw.Rows.Count, 4];
            pExample = new double[Example.Rows.Count,8];
            pExampleAfter=new double[Example.Rows.Count, 4];            
            InitializeComponent();
            GetView.ListBox(MainFormContainers.ProfileID, checkedListBox2);
            GetView.ListBox(MainFormContainers.ukProfileID, checkedListBox1);
        }

        public struct Valley//代表一个吸收谷
        {
            public double StartPosition;
            public double StartValue;
            public double EndPosition;
            public double EndValue;
            public double MinPosition;
            public double MinValue;
            public double xDistance;
            public double Depth;
            public int i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clark(Raw, ref RawAfter, ref pRaw, true, 200);
        } 

        private void button5_Click(object sender, EventArgs e)
        {
            Clark(Example, ref ExampleAfter, ref pExample, false, 200);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DataTable subdt1 = new DataTable();
            DataTable subdt2 = new DataTable();
            InputData id = new InputData();
            subdt1 = id.InputCSV(rootAdddress + "WaveCharacterResult(" + level.ToString() + "," + k.ToString() + ").csv");
            GetView.GetGridView(subdt1, dataGridView1);
            subdt2 = id.InputCSV(rootAdddress + "CorrectResult(" + level.ToString() + "," + k.ToString() + ").csv");
            GetView.GetGridView(subdt2, dataGridView2);
        }

        public void Clark(DataTable dt, ref DataTable et, ref double[,] pArray, bool tt, int R)//Clark外壳系数法-光谱曲线包络线消除法
        {
            DataTable Baoluo = new DataTable();
            Baoluo = dt.Copy();
            double[,,] pMax = new double[dt.Rows.Count, R, 2];//设极大值点不超过T个
            bool[,] IfVisited = new bool[dt.Rows.Count, R];//判断是否被访问过
            //初始化判断数组
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < R; j++)
                {
                    IfVisited[i, j] = false;
                }
            }
            Derivation(dt, ref et, tt, 100, ref pMax, 0.2);//阈值初步定为0.2
            for (int i = (tt) ? rawStartLine : exampleStartLine; i < et.Rows.Count; i++)
            {
                double[,] ptemp = new double[R, 2];
                bool[] itemp = new bool[R];
                for (int m = 0; m < R; m++)
                {
                    ptemp[m, 0] = pMax[i, m, 0];
                    ptemp[m, 1] = pMax[i, m, 1];
                    itemp[m] = IfVisited[i, m];
                }
                int max = Convert.ToInt32(FindMax(ptemp, ref itemp));
                int[] Head = new int[100];
                int[] Tail = new int[100];
                for (int ttk = 0; ttk < 100; ttk++)
                {
                    Head[ttk] = -1;
                    Tail[ttk] = -1;
                }
                double[] Result = new double[100];
                for (int ttj = 0; ttj < 100; ttj++)
                {
                    Result[ttj] = -1;
                }
                int pleft = max;//向头
                int pright = max;//向尾
                int pL = 0;
                int pR = 0;
                while (pleft > 0)
                {
                    double kMin = 999;
                    int MinCode = 0;
                    //向前找到斜率最小的点
                    for (int j = 0; j < pleft; j++)
                    {
                        double tj = CalK(ptemp[j, 0], ptemp[j, 1], ptemp[pleft, 0], ptemp[pleft, 1]);
                        if (tj < kMin)
                        {
                            kMin = tj;
                            MinCode = j;
                        }
                    }
                    Head[pL] = MinCode;
                    pL++;
                    pleft = MinCode;
                }
                while (pright < R)
                {
                    double kMax = -999;
                    int MaxCode = 0;
                    //向后找到斜率最大的点
                    for (int j = pright + 1; j < 1000; j++)
                    {
                        if (ptemp[j, 0] == 0)
                        {
                            break;
                        }
                        double tj = CalK(ptemp[j, 0], ptemp[j, 1], ptemp[pright, 0], ptemp[pright, 1]);
                        if (tj > kMax)
                        {
                            kMax = tj;
                            MaxCode = j;
                        }
                    }
                    if (MaxCode == 0)
                    {
                        break;
                    }
                    Tail[pR] = MaxCode;
                    pR++;
                    pright = MaxCode;
                }
                //将Head与Tail中的端点汇总到Result中，并按照序列排好
                if (tt)
                {
                    Result[0] = rawStart;//数据库数据
                }
                else
                {
                    Result[0] = exampleStart;//样本数据
                }
                int H = 1;
                for (int p = 99; p >= 0; p--)
                {
                    if (Head[p] == -1)
                    {
                        continue;
                    }
                    else
                    {
                        Result[H] = ptemp[Head[p], 0];
                        H++;
                    }
                }
                for (int p = 0; p < 100; p++)
                {
                    if (Tail[p] == -1)
                    {
                        break;
                    }
                    else
                    {
                        Result[H] = ptemp[Tail[p], 0];
                        H++;
                    }
                }
                Result[H] = dt.Columns.Count - 1;
                //Result数组中有端点
                double[] percent = new double[dt.Columns.Count];
                int M = 1;
                for (int n = (tt)?rawStart:exampleStart; n < dt.Columns.Count; n++)
                {
                    if (n > Result[M])
                    {
                        M++;
                        n--;
                    }
                    else
                    {
                        int uj = Convert.ToInt32(Result[M]);
                        int nj = Convert.ToInt32(Result[M - 1]);
                        double ttt = (n - Result[M - 1]) * (Convert.ToDouble(dt.Rows[i][uj]) - Convert.ToDouble(dt.Rows[i][nj])) / (Result[M] - Result[M - 1]) + Convert.ToDouble(dt.Rows[i][nj]);
                        Baoluo.Rows[i][n] = ttt;
                        percent[n] = Convert.ToDouble(dt.Rows[i][n]) / ttt;
                    }

                }
                FindPeak(ref pArray, percent, i, 0.998, ref dt,tt);

            }
            SaveArray(tt, pArray, et);
        }

        private double CalK(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        public void Derivation(DataTable ddd, ref DataTable result, bool tt, int K, ref double[,,] p, double Y)//平滑并求导,tt为真则是对土壤库求导，否则是对样本库求导,K为放大倍数,Y为=0的阈值
        {
            Presolution pre = new Presolution();
            DataTable temp = new DataTable();
            temp = ddd.Copy();
            pre.Plot5(temp);
            SaveTemp(temp);
            if (tt)
            {
                for (int i = rawStartLine; i < temp.Rows.Count; i++)
                {
                    int TEMP = 0;//计算有多少个极大值点
                    for (int j = rawStart+6; j < temp.Columns.Count - 5; j++)
                    {
                        //计算曲线各个位置上的一阶导数（用差分代替微分）
                        result.Rows[i][j] = K * (Convert.ToDouble(temp.Rows[i][j]) - Convert.ToDouble(temp.Rows[i][j - 1]));
                        if (Math.Abs(Convert.ToDouble(result.Rows[i][j])) < Y && (Convert.ToDouble(temp.Rows[i][j - 5]) < Convert.ToDouble(temp.Rows[i][j])) && (Convert.ToDouble(temp.Rows[i][j + 5]) < Convert.ToDouble(temp.Rows[i][j])))
                        {
                            p[i, TEMP, 0] = j;
                            p[i, TEMP, 1] = Convert.ToDouble(temp.Rows[i][j]);
                            TEMP++;
                        }
                    }
                    result.Rows[i][rawStart] = 0;//单独给第一个点赋值
                }
            }
            else
            {
                for (int i = exampleStartLine; i < temp.Rows.Count; i++)
                {
                    int TEMP = 0;
                    for (int j = exampleStart; j < temp.Columns.Count; j++)
                    {
                        //计算曲线各个位置上的一阶导数（用差分代替微分并放大）
                        result.Rows[i][j] = K * ((Convert.ToDouble(temp.Rows[i][j]) - Convert.ToDouble(temp.Rows[i][j - 1])));
                        if (Math.Abs(Convert.ToDouble(result.Rows[i][j])) < Y)
                        {
                            p[i, TEMP, 0] = j;
                            p[i, TEMP, 1] = Convert.ToDouble(temp.Rows[i][j]);
                            TEMP++;
                        }
                    }
                    result.Rows[i][exampleStart] = 0;//单独给第一个点赋值
                }
            }
        }

        public void FindPeak(ref double[,] pArray, double[] per, int i, double pVV, ref DataTable kt,bool israw)//提取位置，反射值，深度，宽度
        {
            List<Valley> V = new List<Valley>();
            Valley tpt1, tpt2;
            tpt1.StartPosition = 0;
            tpt1.StartValue = 0;
            tpt1.MinPosition = 0;
            tpt1.MinValue = 0;
            tpt1.xDistance = 0;
            tpt1.Depth = 0;
            tpt2.StartPosition = 0;
            tpt2.StartValue = 0;
            tpt2.MinPosition = 0;
            tpt2.MinValue = 0;
            tpt2.xDistance = 0;
            tpt2.Depth = 0;
            int flag1 = 0;//0未开始，1正在，2已结束
            int flag2 = 0;
            int j = (israw)?rawStart:exampleStart;
            double MinPosition1 = 0;
            double MinValue1 = pVV;
            double MinPosition2 = 0;
            double MinValue2 = pVV;
            while (j < per.Length)
            {
                if (j <= 1000)
                {
                    j++;
                    continue;
                }
                if (flag2 == 2)
                {
                    break;
                }
                if (flag1 == 0)
                {
                    bool isis1 = true;
                    for (int u = 0; u < 50; u++)
                    {
                        if (per[j + u] > pVV)
                        {
                            isis1 = false;
                            break;
                        }
                    }
                    if (isis1)
                    {
                        tpt1.StartPosition = j;
                        tpt1.StartValue = Convert.ToDouble(kt.Rows[i][j]);
                        MinPosition1 = tpt1.StartPosition;
                        MinValue1 = tpt1.StartValue;
                        tpt1.i = i;
                        flag1 = 1;
                    }
                }
                else if (per[j] <= pVV && flag1 == 1)
                {
                    if (Convert.ToDouble(kt.Rows[i][j]) < MinValue1)
                    {
                        MinValue1 = Convert.ToDouble(kt.Rows[i][j]);
                        MinPosition1 = j;
                    }
                }
                else if (per[j] > pVV && flag1 == 1)
                {
                    tpt1.EndPosition = j - 1;
                    tpt1.EndValue = Convert.ToDouble(kt.Rows[i][j]);
                    tpt1.MinPosition = MinPosition1;
                    tpt1.MinValue = MinValue1;
                    tpt1.xDistance = tpt1.EndPosition - tpt1.StartPosition;
                    tpt1.Depth = (tpt1.StartValue + tpt1.EndValue) / 2 - tpt1.MinValue;
                    flag1 = 2;
                }
                else if (flag1 == 2)
                {
                    if (flag2 == 0)
                    {
                        bool isis2 = true;
                        for (int u = 0; u < 50; u++)
                        {
                            if (per[j + u] > pVV)
                            {
                                isis2 = false;
                                break;
                            }
                        }
                        if (isis2)
                        {
                            tpt2.StartPosition = j;
                            tpt2.StartValue = Convert.ToDouble(kt.Rows[i][j]);
                            tpt2.i = i;
                            MinPosition2 = tpt2.StartPosition;
                            MinValue2 = tpt2.StartValue;
                            flag2 = 1;
                        }
                    }
                    else if (per[j] <= pVV && flag2 == 1)
                    {
                        if (Convert.ToDouble(kt.Rows[i][j]) < MinValue2)
                        {
                            MinPosition2 = j;
                            MinValue2 = Convert.ToDouble(kt.Rows[i][j]);
                        }
                    }
                    else if (per[j] > pVV && flag2 == 1)
                    {
                        tpt2.EndPosition = j - 1;
                        tpt2.EndValue = Convert.ToDouble(kt.Rows[i][j - 1]);
                        tpt2.MinPosition = MinPosition2;
                        tpt2.MinValue = MinValue2;
                        tpt2.xDistance = tpt2.EndPosition - tpt2.StartPosition;
                        tpt2.Depth = (tpt2.StartValue + tpt2.EndValue) / 2 - tpt2.MinValue;
                        flag2 = 2;
                    }
                }
                j++;
            }
            pArray[i, 0] = tpt1.MinPosition;
            pArray[i, 1] = tpt1.MinValue;
            pArray[i, 2] = tpt1.Depth;
            pArray[i, 3] = tpt1.xDistance;
            pArray[i, 4] = tpt2.MinPosition;
            pArray[i, 5] = tpt2.MinValue;
            pArray[i, 6] = tpt2.Depth;
            pArray[i, 7] = tpt2.xDistance;
        }

        private void SaveArray(bool tt, double[,] pA, DataTable ft)
        {
            string s;
            if (tt)
            {
                s = rootAdddress+ "RawCharacter.csv";

            }
            else
            {
                s = rootAdddress + "ExampleCharacter.csv";
            }
            StreamWriter pStreamWriter = new StreamWriter(new FileStream(s, FileMode.Append));
            pStreamWriter.Write("Position1,Value1,Depth1,xDistance1,Position2,Value2,Depth2,xDistance2");
            pStreamWriter.Write("\r\n");
            for (int i = (tt) ? rawStartLine : exampleStartLine; i < ft.Rows.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pStreamWriter.Write("{0},", pA[i, j]);
                }
                pStreamWriter.Write("\r\n");
            }
            pStreamWriter.Close();
        }

        private void SaveTemp(DataTable tempt)
        {
            string s =rootAdddress +"AfterSmooth.csv";
            StreamWriter pStreamWriter = new StreamWriter(new FileStream(s, FileMode.Append));
            for (int i = 0; i < tempt.Rows.Count; i++)
            {
                for (int j = 0; j < tempt.Columns.Count; j++)
                {
                    pStreamWriter.Write("{0},", tempt.Rows[i][j]);
                }
                pStreamWriter.Write("\r\n");
            }
            pStreamWriter.Close();
        }

        public double FindMax(double[,] p, ref bool[] Iv)
        {
            double MAX = 0;
            double MAXCODE = 0;
            int i = 0;
            while (p[i, 0] != 0)
            {
                if (Iv[i])
                {
                    i++;
                    continue;
                }
                else
                {
                    if (Convert.ToDouble(p[i, 1]) > MAX)
                    {
                        MAX = Convert.ToDouble(p[i, 1]);
                        MAXCODE = i;
                    }
                    i++;
                }
            }
            Iv[Convert.ToInt32(MAXCODE)] = true;
            return MAXCODE;
        }       

        public void NNet()
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Initialize();
            engine.Evaluate("library(nnet)");
            string Sentence1 = "wbcd<-read.csv(\"C:/ZWHworksapce/newcreate/RawCharacter(GDnew).csv\",stringsAsFactors = FALSE)";
            string Sentence2 = "code<-wbcd[1]";
            string Sentence3 = "Ucode=unique(code)";
            string Sentence4 = "set.seed(2)";
            string Sentence5 = "ind=sample(2,nrow(Ucode),replace=TRUE,prob=c(0.75,0.25))";
            string Sentence6 = "code_train=Ucode[ind==1,]";
            string Sentence7 = "code_test=Ucode[ind==2,]";
            string Sentence8 = "wbcd_tt<-(wbcd[,1] %in% code_train)";
            string Sentence9 = "t_train<-wbcd[wbcd_tt==TRUE,]";
            string Sentence10 = "t_test<-wbcd[wbcd_tt==FALSE,]";
            string Sentence11 = "wbcd<-rbind(t_train,t_test)";
            string Sentence12 = "wbcd<-wbcd[c(-1,-2,-3,-5)]";
            string Sentence13 = "wbcd$GroupCST<-as.factor(wbcd$GroupCST)";
            string Sentence14 = "wbcd_train<-wbcd[1:nrow(t_train),]";
            string Sentence15 = "wbcd_test<-wbcd[(nrow(t_train)+1):nrow(wbcd),]";
            string Sentence16 = "wbcd_nn=nnet(GroupCST~.,data=wbcd_train,size=20,rang=0.1,decay=5e-4,maxit=200)";
            string Sentence18 = "wbcd_pred=predict(wbcd_nn,wbcd_test,,type=\"class\")";
            string Sentence19 = "wbcd_table<-table(wbcd_test$GroupCST,wbcd_pred)";
            string Sentence20 = "write.csv(wbcd_table,file = \"C:/ZWHworksapce/newcreate/WaveCharacterResult.csv\", quote = F)";
            engine.Evaluate(Sentence1);
            engine.Evaluate(Sentence2);
            engine.Evaluate(Sentence3);
            engine.Evaluate(Sentence4);
            engine.Evaluate(Sentence5);
            engine.Evaluate(Sentence6);
            engine.Evaluate(Sentence7);
            engine.Evaluate(Sentence8);
            engine.Evaluate(Sentence9);
            engine.Evaluate(Sentence10);
            engine.Evaluate(Sentence11);
            engine.Evaluate(Sentence12);
            engine.Evaluate(Sentence13);
            engine.Evaluate(Sentence14);
            engine.Evaluate(Sentence15);
            engine.Evaluate(Sentence16);
            engine.Evaluate(Sentence18);
            engine.Evaluate(Sentence19);
            engine.Evaluate(Sentence20);
        }

        private DataTable Raw;//原始土壤数据库
        private DataTable RawAfter;//一阶平滑求导后土壤库
        private DataTable Example;//原始样本
        private DataTable ExampleAfter;//一阶平滑求导后样本
        private double[,] pRaw;//临时土壤特征值数组
        private double[,] pRawAfter;
        private double[,] pExample;//临时样本特征值数组
        private double[,] pExampleAfter;
        private string rootAdddress = @"F:\WorkSpace\土壤光谱匹配系统\土壤光谱匹配系统V1.0\SMSS\Function\match\WaveCharacter\";//这里保存该算法的所有文件
        private int rawStart = 7;
        private int exampleStart = 0;
        private int rawStartLine = 1;
        private int exampleStartLine = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            level = comboBox1.SelectedIndex;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            AuxiliaryFunc af = new AuxiliaryFunc();
            if (af.TextIsNum(textBox1.Text))
            {
                k = Convert.ToInt32(textBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox1, checkedListBox1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            GetView.AllSelect(checkBox2, checkedListBox2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            rootAdddress = FileBrowser.GetFolder();
            textBox2.Text = rootAdddress;
        }
    }
}
