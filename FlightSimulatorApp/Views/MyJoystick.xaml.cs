using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp.Views
{
    public partial class MyJoystick : UserControl
    {
        public MyJoystick()
        {
            DataContext = (Application.Current as App).JoystickVM;
            InitializeComponent();
        }
    }
}
