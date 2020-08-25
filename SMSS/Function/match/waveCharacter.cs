using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace 系统改良
{
    class waveCharacter
    {
        public waveCharacter(DataTable dt,DataTable et,string st)
        {
            Raw = dt.Copy();
            RawAfter1 = dt.Copy();
            RawAfter2 = dt.Copy();
            Example = et.Copy();
            ExampleAfter1 = et.Copy();
            ExampleAfter2 = et.Copy();
            T = Raw.Rows.Count;
            pRawaddress = st;
        }

        public void Derivation(DataTable ddd,ref DataTable result,bool tt,int K)//平滑并求导,tt为真则是对土壤库求导，否则是对样本库求导,K为放大倍数
        {
            DataTable temp=new DataTable();
            Smooth sth = new Smooth();
            sth.PlotStore(ref ddd, temp);
            if(tt)
            {
                for (int i = 1; i <T /*ddd.Rows.Count*/; i++)
                {
                    for (int j = 8; j < temp.Columns.Count; j++)
                    {
                        //计算曲线各个位置上的一阶导数（用差分代替微分）
                        result.Rows[i][j] = K*(Convert.ToDouble(temp.Rows[i][j]) - Convert.ToDouble(temp.Rows[i][j - 1]));
                    }
                    result.Rows[i][7] = 0;//单独给第一个点赋值
                }             
            }
            else
            {
                for (int i = 0; i < ddd.Rows.Count; i++)
                {
                    for (int j = 1; j < temp.Columns.Count; j++)
                    {
                        //计算曲线各个位置上的一阶导数（用差分代替微分并放大）
                        result.Rows[i][j] = K*((Convert.ToDouble(temp.Rows[i][j]) - Convert.ToDouble(temp.Rows[i][j - 1])));
                    }
                    result.Rows[i][0] = 0;//单独给第一个点赋值
                }     
            }
        }

        public void FindPeak(DataTable ddd,ref DataTable dtt,double[,,] pwavecharacter,bool tt)//tt为真则是对土壤库求峰，否则是对样本库求峰
        {
            DataTable temp = ddd.Copy();
            int KT=(tt)?T:ddd.Rows.Count;
            for (int i=(tt)?1:0; i < KT; i++)
            {
                for (int j=(tt)?7:0; j < ddd.Columns.Count; j++)
                {
                    double ttt = Convert.ToDouble(ddd.Rows[i][j]);
                    if (Math.Abs(ttt) < 0.1)
                    {
                        temp.Rows[i][j] = 0;
                    }
                    else if (ttt > 0)
                    {
                        temp.Rows[i][j] = 1;
                    }
                    else
                    {
                        temp.Rows[i][j] = -1;
                    }
                }
            }
            //结果保存在csv文件中
            string s = pRawaddress;
            StreamWriter pStreamWriter = new StreamWriter(new FileStream(s, FileMode.Append));
            //初始化pwavecharacter数组
            pwavecharacter = new double[KT, 9, 2];
            for (int m = 0; m < KT; m++)
            {
                for (int n = 0; n < 9; n++)
                {
                    pwavecharacter[m, n, 0] = 0;
                    pwavecharacter[m, n, 1] = 0;
                }
            }
            for (int h = (tt)?1:0; h < KT; h++)
            {
                for (int j = 50; j < 1600; j++)
                {
                    if (Convert.ToInt32(temp.Rows[h][j]) == 0)
                    {
                        bool flag = true;
                        for (int k = -20; k < 20; k++)
                        {
                            if (k == 0) continue;
                            else
                            {
                                if (Convert.ToInt32(temp.Rows[h][j + k]) == 0)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (flag)
                        {
                            int num = j + 350;
                            //输入到pwavecharacter数组中
                            if (num >= 380 && num <= 400)
                            {
                                pwavecharacter[h, 0, 0] = num;
                                pwavecharacter[h, 0, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if(num>=410&&num<=430)
                            {
                                pwavecharacter[h, 1, 0] = num;
                                pwavecharacter[h, 1, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if(num>=440&&num<=460)
                            {
                                pwavecharacter[h, 2, 0] = num;
                                pwavecharacter[h, 2, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if (num >= 470 && num <= 490)
                            {
                                pwavecharacter[h, 3, 0] = num;
                                pwavecharacter[h, 3, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if (num >= 560 && num <= 590)
                            {
                                pwavecharacter[h, 4, 0] = num;
                                pwavecharacter[h, 4, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if (num >= 1400 && num <= 1410)
                            {
                                pwavecharacter[h, 5, 0] = num;
                                pwavecharacter[h, 5, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if (num >= 1420 && num <= 1450)
                            {
                                pwavecharacter[h, 6, 0] = num;
                                pwavecharacter[h, 6, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if (num >= 1900 && num <= 1920)
                            {
                                pwavecharacter[h, 7, 0] = num;
                                pwavecharacter[h, 7, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else if (num >= 1930 && num <= 1950)
                            {
                                pwavecharacter[h, 8, 0] = num;
                                pwavecharacter[h, 8, 1] = Convert.ToDouble(dtt.Rows[h][j]);
                            }
                            else
                            { }
                        }
                    }
                }
            }
            pStreamWriter.WriteLine("Code,1Position,1Value,2Position,2Value,3Position,3Value,4Position,4Value,5Position,5Value,6Position,6Value,7Position,7Value,8Position,8Value,9Position,9Value,");
            for (int m = 0; m < KT; m++)
            {
                pStreamWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}", m + 1, pwavecharacter[m, 0, 0], pwavecharacter[m, 0, 1], pwavecharacter[m, 1, 0], pwavecharacter[m, 1, 1], pwavecharacter[m, 2, 0], pwavecharacter[m, 2, 1], pwavecharacter[m, 3, 0], pwavecharacter[m, 3, 1], pwavecharacter[m, 4, 0], pwavecharacter[m, 4, 1], pwavecharacter[m, 5, 0], pwavecharacter[m, 5, 1], pwavecharacter[m, 6, 0], pwavecharacter[m, 6, 1], pwavecharacter[m, 7, 0], pwavecharacter[m, 7, 1], pwavecharacter[m, 8, 0], pwavecharacter[m, 8, 1]);
            }
            pStreamWriter.Close();
        }

        public void CalRaw()
        {
            Derivation(Raw, ref RawAfter1, true, 100);
            DataTable temp = new DataTable();
            Smooth sth = new Smooth();
            sth.PlotStore(ref RawAfter1, temp);;
            Derivation(RawAfter1, ref RawAfter2, true, 10);
            FindPeak(RawAfter2, ref temp,pRawcharacter, true);    
        }




        public void CalExample()
        {
            Derivation(Example, ref ExampleAfter1, false, 100);
            DataTable temp = new DataTable();
            Smooth sth = new Smooth();
            sth.PlotStore(ref ExampleAfter1, temp); ;
            Derivation(ExampleAfter1, ref ExampleAfter2, true, 10);
            FindPeak(ExampleAfter2, ref temp, pexamplecharacter, false); 
        }

        public void Compare()
        {

        }


        

        private DataTable Raw;//原始土壤数据库
        private DataTable RawAfter1;//一阶平滑求导后土壤库
        private DataTable RawAfter2;//二阶平滑求导后土壤库
        private DataTable Example;//原始样本
        private DataTable ExampleAfter1;//一阶平滑求导后样本
        private DataTable ExampleAfter2;//二阶平滑求导后样本
        private double[,,] pRawcharacter;//土壤特征值数组
        private double[, ,] pexamplecharacter;//样本特征值数组
        private int T ;
        private string pRawaddress;//数组文件保存位置
    }
}
