using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FlightSimulatorApp.Views
{
    public class JoystickEventArgs

    {
        private double x;
        private double y;
        public JoystickEventArgs()
        {
            x = 0;
            y = 0;
        }
        public double X
        {
            get { return X; }
            set
            {
                x = value;
            }
        }
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }
    }
    public partial class Joystick : UserControl
    {
        // this methods are for the joystick
        // data members
        private Point knob_point = new Point();
        private Point knob_first_point = new Point();
        private double canvasWidth, canvasHeight;
        private readonly Storyboard centerKnob;

        // properties
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(Joystick), null);
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(Joystick), null);
        public double X
        {
            get { return Convert.ToDouble(GetValue(XProperty)); }
            set
            {
                SetValue(XProperty, value);
            }
        }
        public double Y
        {
            get { return Convert.ToDouble(GetValue(YProperty)); }
            set
            {
                SetValue(YProperty, value);
            }
        }

        // delegates
        public delegate void MovementEventHandler(Joystick sender, JoystickEventArgs args);
        public delegate void EmptyJoystickEventHandler(Joystick sender);
        public delegate void PropertyChangedDelegate(Joystick sender, PropertyChangedEventArgs args);

        // events
        public event MovementEventHandler Moved;
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
        private void CenterKnob_Completed(object sender, EventArgs e)
        {
            X = Y = 0;
            knobPosition.X = 0;
            knobPosition.Y = 0;
            centerKnob.Stop();
            Released?.Invoke(this);
        }
        private void Knob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!Knob.IsMouseCaptured) return;
            knob_point.X = e.GetPosition(this).X - knob_first_point.X;
            knob_point.Y = e.GetPosition(this).Y - knob_first_point.Y;
            double knob_dist = Math.Round(Math.Sqrt(knob_point.X * knob_point.X + knob_point.Y * knob_point.Y));
            if (knob_dist >= canvasHeight / 2 || knob_dist >= canvasWidth / 2)
            {
                return;
            }
            knobPosition.X = knob_point.X;
            knobPosition.Y = knob_point.Y;
            // convert the X,Y coordinates to range [-1,1]
            X = knobPosition.X / 123;
            Y = -knobPosition.Y / 123;
            if (Moved == null)
            {
                return;
            }
            Moved?.Invoke(this, new JoystickEventArgs { X = X, Y = Y });
        }
        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            knob_point = e.GetPosition(this.Base);
            knob_first_point = e.GetPosition(this.Base);
            canvasWidth = Base.ActualWidth - KnobBase.ActualWidth;
            canvasHeight = Base.ActualHeight - KnobBase.ActualHeight;
            Captured?.Invoke(this);
            Knob.CaptureMouse();
            centerKnob.Stop();
        }
        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Knob.ReleaseMouseCapture();
            centerKnob.Begin();
        }
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
