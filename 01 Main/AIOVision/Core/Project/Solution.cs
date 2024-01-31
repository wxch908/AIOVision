//using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HalconDotNet;

namespace AIOVision
{
    [Serializable]
    public class Solution : NotifyPropertyBase
    {
        #region 单例模式
        private static Solution _Instance = null;

        public Solution() { }

        public static Solution Ins
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Solution();
                }
                return _Instance;
            }
            set { _Instance = value; }
        }
        #endregion
        #region 字段
        /// <summary>
        /// 项目名称 
        /// </summary>
        public string SolutionName = string.Empty;

        /// <summary>
        /// 项目路径地址
        /// </summary>
        public string SolutionPath = string.Empty;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string SysFliePath = string.Empty;

        public string ConfigPath = @"NExtVision.nv";

        /// <summary>
        /// 急速模式
        /// </summary>
        public bool RushMode { get; set; } = false;

        /// <summary>
        /// 项目工程列表
        /// </summary>
        public List<Project> g_ProjectList = new List<Project>();

        /// <summary>
        /// 当前项目
        /// </summary>
        public Project Cur_Project = null;

        /// <summary>
        /// 采集设备列表
        /// </summary>
        ///public List<CameraBase> g_CameraList = new List<CameraBase>();

        /// <summary>
        /// 全局变量
        /// </summary>
        public List<DataVar> g_VarList = new List<DataVar>();

        /// <summary>
        /// 是否保存全局变量值
        /// </summary>
        public bool SaveUpValue { get; set; } = false;

        /// <summary>
        /// 显示ROI集合
        /// </summary>
        public List<ModuleROI> g_moduleRoiList = new List<ModuleROI>();

        /// <summary>
        /// 通讯集合
        /// </summary>
        //public List<ECommunacation> g_CommunCation = new List<ECommunacation>();

        /// <summary>
        /// 全局唯一引擎 所以使用静态
        /// </summary>
        [NonSerialized]
        public HDevEngine s_HDevEngine = new HDevEngine();

        /// <summary>
        /// 系统运行状态
        /// </summary>
        public SysStatus g_SysStatus = new SysStatus();

        /// <summary>
        /// 系统流程间隔时间
        /// </summary>
        public int SysInterval;
        #endregion 字段
        #region 属性
        private bool isUseUIDesign = false;
        public bool IsUseUIDesign
        {
            get { return this.isUseUIDesign; }
            set => SetProperty(ref isUseUIDesign, value); 
        }

        #endregion 属性
        #region 方法
        public void LoadCommunacation()
        {
            //if (eCommunacations == null)
            //    return;
            //foreach (var item in eCommunacations)
            //{
            //    item.IsConnected = false;
            //    item.IsHasObjectConnected = false;
            //}
            //EComManageer.setEcomList(eCommunacations); //将反序列化的LIST转为字典，并连接通信设备
        }
        /// <summary>
        /// 解决方案所有项目执行一次
        /// </summary>
        public void Sys_Run_Once()
        {
            Sys_Start(Solution.Ins.g_SysStatus.m_RunMode);
        }

        /// <summary>
        /// 解决方案所有项目循环运行
        /// </summary>
        public void Sys_Run_Cycle()
        {
            Solution.Ins.g_SysStatus.m_RunMode = RunMode.循环运行;
            Sys_Start(Solution.Ins.g_SysStatus.m_RunMode);
        }

        /// <summary>
        /// 系统启动
        /// </summary>
        public void Sys_Start(RunMode runMode)
        {
            for (int i = 0; i < g_ProjectList.Count; i++)
            {
                //判断流程状态,只有主动执行状态下才能够运行
                if (g_ProjectList[i].ProjectInfo.m_Execution == Execution.主动执行)
                {
                    Sys_Start(i, runMode);
                }
            }
        }

        /// <summary>
        /// 指定项目循环运行
        /// </summary>
        /// <param name="index"></param>
        public void Sys_Start(int index, RunMode runMode)
        {
            Sys_ThreadStart(index, runMode);
        }

        public void Sys_ThreadStart(int index, RunMode runMode)
        {
            if (index > Solution.Ins.g_ProjectList.Count - 1)
            {
                return;
            }

            if (Solution.Ins.g_ProjectList[index].m_ModuleObjList.Count == 0)
            {
                //Log.Error(Solution.Ins.g_ProjectList[index].ProjectInfo.m_ProjectName + "无任何模块");
                return;
            }

            foreach (ModuleObjBase item in Solution.Ins.g_ProjectList[index].m_ModuleObjList)
            {
                if (item.ModuleParam.ModuleName.Contains("采集图像"))
                {
                    item.Stop();
                }
                else if (item.ModuleParam.ModuleName.Contains("时间"))
                {
                    item.Stop();
                }
                else if (item.ModuleParam.ModuleName.Contains("文本接收"))
                {
                    item.Stop();
                }
                else if (item.ModuleParam.ModuleName.Contains("数据出队"))
                {
                    item.Stop();
                }
            }

            Solution.Ins.g_ProjectList[index].run = runMode;//流程模块写入
            Solution.Ins.g_ProjectList[index].Thread_Start();
        }
        /// <summary>
        /// 系统检测停止
        /// </summary>
        public void Sys_Stop(int index)
        {
            try
            {
                if (index > Solution.Ins.g_ProjectList.Count - 1)
                {
                    return;
                }

                Solution.Ins.g_ProjectList[index].Thread_Stop();

                foreach (ModuleObjBase item in Solution.Ins.g_ProjectList[index].m_ModuleObjList)
                {
                    if (item.ModuleParam.ModuleName.Contains("采集图像"))
                    {
                        item.Stop();
                    }
                    else if (item.ModuleParam.ModuleName.Contains("时间"))
                    {
                        item.Stop();
                    }
                    else if (item.ModuleParam.ModuleName.Contains("文本接收"))
                    {
                        item.Stop();
                    }
                    else if (item.ModuleParam.ModuleName.Contains("数据出队"))
                    {
                        item.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Measure.HMeasureSYS.Sys_Stop:" + ex.ToString());
            }
        }
        /// <summary>
        /// 根据名称获取模块
        /// </summary>
        /// <returns></returns>
        public ModuleObjBase GetModuleByName(Project prj, string moduelName, int moudleID)
        {
            return prj.m_ModuleObjList.FirstOrDefault(c => c.ModuleParam.ModuleName == moduelName);
        }
        /// <summary>
        /// 保存项目文件
        /// </summary>
        /// <param name="filePath">保存地址</param>
        public void SaveConfig(string filePath)
        {
            string ThePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string temFile = "NE.nv";
            temFile = System.IO.Path.Combine(ThePath, temFile);
            try
            {
                System.GC.Collect();
                using (FileStream fs = new FileStream(temFile, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, Solution.Ins);
                    //fs.Seek(0, SeekOrigin.Begin);
                    //formatter.Serialize(fs, SysProcessPro.g_VarList);//变量

                    //formatter.Serialize(fs, SysProcessPro.g_CameraList);//相机
                    //formatter.Serialize(fs, CameraBase.m_LastDeviceID);

                    //formatter.Serialize(fs, SysProcessPro.g_ProjectList);//模块
                    //formatter.Serialize(fs, Project.m_LastProjectID);

                    //g_CommunCation = EComManageer.GetEcomList();//通讯
                    //formatter.Serialize(fs, SysProcessPro.g_CommunCation);

                    //formatter.Serialize(fs, SysLayout.LayoutFrmNum);//窗体布局
                }

                string outPath = filePath;
                if (filePath.Contains(@":\") == false)
                {
                    outPath = System.IO.Path.Combine(ThePath, filePath);
                }

                File.Copy(temFile, outPath, true);

                //System.GC.Collect();//主动回收下系统未使用的资源
            }
            catch (Exception ex)
            {
                //Log.Error(ex.ToString());
                System.Windows.Forms.MessageBox.Show("保存配置文件失败：" + ex.ToString());
            }
        }
        /// <summary>
        /// 初始化视觉工程文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool InitialVisionPram(string filepath = @"NE.nv")
        {
            try
            {
                string ThePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

                if (filepath.Contains(@":\") == false)
                {
                    filepath = System.IO.Path.Combine(ThePath, filepath);
                }

                if (filepath.Trim() == "" || System.IO.File.Exists(filepath) == false)
                {
                    System.Windows.Forms.MessageBox.Show("输入文件名错误");
                    throw new Exception("视觉测量模块报错:" + filepath + "不存在！");
                }
                else
                {
                    ConfigPath = filepath;
                }

                //设备也要同步关闭
                DisposeDev();

                //关闭通讯连接
                //EComManageer.DisConnectAll();

                //读取保存文件
                ReadConfig(ConfigPath);

                //通讯//反序列化后刷新字典
                //EComManageer.setEcomList(g_CommunCation);

                //初始化驱动界面
                InitDevStatus();

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

                return false;
            }
        }

        /// <summary>
        /// 加载项目文件
        /// </summary>
        /// <param name="filePath">加载地址</param>
        public void ReadConfig(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter binaryFmt = new BinaryFormatter();
                    Solution.Ins = (Solution)binaryFmt.Deserialize(fs);

                    //fs.Seek(0, SeekOrigin.Begin);
                    //SysProcessPro.g_VarList = (List<DataVar>)binaryFmt.Deserialize(fs);

                    //SysProcessPro.g_CameraList = (List<CameraBase>)binaryFmt.Deserialize(fs);

                    //CameraBase.m_LastDeviceID = (int)binaryFmt.Deserialize(fs);

                    //SysProcessPro.g_ProjectList = (List<Project>)binaryFmt.Deserialize(fs);

                    //Project.m_LastProjectID = (int)binaryFmt.Deserialize(fs);

                    //SysProcessPro.g_CommunCation = (List<ECommunacation>)binaryFmt.Deserialize(fs);

                    //SysLayout.LayoutFrmNum = (int)binaryFmt.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex.ToString());
                throw ex;
            }
        }
        /// <summary>
        /// 释放设备
        /// </summary>
        public void DisposeDev()
        {
            //foreach (CameraBase item in Solution.Ins.g_CameraList)
            //{
            //    if (item.m_bConnected)
            //    {
            //        item.DisConnectDev();
            //    }
            //}
        }
        /// <summary>
        /// 初始化设备连接状态
        /// </summary>
        public void InitDevStatus()
        {
            //foreach (CameraBase dev in Solution.Ins.g_CameraList)
            //{
            //    if (dev.m_bConnected)
            //    {
            //        dev.m_bConnected = false;
            //        dev.ConnectDev();
            //        dev.SetSetting();
            //    }
            //    else
            //    {
            //        dev.DisConnectDev();
            //    }
            //}
        }
        #endregion 方法

    }
}
