using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FidgetApp.Classes
{
    /// <summary>
    /// Interaction logic for the Target class
    /// </summary>
    internal class Target
    {
        // Canvas to draw on
        Canvas _parentCanvas { get; set; }

        // Target UIElement
        private Image _uiElement;

        private double _radius;

        private Vector _position;

        /// <summary>
        /// Target constructor
        /// </summary>
        /// <param name="parentCanvas">Canvas to draw on</param>
        /// <param name="radius">Radius of the target</param>
        /// <param name="position">Initial position of the target</param>
        public Target(Canvas parentCanvas, double radius, Vector position)
        {
            _parentCanvas = parentCanvas;

            // Import image
            BitmapImage targetImage = new BitmapImage();
            targetImage.BeginInit();
            targetImage.UriSource = new Uri("../Resources/Images/target.png", UriKind.Relative);
            targetImage.EndInit();

            _uiElement = new Image
            {
                Width = radius * 2,
                Height = radius * 2,
                Source = targetImage,
                Opacity = 1
            };
            _parentCanvas.Children.Add(_uiElement);

            _radius = radius;

            _position = position;

            // Event listeners
            _uiElement.PreviewMouseLeftButtonDown += _uiElement_PreviewMouseLeftButtonDown;
        }

        private void _uiElement_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Refresh();
        }

        public void Draw()
        {
            Vector newPosition = Vector.Subtract(_position, new Vector(_radius, _radius));

            Canvas.SetLeft(_uiElement, newPosition.X);
            Canvas.SetTop(_uiElement, newPosition.Y);
        }

        // Move target to a random position on the canvas
        public void Refresh()
        {
            Random random = new Random();

            int randomX = random.Next((int)this._radius, 
                (int)(_parentCanvas.ActualWidth - this._radius));
            int randomY = random.Next((int)this._radius, 
                (int)(_parentCanvas.ActualHeight - this._radius));

            this._position = new Vector(randomX, randomY);
        }
    }
}
