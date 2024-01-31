using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    [Serializable]
    public class SystemConfig : ObservableObject
    {
        #region 单例
        private static SystemConfig instance = new SystemConfig();
        private SystemConfig() { }
        public static SystemConfig Ins
        {
            get { return instance; }
            set { instance = value; }
        }
        #endregion
        #region 字段
        private bool autoLoadLayout;
        //private string _CurrentCultureName = LanguageNames.Chinese;
        private int _SoftwareVersion;
        private bool _SolutionAutoLoad;
        private bool _SolutionAutoRun;
        private string _SolutionPathText;
        private bool _SoftwareAutoStartup;
        private int _CurrentRecipeIndex;
        private string _CurrentRecipe = "PersistentVar.rep";
        private string _CurrentVersion;
        private string _CompanyName = "有限公司";
        private string _ProjectName = "准同步设备";
        private string _ComputerName = "有限公司";
        private string _SoftwareIcoPath = FilePaths.DefultSoftwareIcon;
        private int _TotalNum;
        private uint _UltralimitTime;
        private int _OKNum;
        private int _NGNum;
        private double _Yield;
        private bool _IsShieldBuzzer = false;

        #endregion
        #region 属性
        /// <summary>
        /// 自动加载布局
        /// </summary>
        public bool AutoLoadLayout
        {
            get { return autoLoadLayout; }
            set => SetProperty(ref autoLoadLayout, value);
        }
        /// <summary>
        /// 当前语言
        /// </summary>
        //public string CurrentCultureName
        //{
        //    get { return _CurrentCultureName; }
        //    set { SetProperty(ref _CurrentCultureName, value); }
        //}
        /// <summary>
        /// 软件版本号
        /// </summary>
        public int SoftwareVersion
        {
            get { return _SoftwareVersion; }
            set { SetProperty(ref _SoftwareVersion, value); }
        }
        /// <summary>
        /// 方案是否自动加载
        /// </summary>
        public bool SolutionAutoLoad
        {
            get { return _SolutionAutoLoad; }
            set { SetProperty(ref _SolutionAutoLoad, value); }
        }
        /// <summary>
        /// 方案是否自动运行
        /// </summary>
        public bool SolutionAutoRun
        {
            get { return _SolutionAutoRun; }
            set { SetProperty(ref _SolutionAutoRun, value); }
        }
        /// <summary>
        /// 方案链接路径
        /// </summary>
        public string SolutionPathText
        {
            get { return _SolutionPathText; }
            set { SetProperty(ref _SolutionPathText, value); }

        }
        /// <summary>
        /// 软件是否自动打开
        /// </summary>
        public bool SoftwareAutoStartup
        {
            get { return _SoftwareAutoStartup; }
            set { SetProperty(ref _SoftwareAutoStartup, value); }
        }

        /// <summary>
        /// 当前配方序号
        /// </summary>
        public int CurrentRecipeIndex
        {
            get { return _CurrentRecipeIndex; }
            set { SetProperty(ref _CurrentRecipeIndex, value); }
        }

        /// <summary>
        /// 当前配方名称
        /// </summary>
        public string CurrentRecipe
        {
            get { return _CurrentRecipe; }
            set { SetProperty(ref _CurrentRecipe, value); }
        }

        /// <summary>
        /// 当前软件版本
        /// </summary>
        public string CurrentVersion
        {
            get { return _CurrentVersion; }
            set { SetProperty(ref _CurrentVersion, value); }
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return _CompanyName; }
            set { SetProperty(ref _CompanyName, value); }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return _ProjectName; }
            set { SetProperty(ref _ProjectName, value); }
        }

        /// <summary>
        /// 电脑名
        /// </summary>
        public string ComputerName
        {
            get { return _ComputerName; }
            set { SetProperty(ref _CompanyName, value); }
        }

        /// <summary>
        /// 软件图标
        /// </summary>
        public string SoftwareIcoPath
        {
            get { return _SoftwareIcoPath; }
            set { SetProperty(ref _SoftwareIcoPath, value); }
        }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalNum
        {
            get { return _TotalNum; }
            set { SetProperty(ref _TotalNum, value); }
        }
        /// <summary>
        /// OK数量
        /// </summary>
        public int OKNum
        {
            get { return _OKNum; }
            set { SetProperty(ref _OKNum, value); }
        }
        /// <summary>
        /// NG数量
        /// </summary>
        public int NGNum
        {
            get { return _NGNum; }
            set { SetProperty(ref _NGNum, value); }
        }
        /// <summary>
        /// 良率
        /// </summary>
        public double Yield
        {
            get { return _Yield; }
            set { SetProperty(ref _Yield, value); }
        }
        /// <summary>
        /// 屏蔽蜂鸣器
        /// </summary>
        [JsonIgnore]
        public bool IsShieldBuzzer
        {
            get { return _IsShieldBuzzer; }
            set { SetProperty(ref _IsShieldBuzzer, value); }
        }
        public uint UltralimitTime
        {
            get { return _UltralimitTime; }
            set { SetProperty(ref _UltralimitTime, value); }
        }
        #endregion
        #region 方法
        public void LoadSystemConfig()
        {
            Ins = SerializeHelp.Deserialize<SystemConfig>(FilePaths.SystemConfig, true);
            if (Ins == null)
            {
                Ins = new SystemConfig();
                SaveSystemConfig();
            }
        }
        private static object Lock_SaveSystemConfig = new object();

        public void SaveSystemConfig()
        {
            lock (Lock_SaveSystemConfig)
            {
                SerializeHelp.SerializeAndSaveFile(SystemConfig.Ins, FilePaths.SystemConfig, true);
            }
        }
        #endregion

    }
}
