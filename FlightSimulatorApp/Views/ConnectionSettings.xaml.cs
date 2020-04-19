using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System.Windows;

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
            m.Connect(svm.VM_IP, svm.VM_Port);
            m.Start();
            this.Close();
        }
    }
}
