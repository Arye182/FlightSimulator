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
    public partial class Joystick : UserControl
    {
        protected bool pressed;
        private Point knob_point = new Point();
        private Point knob_first_point = new Point();
        



        // this methods are for the joystick

        public Joystick()
        {
            InitializeComponent();
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            //lalal
        }

        private void Knob_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                knob_point = e.GetPosition(this);
                knob_first_point = e.GetPosition(this);
                pressed = true;
            }
        }

        private void Knob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                knob_point.X = e.GetPosition(this).X - knob_first_point.X;
                knob_point.Y = e.GetPosition(this).Y- knob_first_point.Y;
                if (Math.Sqrt(knob_point.X * knob_point.X + knob_point.Y * knob_point.Y) < 60)
                {
                    knobPosition.X = knob_point.X;
                    knobPosition.Y = knob_point.Y;
                }
            }
        }

        private void Knob_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }










        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Slider_ValueChanged_1(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
    }

}
