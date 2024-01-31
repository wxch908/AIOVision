using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIOVision
{
    /// <summary>
    /// UIDisplayView.xaml 的交互逻辑
    /// </summary>
    public partial class UIDisplayView : UserControl
    {
        #region 单例
        private static readonly UIDisplayView instance = new UIDisplayView();
        public UIDisplayView()
        {
            InitializeComponent();
        }
        public static UIDisplayView Ins
        {
            get { return instance; }
        }
        #endregion 单例
    }
}
