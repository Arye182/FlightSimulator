using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        readonly FlightSimulatorModel m = (Application.Current as App).Model;
        readonly SettingsViewModel svm = (Application.Current as App).SVM;
        readonly StatusBarViewModel sbvm = (Application.Current as App).SBVM;

        public MenuBar()
        {
            InitializeComponent();
        }

        private void MenuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            m.Disconnect();
        }

        private void MenuConnect_Click(object sender, RoutedEventArgs e)
        {
            m.Connect(svm.VM_IP, svm.VM_Port);
            m.Start();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Res1_Click(object sender, RoutedEventArgs e)
        {
            svm.VM_MainWindowWidth = 800;
            svm.VM_MainWindowHeigth = 600;

        }

        private void Res2_Click(object sender, RoutedEventArgs e)
        {
            svm.VM_MainWindowWidth = 1024;
            svm.VM_MainWindowHeigth = 768;
        }

        private void Res3_Click(object sender, RoutedEventArgs e)
        {
            svm.VM_MainWindowWidth = 1280;
            svm.VM_MainWindowHeigth = 720;
        }

        private void Res4_Click(object sender, RoutedEventArgs e)
        {
            svm.VM_MainWindowWidth = 1920;
            svm.VM_MainWindowHeigth = 1080;
        }

        private void Aerial_Click(object sender, RoutedEventArgs e)
        {
            sbvm.VM_MapModeString = "Aerial";
        }
        private void AerialLabels_Click(object sender, RoutedEventArgs e)
        {
            sbvm.VM_MapModeString = "AerialWithLabels";
        }
        private void Roads_Click(object sender, RoutedEventArgs e)
        {
            sbvm.VM_MapModeString = "Roads";
        }

        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Arye182/FlightSimulator");
        }

        private void Menu_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All rights reserved Arye & Miri 2020 - check GitHub link for more details");
        }

        private void PortIP_Click(object sender, RoutedEventArgs e)
        {
            ConnectionSettings cs = new ConnectionSettings();
            cs.Show();
        }
    }
}
