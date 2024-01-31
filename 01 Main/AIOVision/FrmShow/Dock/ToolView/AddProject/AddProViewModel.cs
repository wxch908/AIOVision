﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AIOVision
{
    public class AddProViewModel : ObservableObject
    {
        //流程名称
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { SetProperty(ref _projectName,value); }
        }

        /// <summary>
        /// 创建一个项目流程
        /// </summary>
        public ICommand CreatePro { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        public ICommand CancelFrm { get; set; }

        public AddProViewModel()
        {
            this.CreatePro = new RelayCommand<object>(AddProject);

            this.CancelFrm = new RelayCommand<object>(new Action<object>((o) =>
            {
                (o as System.Windows.Window).Close();

            }));

        }

        private void AddProject(object obj)
        {

            if (ProjectName == null || ProjectName.Trim().Replace(" ", "").ToLowerInvariant().Length == 0)
            {
                System.Windows.MessageBox.Show("请输入流程名称！");
                //Log.Info("请输入流程名称！");
                return;
            }

            if (SameNmae(ProjectName))
            {
                System.Windows.MessageBox.Show("请输入流程相同,“清修改”！");
                //Log.Info("请输入流程相同,“清修改”！");
                return;
            }

            //新建一个项目流程
            Project P = new Project();

            P.ProjectInfo.m_ProjectName = ProjectName;

            Solution.Ins.g_ProjectList.Add(P);

            (obj as System.Windows.Window).Close();

        }

        private bool SameNmae(string Name)
        {
            int index = -1;
            foreach (Project item in Solution.Ins.g_ProjectList)
            {
                index = Solution.Ins.g_ProjectList.FindIndex(c => c.ProjectInfo.m_ProjectName == Name);
                if (index >= 0)
                {
                    break;
                }
            }
            return index >= 0 ? true : false;
        }

    }
}
