using System;
using System.IO;
using System.Windows.Forms; 

namespace SMSS.Data
{
    class FileBrowser
    {
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <returns></returns>
        public static string GetFile()
        {
            string path = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
            }
            return path;
        }
        /// <summary>
        /// 获取文件夹名称
        /// </summary>
        /// <returns></returns>
        public static string GetFolder()
        {
            string folder = "";
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                folder = fdb.SelectedPath;
            }
            return folder;
        }
        /// <summary>
        /// 获取选择文件的大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int FileSize(string path)
        {
            int size = 0;
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            size = Convert.ToInt32(fi.Length);
            return size;
        }
        /// <summary>  
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹  
        /// </summary>  
        /// <param name="sourcePath">源文件夹</param>  
        /// <param name="destPath">目标文件夹</param>  
        public void MoveFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建  
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件  
                foreach (string file in Directory.GetFiles(sourcePath))
                {
                    string destFile = destPath + Path.GetFileName(file);
                    //覆盖模式  
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Move(file, destFile);
                }
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }
        /// <summary>
        /// 遍历文件夹删除文件
        /// </summary>
        /// <param name="foldname"></param>
        /// <returns></returns>
        public bool DeleteFiles(string foldname)
        {
            foreach (string file in Directory.GetFiles(foldname))
            {
                string destFile = foldname + "\\"  + Path.GetFileName(file); 
                if (File.Exists(destFile))
                {
                    File.Delete(destFile);
                }
            }
            return true;
        }
        /// <summary>
        /// 根据选择文件夹获取文件数量
        /// </summary>
        /// <param name="foldname"></param>
        /// <returns></returns>
        public int FilesCount(string foldname)
        {
            return Directory.GetFiles(foldname).Length;
        }
    }
}
