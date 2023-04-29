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
    internal class Target
    {
        // Canvas to draw on
        Canvas _parentCanvas { get; set; }

        private Image _uiElement;

        private double _radius;

        private Vector _position;

        public Target(Canvas parentCanvas, double radius, Vector position)
        {
            _parentCanvas = parentCanvas;

            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.UriSource = new Uri("../Resources/Images/Target_2.png", UriKind.Relative);
            imageSource.EndInit();

            _uiElement = new Image
            {
                Width = radius * 2,
                Height = radius * 2,
                Source = imageSource,
                Opacity = 0.75
            };
            _parentCanvas.Children.Add(_uiElement);

            _radius = radius;

            _position = position;

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
