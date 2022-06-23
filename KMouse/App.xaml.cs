using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Hardcodet.Wpf.TaskbarNotification;

namespace KMouse
{
    /// <summary>
    /// Interaction logic for App.xamlusing Microsoft.Extensions.Configuration;
    /// </summary>
    public partial class App : Application
    {
        
        public static IConfiguration Configuration { get; private set; }
        
        private TaskbarIcon? notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon?.Dispose();
            base.OnExit(e);
        }
    }
}