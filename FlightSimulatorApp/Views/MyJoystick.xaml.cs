using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp;
using System.ComponentModel;

namespace FlightSimulatorApp.Views
{
    public partial class MyJoystick : UserControl
    {
        public MyJoystick()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).JoystickVM;
        }
    }
}
