using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SMSS.Data
{
    class FreeContainer
    {
        /// <summary>
        /// 清空数据表数据
        /// </summary>
        /// <param name="dt"></param>
        public static bool FreeTable(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Clear();
            }
            if (dt.Columns.Count > 0)
            {
                dt.Columns.Clear();
            }
            return true;
        }
        /// <summary>
        /// 清空视图数据
        /// </summary>
        /// <param name="tv"></param>
        public static bool FreeTree(TreeView tv)
        {
            if (tv.Nodes.Count > 0)
            {
                tv.Nodes.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 清空目录树子节点
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool FreeTreeNode(TreeView tv, int id)
        {
            if (tv.Nodes[id].Nodes.Count > 0)
            {
                tv.Nodes[id].Nodes.Clear();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 清空目录树
        /// </summary>
        /// <param name="dgv"></param>
        public static bool FreeGrid(DataGridView dgv)
        {
            if (dgv.DataSource != null)
            {
                dgv.DataSource = null;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 引入内存清理项
        /// </summary>
        /// <param name="process"></param>
        /// <param name="minSize"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }
}
