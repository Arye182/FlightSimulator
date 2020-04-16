using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
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
    /// Interaction logic for ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : Window
    {
        readonly FlightSimulatorModel m = (Application.Current as App).Model;
        readonly SettingsViewModel svm = (Application.Current as App).SVM;
        public ConnectionSettings()
        {
            DataContext = (Application.Current as App).SVM;
            InitializeComponent();
        }


        private void SaveClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveConnect_Click(object sender, RoutedEventArgs e)
        {
            m.connect(svm.VM_IP, svm.VM_Port);
            m.start();
            this.Close();
        }


    }
}
