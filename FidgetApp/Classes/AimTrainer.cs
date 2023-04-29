using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace FidgetApp.Classes
{
    internal class AimTrainer
    {
        // Canvas to draw on
        private Canvas _parentCanvas;

        private Target _target;

        public AimTrainer(Canvas parentCanvas)
        {
            _parentCanvas = parentCanvas;

            _target = new Target(parentCanvas: parentCanvas, radius: 75, 
                position: new Vector(_parentCanvas.ActualWidth / 2, _parentCanvas.ActualHeight / 2));
        }

        public void Draw()
        {
            _target.Draw();
        }
    }
}
