using AIOVision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plugin.Delay
{
    [Category("00.常用工具")]
    [DisplayName("延时工具")]
    [Serializable]
    public class DelayViewModel:ModuleObjBase
    {
        public DelayViewModel() 
        {
            
        }
        public override void ExeModule(bool blnByHand = false)
        {
            MessageBox.Show("测试");
        }
    }
}
