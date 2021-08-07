using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace PSO2Logger.Modules.Chat.Behaviors {
    class DataGridBehavior {
        public static readonly DependencyProperty AutoscrollProperty = DependencyProperty.RegisterAttached(
            "Autoscroll", 
            typeof(bool), 
            typeof(DataGridBehavior), 
            new PropertyMetadata(default(bool), OnAutoScrollPropertyChanged)
        );

        private static readonly Dictionary<DataGrid, NotifyCollectionChangedEventHandler> handlersDict = new();

        private static void OnAutoScrollPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            if (dependencyObject is not DataGrid dataGrid) {
                throw new InvalidOperationException("Dependency object is not DataGrid.");
            }

            if ((bool)args.NewValue) {
                Subscribe(dataGrid);
                dataGrid.Unloaded += DataGridOnUnloaded;
                dataGrid.Loaded += DataGridOnLoaded;
            } else {
                Unsubscribe(dataGrid);
                dataGrid.Unloaded -= DataGridOnUnloaded;
                dataGrid.Loaded -= DataGridOnLoaded;
            }
        }

        private static void Subscribe(DataGrid dataGrid) {
            var handler = new NotifyCollectionChangedEventHandler((sender, eventArgs) => ScrollToEnd(dataGrid));
            if (handlersDict.TryGetValue(dataGrid, out var _hander) && _hander != handler) {
                handlersDict.Remove(dataGrid);
            }
            handlersDict.Add(dataGrid, handler);
            ((INotifyCollectionChanged)dataGrid.Items).CollectionChanged += handler;
            ScrollToEnd(dataGrid);
        }

        private static void Unsubscribe(DataGrid dataGrid) {
            NotifyCollectionChangedEventHandler handler;
            handlersDict.TryGetValue(dataGrid, out handler);
            if (handler == null) {
                return;
            }
            ((INotifyCollectionChanged)dataGrid.Items).CollectionChanged -= handler;
            handlersDict.Remove(dataGrid);
        }

        private static void DataGridOnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            var dataGrid = (DataGrid)sender;
            if (GetAutoscroll(dataGrid)) {
                Subscribe(dataGrid);
            }
        }

        private static void DataGridOnUnloaded(object sender, RoutedEventArgs routedEventArgs) {
            var dataGrid = (DataGrid)sender;
            if (GetAutoscroll(dataGrid)) {
                Unsubscribe(dataGrid);
            }
        }

        private static void ScrollToEnd(DataGrid datagrid) {
            if (datagrid.Items.Count == 0) {
                return;
            }
            datagrid.ScrollIntoView(datagrid.Items[datagrid.Items.Count - 1]);
        }

        public static void SetAutoscroll(DependencyObject element, bool value) {
            element.SetValue(AutoscrollProperty, value);
        }

        public static bool GetAutoscroll(DependencyObject element) {
            return (bool)element.GetValue(AutoscrollProperty);
        }
    }
}
