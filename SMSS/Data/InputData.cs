using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SMSS.Data
{
    class InputData
    {
        /// <summary>
        /// 读取配置文件,配置位置重写
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ReadConfigFile(string path)
        {
            string strline;
            List<string> l = new List<string>();
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            while ((strline = sr.ReadLine()) != null)
            {
                string[] aryline = strline.Split(',');
                l.Add(aryline[1]);
            }
            Config.workspace = l[0];
            Config.pRawaddress = l[0] + @"\RawCharacter.csv";
            Config.pExampleaddress = l[0] + @"\ExpCharacter.csv";
            Config.soilhistogram = l[0] + @"\soilhistogram.csv";
            Config.uksoilhistogram = l[0] + @"\uksoilhistogram.csv";
            Config.soilfolder = l[1];
            Config.uksoilfolder = l[2];
            Config.soilhabitat = l[3];
            Config.SoilTrainFolder = l[4];
            Config.TestFolder = l[4] + @"\Test";
            Config.TrainFolder = l[4] + @"\Train";
            Config.MatrixFolder = l[4] + @"\Matrix";
            Config.MinWavelen = Convert.ToInt32(l[5]);
            Config.MaxWavelen = Convert.ToInt32(l[6]);
            return true;
        }
        /// <summary>
        /// 读取CSV文件，不超过370M
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public DataTable InputCSV(string path)
        {
            DataTable dt = new DataTable();
            if (File.Exists(path))
            {
                int ColCount = 0;
                bool Flag = true;
                string strline;
                StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding("gb2312"));
                while ((strline = sr.ReadLine()) != null)
                {
                    string[] aryline = strline.Split(new char[] { ',' });        //给datatable加上列名                    
                    if (Flag)                                           //填充数据并加入到datatable中
                    {
                        Flag = false;
                        ColCount = aryline.Length;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            DataColumn dc = new DataColumn(aryline[i], typeof(string));
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < ColCount; i++)                  //添加行数据到datatable中
                        {
                            if (i < aryline.Length)
                                dr[i] = aryline[i];
                            else
                                dr[i] = 0;                                  //超界，则全部赋值为零
                        }
                        dt.Rows.Add(dr);
                    }
                }
                sr.Close();
            }
            return dt;
        }
        /// <summary>
        /// 读取Excel文件，尽量不超过370M
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool InputExcel(string path, DataTable dt)
        {
            if (path != "")
            {
                string strConn = "";
                if (Path.GetExtension(path) == ".xlsx")
                    strConn = "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0; HDR=YES; IMEX=1;'";
                else if (Path.GetExtension(path) == ".xls")
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';Extended Properties='Excel 8.0;HDR=YES;'";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                OleDbDataAdapter myCommand = null;
                DataTable schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                //下面取得第一个表名   
                string strTableName = schema.Rows[0]["TABLE_NAME"].ToString();
                string strExcel = "select * from [" + strTableName + "]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(dt);
                conn.Close();
                MessageBox.Show("数据读取完成！");
                return true;
            }
            else
            {
                MessageBox.Show("文件地址为空！");
                return false;
            }
        }
        /// <summary>
        /// 根据文件获取剖面编号列表
        /// </summary>
        /// <param name="foldername"></param>
        /// <returns></returns>
        public List<string> ProfileList(string foldername)
        {
            List<string> l = new List<string>();
            if (Directory.Exists(foldername))
            {
                DirectoryInfo TheFolder = new DirectoryInfo(foldername);
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    string name = System.IO.Path.GetFileNameWithoutExtension(NextFile.FullName);
                    l.Add(name);
                }
            }
            return l;
        }
        /// <summary>
        /// 判断数据是否完备
        /// </summary>
        /// <returns></returns>
        public bool DataExits()
        {
            bool flag = true;
            string message = "";
            if (MainFormContainers.ProfileID.Count == 0)
            {
                message += "未找到样本集合数据\r\n";
                flag = false;
            }
            if (MainFormContainers.ProfileID.Count == 0)
            {
                message += "未找到测试集合数据\r\n";
                flag = false;
            }
            if (!File.Exists(Config.soilhabitat))
            {
                message += "未找到剖面类型数据";
                flag = false;
            }
            if (!flag)
                MessageBox.Show(message);
            return flag;
        }
    }
}
