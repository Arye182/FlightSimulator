using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // properties of view models and the model
        public FlightSimulatorModel Model { get; internal set; }
        public MyJoystickViewModel JoystickVM { get; internal set; }
        public DashBaordViewModel DashVM { get; internal set; }
        public StatusBarViewModel SBVM { get; internal set; }
        public SettingsViewModel SVM { get; internal set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Model = new FlightSimulatorModel(new MySimulatorConnector());
            JoystickVM = new MyJoystickViewModel();
            DashVM = new DashBaordViewModel();
            SBVM = new StatusBarViewModel();
            SVM = new SettingsViewModel();
            // Create main application window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }


    }
}
