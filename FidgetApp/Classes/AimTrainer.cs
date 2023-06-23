using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace FidgetApp.Classes
{
    /// <summary>
    /// Interaction logic for the AimTrainer Class
    /// </summary>
    internal class AimTrainer
    {
        // Canvas to draw on
        private Canvas _parentCanvas;

        // Target object
        private Target _target;

        /// <summary>
        /// AimTrainer class constructor
        /// </summary>
        /// <param name="parentCanvas">Canvas to draw on</param>
        public AimTrainer(Canvas parentCanvas)
        {
            _parentCanvas = parentCanvas;

            // Target initialization
            _target = new Target(parentCanvas: parentCanvas, radius: 75, 
                position: new Vector(_parentCanvas.ActualWidth / 2, _parentCanvas.ActualHeight / 2));
        }

        public void Draw()
        {
            _target.Draw();
        }
    }
}
