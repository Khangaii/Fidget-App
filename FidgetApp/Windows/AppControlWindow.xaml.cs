using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FidgetApp.Windows
{
    public partial class AppControlWindow : Window
    {
        private Window currentWindow;

        public AppControlWindow()
        {
            InitializeComponent();

            currentWindow = new PhysicsBallWindow
            {
                Tag = "0"
            };

            currentWindow.Show();
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

        private void AppMode_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            string previousTag = currentWindow.Tag.ToString();
            string currentTag = toggleButton.Tag.ToString();

            if (previousTag != currentTag)
            {
                ToggleButton previousToggleButton = AppModes.Children[Convert.ToInt32(previousTag)] as ToggleButton;

                currentWindow.Close();

                switch (currentTag)
                {
                    case "0":
                        currentWindow = new PhysicsBallWindow();
                        break;
                    case "1":
                        currentWindow = new AimTrainerWindow();
                        break;
                }
                currentWindow.Tag = currentTag;

                // Uncheck previous app mode toggle button
                previousToggleButton.IsChecked = false;

                currentWindow.Show();
            }
        }

        private void AppMode_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            if (currentWindow.Tag.ToString() == toggleButton.Tag.ToString())
            {
                toggleButton.IsChecked = true;
            }
        }
    }
}
