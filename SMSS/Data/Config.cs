using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSS.Data
{
    /// <summary>
    /// 配置参数类
    /// </summary>
    class Config
    {
        public static string configflie = AppDomain.CurrentDomain.BaseDirectory + "config.csv";//配置文件位置

        public static string SamplingR = AppDomain.CurrentDomain.BaseDirectory + @"R文件\Sampling.R";//R取样文件位置

        public static string CharaterR = AppDomain.CurrentDomain.BaseDirectory + @"R文件\Character.R";//特征值法R文件

        public static string workspace = "";//工作空间位置

        public static string pRawaddress = "";//识别样本特征文件存放位置

        public static string pExampleaddress = "";//待识别样本特征文件存放位置

        public static string soilfolder = "";//识别样本源文件

        public static string soilhabitat = "";//样本集合类型文件

        public static string soilhistogram = "";//样本集合直方图

        public static string uksoilhistogram = "";//测试集合直方图

        public static string uksoilfolder = "";//待识别样本源文件

        public static int MinWavelen = 350;//光谱采样波段最小值

        public static int MaxWavelen = 2481;//光谱采样波段最大值

        public static string SoilTrainFolder = " ";

        public static string TestFolder = "";//测试文件夹

        public static string TrainFolder = "";//样本文件夹

        public static string MatrixFolder = "";//匹配结果矩阵文件夹

    }
}
