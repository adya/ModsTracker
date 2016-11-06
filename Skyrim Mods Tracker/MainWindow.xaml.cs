using SMT.Managers;
using SMT.Utils;
using SMT.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(ModsManager.Mods);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Would you like to save changes?", "Unsaved Changes", MessageBoxButton.YesNoCancel)) {
                case MessageBoxResult.Cancel: e.Cancel = true; break;
                case MessageBoxResult.Yes: StorageManager.Sync(); break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var servers = new ServersWindow();
            servers.DataContext = new ServersViewModel(ServersManager.Servers);
            servers.ShowDialog();
        }

        private void dgMods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = sender as Selector;
            DataGrid dataGrid = selector as DataGrid;
            if (dataGrid != null && selector.SelectedItem != null && dataGrid.SelectedIndex >= 0)
            {
                dataGrid.ScrollIntoView(selector.SelectedItem);
            }
        }

        private void dgMods_DragOver(object sender, DragEventArgs e)
        {

        }

        private void dgSources_Drop(object sender, DragEventArgs e)
        {
            var viewModel = (DataContext as MainViewModel);
            var frmts = e.Data.GetFormats();

            if (e.Data.GetDataPresent(ChromeUtils.DDChromeBookmarks))
            {
                using (MemoryStream ms = e.Data.GetData(ChromeUtils.DDChromeBookmarks) as MemoryStream)
                {
                    var urls = ChromeUtils.ReadBookmarks(ms.ToArray());
                    foreach (var url in urls)
                        viewModel.AddModSource(url.URL);
                }
            }
            else if (e.Data.GetDataPresent(ChromeUtils.DDText))
            {
                viewModel.AddModSource(e.Data.GetData(ChromeUtils.DDText) as string);
            }
            
        }

        private void dgSources_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(ChromeUtils.DDText) || e.Data.GetDataPresent(ChromeUtils.DDChromeBookmarks))
                e.Effects = DragDropEffects.Link;
            else
                e.Effects = DragDropEffects.None;
        }

        private void GroupBox_DragEnter(object sender, DragEventArgs e)
        {
            var item = e.Data;
        }
    }
}
