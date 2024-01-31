using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System.IO;
using AvalonDock.Layout.Serialization;
using AvalonDock.Layout;
using System.Windows.Controls;
using HalconControl;

namespace AIOVision
{
    public class MainViewModel : ObservableObject
    {
        #region 字段
        private Dispatcher m_Dispatcher = Dispatcher.CurrentDispatcher;

        private DrawerHost drawerHost;//侧边栏窗口的本体

        private ObservableCollection<MenuBar> menuBarItems;

        private string currentSolution;

        private string currentTime;
        private bool isCheckedVision = true;
        private bool isCheckedUIDesign = false;
        private bool isCheckedTool = true;
        private bool isCheckedProcess = true;
        private bool isCheckedLog = true;
        private bool isCheckedData = true;
        private bool isCheckedModuleOut = true;
        private bool isCheckedDeviceState = true;
        private string currentUserName;

        #endregion
        #region 属性
        /// <summary>
        /// 显示的窗体
        /// </summary>
        private Grid _dispHindow;
        public Grid DispHindow
        {
            get { return _dispHindow; }
            set { SetProperty(ref _dispHindow, value); }
        }
        /// <summary>
        /// 菜单栏集合
        /// </summary>
        public ObservableCollection<MenuBar> MenuBarItems
        {
            get => menuBarItems;
            set => SetProperty(ref menuBarItems, value);
        }
        /// <summary>
        /// 当前解决方案
        /// </summary>
        public string CurrentSolution
        {
            get { return currentSolution; }
            set { SetProperty(ref currentSolution, value); }
        }
        #region 窗体操作
        /// <summary>
        /// 打开子窗体
        /// </summary>
        public ICommand OpenSubViewCommand { get; set; }
        /// <summary>
        /// 窗体加载
        /// </summary>
        public ICommand LoadedCommand { get; set; }
        /// <summary>
        /// 窗体最小化
        /// </summary>
        public ICommand WindowToMinCommand { get; set; }
        /// <summary>
        /// 窗体最大化
        /// </summary>
        public ICommand WindowToMaxCommand { get; set; }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        public ICommand WindowToCloseCommand { get; set; }
        #endregion 窗体操作
        #region 工具栏操作

