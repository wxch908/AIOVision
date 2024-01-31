using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace AIOVision
{
    public class CreatePrjViewModel : ObservableObject
    {

        public CreatePrjModel CreatePrjModel { get; set; } = new CreatePrjModel();

        /// <summary>
        /// 创建一个新的解决方案
        /// </summary>
        public ICommand CreatePrj { get; set; }

        /// <summary>
        /// 确定
        /// </summary>
        public ICommand FindPath { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        public ICommand CancelFrm { get; set; }

        public CreatePrjViewModel()
        {
            this.CreatePrj = new RelayCommand<object>(CreateProject);

            this.FindPath = new RelayCommand<object>(FindTxtPath);

            this.CancelFrm = new RelayCommand<object>(new Action<object>((o) =>
            {
                (o as System.Windows.Window).Close();

            }));

        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="o"></param>
        private void CreateProject(object obj)
        {
            if (CreatePrjModel.ProjectName == null || CreatePrjModel.ProjectName.Trim().Replace(" ", "").ToLowerInvariant().Length == 0)
            {
                System.Windows.MessageBox.Show("请输入解决方案名称!");
                //Log.Info("请输入解决方案名称!");
                return;
            }

            if (CreatePrjModel.ProjectPath == null || CreatePrjModel.ProjectPath.Trim().Replace(" ", "").ToLowerInvariant().Length == 0)
            {
                System.Windows.MessageBox.Show("请选择解决方案保存路径!");
                //Log.Info("请选择解决方案保存路径!");
                return;
            }

            //清空列表
            Solution.Ins.g_ProjectList.Clear();

            //解决方案名称
            Solution.Ins.SolutionName = CreatePrjModel.ProjectName.Trim().Replace(" ", "").ToLowerInvariant();

            //解决方案路径
            Solution.Ins.SolutionPath = CreatePrjModel.ProjectPath
                + "\\" + CreatePrjModel.ProjectName.Trim().Replace(" ", "").ToLowerInvariant() + ".nv";

            //更新底部状态栏显示
            DataEventChange.Footdata = Solution.Ins.SolutionPath;

            //Log.Info("解决方案创建成功！");

            (obj as System.Windows.Window).Close();

        }

        /// <summary>
        /// 打开窗体，获取位置
        /// </summary>
        /// <param name="o"></param>
        private void FindTxtPath(object o)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreatePrjModel.ProjectPath = dialog.SelectedPath;
            }
        }

    }
}
