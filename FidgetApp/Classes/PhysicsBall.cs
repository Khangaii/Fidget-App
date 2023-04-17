using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;

namespace FidgetApp.Classes
{
    internal class PhysicsBall
    {
        // Canvas to draw on
        public Canvas _parentCanvas { get; set; }

        // Mouse position of previous frame
        private Vector _previousMousePosition;

        // Ball UIElement
        private readonly Ellipse _uiElement;

        private double _width { get { return _uiElement.Width; } }
        private double _height { get { return _uiElement.Height; } }
        private double _radius { get { return _uiElement.Width / 2; } }

        // Offset between the corner from which the ball is drawn and the center of the ball
        private Vector _positionOffset;
        // Offset between the center of the ball and the dragged point
        private Vector _dragOffsest;

        // The i and j unit vectors
        private readonly Vector _iVector = new Vector(1, 0);
        private readonly Vector _jVector = new Vector(0, 1);

        private Vector _position;
        private Vector _velocity;
        private Vector _acceleration;

        private double _mass;

        // Environmental constants
        private double _gravitationalConstant = 1;
        private double _windResistanceConstant = 0.00005;
        private double _bounceDampeningConstant = 0.97;
        //private double _frictionalForceConstant = 0.01;

        public PhysicsBall(Canvas parentCanvas, double radius, Color color, 
                           Vector position, Vector? velocity = null, Vector? acceleration = null)
        {
            _parentCanvas = parentCanvas;

            _uiElement = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = new SolidColorBrush(color),
                StrokeThickness = 0
            };
            _parentCanvas.Children.Add(_uiElement);

            _mass = radius * radius / 100;

            _positionOffset = new Vector(_radius, _radius);
            _position = position;
            _velocity = velocity ?? new Vector(0, 0);
            _acceleration = acceleration ?? new Vector(0, 0);

            _uiElement.PreviewMouseLeftButtonDown += _uiElement_PreviewMouseLeftButtonDown;
            _uiElement.PreviewMouseMove += _uiElement_PreviewMouseMove;
            _uiElement.PreviewMouseLeftButtonUp += _uiElement_PreviewMouseLeftButtonUp;
        }

        private void _uiElement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Grab the ball
            _uiElement.CaptureMouse();

            _velocity.X = 0;
            _velocity.Y = 0;

            _dragOffsest = (Vector)e.GetPosition(_parentCanvas) - _position;
        }

        private void _uiElement_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Drag the ball with a slight delay
            if (_uiElement.IsMouseCaptured)
            {
                Vector dragPosition = (Vector)e.GetPosition(_parentCanvas) - _dragOffsest;
                _velocity = Vector.Subtract( dragPosition, _position ) / 2;
            }
        }

        private void _uiElement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Release the ball
            _uiElement.ReleaseMouseCapture();
        }

        private void ApplyForce(Vector force)
        {
            _acceleration += force / _mass;
        }

        private Vector getGravityForce()
        {
            Vector gravity = new Vector(0, _gravitationalConstant);

            gravity *= _mass;

            return gravity;
        }

        private Vector getWindResistanceForce()
        {
            Vector windResistance = _velocity;

            if (_velocity.LengthSquared != 0) { windResistance.Normalize(); }
            windResistance *= -_windResistanceConstant * _radius * _velocity.LengthSquared;

            return windResistance;
        }

        public void Update()
        {
            if(!_uiElement.IsMouseCaptured)
                ApplyForce(getGravityForce());

            ApplyForce(getWindResistanceForce());

            _velocity += _acceleration;
            _position += _velocity;

            _acceleration.X = 0;
            _acceleration.Y = 0;

            DetectCollision();

            _previousMousePosition = (Vector)Mouse.GetPosition(_parentCanvas);
        }

        public void Draw()
        {
            Vector newPosition = Vector.Subtract(_position, new Vector(_radius, _radius));

            Canvas.SetLeft(_uiElement, newPosition.X);
            Canvas.SetTop(_uiElement, newPosition.Y);
        }

        private void DetectCollision()
        {
            bool isTouchingHorizontalWalls = false;
            bool isTouchingVerticalWalls = false;

            if (_position.X - _radius < 0)
            {
                _position.X = _radius;
                _velocity.X *= -_bounceDampeningConstant;

                isTouchingHorizontalWalls = true;
            } else if (_position.X + _radius > _parentCanvas.ActualWidth)
            {
                _position.X = _parentCanvas.ActualWidth - _radius;
                _velocity.X *= -_bounceDampeningConstant;

                isTouchingHorizontalWalls = true;
            }

            if (_position.Y - _radius < 0)
            {
                _position.Y = _radius;
                _velocity.Y *= -_bounceDampeningConstant;

                isTouchingVerticalWalls = true;
            } else if (_position.Y + _radius > _parentCanvas.ActualHeight)
            {
                _position.Y = _parentCanvas.ActualHeight - _radius;
                _velocity.Y *= -_bounceDampeningConstant;

                isTouchingVerticalWalls = true;
            }

            if (_uiElement.IsMouseCaptured)
            {
                if (isTouchingHorizontalWalls)
                {
                    _velocity.X = 0;
                }
                if (isTouchingVerticalWalls)
                {
                    _velocity.Y = 0;
                }
            }
        }
    }
}
