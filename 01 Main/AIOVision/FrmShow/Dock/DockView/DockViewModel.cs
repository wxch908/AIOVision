using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    public class DockViewModel
    {
        #region 单例
        private static readonly DockViewModel instance = new DockViewModel();
        private DockViewModel()
        {
            
        }
        public static DockViewModel Ins
        { 
            get { return instance; } 
        }
        #endregion 单例

    }
}
