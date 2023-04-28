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
    public partial class PhysicsBallWindow : Window
    {
        public Vector previousMousePosition;

        private PhysicsBall physicsBall;
        private double ballRadius = 75;
        private Color ballColor;

        public PhysicsBallWindow()
        {
            InitializeComponent();

            ballColor = Color.FromArgb(255, 114, 134, 211);
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            physicsBall.Update();
            physicsBall.Draw();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            previousMousePosition.X = Mouse.GetPosition(this).X;
            previousMousePosition.Y = Mouse.GetPosition(this).Y;

            physicsBall = new PhysicsBall(parentCanvas: AppCanvas, radius: ballRadius, color: ballColor,
                position: new Vector(this.Width / 2, this.Height / 2),
                velocity: new Vector(0, -20));

            physicsBall.Draw();

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void SetUiElementPosition(UIElement uiElement, double elementPositionX = 0, double elementPositionY = 0)
        {
            Canvas.SetLeft(uiElement, elementPositionX);
            Canvas.SetTop(uiElement, elementPositionY);
        }

        private void SetUiElementPosition(UIElement uiElement, Point elementPosition)
        {
            Canvas.SetLeft(uiElement, elementPosition.X);
            Canvas.SetTop(uiElement, elementPosition.Y);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }
    }
}
