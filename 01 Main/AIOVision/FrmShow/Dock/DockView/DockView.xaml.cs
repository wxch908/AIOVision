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
    /// DockView.xaml 的交互逻辑
    /// </summary>
    public partial class DockView : UserControl
    {
        #region 单例
        private static DockView instance;
        private DockView()
        {
            InitializeComponent();
            this.DataContext = DockViewModel.Ins;
        }
        public static DockView Ins
        {
            get 
            {
                if (instance == null)
                    instance = new DockView();
                return instance; 
            }
        }
        #endregion 
    }
}
