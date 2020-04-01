using FlightSimulatorApp.Model;
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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        FlightSimulatorModel m = (Application.Current as App).Model;
        public MenuBar()
        {
            InitializeComponent();
        }
        private void MenuDisconnect_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuConnect_Click(object sender, RoutedEventArgs e)
        {
            m.connect("127.0.0.1", 5402);
            m.start();
        }
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void FullScreen_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
