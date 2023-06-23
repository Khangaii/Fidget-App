using FidgetApp.Classes;
using FidgetApp.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FidgetApp.Windows
{
    /// <summary>
    /// Back-end code for the PhysicsBallWindow
    /// </summary>
    public partial class PhysicsBallWindow : Window
    {
        // The mouse position from the previous frame
        public Vector previousMousePosition;

        // PhysicsBall object
        private PhysicsBall physicsBall;

        // Ball variables
        private double ballRadius = 75;
        private Color ballColor;

        public PhysicsBallWindow()
        {
            InitializeComponent();

            ballColor = Color.FromArgb(255, 114, 134, 211);
        }

        /// <summary>
        /// Graphic renderer function
        /// </summary>
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            physicsBall.Update();
            physicsBall.Draw();

            previousMousePosition.X = Mouse.GetPosition(this).X;
            previousMousePosition.Y = Mouse.GetPosition(this).Y;
        }

        /// <summary>
        /// Window Content Rendered event handler
        /// </summary>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            previousMousePosition.X = Mouse.GetPosition(this).X;
            previousMousePosition.Y = Mouse.GetPosition(this).Y;

            physicsBall = new PhysicsBall(parentCanvas: AppCanvas, radius: ballRadius, color: ballColor,
                position: new Vector(this.Width / 2, this.Height / 2),
                velocity: new Vector(0, -20));

            physicsBall.Draw();

            // Set graphic renderer
            CompositionTarget.Rendering += CompositionTarget_Rendering;
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
