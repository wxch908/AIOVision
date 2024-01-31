using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;

namespace AIOVision
{
    public class AddProjectViewModel:ObservableObject
    {
        #region 属性
        private string _process;
        /// <summary>
        /// 当前选定流程名称
        /// </summary>
        public string Process
        {
            get { return _process; }
            set { SetProperty(ref _process, value); }
        }
        
        private string _selectProName;
        /// <summary>
        /// 当前选定流程名称
        /// </summary>
        public string SelectProName
        {
            get { return _selectProName; }
            set { SetProperty(ref _selectProName,value); }
        }
        /// <summary>
        /// 新增流程
        /// </summary>
        public ICommand Addproject { get; set; }

        /// <summary>
        /// 删除流程
        /// </summary>
        public ICommand Deteproject { get; set; }

        /// <summary>
        /// 选中流程
        /// </summary>
        public ICommand Selectproject { get; set; }

        /// <summary>
        /// 鼠标右击
        /// </summary>
        public ICommand RightButtonDownCom { get; set; }

        /// <summary>
        /// 复制
        /// </summary>
        public ICommand CopyCom { get; set; }

        /// <summary>
        /// 粘贴
        /// </summary>
        public ICommand PasteCom { get; set; }

        /// <summary>
        /// 重命名
        /// </summary>
        public ICommand ReNameCom { get; set; }

        /// <summary>
        /// 删除流程
        /// </summary>
        public ICommand DeleteCom { get; set; }

        /// <summary>
        /// 拷贝的流程
        /// </summary>
        private Project CopyPrj;
        
        #region 控件的Enabel

        /// <summary>
        /// 控件的enable
        /// </summary>
        private bool _addcontrolIsEnabled = true;

        public bool AddControlIsEnabled
        {
            get { return _addcontrolIsEnabled; }
            set { SetProperty(ref _addcontrolIsEnabled, value); }
        }

        #endregion
        public ObservableCollection<ProjectModel> ProName { get; set; } = new ObservableCollection<ProjectModel>();

        private CommonBase common = new CommonBase();

