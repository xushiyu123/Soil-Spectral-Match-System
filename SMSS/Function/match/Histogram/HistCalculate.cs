using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using SMSS.Data;

namespace SMSS.Function.match.Histogram
{
    class HistCalculate
    {
        public List<double> Histogram(DataTable dt, int groups)
        {
            List<double> l = new List<double>();
            int[] t = new int[groups];
            for (int i = 0; i < t.Length; i++)
                t[i] = 0;
            double intervals = 2.0 / Convert.ToDouble(groups - 1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 3; j < dt.Columns.Count; j++)
                {
                    t[(int)((Convert.ToDouble(dt.Rows[i][j]) - -1.0) / intervals)]++;
                }
            }
            for (int i = 0; i < t.Length; i++)
                l.Add(Convert.ToDouble(t[i]) / (Convert.ToDouble(dt.Rows.Count) * Convert.ToDouble(dt.Columns.Count - 3)));
            return l;
        }

        public bool HistCharacter(string foldername,string filename,int groups)
        {
            if (Directory.Exists(foldername))
            {
                InputData id = new InputData();
                DirectoryInfo TheFolder = new DirectoryInfo(foldername);
                if (File.Exists(filename)) File.Delete(filename);
                StreamWriter sw = new StreamWriter(new FileStream(filename, FileMode.CreateNew), System.Text.Encoding.Default);
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    AuxiliaryFunc.percent += 50 / TheFolder.GetFiles().Length;
                    DataTable dt = id.InputCSV(NextFile.FullName);
                    List<double> l = Histogram(dt, groups);
                    string strline = System.IO.Path.GetFileNameWithoutExtension(NextFile.FullName);
                    foreach (double item in l)
                    {
                        strline += "," + item.ToString();
                    }
                    sw.WriteLine(strline);
                }
                sw.Close();
            }
            return true;
        }
    }
}
