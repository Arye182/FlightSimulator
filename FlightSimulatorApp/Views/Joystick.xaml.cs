using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace FlightSimulatorApp.Views
{
    public partial class Joystick : UserControl
    {
        // this methods are for the joystick

        private Point knob_point = new Point();
        private Point knob_first_point = new Point();

        private double elevatorValue ;
        private double rudderValue ;

        private double x;
        private double y;

        public double X_Joy
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public double Y_Joy
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public double ElevatorValue
        {
            get
            {
                return (this.elevatorValue);
            }
            set
            {
                this.elevatorValue = value;
            }
        }
        public double RudderValue
        {
            get
            {
                return (this.rudderValue);
            }
            set
            {
                this.rudderValue = value;
            }
        }


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
            if(e.ChangedButton == MouseButton.Left)
            {
                knob_point = e.GetPosition(this);
                knob_first_point = e.GetPosition(this);
                Knob.CaptureMouse();
            }
        }

        private void Knob_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                knob_point.X = e.GetPosition(this).X - knob_first_point.X;
                knob_point.Y = e.GetPosition(this).Y - knob_first_point.Y;
                if (Math.Sqrt(knob_point.X * knob_point.X + knob_point.Y * knob_point.Y) <= 67)
                {
                    knobPosition.X = knob_point.X;
                    knobPosition.Y = knob_point.Y;
                    X_Joy = knobPosition.X;
                    Y_Joy = knobPosition.Y;
                    // convert the X,Y coordinates to range [-1,1]
                    this.elevatorValue = (-(knobPosition.Y) / 65);
                    //this.elevatorValueString = this.elevatorValue.ToString("0.00");
                    this.rudderValue = (2 * (knobPosition.X + 65) / 130 - 1);
                    RudderValue = this.rudderValue;
                    ElevatorValue = this.elevatorValue;
                    
                   
                }
            }
        }

        private void Knob_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            this.elevatorValue = 0;
            this.rudderValue = 0;
            Knob.ReleaseMouseCapture();
        }



    }

}
