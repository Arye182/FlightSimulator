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
using FlightSimulatorApp.Views;
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
          
            FlightSimulatorModel m = (Application.Current as App).Model;
            m.connect("127.0.0.1", 5402);
            ISimulatorConnector s = m.connector;
            m.start();
        }
        public void item_Click(Object sender, RoutedEventArgs e)
        {
            
            if (e.ToString().Equals("800 × 600"))
            {
                MessageBox.Show("800 × 600!");
            }
            
        }
    }
}
