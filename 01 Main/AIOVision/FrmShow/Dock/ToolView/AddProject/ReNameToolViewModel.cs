using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIOVision
{
    /// <summary>
    /// 重命名
    /// </summary>
    public class ReNameToolViewModel : ObservableObject
    {
        /// <summary>
        /// 旧名称
        /// </summary>
        private string _oldName;

        public string m_oldName
        {
            get { return _oldName; }
            set { SetProperty(ref _oldName, value); }
        }

        /// <summary>
        /// 新名称
        /// </summary>
        private string _newName;

        public string m_newName
        {
            get { return _newName; }
            set { SetProperty(ref _newName, value); }
        }

        private int _ID;

        public int m_ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        /// <summary>
        /// 确定
        /// </summary>
        public ICommand ConfirmCom { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        public ICommand CancelfirmCom { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReNameToolViewModel(string FrmName, int id)
        {
            m_oldName = FrmName;
            m_ID = id;

            this.ConfirmCom = new RelayCommand<object>(Confirm);

            this.CancelfirmCom = new RelayCommand<object>(Cancelfirm);

        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="obj"></param>
        private void Confirm(object obj)
        {
            //判断输入是否为空
            if (m_newName == null || m_newName.Length == 0)
            {

                MessageBox.Show("输入为空！");
                return;
            }

            //判断是否有重复名称
            int index = Solution.Ins.g_ProjectList.FindIndex(c => c.ProjectInfo.m_ProjectName == m_newName);
            if (index > -1)
            {
                MessageBox.Show("存在重复的流程名称！");
                return;
            }

            Solution.Ins.Cur_Project.ProjectInfo.m_ProjectName = m_newName;

            (obj as System.Windows.Window).Close();

        }
        private void Cancelfirm(object obj)
        {
            (obj as System.Windows.Window).Close();
        }


    }
}
