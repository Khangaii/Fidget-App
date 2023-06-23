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
    /// <summary>
    /// Back-end code for the AppControlWindow
    /// </summary>
    public partial class AppControlWindow : Window
    {
        // Window corresponding to current mode
        private Window currentWindow;

        // NotifyIcon
        private TaskbarIcon notifyIcon;
        
        // CommandBinding for the Exit command
        private readonly CommandBinding exitBinding;
        public static readonly RoutedCommand Exit = new RoutedCommand("Exit", typeof(CustomCommands));

        /// <summary>
        /// AppControlWindow constructor
        /// </summary>
        public AppControlWindow()
        {
            InitializeComponent();

            // Bind the exit command to self
            exitBinding = new CommandBinding(CustomCommands.Exit);
            exitBinding.CanExecute += ExitCommand_CanExecute;
            exitBinding.Executed += ExitCommand_Executed;
            this.CommandBindings.Add(exitBinding);

            // Bind the exit command to the NotifyIcon context menu
            notifyIcon = (TaskbarIcon)FindResource("AppNotifyIcon");
            notifyIcon.ContextMenu.CommandBindings.Add(exitBinding);
        }

        /// <summary>
        /// Window Content Rendered event handler
        /// </summary>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Default starting mode is PhysicsBallWindow
            currentWindow = new PhysicsBallWindow
            {
                Tag = "0" // To differentiate between child windows
            };
            currentWindow.CommandBindings.Add(exitBinding);
            currentWindow.Owner = this;

            currentWindow.Show();
        }

        /// <summary>
        /// ExitCommand CanExecute method
        /// </summary>
        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// ExitCommand Executed method that quits the application
        /// </summary>
        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Drag the window when the drag bar is clicked
        /// </summary>
        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Collapse or expand app menu when the toggle button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppMenu_ToggleCollapse(object sender, RoutedEventArgs e)
        {
            if (MenuCollapse_ToggleButton.IsChecked == true) // Collapse
            {
                CollapseArrow_Image.RenderTransform = new ScaleTransform() { ScaleY = -1 };
                MenuCollapse_ToggleButton.Style = (Style)FindResource("Button_Round");
                for (int i = 1; i < AppMenu.RowDefinitions.Count; i++)
                {
                    AppMenu.RowDefinitions[i].Height = new GridLength(0);
                }
                
            }
            else // Expand
            {
                CollapseArrow_Image.RenderTransform = new ScaleTransform() { ScaleY = 1 };
                MenuCollapse_ToggleButton.Style = (Style)FindResource("Button_RoundTop");
                for (int i = 1; i < AppMenu.RowDefinitions.Count; i++)
                {
                    AppMenu.RowDefinitions[i].Height = GridLength.Auto;
                }
            }
        }

        /// <summary>
        /// Change app mode
        /// </summary>
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
                currentWindow.Owner = this;
                currentWindow.Tag = currentTag;
                currentWindow.CommandBindings.Add(exitBinding);

                // Uncheck previous app mode toggle button
                previousToggleButton.IsChecked = false;

                currentWindow.Show();
            }
        }

        /// <summary>
        /// Make app modes non-uncheckable
        /// </summary>
        private void AppMode_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            if (currentWindow.Tag.ToString() == toggleButton.Tag.ToString())
            {
                toggleButton.IsChecked = true;
            }
        }

        /// <summary>
        /// Keep window on top
        /// </summary>
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }
    }
}
