﻿using System;
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
using FlightSimulatorApp.Model;
using FlightSimulatorApp;

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
            IFlightSimulatorModel model = (Application.Current as App).Model; ;
            try
            {
                model.connect("127.0.0.1", 5402);
                model.start();

            }
            catch(Exception ex)
            {
                Console.WriteLine("eror connecting");

            }
        }
    }
}
