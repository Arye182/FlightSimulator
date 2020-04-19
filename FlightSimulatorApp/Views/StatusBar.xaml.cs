using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// // clock feature taken from internet (stack overflow) https://stackoverflow.com/questions/14470768/a-clock-in-c-sharp-wpf-application
    /// </summary>
    public partial class StatusBar : UserControl, INotifyPropertyChanged
    {
        private string _currenttime;
        private TimeZoneInfo _selectedTimeZone;

        public StatusBar()
        {
            DataContext = (Application.Current as App).SBVM;
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromSeconds(1),
                IsEnabled = true
            };
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class ValueConverter : IValueConverter
    {
        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri(value.ToString()));
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double doubleType = (double)value;
            return doubleType.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (double.TryParse(strValue, out double resultDouble))
            {
                return resultDouble;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
