using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;

namespace SMSS.Data
{
    public class OutputData
    {
        static double intervals = 0;
        /// <summary>
        /// 保存表格
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        public static void ExportExcel(string fileName, DataTable dt1, DataTable dt2, DataTable dt3, string[] SheetName)
        {
            if (dt1.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                AuxiliaryFunc.percent = 0;
                string saveFileName = "";
                Savefile(fileName, out saveFileName,".xls");
                if (saveFileName.IndexOf(":") < 0) return; //被点了取消 
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的设备未安装Excel");
                    return;
                }
                xlApp.SheetsInNewWorkbook = 3;
                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add();
                intervals = 100.0 / (dt1.Rows.Count + dt2.Rows.Count + dt3.Rows.Count); 
                WriteSheet(dt1, (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1]);               
                WriteSheet(dt2, (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[2]);
                WriteSheet(dt3, (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[3]);
                for (int i = 1; i < 4; i++)
                {
                    workbook.Worksheets[i].Name = SheetName[i - 1];
                }
                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }

                }
                xlApp.Quit();
                GC.Collect();//强行销毁   
                MessageBox.Show(fileName + "的结果保存成功", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("结果为空,无结果需要导出", "提示", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 写入表单
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="worksheet"></param>
        private static bool WriteSheet(DataTable dt, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                }
                //写入数值  
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    AuxiliaryFunc.percent += intervals;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                    }
                    Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应 
            }
            return true;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="chart"></param>
        public static bool saveChart(string filename, Chart chart)
        {
            if (chart.Width > 0)
            {
                string saveFileName = "";
                Savefile(filename, out saveFileName,".jpg");
                chart.SaveImage(saveFileName, ChartImageFormat.Jpeg);
                return true;
            }
            else
            {
                MessageBox.Show("图表为空！");
                return false;
            }
        }
        /// <summary>
        /// 生成保存文件的文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="saveFileName"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        private static bool Savefile(string fileName, out string saveFileName,string ext)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ext;
            saveDialog.Filter = "所有文件(*.*)|*.*";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            return true;
        }
        /// <summary>
        /// 将结果保存为CSV文件
        /// </summary>
        /// <param name="foldname"></param>
        /// <param name="filename"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool ExportToCsv(string foldname,string filename,DataTable dt)
        {
            string strPath = foldname + "\\" + filename + ".csv";
            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            }
            //先打印标头
            StringBuilder strColu = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            int i = 0;
            try
            {
                StreamWriter sw = new StreamWriter(new FileStream(strPath, FileMode.CreateNew), Encoding.GetEncoding("GB2312"));
                for (i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    strColu.Append(dt.Columns[i].ColumnName);
                    strColu.Append(",");
                }
                strColu.Remove(strColu.Length - 1, 1);//移出掉最后一个,字符
                sw.WriteLine(strColu);
                foreach (DataRow dr in dt.Rows)
                {
                    strValue.Remove(0, strValue.Length);//移出
                    for (i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        strValue.Append(dr[i].ToString());
                        strValue.Append(",");
                    }
                    strValue.Remove(strValue.Length - 1, 1);//移出掉最后一个,字符
                    sw.WriteLine(strValue);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }
        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SaveConfigFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.CreateNew), Encoding.GetEncoding("GB2312"));
            string strline = "";
            strline = "工作空间," + Config.workspace;
            sw.WriteLine(strline);
            strline = "样本集合位置," + Config.soilfolder;
            sw.WriteLine(strline);
            strline = "测试集合位置," + Config.uksoilfolder;
            sw.WriteLine(strline);
            strline = "类型数据位置," + Config.soilhabitat;
            sw.WriteLine(strline);
            strline = "训练位置," + Config.SoilTrainFolder.ToString();
            sw.WriteLine(strline);
            strline = "波长最小值," + Config.MinWavelen.ToString();
            sw.WriteLine(strline);
            strline = "波长最大值," + Config.MaxWavelen.ToString();
            sw.WriteLine(strline);
            sw.Close();
            return true;
        }
        /// <summary>
        /// 配置分层抽样R脚本文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="times">抽样次数</param>
        /// <param name="rate">测试集比例</param>
        /// <returns></returns>
        public bool WriteR(string path, int times, double rate)
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            int rows = 0;
            string[] strlines = new string[50];
            string strline = "";
            while ((strline = sr.ReadLine()) != null)
            {
                strlines[rows] = strline;
                rows++;
            }
            sr.Close();
            File.Delete(path);//删除原文件
            strlines[0] = "times = " + times.ToString();//修改配置信息
            strlines[1] = "rate = " + rate.ToString();
            strlines[2] = "soilhabitat = \"" + Config.soilhabitat.Replace("\\", "/") + "\"";
            strlines[3] = "trainfile = \"" + (Config.TrainFolder + "\\").Replace("\\", "/") + "Train\"";
            strlines[4] = "testfile = \"" + (Config.TestFolder + "\\").Replace("\\", "/") + "Test\"";
            StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.CreateNew), System.Text.Encoding.Default);
            for (int i = 0; i < rows; i++)//重写脚本文件
            {
                sw.WriteLine(strlines[i]);
            }
            sw.Close();
            return true;
        }
    }
}
