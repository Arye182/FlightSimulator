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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulatorApp.Model;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ISimulatorConnector s = new MySimulatorConnector();
            try
            {
                s.connect("127.0.0.1", 5402);
                s.write("get /controls/flight/rudder\n");
                s.read();
            }
            catch(Exception ex)
            {
                Console.WriteLine("eror connecting");
            }

        }

        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Joystick_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Joystick_Loaded_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
