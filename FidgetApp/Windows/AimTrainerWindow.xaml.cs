using FidgetApp.Classes;
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
    /// Interaction logic for AimTrainerWindow.xaml
    /// </summary>
    public partial class AimTrainerWindow : Window
    {
        public Vector previousMousePosition;

        AimTrainer aimTrainer;

        public AimTrainerWindow()
        {
            InitializeComponent();
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            aimTrainer.Draw();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            previousMousePosition.X = Mouse.GetPosition(this).X;
            previousMousePosition.Y = Mouse.GetPosition(this).Y;

            aimTrainer = new AimTrainer(parentCanvas: AppCanvas);

            aimTrainer.Draw();

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }
    }
}
