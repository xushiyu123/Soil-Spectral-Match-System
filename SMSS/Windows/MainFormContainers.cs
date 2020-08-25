using System.Collections.Generic;
using System.Data;

namespace SMSS
{
    class MainFormContainers
    {
        #region 容器 
        /// <summary>
        /// 容器定义
        /// </summary>
        public static DataTable Spectral = new DataTable();//光谱临时表，用于查看
        public static DataTable Testset = new DataTable();
        public static DataTable Trainset = new DataTable();
        public static DataTable SoilType = new DataTable();
        public static List<string> ProfileID = new List<string>();
        public static List<string> ukProfileID = new List<string>();
        public static List<string> TestPro = new List<string>();
        #endregion
    }
}
