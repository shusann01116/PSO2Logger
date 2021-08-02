using ControlzEx.Theming;
using MahApps.Metro.Controls;
using PSO2Logger.Core;
using System.Windows;

namespace PSO2Logger.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();

            ThemeManager.Current.ChangeTheme(this, AppThemes.LightRed);
            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;
            ThemeManager.Current.SyncTheme();
        }
    }
}
