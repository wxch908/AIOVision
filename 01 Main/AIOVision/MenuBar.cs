using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    public class MenuBar : ObservableObject
    {
        #region 字段

        private string name;
        private string tag;
        private string icon;
        private bool enable;

        #endregion

        #region 属性

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag
        {
            get => tag;
            set => SetProperty(ref tag, value);
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        /// <summary>
        /// 使能
        /// </summary>
        public bool Enable
        {
            get => enable;
            set => SetProperty(ref enable, value);
        }
        #endregion

        #region 方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public MenuBar()
        {

        }

        #endregion
    }
}
