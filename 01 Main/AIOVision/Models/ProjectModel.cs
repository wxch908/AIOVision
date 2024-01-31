using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    public class ProjectModel : ObservableObject
    {
        /// <summary>
        /// 显示ID
        /// </summary>
        private string _ID;

        public string m_ID
        {
            get { return _ID; }
            set { SetProperty(ref _ID,value); }
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        private string _ProjectName;

        public string m_ProjectName
        {
            get { return _ProjectName; }
            set { SetProperty(ref _ProjectName, value); }
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        private int _ProjectID;

        public int m_ProjectID
        {
            get { return _ProjectID; }
            set { SetProperty(ref _ProjectID, value); }
        }

        /// <summary>
        /// 注释
        /// </summary>
        private string _ProjectTip;

        public string m_ProjectTip
        {
            get { return _ProjectTip; }
            set { SetProperty(ref _ProjectTip,value); }
        }

    }
}
