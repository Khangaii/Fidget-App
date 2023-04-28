using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FidgetApp.Windows
{
    public partial class AppControlWindow : Window
    {
        private PhysicsBallWindow physicsBallWindow;

        public AppControlWindow()
        {
            InitializeComponent();

            physicsBallWindow = new PhysicsBallWindow();

            physicsBallWindow.Show();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AppMenu_ToggleCollapse(object sender, RoutedEventArgs e)
        {
            if (MenuCollapse_ToggleButton.IsChecked == true)
            {
                MenuCollapse_ToggleButton.Content = "△";
                for (int i = 1; i < AppMenu.RowDefinitions.Count; i++)
                {
                    AppMenu.RowDefinitions[i].Height = new GridLength(0);
                }
            }
            else
            {
                MenuCollapse_ToggleButton.Content = "▽";
                for (int i = 1; i < AppMenu.RowDefinitions.Count; i++)
                {
                    AppMenu.RowDefinitions[i].Height = GridLength.Auto;
                }
            }
        }
    }
}