        private Dispatcher m_Dispatcher = Dispatcher.CurrentDispatcher;
        #endregion 属性
        public AddProjectViewModel()
        {
            //项目流程界面更新
            DataEventChange.changedProjectEvent += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    RefreshListBox();
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {
                        RefreshListBox();
                    }));
                }
            };
            //Nva窗体，运行按钮控制添加窗体控件使能状态
            //Nva窗体，所有项目执行一次
            DataEventChange.NvaControlEnabelChangeHandler += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    AddControlIsEnabled = e;
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {
                        AddControlIsEnabled = e;
                    }));
                }
            };
            //模块来显示启动按钮是否运行中
            DataEventChange.ProjectChangedEvent += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    if (e)
                    {
                        AddControlIsEnabled = e;
                    }
                }
                else
                {
                    m_Dispatcher.BeginInvoke((Action)(() =>
                    {
                        if (e)
                        {
                            AddControlIsEnabled = e;
                        }
                    }));
                }
            };
            //事件来自Control
            DataEventChange.PrecessChangeHandler += (e) =>
            {
                if (m_Dispatcher.CheckAccess())
                {
                    AddControlIsEnabled = e;
                }
                else
                {

                    AddControlIsEnabled = e;
                }
            };
            this.Addproject = new RelayCommand<object>(AddNewProject);

            this.Deteproject = new RelayCommand<object>(DelectProject);

            this.Selectproject = new RelayCommand<object>(SelectLstProject);

            this.CopyCom = new RelayCommand<object>(Copy);

            this.PasteCom = new RelayCommand<object>(Paste);

            this.ReNameCom = new RelayCommand<object>(ReName);

            this.DeleteCom = new RelayCommand<object>(Delete);

            this.RightButtonDownCom = new RelayCommand<object>(RightButtonDown);
        }

        /// <summary>
        /// 刷新ListBox
        /// </summary>
        private void RefreshListBox()
        {
            ProName.Clear();
            for (int i = 0; i < Solution.Ins.g_ProjectList.Count; i++)
            {
                ProName.Add(new ProjectModel
                {
                    m_ID = i + ".",
                    m_ProjectName = Solution.Ins.g_ProjectList[i].ProjectInfo.m_ProjectName,
                    m_ProjectID = Solution.Ins.g_ProjectList[i].ProjectInfo.m_ProjectID,
                });
            }
        }
        /// <summary>
        /// 刷新ListBox
        /// </summary>
        private void RefreshListBox(System.Windows.Controls.ListBox listBox)
        {
            int index = 0;
            if (listBox != null)
            {
                index = listBox.SelectedIndex;
            }
            ProName.Clear();
            for (int i = 0; i < Solution.Ins.g_ProjectList.Count; i++)
            {
                ProName.Add(new ProjectModel
                {
                    m_ID = i + ".",
                    m_ProjectName = Solution.Ins.g_ProjectList[i].ProjectInfo.m_ProjectName,
                    m_ProjectID = Solution.Ins.g_ProjectList[i].ProjectInfo.m_ProjectID,
                });
            }
            if (listBox != null)
            {
                listBox.SelectedIndex = index;
            }
        }
        /// <summary>
        /// 添加新的流程
        /// </summary>
        /// <param name="obj"></param>
        private void AddNewProject(object obj)
        {
            //项目名称，项目路径地址
            if (Solution.Ins.SolutionName == string.Empty || Solution.Ins.SolutionPath == string.Empty)
            {
                System.Windows.MessageBox.Show("未创建解决方案！");
                //Log.Info("未创建解决方案！");
                return;
            }

            FrmAddPro frmAddPro = new FrmAddPro();
            frmAddPro.ShowDialog();
            Solution.Ins.Cur_Project = null;
            RefreshListBox();
        }
        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="obj"></param>
        private void DelectProject(object obj)
        {
            try
            {
                System.Windows.Controls.ListBox listBox = obj as System.Windows.Controls.ListBox;
                if (obj != null)
                {
                    int selectIndex = listBox.SelectedIndex;
                    //int index = SysProcessPro.g_ProjectList.FindIndex(c => c.ProjectInfo.m_ProjectName == SelectProName);
                    if (selectIndex > -1)
                    {
                        Solution.Ins.g_ProjectList.RemoveAt(selectIndex);
                        Solution.Ins.Cur_Project = null;
                        RefreshListBox(listBox);
                        listBox.SelectedIndex = selectIndex - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //Log.Error(ex.ToString());
            }
        }
        /// <summary>
        /// 选中列表
        /// </summary>
        /// <param name="obj"></param>
        private void SelectLstProject(object obj)
        {
            if (obj != null)
            {
                ProjectModel project = (ProjectModel)obj;
                common.RefreshToolList(project.m_ProjectName);
                SelectProName = project.m_ProjectName;
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="obj"></param>
        private void Copy(Object obj)
        {
            try
            {
                System.Windows.Controls.ListBox listBox = obj as System.Windows.Controls.ListBox;
                if (listBox != null)
                {
                    int selectIndex = listBox.SelectedIndex;
                    if (selectIndex > -1)
                    {
                        CopyPrj = null;
                        CopyPrj = (Project)Solution.Ins.g_ProjectList[selectIndex].Clone();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //Log.Error(ex.ToString());
            }
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="obj"></param>
        private void Paste(Object obj)
        {
            try
            {
                if (SelectProName != null && CopyPrj != null)
                {
                    System.Windows.Controls.ListBox listBox = obj as System.Windows.Controls.ListBox;
                    int selectIndex = listBox.SelectedIndex;
                    int index = Solution.Ins.g_ProjectList.FindIndex(c => c.ProjectInfo.m_ProjectID == ProName[selectIndex].m_ProjectID &&
                    c.ProjectInfo.m_ProjectName == ProName[selectIndex].m_ProjectName);
                    if (index > -1)
                    {
                        Project project = new Project();

                        //流程ID
                        CopyPrj.ProjectInfo.m_ProjectID = project.ProjectInfo.m_ProjectID;

                        //清空变量列表
                        CopyPrj.m_Var_List.Clear();

                        foreach (ModuleObjBase item in CopyPrj.m_ModuleObjList)
                        {
                            item.ModuleParam.ProjectID = CopyPrj.ProjectInfo.m_ProjectID;
                        }

                        //名称
                        CopyPrj.ProjectInfo.m_ProjectName = CopyPrj.ProjectInfo.m_ProjectName + "备份";
                        //添加数据
                        Solution.Ins.g_ProjectList.Insert(selectIndex + 1, CopyPrj);
                        RefreshListBox(listBox);
                        listBox.SelectedIndex = selectIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //Log.Error(ex.ToString());
            }
        }
        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="obj"></param>
        private void ReName(object obj)
        {
            try
            {
                System.Windows.Controls.ListBox listBox = obj as System.Windows.Controls.ListBox;
                if (listBox != null)
                {
                    int selectIndex = listBox.SelectedIndex;
                    if (selectIndex > -1)
                    {
                        FrmReName frmReName = new FrmReName(SelectProName, ProName[selectIndex].m_ProjectID);
                        frmReName.ShowDialog();

                        Solution.Ins.Cur_Project = null;
                        RefreshListBox(listBox);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //Log.Error(ex.ToString());
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        private void Delete(Object obj)
        {
            try
            {
                System.Windows.Controls.ListBox listBox = obj as System.Windows.Controls.ListBox;
                if (obj != null)
                {
                    int selectIndex = listBox.SelectedIndex;
                    if (selectIndex > -1)
                    {
                        Solution.Ins.g_ProjectList.RemoveAt(selectIndex);
                        Solution.Ins.Cur_Project = null;
                        RefreshListBox(listBox);
                        listBox.SelectedIndex = selectIndex - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //Log.Error(ex.ToString());
            }
        }
        /// <summary>
        /// 鼠标右击事件
        /// </summary>
        /// <param name="obj"></param>
        private void RightButtonDown(object obj)
        {
            if (obj != null)
            {
                SetProject set = new SetProject();
                set.ShowDialog();
            }
        }
    }
}
