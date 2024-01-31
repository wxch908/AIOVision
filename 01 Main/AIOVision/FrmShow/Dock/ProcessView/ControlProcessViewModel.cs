using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace AIOVision
{
    /// <summary>
    /// 流程单独控制
    /// </summary>
    public class ControlProcessViewModel : ObservableObject
    {

        /// <summary>
        /// 当前流程名称
        /// </summary>
        private string _processName;

        public string ProcessName
        {
            get { return _processName; }
            set {SetProperty(ref _processName,value); }
        }

        //运行一次
        public ICommand RunOnceCom { get; set; }

        //执行连续运行
        public ICommand RunCycleCom { get; set; }

        //停止连续运行
        public ICommand StopCycleCom { get; set; }

        #region 运行状态控件的Enabel

        /// <summary>
        /// 控件的enable
        /// </summary>
        private bool _ProcesscontrolIsEnabled = true;

        public bool ProcessControlIsEnabled
        {
            get { return _ProcesscontrolIsEnabled; }
            set { SetProperty(ref _ProcesscontrolIsEnabled,value); }
        }

        #endregion

        #region 停止状态控件的Enabel

        /// <summary>
        /// 控件的enable
        /// </summary>
        private bool _stopIsEnabled = false;

        public bool StopIsEnabled
        {
            get { return _stopIsEnabled; }
            set { SetProperty(ref _stopIsEnabled,value); }
        }

        #endregion

        private Dispatcher m_Dispatcher = Dispatcher.CurrentDispatcher;

        public ControlProcessViewModel()
        {
            //来自Nva窗体，点击运行一次
            //来自Nva窗体，点击循环运行
            //只控制控件使能开启
            DataEventChange.NvaControlEnabelChangeHandler += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    ProcessControlIsEnabled = e ? true : false;
                    StopIsEnabled = e ? false : true;
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {
                        ProcessControlIsEnabled = e ? true : false;
                        StopIsEnabled = e ? false : true;
                    }));
                }
            };

            //模块来显示启动按钮是否运行中
            DataEventChange.ProjectChangedEvent += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    ProcessControlIsEnabled = e ? true : false;
                    StopIsEnabled = e ? false : true;
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {

                        ProcessControlIsEnabled = e ? true : false;
                        StopIsEnabled = e ? false : true;
                    }));
                }
            };

            //模块来显示启动按钮是否运行中
            DataEventChange.PrecessChangeHandler += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    ProcessControlIsEnabled = e ? true : false;
                    StopIsEnabled = e ? false : true;
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {

                        ProcessControlIsEnabled = e ? true : false;
                        StopIsEnabled = e ? false : true;
                    }));
                }
            };

            //模块来显示启动按钮是否运行中
            DataEventChange.SingleProjectChangedEvent += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    ProcessControlIsEnabled = e ? true : false;
                    StopIsEnabled = e ? false : true;
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {

                        ProcessControlIsEnabled = e ? true : false;
                        StopIsEnabled = e ? false : true;
                    }));
                }
            };

            //切换流程显示
            DataEventChange.propertyChanged += (val) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    if (Solution.Ins.Cur_Project != null)
                    {
                        ProcessName = Solution.Ins.Cur_Project.ProjectInfo.m_ProjectName;
                        ProcessControlIsEnabled = !Solution.Ins.Cur_Project.m_ThreadStatus;
                        StopIsEnabled = Solution.Ins.Cur_Project.m_ThreadStatus;
                    }
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {
                        if (Solution.Ins.Cur_Project != null)
                        {
                            ProcessName = Solution.Ins.Cur_Project.ProjectInfo.m_ProjectName;
                            ProcessControlIsEnabled = !Solution.Ins.Cur_Project.m_ThreadStatus;
                            StopIsEnabled = Solution.Ins.Cur_Project.m_ThreadStatus;
                        }
                    }));
                }
            };

            this.RunOnceCom = new RelayCommand<object>(Run_Once);//运行一次

            this.RunCycleCom = new RelayCommand<object>(Run_Cycle);//循环运行

            this.StopCycleCom = new RelayCommand<object>(Stop_Cycle);//停止运行

        }

        /// <summary>
        /// 当前流程运行一次
        /// </summary>
        /// <param name="obj"></param>
        private void Run_Once(object obj)
        {
            if (Solution.Ins.g_ProjectList.Count == 0)
            {
                //Log.Info("无可运行解决方案！");
                return;
            }

            if (Solution.Ins.Cur_Project == null)
            {
                //Log.Info("当前流程为空！");
                return;
            }

            if (Solution.Ins.Cur_Project.m_ModuleObjList.Count <= 0)
            {
                //Log.Info("当前流程无可运行模块！");
                return;
            }

            Solution.Ins.Cur_Project.run = RunMode.执行一次;
            Solution.Ins.Cur_Project.Thread_Start();

            ProcessControlIsEnabled = false;
            StopIsEnabled = true;
            DataEventChange.ProcessChangeEnabel = ProcessControlIsEnabled;

        }

        /// <summary>
        /// 当前流程循环运行
        /// </summary>
        /// <param name="obj"></param>
        private void Run_Cycle(object obj)
        {
            if (Solution.Ins.g_ProjectList.Count == 0)
            {
                //Log.Info("无可运行解决方案！");
                return;
            }

            if (Solution.Ins.Cur_Project == null)
            {
                //Log.Info("当前流程为空！");
                return;
            }

            if (Solution.Ins.Cur_Project.m_ModuleObjList.Count <= 0)
            {
                //Log.Info("当前流程无可运行模块！");
                return;
            }

            if (!Solution.Ins.Cur_Project.m_ThreadStatus)
            {
                Solution.Ins.Cur_Project.run = RunMode.循环运行;
                Solution.Ins.Cur_Project.Thread_Start();

                ProcessControlIsEnabled = false;
                StopIsEnabled = true;
                DataEventChange.ProcessChangeEnabel = ProcessControlIsEnabled;

            }

        }

        /// <summary>
        /// 停止当前流程
        /// </summary>
        /// <param name="obj"></param>
        private void Stop_Cycle(object obj)
        {
            if (Solution.Ins.g_ProjectList.Count == 0)
            {
                //Log.Info("无可运行解决方案！");
                return;
            }

            if (Solution.Ins.Cur_Project == null)
            {
                //Log.Info("当前流程为空！");
                return;
            }

            if (Solution.Ins.Cur_Project.m_ModuleObjList.Count <= 0)
            {
                //Log.Info("当前流程无可运行模块！");
                return;
            }

            if (Solution.Ins.Cur_Project.m_ThreadStatus)
            {
                int index = Solution.Ins.g_ProjectList.FindIndex(c => c.CurModuleID == Solution.Ins.Cur_Project.CurModuleID);
                Solution.Ins.Sys_Stop(index);
                ProcessControlIsEnabled = true;
                StopIsEnabled = false;
                DataEventChange.ProcessChangeEnabel = StopIsEnabled;
            }
        }

    }
}
