using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



//using FlightSimulator.Model.EventArgs;


namespace FlightSimulatorApp.Views
{
    public partial class Joystick : UserControl
    {
        // this methods are for the joystick

        // data members
        private Point knob_point = new Point();
        private Point knob_first_point = new Point();
        private double canvasWidth, canvasHeight;
        private readonly Storyboard centerKnob;

        // properties
        public static readonly DependencyProperty RudderProp = DependencyProperty.Register("Aileron", typeof(double), typeof(Joystick), null);
        public static readonly DependencyProperty ElevatorProp = DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);
        public double Rudder
        {
            get { return Convert.ToDouble(GetValue(RudderProp)); }
            set { SetValue(RudderProp, value); }
        }
        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(ElevatorProp)); }
            set
            {
                SetValue(ElevatorProp, value);
            }
        }

        // delegates
        //public delegate void OnScreenJoystickEventHandler(Joystick sender, VirtualJoystickEventArgs args);
        public delegate void EmptyJoystickEventHandler(Joystick sender);

        // events
        //public event OnScreenJoystickEventHandler Moved;
        public event EmptyJoystickEventHandler Released;
        public event EmptyJoystickEventHandler Captured;
        public event PropertyChangedEventHandler PropertyChanged;

        // this methods are for the joystick
        public Joystick()
        {
            InitializeComponent();
            Knob.MouseLeftButtonDown += Knob_MouseLeftButtonDown;
            Knob.MouseLeftButtonUp += Knob_MouseLeftButtonUp;
            Knob.MouseMove += Knob_MouseMove;
            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }
        private void centerKnob_Completed(object sender, EventArgs e)
        {
            centerKnob.Stop();
        }
        private void Knob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            knob_point.X = e.GetPosition(this).X - knob_first_point.X;
            knob_point.Y = e.GetPosition(this).Y - knob_first_point.Y;
            if (Math.Sqrt(knob_point.X * knob_point.X + knob_point.Y * knob_point.Y) <= 67)
            {
                knobPosition.X = knob_point.X;
                knobPosition.Y = knob_point.Y;
                // convert the X,Y coordinates to range [-1,1]
                Rudder = (2 * (knobPosition.X + 65) / 130 - 1);
                Elevator = (-(knobPosition.Y) / 65);

                Console.WriteLine(GetValue(ElevatorProp));
                Console.WriteLine(GetValue(RudderProp));
                Console.WriteLine(knobPosition.X);
                Console.WriteLine(knobPosition.Y);
            }
/*            if (Moved == null)
            {
                return;
            }*/
        }
        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            knob_point = e.GetPosition(this);
            knob_first_point = e.GetPosition(this);
            Captured?.Invoke(this);
            Knob.CaptureMouse();
        }
        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Elevator = 0;
            Rudder = 0;
            Knob.ReleaseMouseCapture();
        }
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
