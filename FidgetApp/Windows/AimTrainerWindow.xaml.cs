using FidgetApp.Classes;
using FidgetApp.Commands;
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
    /// <summary>
    /// Back-end logic for the AimTrainerWindow
    /// </summary>
    public partial class AimTrainerWindow : Window
    {
        // Mouse position from the previous frame
        public Vector previousMousePosition;

        // Aim trainer object
        AimTrainer aimTrainer;

        public AimTrainerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Graphic renderer function
        /// </summary>
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            aimTrainer.Draw();
        }

        /// <summary>
        /// Window Content Rendered event handler
        /// </summary>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            previousMousePosition.X = Mouse.GetPosition(this).X;
            previousMousePosition.Y = Mouse.GetPosition(this).Y;

            aimTrainer = new AimTrainer(parentCanvas: AppCanvas);

            aimTrainer.Draw();

            // Set graphic renderer
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        /// <summary>
        /// Window deactivated event handler to keep window on top
        /// </summary>
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }
    }
}
