using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace FlightSimulatorApp.Views
{
    public partial class MyJoystick : UserControl
    {

        private double x;
        private double y;

        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public double Y
        {
            get {return this.y;}
            set { this.y = value; }
        }

        // this methods are for the joystick
        public MyJoystick()
        {
            InitializeComponent();
        }
        
        // sliders change the value!
        // throttle slider
        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            this.throttle_value.Content = e.NewValue.ToString("0.00");
        }

        // ailron slider
        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ailron_value.Content = e.NewValue.ToString("0.00");
        }


    }

}
