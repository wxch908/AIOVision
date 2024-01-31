﻿using System;
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
using System.Windows.Shapes;

namespace AIOVision
{
    /// <summary>
    /// FrmReName.xaml 的交互逻辑
    /// </summary>
    public partial class FrmReName : Window
    {
        public FrmReName(string FrmName, int id)
        {
            InitializeComponent();
            this.DataContext = new ReNameToolViewModel(FrmName, id);
        }
    }
}
