using RDotNet;

namespace SMSS.Data
{
    /// <summary>
    /// 初始化系统，加载必要文件
    /// </summary>
    class Initialize
    {
        /// <summary>
        /// 读入土壤剖面编号
        /// </summary>
        /// <returns></returns>
        public bool MainFormInitial()
        {
            InputData id = new InputData();
            if (MainFormContainers.ProfileID.Count > 0)//清空样本编号列表
                MainFormContainers.ProfileID.Clear();
            if (MainFormContainers.ukProfileID.Count > 0)//清空样本编号列表
                MainFormContainers.ukProfileID.Clear();
            MainFormContainers.ProfileID = id.ProfileList(Config.soilfolder);
            MainFormContainers.ukProfileID = id.ProfileList(Config.uksoilfolder);
            MainFormContainers.SoilType = id.InputCSV(Config.soilhabitat);
            id.DataExits();
            return true;
        }       
        /// <summary>
        /// 安装必要的R语言包
        /// </summary>
        /// <returns></returns>
        public bool InstallRpackages()
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Initialize();
            engine.Evaluate("install.packages(\"caret\")");
            engine.Evaluate("install.packages(\"sampling\")");
            return true;
        }
    }
}
