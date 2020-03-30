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
using System.ComponentModel;

namespace FlightSimulatorApp.Views
{
    public partial class MyJoystick : UserControl
    {
        MyJoystickVM vm;
        public MyJoystick()
        {
            InitializeComponent();
            vm = new MyJoystickVM();
            DataContext = vm;
        }
    }
}
