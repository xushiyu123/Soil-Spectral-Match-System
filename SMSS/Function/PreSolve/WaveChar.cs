using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RDotNet;
using 系统改良;
using 系统改良.Function;
using 系统改良.Data;

namespace 系统改良
{
    public partial class WaveChar : Form
    {
        public WaveChar(DataTable dt, DataTable et)
        {
            Raw = dt.Copy();
            Example = et.Copy();
            ExampleAfter1 = et.Copy();
            RawAfter1 = dt.Copy();
            InitializeComponent();
            pRaw = new double[Raw.Rows.Count, 8];
            pExample = new double[Example.Rows.Count,8];
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
            pRawaddress = FileReader.getfile();
            Clark(Raw, ref RawAfter1, ref pRaw, 200,true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pRawaddress = FileReader.getfile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string P="3";
            string KNN;
            DataTable subdt = new DataTable();
            if (textBox1.Text == null)
            {
                KNN = Convert.ToString(201);
            }
            else
            {
                KNN = textBox1.Text;
            }
            kNN(P, KNN);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pExampleaddress = FileReader.getfile();
            Clark(Example, ref ExampleAfter1, ref pExample, 200, false);
        }

        public void Clark(DataTable dt, ref DataTable et, ref double[,] pArray, int R,bool tt)//Clark外壳系数法-光谱曲线包络线消除法
        {
            DataTable Baoluo = new DataTable();
            Baoluo = dt.Copy();
            double[, ,] pMax = new double[dt.Rows.Count, R, 2];//设极大值点不超过T个
            bool[,] IfVisited = new bool[dt.Rows.Count, R];//判断是否被访问过
            //初始化判断数组
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < R; j++)
                {
                    IfVisited[i, j] = false;
                }
            }
            Derivation( dt, ref et , 100 , ref pMax , 0.2 );//阈值初步定为0.2
            for (int i = 1; i < et.Rows.Count; i++)
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
                Result[0] = 7;
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
                for (int n = 7; n < dt.Columns.Count; n++)
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
                FindPeak(ref pArray, percent, i, 0.998, ref dt);
            }
            SaveArray(tt,pArray, et);
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

