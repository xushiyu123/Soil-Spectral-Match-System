using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SMSS.Data
{
    /// <summary>
    /// 一些辅助函数
    /// </summary>
    class AuxiliaryFunc
    {
        public static double percent = 0;

        /// <summary>
        /// 判断文本框输入的字符串是否为纯数字，为纯数字返回true，否则返回false
        /// </summary>
        /// <param name="text">文本框的字符串</param>
        /// <returns></returns>
        public bool TextIsNum(string text)
        {
            Regex reg = new Regex("^[0-9]+$");              //判断是否为数字
            Match ma = reg.Match(text);
            if (ma.Success)
            {
                return true;
            }
            else {
                return false;    
            }
        }
        /// <summary>
        /// 根据选择文件夹获取文件列表
        /// </summary>
        /// <param name="l"></param>
        /// <param name="foldname"></param>
        /// <param name="clb"></param>
        /// <returns></returns>
        public bool FileList(out List<string> l,string foldname ,CheckedListBox clb)
        {
            l = new List<string>();
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (clb.GetItemChecked(i))
                    l.Add(foldname + "\\" + clb.Items[i].ToString() + ".csv");
            }
            return true;
        }
    }
}
