using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIOVision
{
    public class LoadSolution : LongTimeTask
    {
        private Thread m_threadWorking;
        private LoadWindow m_dlgWaiting;

        private System.Windows.Forms.OpenFileDialog openFileDialog;

        private CommonBase common = new CommonBase();//根据值显示一个流程
        

        public LoadSolution(System.Windows.Forms.OpenFileDialog opf)
        {
            openFileDialog = opf;
        }
        public void Start(LoadWindow load)
        {
            m_dlgWaiting = load;
            m_threadWorking = new Thread(Working);
            m_threadWorking.Start();
        }
        private void Working()
        {
            //获取选中文件文件夹名称
            string FolderName = System.IO.Path.GetDirectoryName(openFileDialog.FileName);

            //程序名称
            string NvName = System.IO.Path.GetFullPath(openFileDialog.FileName);

            //程序初始化
            Solution.Ins.InitialVisionPram(NvName);

            if (Solution.Ins.g_ProjectList == null)
            {
                Solution.Ins.g_ProjectList = new List<Project>();
            }

            if (Solution.Ins.g_ProjectList.Count < 1)
            {
                Solution.Ins.g_ProjectList.Add(new Project()); 
            }

            Solution.Ins.Cur_Project = Solution.Ins.g_ProjectList[0];

            Solution.Ins.SolutionPath = NvName.ToLowerInvariant();

            Solution.Ins.SysFliePath = FolderName;

            string filename = System.IO.Path.GetFileNameWithoutExtension(NvName);

            Solution.Ins.SolutionName = filename;

            DataEventChange.Footdata = Solution.Ins.SolutionPath;//更新底部窗体显示

            DataEventChange.FrmNum = SysLayout.LayoutFrmNum;//窗体布局的更新

            DataEventChange.ProjectData = true;//更新流程窗体显示

            common.RefreshToolList(Solution.Ins.Cur_Project.ProjectInfo.m_ProjectName);//委托刷新Process，UI界面

            //DataEventChange.DeviceFrmStatus = true;//更新通讯界面

            m_dlgWaiting.TaskEnd(null);
        }

    }
}
