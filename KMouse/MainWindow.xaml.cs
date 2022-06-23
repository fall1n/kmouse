using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using KMouse.ViewModel;

namespace KMouse
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private static KmouseInterop.LowLevelKeyboardProc _proc = HookCallback;

        public static int Sens = 0;

        public MainWindow()
        {
            KmouseInterop.HookId = KmouseInterop.SetHook(_proc);
            InitializeComponent();
            DataContext = _viewModel = new MainWindowViewModel();
            Closing += OnClosing;
            IsVisibleChanged += OnIsVisibleChanged;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _viewModel.Init();
            Sens = _viewModel.SensValue;
            Hide();
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            /*if ((bool)e.NewValue)
            {
                UnhookWindowsHookEx(_hookID);    
            }
            else
            {
                _hookID = SetHook(_proc);
            }*/
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
        }


        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var w32Mouse = new KmouseInterop.Win32Point();
                KmouseInterop.GetCursorPos(ref w32Mouse);

                if (Keyboard.IsKeyDown(Key.Right) && !Keyboard.IsKeyDown(Key.Up) && !Keyboard.IsKeyDown(Key.Down))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X + Sens, w32Mouse.Y);
                }

                if (Keyboard.IsKeyDown(Key.Right) && Keyboard.IsKeyDown(Key.Up))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X + Sens, w32Mouse.Y - Sens);
                }

                if (Keyboard.IsKeyDown(Key.Right) && Keyboard.IsKeyDown(Key.Down))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X + Sens, w32Mouse.Y + Sens);
                }

                if (Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Up) && !Keyboard.IsKeyDown(Key.Down))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X - Sens, w32Mouse.Y);
                }

                if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Up))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X - Sens, w32Mouse.Y - Sens);
                }

                if (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Down))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X - Sens, w32Mouse.Y + Sens);
                }

                if (Keyboard.IsKeyDown(Key.Up) && !Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Right))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X, w32Mouse.Y - Sens);
                }

                if (Keyboard.IsKeyDown(Key.Down) && !Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Right))
                {
                    KmouseInterop.SetCursorPos(w32Mouse.X, w32Mouse.Y + Sens);
                }

                if (Keyboard.IsKeyDown(Key.Space))
                {
                    KmouseInterop.mouse_event(KmouseInterop.MOUSEEVENTF_LEFTDOWN, w32Mouse.X, w32Mouse.Y, 0, 0);
                    KmouseInterop.mouse_event(KmouseInterop.MOUSEEVENTF_LEFTUP, w32Mouse.X, w32Mouse.Y, 0, 0);
                }
            }

            return KmouseInterop.CallNextHookEx(KmouseInterop.HookId, nCode, wParam, lParam);
        }
    }
}