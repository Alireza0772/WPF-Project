using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    public class ModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public string this[string propertyName] 
        {
            get {
                Validate(propertyName);
                return Error;
            }
        }

        public string Error { get; protected set; }

        protected virtual void Validate(string propertyName)
        {
            Error = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
