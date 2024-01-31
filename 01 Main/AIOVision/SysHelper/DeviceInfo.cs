using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    public class DeviceInfo
    {
        //驱动名称
        public string DeviceName { get; set; }
        //驱动类型
        public string DeviceType { get; set; }
        //显示图标
        public object IconImage { get; set; }
        //背景色
        public string ImageColor { get; set; }
        //是否链接
        public bool IsConnected { get; set; }

    }
}
