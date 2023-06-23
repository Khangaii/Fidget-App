using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FidgetApp.Commands
{
    /// <summary>
    /// Class containing user-defined custom commands
    /// </summary>
    public static class CustomCommands
    {
        /// <summary>
        /// Command to completely exit the application
        /// </summary>
        public static readonly RoutedCommand Exit = new RoutedCommand
            (
                "Exit",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );
    }
}
