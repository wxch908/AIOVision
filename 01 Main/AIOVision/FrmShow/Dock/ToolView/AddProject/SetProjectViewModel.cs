using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIOVision
{
    /// <summary>
    /// 选择流程
    /// </summary>
    public class SetProjectViewModel : ObservableObject
    {
        public ObservableCollection<ProjectInfo> ProName { get; set; } = new ObservableCollection<ProjectInfo>();

        /// <summary>
        /// 确认
        /// </summary>
        public ICommand ConfirmCom { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        public ICommand CancelFrmCom { get; set; }

        /// <summary>
        /// 选中流程
        /// </summary>
        public ICommand SelectCom { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SetProjectViewModel()
        {
            InitProjectInfo();

            this.ConfirmCom = new RelayCommand<object>(Confirm);

            this.CancelFrmCom = new RelayCommand<object>(CancelFrm);

            this.SelectCom = new RelayCommand<object>(Select);

        }

        /// <summary>
        /// 初始化流程信息
        /// </summary>
        private void InitProjectInfo()
        {
            foreach (Project item in Solution.Ins.g_ProjectList)
            {
                ProName.Add(item.ProjectInfo);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="obj"></param>
        private void Confirm(object obj)
        {
            Window win = obj as Window;

            if (win != null)
            {
                win.Close();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="obj"></param>
        private void CancelFrm(object obj)
        {
            Window win = obj as Window;

            if (win != null)
            {
                win.Close();
            }
        }

        /// <summary>
        /// 选中项
        /// </summary>
        /// <param name="obj"></param>
        private void Select(object obj)
        {

        }



    }
}