        private double CalK(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        public void Derivation(DataTable ddd, ref DataTable result,int K, ref double[, ,] p, double Y)//平滑并求导,tt为真则是对土壤库求导，否则是对样本库求导,K为放大倍数,Y为=0的阈值
        {
            DataTable temp = new DataTable();
            Smooth sth = new Smooth();
            sth.Plot5(ddd, ref temp);
            for (int i = 1; i < ddd.Rows.Count; i++)
            {
                int TEMP = 0;//计算有多少个极大值点
                for (int j = 13; j < temp.Columns.Count - 5; j++)
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
                result.Rows[i][7] = 0;//单独给第一个点赋值
            }
        }

        public void FindPeak(ref double[,] pArray, double[] per, int i, double pVV, ref DataTable kt)//提取位置，反射值，深度，宽度
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
            int j = 7;
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

        private void SaveArray(bool tt,double[,] pA, DataTable ft)
        {
            string s;
            if (tt)
            {
                s = pRawaddress;

            }
            else
            {
                s = pExampleaddress;
            }

            MLApp.MLApp matlab = null;
            Type matlabAppType = System.Type.GetTypeFromProgID("Matlab.Application");
            matlab = System.Activator.CreateInstance(matlabAppType) as MLApp.MLApp;
            string command;
            //读入数据
            command = @"data=xlsread('" + MainForm.TrainPath + "')";
            matlab.Execute(command);
            //删除第一行
            command = "data([1],:)=[]";
            matlab.Execute(command);
            //删除前两列
            command = "data(:,[1,3])=[]";
            matlab.Execute(command);
            //得到data行列
            command = "[m,n]=size(data)";
            matlab.Execute(command);
            //保存为矩阵
            command = "y=zeros(m,4)";
            matlab.Execute(command);
            StreamWriter pStreamWriter = new StreamWriter(new FileStream(s, FileMode.Append));
            pStreamWriter.Write("Code,OrderCST,SuborderCST,GroupCST,SubgroupCST,");
            pStreamWriter.Write("Position1,Value1,Depth1,xDistance1,Position2,Value2,Depth2,xDistance2");
            pStreamWriter.Write("\r\n");
            for (int i = 1; i < ft.Rows.Count; i++)
            {
                command = "temp1=smooth(data(" + Convert.ToString(i) + ",:))";
                matlab.Execute(command);
                command = "temp2=diff(temp1)";
                matlab.Execute(command);
                command = "temp3=smooth(temp2)";
                matlab.Execute(command);
                command = "temp4=[temp3(750:1250)]";
                matlab.Execute(command);
                command = "[a,b]=findpeaks(temp4,'minpeakdistance',500)";
                matlab.Execute(command);
                command = "y(" + Convert.ToString(i) + ",1)=a";
                matlab.Execute(command);
                command = "y(" + Convert.ToString(i) + ",2)=b";
                matlab.Execute(command);
                command = "temp5=1-temp4";
                matlab.Execute(command);
                command = "[c,d]=findpeaks(temp5,'minpeakdistance',500)";
                matlab.Execute(command);
                command = "y(" + Convert.ToString(i) + ",3)=c";
                matlab.Execute(command);
                command = "y(" + Convert.ToString(i) + ",4)=d";
                matlab.Execute(command);
                pStreamWriter.Write("{0},{1},{2},{3},{4},", (tt) ? Raw.Rows[i][0] : i, (tt) ? Raw.Rows[i][1] : i, (tt) ? Raw.Rows[i][2] : i, (tt) ? Raw.Rows[i][3] : i, (tt) ? Raw.Rows[i][4] : i);
                for (int j = 0; j < 8; j++)
                {
                    pStreamWriter.Write("{0},", pA[i, j]);
                }
                command = @"csvwrite('" + s + "',y(" + Convert.ToString(i) + ",:))";
                matlab.Execute(command);
                pStreamWriter.Write("\r\n");
            }
            pStreamWriter.Close();
        }

        public void kNN(string p, string knn)
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Initialize();
            engine.Evaluate("library (class)");
            engine.Evaluate("library(gmodels)");
            string Sentence1 = "t_train<-read.csv(\""+pRawaddress+"\",stringsAsFactors = FALSE)";
            string Sentence2 = "t_test<-read.csv(\"" + pRawaddress + "\",stringsAsFactors = FALSE)";
            string Sentence11 = "wbcd<-rbind(t_train,t_test)";
            string Sentence12 = "wbcd<-wbcd[-1]";
            string Sentence112 = "normalize<-function(x){return ((x-min(x))/(max(x)-min(x)))}";
            string Sentence13 = "wbcd_n<-as.data.frame(lapply(wbcd[8:20],normalize))";
            string Sentence14 = "wbcd_train<-wbcd_n[1:nrow(t_train),]";
            string Sentence15 = "wbcd_test<-wbcd_n[(nrow(t_train)+1):nrow(wbcd_n),]";
            string Sentence16 = "wbcd_train_labels<-wbcd[1:nrow(t_train),3]";
            string Sentence17 = "wbcd_test_labels<-wbcd[(nrow(t_train)+1):nrow(wbcd_n),3]";
            string Sentence18 = "wbcd_pred<-knn(wbcd_train,wbcd_test,wbcd_train_labels," + knn + ")";//可修改k的值
            string Sentence19 = "wbcd_table<-table(wbcd_pred,wbcd_test_labels   )";
            string Sentence29 = "NegAcc = sum(wbcd_pred==wbcd_test_labels)/nrow(wbcd_test);";
            string Sentence20 = "write.csv(wbcd_table,file = \"C:/ZWHworksapce/newcreate/WaveCharacterResult(" + p + "," + knn + ").csv\", quote = F)";
            string Sentence21 = "write.csv(wbcd_pred,file = \"C:/ZWHworksapce/newcreate/tempR(" + p + "," + knn + ").csv\", quote = F)";
            string Sentence30 = "write.csv(NegAcc,file = \"C:/ZWHworksapce/newcreate/isCorrect.csv\", quote = F)";
            string Sentence22;
            if (Convert.ToInt32(p) == 1)
            {
                Sentence22 = "write.table(\"土纲分类结果：\",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)";
            }
            else if (Convert.ToInt32(p) == 2)
            {
                Sentence22 = "write.table(\"亚纲分类结果：\",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)";
            }
            else if (Convert.ToInt32(p) == 3)
            {
                Sentence22 = "write.table(\"土类分类结果：\",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)";
            }
            else if (Convert.ToInt32(p) == 4)
            {
                Sentence22 = "write.table(\"亚类分类结果：\",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)";
            }
            else
            {
                return;
            }
            string Sentence23 = "newtest=read.csv(\"C:/ZWHworksapce/newcreate/tempR(" + p + "," + knn + ").csv\")";
            string Sentence25 = "rawtest=read.csv(\"C:/ZWHworksapce/newcreate/Test_Code.csv\")";
            engine.Evaluate(Sentence1);
            engine.Evaluate(Sentence2);
            engine.Evaluate(Sentence11);
            engine.Evaluate(Sentence12);
            engine.Evaluate(Sentence112);
            engine.Evaluate(Sentence13);
            engine.Evaluate(Sentence14);
            engine.Evaluate(Sentence15);
            engine.Evaluate(Sentence16);
            engine.Evaluate(Sentence17);
            engine.Evaluate(Sentence18);
            engine.Evaluate(Sentence19);
            engine.Evaluate(Sentence29);
            engine.Evaluate(Sentence20);
            engine.Evaluate(Sentence21);
            engine.Evaluate(Sentence22);
            engine.Evaluate(Sentence23);
            engine.Evaluate(Sentence25);
            engine.Evaluate(Sentence30);
            string Sentence31 = "Start = 1";
            string Sentence32 = "K = 0";
            engine.Evaluate(Sentence31);
            engine.Evaluate(Sentence32);
            for (int i = 0; i < 96; i++)
            {
                string Sentencett0 = "K<-rawtest[" + (i + 1) + ",2]";
                string Sentencett1 = "p" + i + "<-newtest[Start:Start+K-1,]";
                string Sentencett2 = "o" + i + "<-names(table(p" + i + "[2]))[which.max(table(p" + i + "[2]))]";
                string Sentencett3 = "write.table(\"第" + (i + 1) + "个样本:\",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)";
                string Sentencett31 = "if(o" + i + "==rawtest[" + (i + 1) + ",3]) {write.table(\"正确\",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)}else {write.table(\"错误\",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)}";
                string Sentencett4 = "write.table(o" + i + ",\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)";
                string Sentencett5 = "write.table(rawtest[" + (i + 1) + ",3],\"C:/ZWHworksapce/newcreate/result.txt\",append=T,row.names = F, col.names=F,quote = F)";
                string Sentencett6 = "Start = Start + K";
                engine.Evaluate(Sentencett0);
                engine.Evaluate(Sentencett1);
                engine.Evaluate(Sentencett2);
                engine.Evaluate(Sentencett3);
                engine.Evaluate(Sentencett31);
                engine.Evaluate(Sentencett4);
                engine.Evaluate(Sentencett5);
                engine.Evaluate(Sentencett6);
            }
        }