        /// <summary>
        /// 启动
        /// </summary>
        public ICommand StartCommand { get; set; }
        /// <summary>
        /// 暂停/恢复
        /// </summary>
        public ICommand PauseCommand { get; set; }
        /// <summary>
        /// 停止
        /// </summary>
        public ICommand StopCommand { get; set; }
        /// <summary>
        /// 添加解决方案
        /// </summary>
        public ICommand AddSolutionCommand { get; set; }
        /// <summary>
        /// 打开解决方案
        /// </summary>
        public ICommand OpenSolutionCommand { get; set; }
        /// <summary>
        /// 保存解决方案
        /// </summary>
        public ICommand SaveSolutionCommand { get; set; }
        #endregion 工具栏操作
        #region Dock相关
        /// <summary>
        /// 加载Dock布局
        /// </summary>
        public ICommand LoadLayoutCommand { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentTime
        {
            get => currentTime;
            set => SetProperty(ref currentTime, value);
        }
        /// <summary>
        /// 视图是否选中
        /// </summary>
        public bool IsCheckedVision
        {
            get { return isCheckedVision; }
            set
            {
                SetProperty(ref isCheckedVision, value);
                VisibleOrHideView(isCheckedVision, "Vision", "LayoutDocument");
            }
        }
        /// <summary>
        /// UI设计器是否被选中
        /// </summary>
        public bool IsCheckedUIDesign
        {
            get { return this.isCheckedUIDesign; }
            set
            {
                SetProperty(ref this.isCheckedUIDesign, value);
                this.VisibleOrHideView(this.isCheckedUIDesign, "UIDesign", "LayoutDocument");
            }
        }
        /// <summary>
        /// 视图是否选中
        /// </summary>
        public bool IsCheckedTool
        {
            get { return isCheckedTool; }
            set
            {
                SetProperty(ref isCheckedTool, value);
                VisibleOrHideView(isCheckedTool, "Tool");
            }
        }
        /// <summary>
        /// 视图是否选中
        /// </summary>
        public bool IsCheckedProcess
        {
            get { return isCheckedProcess; }
            set
            {
                SetProperty(ref isCheckedProcess, value);
                VisibleOrHideView(isCheckedProcess, "Process");
            }
        }
        /// <summary>
        /// 视图是否选中
        /// </summary>
        public bool IsCheckedLog
        {
            get { return isCheckedLog; }
            set
            {
                SetProperty(ref isCheckedLog, value);
                VisibleOrHideView(isCheckedLog, "Log");
            }
        }
        /// <summary>
        /// 视图是否选中
        /// </summary>
        public bool IsCheckedData
        {
            get { return isCheckedData; }
            set
            {
                SetProperty(ref isCheckedLog, value);
                VisibleOrHideView(isCheckedData, "Data");
            }
        }
        /// <summary>
        /// 视图是否选中
        /// </summary>
        public bool IsCheckedModuleOut
        {
            get { return isCheckedModuleOut; }
            set
            {
                SetProperty(ref isCheckedLog, value);
                VisibleOrHideView(isCheckedModuleOut, "ModuleOut");
            }
        }
        /// <summary>
        /// 视图是否选中
        /// </summary>
        public bool IsCheckedDeviceState
        {
            get { return isCheckedDeviceState; }
            set
            {
                SetProperty(ref isCheckedDeviceState, value);
                VisibleOrHideView(isCheckedDeviceState, "DeviceState");
            }
        }
        #endregion Dock相关
        /// <summary>
        /// 当前用户名
        /// </summary>
        public string CurrentUserName
        {
            get { return currentUserName; }
            set
            {
                SetProperty(ref currentUserName, value);

                IsCheckedUIDesign = Solution.Ins.IsUseUIDesign;
                switch (currentUserName)
                {
                    case UserType.Developer_en:
                    case UserType.Developer: //
                                             //Logger.AddLog($"{Resource.DeveloperLoginSystem}");
                        RightControl.Ins.CameraSet = true;
                        RightControl.Ins.QuickMode = true;
                        RightControl.Ins.HardwareConfig = true;
                        RightControl.Ins.CommunicationSet = true;
                        RightControl.Ins.Camera = true;
                        RightControl.Ins.Temperature = true;
                        RightControl.Ins.Barcode = true;
                        RightControl.Ins.HeightSensor = true;
                        RightControl.Ins.PressureSensor = true;
                        RightControl.Ins.InputAndOutput = true;
                        RightControl.Ins.ServoDebug = true;
                        RightControl.Ins.IODebug = true;
                        RightControl.Ins.Home = true;
                        RightControl.Ins.Open = true;
                        RightControl.Ins.Edit = true;
                        RightControl.Ins.Save = true;
                        //RightControl.Ins.RunOnce = true;
                        //RightControl.Ins.RunCycle = true;
                        RightControl.Ins.Stop = true;
                        RightControl.Ins.OpenFile = true;
                        RightControl.Ins.SaveFile = true;
                        RightControl.Ins.SystemConfig = true;
                        RightControl.Ins.OpenOrCloseCamera = true;
                        RightControl.Ins.RedLight = true;
                        RightControl.Ins.DeviceParam = true;
                        RightControl.Ins.SystemParam = true;
                        RightControl.Ins.View = true;
                        RightControl.Ins.ManufactureParam = true;
                        RightControl.Ins.TemplateLayout = true;
                        RightControl.Ins.CameraSetting = true;
                        RightControl.Ins.LaserDebug = true;
                        RightControl.Ins.PowerDebug = true;
                        RightControl.Ins.NewSolution = true;
                        RightControl.Ins.SolutionList = true;
                        RightControl.Ins.GlobalVar = true;
                        RightControl.Ins.UIDesign = true;
                        IsCheckedTool = true;
                        IsCheckedProcess = true;
                        IsCheckedData = true;
                        IsCheckedModuleOut = true;
                        IsCheckedDeviceState = true;
                        LoadLayoutCommand.Execute(0);
                        //MainWindow.Ins.toolBar.Visibility = Visibility.Visible;
                        break;
                    case UserType.Administrator_en:
                    case UserType.Administrator: //
                                                 //Logger.AddLog($"{Resource.AdministratorLoginSystem}");
                        RightControl.Ins.CameraSet = false;
                        RightControl.Ins.QuickMode = true;
                        RightControl.Ins.HardwareConfig = false;
                        RightControl.Ins.CommunicationSet = false;
                        RightControl.Ins.Camera = false;
                        RightControl.Ins.Temperature = false;
                        RightControl.Ins.Barcode = false;
                        RightControl.Ins.HeightSensor = false;
                        RightControl.Ins.PressureSensor = false;
                        RightControl.Ins.InputAndOutput = false;
                        RightControl.Ins.ServoDebug = true;
                        RightControl.Ins.IODebug = true;
                        RightControl.Ins.Home = true;
                        RightControl.Ins.Open = true;
                        RightControl.Ins.Edit = true;
                        RightControl.Ins.Save = true;
                        //RightControl.Ins.RunOnce = true;
                        //RightControl.Ins.RunCycle = true;
                        RightControl.Ins.Stop = true;
                        RightControl.Ins.OpenFile = true;
                        RightControl.Ins.SaveFile = true;
                        RightControl.Ins.SystemConfig = true;
                        RightControl.Ins.OpenOrCloseCamera = true;
                        RightControl.Ins.RedLight = true;
                        RightControl.Ins.DeviceParam = true;
                        RightControl.Ins.SystemParam = true;
                        RightControl.Ins.View = true;
                        RightControl.Ins.ManufactureParam = true;
                        RightControl.Ins.TemplateLayout = true;
                        RightControl.Ins.CameraSetting = true;
                        RightControl.Ins.LaserDebug = false;
                        RightControl.Ins.PowerDebug = false;
                        RightControl.Ins.NewSolution = false;
                        RightControl.Ins.SolutionList = true;
                        RightControl.Ins.GlobalVar = false;
                        RightControl.Ins.UIDesign = true;
                        IsCheckedTool = true;
                        IsCheckedProcess = true;
                        IsCheckedData = true;
                        IsCheckedModuleOut = true;
                        IsCheckedDeviceState = true;
                        LoadLayoutCommand.Execute(0);
                        //MainWindow.Ins.toolBar.Visibility = Visibility.Visible;
                        break;
                    case UserType.Operator_en:
                    case UserType.Operator: //
                                            //Logger.AddLog($"{Resource.OperatorLoginSystem}");
                        RightControl.Ins.CameraSet = false;
                        RightControl.Ins.QuickMode = false;
                        RightControl.Ins.HardwareConfig = false;
                        RightControl.Ins.CommunicationSet = false;
                        RightControl.Ins.Camera = false;
                        RightControl.Ins.Temperature = false;
                        RightControl.Ins.Barcode = false;
                        RightControl.Ins.HeightSensor = false;
                        RightControl.Ins.PressureSensor = false;
                        RightControl.Ins.InputAndOutput = false;
                        RightControl.Ins.ServoDebug = false;
                        RightControl.Ins.IODebug = true;
                        RightControl.Ins.Home = true;
                        RightControl.Ins.Open = true;
                        RightControl.Ins.Edit = false;
                        RightControl.Ins.Save = false;
                        //RightControl.Ins.RunOnce = true;
                        //RightControl.Ins.RunCycle = true;
                        RightControl.Ins.Stop = true;
                        RightControl.Ins.OpenFile = true;
                        RightControl.Ins.SaveFile = true;
                        RightControl.Ins.SystemConfig = false;
                        RightControl.Ins.OpenOrCloseCamera = false;
                        RightControl.Ins.RedLight = false;
                        RightControl.Ins.DeviceParam = false;
                        RightControl.Ins.SystemParam = false;
                        RightControl.Ins.View = false;
                        RightControl.Ins.ManufactureParam = false;
                        RightControl.Ins.TemplateLayout = false;
                        RightControl.Ins.CameraSetting = false;
                        RightControl.Ins.LaserDebug = false;
                        RightControl.Ins.PowerDebug = false;
                        RightControl.Ins.NewSolution = false;
                        RightControl.Ins.SolutionList = false;
                        RightControl.Ins.GlobalVar = false;
                        RightControl.Ins.UIDesign = false;
                        IsCheckedTool = false;
                        IsCheckedProcess = false;
                        IsCheckedData = false;
                        IsCheckedModuleOut = false;
                        IsCheckedDeviceState = false;
                        //MainWindow.Ins.toolBar.Visibility = Visibility.Collapsed;

                        break;

                    default:
                        RightControl.Ins.Camera = false;
                        RightControl.Ins.HardwareConfig = false;
                        RightControl.Ins.CommunicationSet = false;
                        RightControl.Ins.Temperature = false;
                        RightControl.Ins.Barcode = false;
                        RightControl.Ins.HeightSensor = false;
                        RightControl.Ins.PressureSensor = false;
                        RightControl.Ins.InputAndOutput = false;
                        RightControl.Ins.ServoDebug = false;
                        RightControl.Ins.IODebug = false;
                        RightControl.Ins.Home = false;
                        RightControl.Ins.Open = false;
                        RightControl.Ins.Edit = false;
                        RightControl.Ins.Save = false;
                        RightControl.Ins.RunOnce = true;
                        RightControl.Ins.RunCycle = true;
                        RightControl.Ins.Stop = true;
                        RightControl.Ins.OpenFile = false;
                        RightControl.Ins.SaveFile = false;
                        RightControl.Ins.SystemConfig = false;
                        RightControl.Ins.OpenOrCloseCamera = false;
                        RightControl.Ins.RedLight = false;
                        RightControl.Ins.DeviceParam = false;
                        RightControl.Ins.SystemParam = false;
                        RightControl.Ins.View = false;
                        RightControl.Ins.ManufactureParam = false;
                        RightControl.Ins.TemplateLayout = false;
                        RightControl.Ins.CameraSetting = false;
                        RightControl.Ins.LaserDebug = false;
                        RightControl.Ins.PowerDebug = false;
                        RightControl.Ins.NewSolution = false;
                        RightControl.Ins.SolutionList = false;
                        RightControl.Ins.GlobalVar = false;
                        RightControl.Ins.UIDesign = false;
                        //MainWindow.Ins.toolBar.Visibility = Visibility.Collapsed;
                        IsCheckedTool = false;
                        IsCheckedProcess = false;
                        IsCheckedData = false;
                        IsCheckedModuleOut = false;
                        IsCheckedDeviceState = false;
                        break;
                }


            }
        }
        #endregion 属性
        #region 方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public MainViewModel()
        {
            this.LoadedCommand = new RelayCommand<object>(Load);
            this.WindowToMinCommand = new RelayCommand<Window>(WindowToMin);
            this.WindowToMaxCommand = new RelayCommand<Window>(WindowToMax);
            this.WindowToCloseCommand = new RelayCommand<Window>(WindowToClose);
            this.OpenSubViewCommand = new RelayCommand<object>(OpenSubView);
            this.StartCommand = new RelayCommand(Start);
            this.PauseCommand = new RelayCommand(Pause);
            this.StopCommand = new RelayCommand(Stop);
            this.AddSolutionCommand = new RelayCommand(AddSolution);
            this.OpenSolutionCommand = new RelayCommand(OpenSolutionFunction);
            this.SaveSolutionCommand = new RelayCommand(SaveSolution);

            this.LoadLayoutCommand = new RelayCommand(LoadLayout);

            MenuBarItems = new ObservableCollection<MenuBar>();
            MenuBarItems = GetMenuBarItems();
            //底部状态栏的当前产品状态更新
            DataEventChange.footChangeEvent += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    CurrentSolution = e;
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {
                        CurrentSolution = e;
                    }));
                }
            };
            //布局窗体，变更事件
            DataEventChange.LayFrmChangeHandler += (e) =>
            {
                if (!m_Dispatcher.CheckAccess())
                {
                    m_Dispatcher.Invoke(new Action(() =>
                    {
                        CreateLayOutFrm(e);
                    }));
                }
                else
                {
                    CreateLayOutFrm(e);
                }
            };
            
        }


        #region 窗体方法实现
        /// <summary>
        /// 加载窗体
        /// </summary>
        private void Load(Object obj)
        {
            //输入参数提取（在xaml中的command里定义了两个参数)
            object[] multiObject = obj as object[];
            this.drawerHost = multiObject[0] as DrawerHost;
            var b = multiObject[1] as ToggleButton;

            //【1】当前时间
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer100ms_Tick;
            timer.Start();
            //【3】注册快捷键
            //RegisterHotKey();
            //UnregisterHotKey();
            //【4】初始化UI
            InitUI();
            //【6】数据库配置
            //SQLiteHelper.Ins.Init();
            
            //【18】关闭splashScreen
            App.splashScreen.Close(TimeSpan.FromMilliseconds(100));
            //【21】软件启动成功
            //Logger.AddLog("软件启动成功");
            //【22】加载配方
            if (SystemConfig.Ins.SolutionAutoLoad)
            {
                OpenSolution(SystemConfig.Ins.SolutionPathText);
                if (SystemConfig.Ins.SolutionAutoRun)
                {
                    //Solution.Ins.QuickMode = true;
                    //MainView.Ins.btnQuickMode.Foreground = Brushes.Lime;
                    //Solution.Ins.StartRun();
                }
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="window"></param>
        private void WindowToMin(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="window"></param>
        private void WindowToMax(Window window)
        {
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void WindowToClose(Window window)
        {
            //ToDo 关闭窗口前的判断，比如判断自动是否在运行，关闭相机等事项
            window?.Close();
        }
        #endregion 窗体方法实现
        #region 菜单栏窗体相关
        /// <summary>
        /// --------------------------------------------------菜单栏加载子窗体------------------------------------------
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<MenuBar> GetMenuBarItems()
        {
            MenuBarItems.Add(new MenuBar { Name = SubViewNameEnum.Setting.ToString(), Icon = "CursorPointer", Tag = "产品设置", Enable = true });
            MenuBarItems.Add(new MenuBar { Name = SubViewNameEnum.IOMonitor.ToString(), Icon = "CursorPointer", Tag = "IO监控", Enable = true });
            MenuBarItems.Add(new MenuBar { Name = SubViewNameEnum.DataQuery.ToString(), Icon = "CursorPointer", Tag = "数据查询", Enable = true });
            MenuBarItems.Add(new MenuBar { Name = SubViewNameEnum.AlarmRecord.ToString(), Icon = "CursorPointer", Tag = "报警查询", Enable = true });
            MenuBarItems.Add(new MenuBar { Name = SubViewNameEnum.ProductManage.ToString(), Icon = "CursorPointer", Tag = "产品管理", Enable = true });
            MenuBarItems.Add(new MenuBar { Name = SubViewNameEnum.ManualOperation.ToString(), Icon = "CursorPointer", Tag = "手动操作", Enable = true });
            return MenuBarItems;
        }
        /// <summary>
        /// ----------------------------------------------------注册子窗口------------------------------------------
        /// </summary>
        private void GetFunctionStack()
        {
            //FunctionStack.Register(SubViewNameEnum.UserLogin.ToString(), ShowUserLoginView);
            //FunctionStack.Register(SubViewNameEnum.IOMonitor.ToString(), ShowIOMonitorView);
            //FunctionStack.Register(SubViewNameEnum.ProductManage.ToString(), ShowProductManageView);
            //FunctionStack.Register(SubViewNameEnum.Setting.ToString(), ShowSettingView);
            //FunctionStack.Register(SubViewNameEnum.ManualOperation.ToString(), ShowManualOperationView);
            //FunctionStack.Register(SubViewNameEnum.DataQuery.ToString(), ShowDataQueryView);
            //FunctionStack.Register(SubViewNameEnum.AlarmRecord.ToString(), ShowAlarmRecordView);
        }
        /// <summary>
        /// ------------------------------------------------------打开子窗体-----------------------------------------
        /// </summary>
        private async void OpenSubView(object obj)
        {
            switch ((SubViewNameEnum)Enum.Parse(typeof(SubViewNameEnum), obj.ToString()))
            {
                case SubViewNameEnum.UserLogin:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.UserLogin.ToString(), null);
                    break;
                case SubViewNameEnum.IOMonitor:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.IOMonitor.ToString(), null);
                    break;
                case SubViewNameEnum.ProductManage:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.ProductManage.ToString(), null);
                    break;
                case SubViewNameEnum.Setting:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.Setting.ToString(), null);
                    break;
                case SubViewNameEnum.ManualOperation:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.ManualOperation.ToString(), null);
                    break;
                case SubViewNameEnum.DataQuery:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.DataQuery.ToString(), null);
                    break;
                case SubViewNameEnum.AlarmRecord:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.AlarmRecord.ToString(), null);
                    break;
                case SubViewNameEnum.VisionEdit:
                    await FunctionStack.ExecuteAsync(SubViewNameEnum.VisionEdit.ToString(), null);
                    break;
                default:
                    break;
            }
        }
        #region -------------------------------------------------子窗体打开------------------------------------------
        /// <summary>
        /// 登录窗口打开
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public async Task<bool> ShowUserLoginView(object obj)
        //{
        //    this.drawerHost.IsLeftDrawerOpen = false;

        //    return await Task.Run(() =>
        //    {
        //        return Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            LoginView loginView = new LoginView();
        //            return loginView.ShowDialog() == true;
        //        });

        //    });
        //}
        /// <summary>
        /// IO监控窗口打开
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public async Task<bool> ShowIOMonitorView(object obj)
        //{
        //    this.drawerHost.IsLeftDrawerOpen = false;

        //    return await Task.Run(() =>
        //    {
        //        return Application.Current.Dispatcher.Invoke<bool>(() =>
        //        {
        //            IOMonitorView iOMonitorView = new IOMonitorView();
        //            return iOMonitorView.ShowDialog() == true;
        //        });

        //    });

        //}
        /// <summary>
        /// 产品管理窗口打开
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public async Task<bool> ShowProductManageView(object obj)
        //{
        //    this.drawerHost.IsLeftDrawerOpen = false;

        //    return await Task.Run(() =>
        //    {
        //        return Application.Current.Dispatcher.Invoke<bool>(() =>
        //        {
        //            ProductManageView productManageView = new ProductManageView();
        //            return productManageView.ShowDialog() == true;
        //        });

        //    });
        //}

        /// <summary>
        /// 设置窗口打开
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public async Task<bool> ShowSettingView(object obj)
        //{
        //    this.drawerHost.IsLeftDrawerOpen = false;

        //    return await Task.Run(() =>
        //    {
        //        return Application.Current.Dispatcher.Invoke<bool>(() =>
        //        {
        //            SettingView settingView = new SettingView();
        //            return settingView.ShowDialog() == true;
        //        });

        //    });
        //}
        /// <summary>
        /// 手动操作界面打开
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public async Task<bool> ShowManualOperationView(object obj)
        //{
        //    this.drawerHost.IsLeftDrawerOpen = false;

        //    return await Task.Run(() =>
        //    {
        //        return Application.Current.Dispatcher.Invoke<bool>(() =>
        //        {
        //            ManualOperationView manualOperationView = new ManualOperationView();
        //            return manualOperationView.ShowDialog() == true;
        //        });

        //    });
        //}
        /// <summary>
        /// 数据查询界面打开
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public async Task<bool> ShowDataQueryView(object obj)
        //{
        //    this.drawerHost.IsLeftDrawerOpen = false;

        //    return await Task.Run(() =>
        //    {
        //        return Application.Current.Dispatcher.Invoke<bool>(() =>
        //        {
        //            DataQueryView dataQueryView = new DataQueryView();
        //            return dataQueryView.ShowDialog() == true;
        //        });

        //    });
        //}
        /// <summary>
        /// 数据查询界面打开
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public async Task<bool> ShowAlarmRecordView(object obj)
        //{
        //    this.drawerHost.IsLeftDrawerOpen = false;

        //    return await Task.Run(() =>
        //    {
        //        return Application.Current.Dispatcher.Invoke<bool>(() =>
        //        {
        //            AlarmRecordView alarmRecord = new AlarmRecordView();
        //            return alarmRecord.ShowDialog() == true;
        //        });

        //    });
        //}

        #endregion
        #endregion 菜单栏窗体相关
        #region 工具栏方法实现
        /// <summary>
        /// 启动
        /// </summary>
        private void Start()
        {

        }
        /// <summary>
        /// 暂停/恢复
        /// </summary>
        private void Pause()
        {

        }
        /// <summary>
        /// 停止
        /// </summary>
        private void Stop()
        {

        }
        #endregion 工具栏方法实现
        private void AddSolution()
        {
            FrmCreateProject frmCreate = new FrmCreateProject();
            frmCreate.ShowDialog();
        }
        private void LoadLayout()
        {
            if (File.Exists(FilePaths.DockLayout))
            {
                //MessageView.Ins.MessageBoxShow("ffd", eMsgType.Error);
                //Avalon的Dock布局反序列化
                var layoutSerializer = new XmlLayoutSerializer(DockView.Ins.dockManager);
                layoutSerializer.Deserialize(FilePaths.DockLayout);
            }
        }
        private void SaveSolution()
        {
            try
            {
                if (Solution.Ins.g_ProjectList != null && Solution.Ins.g_ProjectList.Count != 0)
                {
                    string str = Solution.Ins.SolutionPath;

                    //加载窗体
                    LoadWindow dlg = new LoadWindow(new SaveSolution(str));
                    dlg.ShowInTaskbar = false;
                    dlg.ShowDialog();

                    //消息提示窗体
                    //NotifyBox notify = new NotifyBox() { NotifyMessage = SysProcessPro.SolutionPath };
                    //notify.ShowInTaskbar = false;
                    //notify.Show();

                    //Log.Info(SysProcessPro.SolutionName + ".nv" + "项目保存完成");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        private void OpenSolutionFunction()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "工程文件|*.nv";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //加载窗体
                    LoadWindow dlg = new LoadWindow(new LoadSolution(openFileDialog));
                    dlg.ShowInTaskbar = false;
                    dlg.ShowDialog();

                    //消息提示窗体
                    //NotifyBox notify = new NotifyBox() { NotifyMessage = SysProcessPro.SolutionPath };
                    //notify.ShowInTaskbar = false;
                    //notify.Show();

                    System.GC.Collect();//主动回收下系统未使用的资源
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void OpenSolution(string fileName)
        {
            CurrentSolution = fileName;
            Solution.Ins = SerializeHelp.BinDeserialize<Solution>(fileName);
            Solution.Ins.LoadCommunacation();
            if (Solution.Ins.RushMode)
            {
                //MainWindow.Ins.btnQuickMode.Foreground = Brushes.Lime;
            }
            else
            {
                //MainWindow.Ins.btnQuickMode.Foreground = Brushes.LightGray;
            }
            //VisionView.Ins.ViewMode = Solution.Ins.ViewMode;
            ////ToolView.Ins.UpdateTree();
            //Solution.Ins.CurrentProject = Solution.Ins.GetProjectById(
            //    Solution.Ins.CurrentProjectID
            //);
            ////ProcessView.Ins.UpdateTree();
            ////UIDesignView.UpdateUIDesign(true);
        }
        private void Timer100ms_Tick(object sender, EventArgs e)
        {
            this.CurrentTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        //初始化UI
        private void InitUI()
        {
            //【1】加载布局
            if (SystemConfig.Ins.AutoLoadLayout)
            {
                LoadLayoutCommand.Execute(1);
            }
            //【2】急速模式背景显示
            if (Solution.Ins.RushMode)
            {
                //MainWindow.Ins.btnQuickMode.Foreground = Brushes.Lime;
            }
            else
            {
                //MainWindow.Ins.btnQuickMode.Foreground = Brushes.LightGray;
            }
            //【3】图像视图显示
            IsCheckedVision = true;
            //【4】视觉窗体初始化
            CreateLayOutFrm(1);
        }
        #region VisibleOrHideView
        public static LayoutDocument LayoutDocument_Vision = new LayoutDocument
        {
            Title = "图像视图",
            CanClose = false,
            ContentId = "Vision",
            Content = VisionView.Ins
        };

        public static LayoutDocument LayoutDocument_UIDesign = new LayoutDocument
        {
            Title = "主界面",
            CanClose = false,
            ContentId = "UIDesign",
            Content = UIDisplayView.Ins
        };
        private bool LayoutActive = true;
        /// <summary>
        /// 根据图像窗体布局创建窗体
        /// </summary>
        private void CreateLayOutFrm(int FrmNum)
        {
            SysLayout.LayoutFrmNum = FrmNum;
            SysLayout.CreateLayoutFrm(VisionView.Ins.grid);
        }

        public void LayoutStatusChanged()
        {
            LayoutActive = false;
            IsCheckedTool = GetLayoutStatus("Tool");
            IsCheckedProcess = GetLayoutStatus("Process");
            IsCheckedLog = GetLayoutStatus("Log");
            IsCheckedData = GetLayoutStatus("Data");
            IsCheckedModuleOut = GetLayoutStatus("ModuleOut");
            IsCheckedDeviceState = GetLayoutStatus("DeviceState");
            LayoutActive = true;
        }
        public bool GetLayoutStatus(string contentId)
        {
            if (DockView.Ins == null)
                return false;
            var toolWindow1 = DockView.Ins.dockManager.Layout
                .Descendents()
                .OfType<LayoutAnchorable>()
                .Single(a => a.ContentId == contentId);
            if (toolWindow1.IsHidden)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void VisibleOrHideView(bool isChecked, string contentId, string typeOfLayout = "LayoutAnchorable")
        {
            if (DockView.Ins == null)
                return;
            if (typeOfLayout == "LayoutAnchorable")
            {
                var toolWindow1 = DockView.Ins.dockManager.Layout
                    .Descendents()
                    .OfType<LayoutAnchorable>()
                    .Single(a => a.ContentId == contentId);
                if (isChecked == true)
                {
                    if (toolWindow1.IsHidden)
                    {
                        toolWindow1.Show();
                    }
                    if (LayoutActive)
                    {
                        toolWindow1.IsActive = true;
                    }
                }
                else
                {
                    if (toolWindow1.IsVisible)
                    {
                        toolWindow1.Hide();
                    }
                    else if (toolWindow1.IsHidden) { }
                    else
                    {
                        toolWindow1.AddToLayout(
                            DockView.Ins.dockManager,
                            AnchorableShowStrategy.Bottom | AnchorableShowStrategy.Most
                        );
                    }
                }
            }
            else if (typeOfLayout == "LayoutDocument")
            {
                try
                {
                    var layoutDocumentPaneGroup = DockView.Ins.dockManager.Layout
                        .Descendents()
                        .OfType<LayoutDocumentPaneGroup>()
                        .FirstOrDefault();
                    if (layoutDocumentPaneGroup != null)
                    {
                        LayoutDocumentPane layoutDocumentPane = new LayoutDocumentPane();
                        LayoutDocument item = new LayoutDocument();

                        if (contentId == "UIDesign")
                        {
                            item = MainViewModel.LayoutDocument_UIDesign;
                            if (layoutDocumentPaneGroup.Children.Count < 2)
                            {
                                layoutDocumentPaneGroup.Children.Add(new LayoutDocumentPane());
                            }
                            layoutDocumentPane = (LayoutDocumentPane)
                                layoutDocumentPaneGroup.Children[1];
                        }
                        else if (contentId == "Vision")
                        {
                            item = MainViewModel.LayoutDocument_Vision;
                            if (layoutDocumentPaneGroup.Children.Count < 1)
                            {
                                layoutDocumentPaneGroup.Children.Add(new LayoutDocumentPane());
                            }
                            layoutDocumentPane = (LayoutDocumentPane)
                                layoutDocumentPaneGroup.Children[0];
                        }
                        if (isChecked)
                        {
                            if (!layoutDocumentPane.Children.Contains(item))
                            {
                                layoutDocumentPane.Children.Add(item);
                            }
                        }
                        else if (layoutDocumentPane.Children.Contains(item))
                        {
                            layoutDocumentPane.Children.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Logger.GetExceptionMsg(ex, "", true);
                }
            }
        }
        #endregion VisibleOrHideView
        #endregion
    }
}
