using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using FlightSimulatorApp.Views;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // properties of view models and the model
        public IFlightSimulatorModel Model { get; internal set; }
        public MyJoystickVM JoystickVM { get; internal set; }
        public DashBaordViewModel DashVM { get ; internal set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            Model = new FlightSimulatorModel(new MySimulatorConnector());
            JoystickVM = new MyJoystickVM();
            DashVM = new DashBaordViewModel();

            // Create main application window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

        }


    }
}
