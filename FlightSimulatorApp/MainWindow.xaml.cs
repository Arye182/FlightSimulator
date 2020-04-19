using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = (Application.Current as App).SVM;
            InitializeComponent();
            this.Width = 0.75 * SystemParameters.PrimaryScreenWidth;
            this.Height = 0.75 * SystemParameters.PrimaryScreenHeight;
        }
    }
}
