using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using SMSS.Data;

namespace SMSS.Windows.Windows_Refresh
{ 
    /// <summary>
    /// 更新主界面土壤光谱表格区域
    /// </summary>
    class GetView
    {
        public static int Page = 1;
        public static int MaxPage = (Config.MaxWavelen - Config.MinWavelen + 3) / 100 + 1;//最大页码

        public static void GetGridView(DataTable tb,DataGridView view)
        {          
            try
            {
                view.DataSource = null;
                DataTable ntb = new DataTable();
                bool Flag = true;
                DataRow dr;
                int cols = 100;               
                int FirstCol = (Page - 1) * 100;
                int LastCol = Page * 100;
                if (LastCol > tb.Columns.Count) LastCol = tb.Columns.Count;
                if ((tb.Columns.Count - FirstCol) < 100) cols = tb.Columns.Count - FirstCol;

                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    if (Flag)
                    {
                        Flag = false;
                        for (int k = 0; k < 3; k++)
                        {
                            DataColumn dc = new DataColumn(tb.Columns[k].ColumnName);
                            ntb.Columns.Add(dc);
                        }
                        for (int k = FirstCol + 3; k < LastCol; k++)
                        {
                            DataColumn dc = new DataColumn(tb.Columns[k].ColumnName);
                            ntb.Columns.Add(dc);
                        }
                    }
                    dr = ntb.NewRow();
                    dr[0] = tb.Rows[i][0];
                    dr[1] = tb.Rows[i][1];
                    dr[2] = tb.Rows[i][2];
                    for (int j = FirstCol + 3; j < LastCol; j++)
                    {
                        dr[j - FirstCol] = tb.Rows[i][j];
                    }
                    ntb.Rows.Add(dr);
                }
                view.DataSource = ntb;
            }
            catch (Exception e) 
            {
                throw(e);
            }
        }
        /// <summary>
        /// 目录树全选触发函数
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="clb"></param>
        /// <returns></returns>
        public static bool AllSelect(CheckBox cb, CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, cb.Checked);
            }
            return true;
        }
        /// <summary>
        /// 根据目录树选择项生成下拉菜单
        /// </summary>
        /// <param name="l"></param>
        /// <param name="clb"></param>
        /// <returns></returns>
        public static bool ListBox(List<string> l, CheckedListBox clb)
        {
            clb.Items.Clear();
            for (int i = 0;i<l.Count;i++)
                clb.Items.Add(l[i]);
            return true;
        }
        /// <summary>
        /// 根据目录树选择项生成剖面列表
        /// </summary>
        /// <param name="l"></param>
        /// <param name="clb"></param>
        /// <returns></returns>
        public static bool ProfileList(out List<string> l, CheckedListBox clb)
        {
            l = new List<string>();
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (clb.GetItemChecked(i))
                    l.Add(clb.Items[i].ToString());
            }
            return true;
        }
    }

}
