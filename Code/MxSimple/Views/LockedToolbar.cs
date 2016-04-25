using System.Windows;
using System.Windows.Controls;

namespace MxSimple.Views
{
    public class LockedToolBar : ToolBar
    {
        public LockedToolBar()
        {
            Loaded += new RoutedEventHandler(LockedToolBar_Loaded);
            ToolBarTray.SetIsLocked(this, true);
            Menu menu = new Menu();
            ToolBar.SetOverflowMode(menu, OverflowMode.Never);
            Items.Add(menu);
        }

        private void LockedToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
