using System.Windows.Forms;
using System.Collections.Generic;

namespace SMSS.Windows.Windows_Refresh
{
    /// <summary>
    /// 更新目录树
    /// </summary>
    class AddTree
    {
        /// <summary>
        /// 更新一级子节点
        /// </summary>
        public static bool AddNode(string str,TreeView tv,int index)
        {
            if (tv.Nodes[index].Nodes.Count > 0)
            {
                tv.Nodes[index].Nodes.Clear();
            }
            tv.Nodes[index].Nodes.Add(str);
            tv.Nodes[index].Expand();
            return true;
        }
        /// <summary>
        /// 更新二级子节点
        /// </summary>
        /// <param name="l"></param>
        /// <param name="tv"></param>
        /// <param name="index1"></param>
        /// <returns></returns>
        public static bool AddNodes(List<string> l,TreeView tv,int index1)
        {
            if (l.Count == 0)
            {
                return false;
            }
            else 
            {
                if (tv.Nodes[index1].Nodes.Count > 0)
                {
                    tv.Nodes[index1].Nodes.Clear();
                }
                for (int i = 0; i < l.Count; i++)
                {
                    tv.Nodes[index1].Nodes.Add(l[i]);
                    tv.Nodes[index1].Nodes[i].ImageIndex = 1;
                }
                tv.Nodes[index1].Expand();
                return true;
            }
        }
    }
}
