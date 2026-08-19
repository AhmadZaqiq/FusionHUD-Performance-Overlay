using FusionHUD_Performance_Overlay.Interfaces;
using FusionHUD_Performance_Overlay.Models;
using FusionHUD_Performance_Overlay.ViewModels;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace FusionHUD_Performance_Overlay
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _PerformanceTimer = new DispatcherTimer();

        private MainViewModel _ViewModel;

        private readonly IHotkeyService _HotkeyService;

        private readonly IStartupService _StartupService;

        private readonly IOverlaySettingsService _OverlaySettingsService;

        private readonly IOverlayPositionService _OverlayPositionService;

        private readonly IOverlaySizeService _OverlaySizeService;

        private readonly IOverlayColorService _OverlayColorService;

        private readonly IOverlayAlignmentService _OverlayAlignmentService;

        public MainWindow(IHotkeyService HotkeyService, IStartupService StartupService,
                          IPerformanceMonitorService PerformanceMonitorService, IOverlaySettingsService OverlaySettingsService,
                          IOverlayPositionService OverlayPositionService, IOverlaySizeService OverlaySizeService,
                          IOverlayColorService OverlayColorService, IOverlayAlignmentService OverlayAlignmentService)
        {
            InitializeComponent();

            _HotkeyService = HotkeyService;

            _StartupService = StartupService;

            _OverlaySettingsService = OverlaySettingsService;

            _OverlayPositionService = OverlayPositionService;

            _OverlaySizeService = OverlaySizeService;

            _OverlayColorService = OverlayColorService;

            _OverlayAlignmentService = OverlayAlignmentService;

            _ViewModel = new MainViewModel(PerformanceMonitorService);

            DataContext = _ViewModel;

            _SetOverlaySize();

            _SetOverlayPosition();

            _SetOverlayColor();

            _StartupService.EnableStartup();

            _MakeClickThrough();

            _StartMonitoring();
        }

        private void _SetOverlayPosition()
        {
            OverlayPosition Position = _OverlaySettingsService.Settings.Position;

            _OverlayPositionService.ApplyPosition(this, Position);

            _OverlayAlignmentService.ApplyAlignment(this, Position);
        }

        private void _SetOverlaySize()
        {
            _OverlaySizeService.ApplySize(this, _OverlaySettingsService.Settings.Size);
        }

        private void _SetOverlayColor()
        {
            _OverlayColorService.ApplyColor(this, _OverlaySettingsService.Settings.Color);
        }

        private void _StartMonitoring()
        {
            _PerformanceTimer.Interval = TimeSpan.FromMilliseconds(250);

            _PerformanceTimer.Tick += _PerformanceTimer_Tick;

            _PerformanceTimer.Start();
        }

        private void _PerformanceTimer_Tick(object sender, EventArgs e)
        {
            _ViewModel.Update();
        }

        private void _MakeClickThrough()
        {
            IntPtr Handle = new WindowInteropHelper(this).Handle;

            int Style = GetWindowLong(Handle, -20);

            SetWindowLong(Handle, -20, Style | 0x80000 | 0x20);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            IntPtr Handle = new WindowInteropHelper(this).Handle;

            HwndSource Source = HwndSource.FromHwnd(Handle);

            Source.AddHook(_WindowHook);

            _HotkeyService.Register(Handle);
        }

        private IntPtr _WindowHook(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            OverlayHotkeyAction? Action = _HotkeyService.GetHotkeyAction(message, wParam);

            if (Action == OverlayHotkeyAction.ToggleVisibility)
            {
                Visibility = Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

                handled = true;
            }
            else if (Action == OverlayHotkeyAction.ChangePosition)
            {
                _OverlaySettingsService.MoveToNextPosition();

                _SetOverlayPosition();

                handled = true;
            }
            else if (Action == OverlayHotkeyAction.ChangeSize)
            {
                _OverlaySettingsService.MoveToNextSize();

                _SetOverlaySize();

                UpdateLayout();

                _SetOverlayPosition();

                handled = true;
            }

            else if (Action == OverlayHotkeyAction.ChangeColor)
            {
                _OverlaySettingsService.MoveToNextColor();

                _SetOverlayColor();

                handled = true;
            }

            return IntPtr.Zero;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int Index);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int Index, int Value);
    }
}