        //public void CalCharAfter()
        //{
        //    MLApp.MLApp matlab = null;
        //    Type matlabAppType = System.Type.GetTypeFromProgID("Matlab.Application");
        //    matlab = System.Activator.CreateInstance(matlabAppType) as MLApp.MLApp;
        //    string command;
        //    //读入数据
        //    command = @"data=xlsread('C:\ZWHworksapce\newcreate\RawData.csv')";
        //    matlab.Execute(command);
        //    //删除第一行
        //    command = "data([1],:)=[]";
        //    matlab.Execute(command);
        //    //删除前两列
        //    command = "data(:,[1,3])=[]";
        //    matlab.Execute(command);
        //    //得到data行列
        //    command = "[m,n]=size(data)";
        //    matlab.Execute(command);
        //    //保存为矩阵
        //    command = "y=zeros(m,4)";
        //    matlab.Execute(command);
        //    //进行计算
        //    for (int i = 1; i < Raw.Rows.Count; i++)
        //    {
        //        command = "temp1=smooth(data(" + Convert.ToString(i) + ",:))";
        //        matlab.Execute(command);
        //        command = "temp2=diff(temp1)";
        //        matlab.Execute(command);
        //        command = "temp3=smooth(temp2)";
        //        matlab.Execute(command);
        //        command = "temp4=[temp3(750:1250)]";
        //        matlab.Execute(command);
        //        command = "[a,b]=findpeaks(temp4,'minpeakdistance',500)";
        //        matlab.Execute(command);
        //        command = "y(" + Convert.ToString(i) + ",1)=a";
        //        matlab.Execute(command);
        //        command = "y(" + Convert.ToString(i) + ",2)=b";
        //        matlab.Execute(command);
        //        command = "temp5=1-temp4";
        //        matlab.Execute(command);
        //        command = "[c,d]=findpeaks(temp5,'minpeakdistance',500)";
        //        matlab.Execute(command);
        //        command = "y(" + Convert.ToString(i) + ",3)=c";
        //        matlab.Execute(command);
        //        command = "y(" + Convert.ToString(i) + ",4)=d";
        //        matlab.Execute(command);
        //    }
        //    //将数据保存出去
        //    command = @"csvwrite('C:\ZWHworksapce\newcreate\RawCharacter2.csv',y)";
        //    matlab.Execute(command);
        //}


        private DataTable Raw;//原始训练集
        private DataTable Example;//原始样本集
        private DataTable RawAfter1;//一阶平滑求导后土壤库
        private DataTable ExampleAfter1;//一阶平滑求导后样本
        private double[,] pRaw;//临时土壤特征值数组
        private double[,] pExample;//临时样本特征值数组
        private double[, ,] pRawCharacter;//土壤特征值数组
        private double[, ,] pExampleCharacter;//土壤特征值数组
        private string pRawaddress;//样本集特征文件保存位置
        private string pExampleaddress;//测试集特征文件保存位置
        private int TC = 4;//特征数量
        private int TTT = 100;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

        

        
    }
}
