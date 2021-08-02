﻿using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Windows;

namespace PSO2Logger.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();

            ThemeManager.Current.SyncTheme(ThemeSyncMode.SyncAll);
        }
    }
}