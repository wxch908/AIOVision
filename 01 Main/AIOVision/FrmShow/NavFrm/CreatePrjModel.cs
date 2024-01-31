using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    public class CreatePrjModel : ObservableObject
    {

        /// <summary>
        /// 工程名称
        /// </summary>
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { SetProperty(ref _projectName,value); }
        }

        /// <summary>
        /// 工程路径
        /// </summary>
        private string _projectPath;

        public string ProjectPath
        {
            get { return _projectPath; }
            set { SetProperty(ref _projectPath,value); }
        }

    }
}
