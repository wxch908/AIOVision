using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AIOVision
{
    public class VisionViewModel
    {
        #region 单例
        private static readonly VisionViewModel instance = new VisionViewModel();
        private VisionViewModel()
        {
            
        }
        public static VisionViewModel Ins
        {
            get { return instance; }
        }
        #endregion
        /// <summary>
        /// 显示的窗体
        /// </summary>
        private Grid _dispHindow;
        public Grid DispHindow
        {
            get { return _dispHindow; }
            set { _dispHindow = value; }
        }
    }
}
