using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIOVision
{
    public class RightControl : ObservableObject
    {
        #region 单例模式
        private static Lazy<RightControl> Instance = new Lazy<RightControl>(() => new RightControl());
        public RightControl() { }
        public static RightControl Ins { get; set; } = Instance.Value;
        #endregion
        #region 属性
        private bool _QuickMode = true;
        public bool QuickMode
        {
            get { return _QuickMode; }
            set => SetProperty(ref _QuickMode, value);
        }

        private bool _CommunicationSet = true;
        public bool CommunicationSet
        {
            get { return _CommunicationSet; }
            set => SetProperty(ref _CommunicationSet, value);
        }
        private bool _HardwareConfig = true;
        public bool HardwareConfig
        {
            get { return _HardwareConfig; }
            set => SetProperty(ref _HardwareConfig, value);
        }
        private bool _Camera = true;
        public bool Camera
        {
            get { return _Camera; }
            set => SetProperty(ref _Camera, value);
        }
        private bool _Temperature = true;
        public bool Temperature
        {
            get { return _Temperature; }
            set => SetProperty(ref _Temperature, value);
        }
        private bool _Barcode = true;
        public bool Barcode
        {
            get { return _Barcode; }
            set
            {
                SetProperty(ref _Barcode, value);
            }
        }
        private bool _HeightSensor = true;
        public bool HeightSensor
        {
            get { return _HeightSensor; }
            set
            {
                SetProperty(ref _HeightSensor, value);
            }
        }
        private bool _PressureSensor = true;
        public bool PressureSensor
        {
            get { return _PressureSensor; }
            set
            {
                SetProperty(ref _PressureSensor, value);
            }
        }

        private bool _InputAndOutput = true;
        public bool InputAndOutput
        {
            get { return _InputAndOutput; }
            set
            {
                SetProperty(ref _InputAndOutput, value);
            }
        }
        private bool _open = true;
        public bool Open
        {
            get { return _open; }
            set
            {
                SetProperty(ref _open, value);
            }
        }
        private bool _edit = true;
        public bool Edit
        {
            get { return _edit; }
            set
            {
                SetProperty(ref _edit, value);
            }
        }
        private bool _save = true;
        public bool Save
        {
            get { return _save; }
            set
            {
                SetProperty(ref _save, value);
            }
        }

        private bool _runOnce = true;
        public bool RunOnce
        {
            get { return _runOnce; }
            set
            {
                SetProperty(ref _runCycle, value);
            }
        }
        private bool _runCycle = true;
        public bool RunCycle
        {
            get { return _runCycle; }
            set => SetProperty(ref _runCycle, value);
        }
        private bool _stop = true;
        public bool Stop
        {
            get { return _stop; }
            set => SetProperty(ref _stop, value);
        }

        private bool _OpenFile = true;
        public bool OpenFile
        {
            get { return _OpenFile; }
            set => SetProperty(ref _OpenFile, value);
        }
        private bool _SaveFile = true;
        public bool SaveFile
        {
            get { return _SaveFile; }
            set => SetProperty(ref _SaveFile, value);
        }
        private bool _systemConfig = true;
        public bool SystemConfig
        {
            get { return _systemConfig; }
            set => SetProperty(ref _systemConfig, value);
        }
        private bool _OpenOrCloseCamera = true;
        public bool OpenOrCloseCamera
        {
            get { return _OpenOrCloseCamera; }
            set => SetProperty(ref _OpenOrCloseCamera, value);
        }
        private bool _RedLight = true;
        public bool RedLight
        {
            get { return _RedLight; }
            set => SetProperty(ref _RedLight, value);
        }
        private bool _DeviceParam = true;
        public bool DeviceParam
        {
            get { return _DeviceParam; }
            set => SetProperty(ref _DeviceParam, value);
        }
        private bool _SystemParam = true;
        public bool SystemParam
        {
            get { return _SystemParam; }
            set => SetProperty(ref _SystemParam, value);
        }
        private bool _View = true;
        public bool View
        {
            get { return _View; }
            set => SetProperty(ref _View, value);
        }
        private bool _ManufactureParam = true;
        public bool ManufactureParam
        {
            get { return _ManufactureParam; }
            set => SetProperty(ref _ManufactureParam, value);
        }
        private bool _TemplateLayout = true;
        public bool TemplateLayout
        {
            get { return _TemplateLayout; }
            set => SetProperty(ref _Temperature, value);
        }
        private bool _CameraSetting = true;
        public bool CameraSetting
        {
            get { return _CameraSetting; }
            set => SetProperty(ref _CameraSetting, value);
        }
        private bool _ServoDebug = true;
        public bool ServoDebug
        {
            get { return _ServoDebug; }
            set => SetProperty(ref _ServoDebug, value);
        }
        private bool _IODebug = true;
        public bool IODebug
        {
            get { return _IODebug; }
            set => SetProperty(ref _IODebug, value);
        }
        private bool _Home = true;
        public bool Home
        {
            get { return _Home; }
            set => SetProperty(ref _Home, value);
        }
        private bool _UIDesign = true;
        public bool UIDesign
        {
            get { return _UIDesign; }
            set => SetProperty(ref _UIDesign, value);
        }
        private bool _PowerDebug = true;
        public bool PowerDebug
        {
            get { return _PowerDebug; }
            set => SetProperty(ref _PowerDebug, value);
        }
        private bool _LaserDebug = true;
        public bool LaserDebug
        {
            get { return _LaserDebug; }
            set => SetProperty(ref _LaserDebug, value);
        }
        private bool _newSolution = true;
        public bool NewSolution
        {
            get { return _newSolution; }
            set => SetProperty(ref _newSolution, value);
        }
        private bool _SolutionList = true;
        public bool SolutionList
        {
            get { return _SolutionList; }
            set => SetProperty(ref _SolutionList, value);
        }
        private bool _GlobalVar = true;
        public bool GlobalVar
        {
            get { return _GlobalVar; }
            set => SetProperty(ref _GlobalVar, value);
        }
        private bool _CameraSet = true;
        public bool CameraSet
        {
            get { return _CameraSet; }
            set => SetProperty(ref _CameraSet, value);
        }
        private bool _LaserSet = true;
        public bool LaserSet
        {
            get { return _LaserSet; }
            set => SetProperty(ref _LaserSet, value);
        }
        #endregion
    }
}
