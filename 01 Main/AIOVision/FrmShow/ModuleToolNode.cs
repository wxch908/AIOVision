using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AIOVision
{
    public class ModuleToolNode
    {
        public List<ModuleToolNode> Children { get; set; }
        public ModuleToolNode()
        {
            Children = new List<ModuleToolNode>();
        }

        public bool IsCategory { get; set; } = false;

        public ImageSource IconImage { get; set; }

        private string m_Name;

        public string Name
        {
            get { return m_Name?.Trim(); }
            set { m_Name = value; }
        }

        //排序使用
        public int SortNo { get; set; }
    }
}
