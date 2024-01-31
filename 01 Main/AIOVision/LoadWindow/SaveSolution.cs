using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIOVision
{
    public class SaveSolution : LongTimeTask
    {
        private Thread m_threadWorking;
        private LoadWindow m_dlgWaiting;
        private string files;
        public void Start(LoadWindow load)
        {
            m_dlgWaiting = load;
            m_threadWorking = new Thread(Working);
            m_threadWorking.Start();
        }

        public SaveSolution(string str)
        {
            files = str;
        }

        private void Working()
        {
            if (files.Length != 0)
            {
                Solution.Ins.SaveConfig(files);
                System.GC.Collect();//主动回收下系统未使用的资源
            }
            m_dlgWaiting.TaskEnd(null);
        }

    }
}
