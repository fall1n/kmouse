using System.Windows;
using System.Windows.Input;

namespace KMouse.NotifyIcon;

public class NotifyIconViewModel
{
    /// <summary>
    /// Shows a window, if none is already open.
    /// </summary>
    public ICommand ShowWindowCommand
    {
        get
        {
            return new DelegateCommand
            {
                CanExecuteFunc = () => Application.Current.MainWindow == null ||
                                       !Application.Current.MainWindow.IsVisible,

                CommandAction = () =>
                {
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.Show();
                }
            };
        }
    }

    /// <summary>
    /// Hides the main window. This command is only enabled if a window is open.
    /// </summary>
    public ICommand HideWindowCommand
    {
        get
        {
            return new DelegateCommand
            {
                CommandAction = () => Application.Current.MainWindow.Hide(),
                CanExecuteFunc = () => Application.Current.MainWindow != null &&
                                       Application.Current.MainWindow.IsVisible
            };
        }
    }


    /// <summary>
    /// Shuts down the application.
    /// </summary>
    public ICommand ExitApplicationCommand
    {
        get { return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() }; }
    }
}