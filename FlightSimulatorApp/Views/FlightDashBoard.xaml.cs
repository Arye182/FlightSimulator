using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for FlightData.xaml
    /// </summary>
    public partial class FlightDashBoard : UserControl
    {
        public FlightDashBoard()
        {
            DataContext = (Application.Current as App).DashVM;
            InitializeComponent();
        }
    }
}
