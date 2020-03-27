using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorApp.Model;
namespace FlightSimulatorApp.ViewModel
{//b
    class DashBaordViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName) { }
        public DashBaordViewModel(IFlightSimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        
       
        public Double VM_Latitude
        {
            get { return model.Altitude; }
        }
    }
}
