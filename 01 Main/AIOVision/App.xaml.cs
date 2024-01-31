using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AIOVision
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static SplashScreen splashScreen;//启动画面
        //protected override void OnStartup(StartupEventArgs e)//另一种启动方式
        //{
        //    base.OnStartup(e);
        //    Application.Current.Shutdown();
        //}
        Mutex mutex;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                mutex = new Mutex(true, "onlyRunByAIOVision");
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("该程序正在运行", "提示信息");
                    Shutdown();
                }
                else
                {
                    //【1】全局异常捕获
                    this.DispatcherUnhandledException += App_DispatcherUnhandledException;// 未捕获的App异常
                    Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;// 未捕获的Current异常
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDoShell_UnhandledException);// 未捕获的CurrentDoShell异常
                    this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;// 未捕获的异常
                    TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;// Task线程内未捕获异常处理
                    //【2】SplashScreen
                    splashScreen = new SplashScreen(@"/Assets/Images/SplashScreen.png");
                    splashScreen.Show(false);
                    //【3】加载系统配置文件
                    SystemConfig.Ins.LoadSystemConfig();
                    //【3】初始化语言
                    InitializeCulture();

                    //【4】Halcon初始化
                    HOperatorSet.SetSystem("clip_region", "false");
                    //【4】图像脚本初始化
                    Solution.Ins.s_HDevEngine.IsInitialized();
                    //【4】设置临时目录为路径
                    Solution.Ins.s_HDevEngine.SetProcedurePath(Environment.GetEnvironmentVariable("TEMP"));
                    //【4】增加预编译,在脚本里有大量的循环的时候 速度会提示,否则没什么效果  magical 2019-5-23 10:46:24
                    Solution.Ins.s_HDevEngine.SetEngineAttribute("execute_procedures_jit_compiled", "true");

                    //【5】加载插件
                    PluginService.InitPlugin();
                    //【8】定期清理内存
                    //ClearMemoryHelper.ClearMemory();

                    //【10】MainWindow启动
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        AIOVision.MainWindow.Ins.Show();
                    }));

                }
            }
            catch (Exception ex)
            {
                //Logger.GetExceptionMsg(ex);
            }
        }
        #region 异常信息处理方法
        /// <summary>
        /// Task线程内未捕获异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                if (e != null && e.Exception != null)
                {
                    //Logger.GetExceptionMsg(e.Exception);
                }
            }
            catch { }
        }

        /// <summary>
        /// 显示未捕获的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = null;
                if (e != null)
                    ex = e.Exception;

                if (ex != null)
                {
                    //Logger.GetExceptionMsg(ex);
                }
                e.Handled = true;
            }
            catch { }
        }
        /// <summary>
        /// 显示未捕获的CurrentDoShell异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDoShell_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                //记录dump文件
                MiniDump.TryDump($"dumps\\VM_{DateTime.Now.ToString("HH-mm-ss-ms")}.dmp");
                Exception ex = null;
                if (e != null)
                    ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    //Logger.GetExceptionMsg(ex);
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 显示未捕获的Current异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                if (e != null && e.Exception != null)
                {
                    //Logger.GetExceptionMsg(e.Exception);
                }
                e.Handled = true;
            }
            catch { }
        }
        /// <summary>
        /// 显示未捕获的App异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                if (e != null && e.Exception != null)
                {
                    //Logger.GetExceptionMsg(e.Exception);
                }
                e.Handled = true;
            }
            catch { }
        }
        #endregion
        /// <summary>
        /// 初始化语言
        /// </summary>
        private void InitializeCulture()
        {
            // 切换语言
            //CultureInfo cultureInfo = new CultureInfo(SystemConfig.Ins.CurrentCultureName);
            //LocalizeDictionary.Instance.Culture = cultureInfo;
        }
    }
}
