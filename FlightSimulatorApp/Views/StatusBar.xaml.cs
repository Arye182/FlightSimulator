﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// // clock feature taken from internet (stack overflow) https://stackoverflow.com/questions/14470768/a-clock-in-c-sharp-wpf-application
    /// </summary>
    public partial class StatusBar : UserControl, INotifyPropertyChanged
    {

        private bool connectionStatus = false;
        private string _currenttime;
        private TimeZoneInfo _selectedTimeZone;
        public bool Connected { get { return this.connectionStatus; } set { this.connectionStatus = value; } }
        public StatusBar()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                UpdateTime();
            };
        }
        public List<TimeZoneInfo> TimeZones
        {
            get { return TimeZoneInfo.GetSystemTimeZones().ToList(); }
        }
        public string CurrentTime
        {
            get { return _currenttime; }
            set { _currenttime = value; OnPropertyChanged("CurrentTime"); }
        }
        public TimeZoneInfo SelectedTimeZone
        {
            get { return _selectedTimeZone; }
            set
            {
                _selectedTimeZone = value;
                OnPropertyChanged("SelectedTimeZone");
                UpdateTime();
            }
        }
        private void UpdateTime()
        {
            CurrentTime = SelectedTimeZone == null
                   ? DateTime.Now.ToLongTimeString()
                   : DateTime.UtcNow.AddHours(SelectedTimeZone.BaseUtcOffset.TotalHours).ToLongTimeString();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
