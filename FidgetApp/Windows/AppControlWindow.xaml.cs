using FidgetApp.Commands;
using Hardcodet.Wpf.TaskbarNotification;
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
        // Window corresponding to current mode
        private Window currentWindow;

        // NotifyIcon
        private TaskbarIcon notifyIcon;
        
        private readonly CommandBinding exitBinding;
        public static readonly RoutedCommand Exit = new RoutedCommand("Exit", typeof(CustomCommands));

        public AppControlWindow()
        {
            InitializeComponent();

            notifyIcon = (TaskbarIcon)FindResource("AppNotifyIcon");

            // Bind the exit command to self and children
            exitBinding = new CommandBinding(CustomCommands.Exit);
            exitBinding.CanExecute += ExitCommand_CanExecute;
            exitBinding.Executed += ExitCommand_Executed;
            this.CommandBindings.Add(exitBinding);

            currentWindow = new PhysicsBallWindow
            {
                Tag = "0" // To differentiate between child windows
            };
            currentWindow.CommandBindings.Add(exitBinding);

            currentWindow.Show();

            var appNotifyIcon = (TaskbarIcon)FindResource("AppNotifyIcon");
            appNotifyIcon.ContextMenu.CommandBindings.Add(exitBinding);
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Window drag
        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // Collapse or expand app menu when the toggle button is clicked
        private void AppMenu_ToggleCollapse(object sender, RoutedEventArgs e)
        {
            if (MenuCollapse_ToggleButton.IsChecked == true) // Collapse
            {
                MenuCollapse_ToggleButton.Content = "△";
                for (int i = 1; i < AppMenu.RowDefinitions.Count; i++)
                {
                    AppMenu.RowDefinitions[i].Height = new GridLength(0);
                }
            }
            else // Expand
            {
                MenuCollapse_ToggleButton.Content = "▽";
                for (int i = 1; i < AppMenu.RowDefinitions.Count; i++)
                {
                    AppMenu.RowDefinitions[i].Height = GridLength.Auto;
                }
            }
        }

        // Change app mode
        private void AppMode_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            string previousTag = currentWindow.Tag.ToString();
            string currentTag = toggleButton.Tag.ToString();

            if (previousTag != currentTag)
            {
                ToggleButton previousToggleButton = AppModes.Children[Convert.ToInt32(previousTag)] as ToggleButton;

                currentWindow.Close();

                // Change app corresponding to tag
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
                currentWindow.CommandBindings.Add(exitBinding);

                // Uncheck previous app mode toggle button
                previousToggleButton.IsChecked = false;

                currentWindow.Show();
            }
        }

        // Make app modes non-uncheckable
        private void AppMode_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            if (currentWindow.Tag.ToString() == toggleButton.Tag.ToString())
            {
                toggleButton.IsChecked = true;
            }
        }

        // Keep app on top
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }
    }
}
