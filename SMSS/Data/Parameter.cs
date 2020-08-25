using System.Data;

namespace SMSS.Data
{
    class Parameter
    {
        /// <summary>
        /// 获取土壤剖面样本的基本信息
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public static DataTable BriefInformation(string function)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < 2; i++)
            {
                DataColumn dc = new DataColumn("列" + (i+1).ToString());
                dt.Columns.Add(dc);
            }
            DataRow r1 = dt.NewRow();
            r1[0] = "匹配方法";
            r1[1] = function;
            dt.Rows.Add(r1);
            DataRow r2 = dt.NewRow();
            r2[0] = "匹配时间";
            r2[1] = System.DateTime.Today.ToString();
            dt.Rows.Add(r2);
            return dt;
        }
    }
}
