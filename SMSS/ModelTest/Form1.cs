using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace RM_2
{
    public partial class Form1 : Form
    {
        public int k = 1;
        public string foldname = AppDomain.CurrentDomain.BaseDirectory + "SamplingData\\";
        List<string> TestList = new List<string>();
        List<string> TrainList = new List<string>();

        public Form1()
        {
            InitializeComponent();
            SampleList(foldname , checklistbox1); SampleList(foldname , checklistbox2);
            label3.Text = "共" + checklistbox1.Items.Count.ToString() + "组";
            label4.Text = "共" + checklistbox2.Items.Count.ToString() + "组";
            timer1.Start();
        }

        bool SampleList(string fold, CheckedListBox clb)
        {
            clb.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(fold);
            foreach (FileInfo file in di.GetFiles())
                clb.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
            return true;
        }

        /// <summary>
        /// 全选框
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="clb"></param>
        /// <returns></returns>
        bool AllSelect(CheckBox cb, CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, cb.Checked);
            }
            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AllSelect(checkBox1, checklistbox1);
            AllSelect(checkBox1, checklistbox2);
        }

        private bool FileList(out List<string> l, CheckedListBox clb)
        {
            l = new List<string>();
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (clb.GetItemChecked(i))
                    l.Add(foldname + clb.Items[i].ToString() + ".csv");
            }
            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FileList(out TestList, checklistbox1); FileList(out TrainList, checklistbox2);
            ReadFile.GetAnswer(TestList, TrainList);
            AccuForm af = new AccuForm();
            af.dataGridView1.DataSource = ReadFile.BriefAccu;
            af.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            RecParam rp = new RecParam();
            rp.Show();
        }

        /// <summary>
        /// 定时清内存
        /// </summary>
        /// <param name="process"></param>
        /// <param name="minSize"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        private void timer1_Tick(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private void checklistbox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < checklistbox2.Items.Count; i++)
            {
                checklistbox2.SetItemChecked(i, checklistbox1.GetItemChecked(i));
            }
        }

        private void checklistbox1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < checklistbox2.Items.Count; i++)
            {
                checklistbox2.SetItemChecked(i, checklistbox1.GetItemChecked(i));
            }
        }
    }
}
