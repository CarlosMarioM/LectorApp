using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QRLectorApp.Model
{
    class QrColorApp : INotifyPropertyChanged
    {
        public string colorApp { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
