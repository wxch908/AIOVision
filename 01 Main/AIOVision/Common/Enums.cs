using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    /// <summary>
    /// 子窗体名称
    /// </summary>
    public enum SubViewNameEnum
    {
        UserLogin,
        DetectTool,
        CommunicationDevice,
        ProductManage,
        DataQuery,
        AlarmRecord,
        ManualOperation,
        IOMonitor,
        Setting,
        VisionEdit
    }
    /// <summary>
    /// 状态颜色
    /// </summary>
    public struct StatusColorStruct
    {
        public const string WHITE1 = "White";
        public const string WHITE2 = "#D6D6D6";
        public const string BLACK = "Black";
        public const string RED1 = "Red";
        public const string RED2 = "#861B2D";
        public const string RED3 = "#B30B00";
        public const string GREEN1 = "Green";
        public const string GREEN2 = "LimeGreen";
        public const string YELLOW = "Yellow";
        public const string GRAY1 = "Gray";
        public const string GRAY2 = "LightGray";
        public const string GRAY3 = "#424242";
        public const string GRAY4 = "#303030";
        public const string GRAY5 = "#252525";
        public const string GRAY6 = "#6B6B6B";
        public const string BLUE1 = "Blue";
        public const string BLUE2 = "#9A67EA";
        public const string BLUE3 = "#673AB7";
        public const string PURPLE1 = "Purple";
        public const string PURPLE2 = "#8F3FCC";
    }
    /// <summary>
    /// PLC输入名称
    /// </summary>
    public enum PLCInputNameEnum
    {
        启动压力检测,
        备用1,
        手自动切换,
        运转准备,
        A产品到位检测,
        B产品到位检测,
        A产品长度检测,
        B产品长度检测,
        A工位启动,
        A工位复位,
        A工位急停,
        A光栅信号,
        备用2,
        B光栅信号,
        备用3,
        备用4,
        B工位启动,
        B工位复位,
        B工位急停,
        备用5,
        备用6,
        备用7,
        备用8,
        备用9,
        A检漏仪测试OK,
        A检漏仪测试正压NG,
        A检漏仪测试负压NG,
        A检漏仪测试PNG,
        A检漏仪RDY,
        A检漏仪ERR,
        A检漏仪END,
        备用10,
        B检漏仪测试OK,
        B检漏仪测试正压NG,
        B检漏仪测试负压NG,
        B检漏仪测试PNG,
        B检漏仪RDY,
        B检漏仪ERR,
        B检漏仪END,
        备用11,
        A1气缸动点,
        A1气缸原点,
        A2气缸动点,
        A2气缸原点,
        A3气缸动点,
        A3气缸原点,
        A4气缸动点,
        A4气缸原点,
        A5气缸动点,
        A5气缸原点,
        A6气缸动点,
        A6气缸原点,
        备用12,
        备用13,
        备用14,
        备用15,
        B1气缸动点,
        B1气缸原点,
        B2气缸动点,
        B2气缸原点,
        B3气缸动点,
        B3气缸原点,
        B4气缸动点,
        B4气缸原点,
        B5气缸动点,
        B5气缸原点,
        B6气缸动点,
        B6气缸原点,
        备用16,
        备用17,
        备用18,
        备用19
    }

    /// <summary>
    /// PLC输出名称
    /// </summary>
    public enum PLCOutputNameEnum
    {
        设备运转中绿,
        设备准备中黄,
        设备故障,
        A真空泵,
        B真空泵,
        备用1,
        备用2,
        备用3,
        A工位产品OK,
        A工位产品NG,
        A工位测试中,
        A检漏仪Start,
        A检漏仪Reset,
        A检漏仪BUBL,
        A检漏仪CH1,
        A检漏仪CH2,
        B工位产品OK,
        B工位产品NG,
        B工位测试中,
        B检漏仪Start,
        B检漏仪Reset,
        B检漏仪BUBL,
        B检漏仪CH1,
        B检漏仪CH2,
        A1气缸伸出,
        A1气缸缩回,
        A2气缸伸出,
        A2气缸缩回,
        A3气缸伸出,
        A3气缸缩回,
        A4气缸伸出,
        A4气缸缩回,
        A5气缸伸出,
        A5气缸缩回,
        备用4,
        产品信号1,
        产品信号2,
        产品信号3,
        产品信号4,
        备用5,
        B1气缸伸出,
        B1气缸缩回,
        B2气缸伸出,
        B2气缸缩回,
        B3气缸伸出,
        B3气缸缩回,
        B4气缸伸出,
        B4气缸缩回,
        B5气缸伸出,
        B5气缸缩回,
        备用6,
        备用7,
        备用8,
        备用9,
        备用10,
        照明,
        备用11,
        备用12,
        备用13,
        备用14,
        备用15,
        备用16,
        备用17,
        备用18
    }
    /// <summary>
    /// 执行方式
    /// </summary>
    [Serializable]
    public enum Execution
    {
        主动执行,
        调用时执行,
        停止时执行,
    }
    
    internal class Enums
    {
    }
}
