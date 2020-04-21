using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPFProject.Models
{
    public class ModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        private string error;
        [XmlIgnore]
        public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();
        public string this[string propertyName]
        {
            get {
                Validate(propertyName);
                if (ErrorCollection.ContainsKey(propertyName))
                {
                    ErrorCollection[propertyName] = Error;
                }
                else if (Error != null)
                {
                    ErrorCollection.Add(propertyName, Error);
                }
                OnPropertyChanged(nameof(ErrorCollection));
                return Error;
            }
        }
        [XmlIgnore]
        public string Error
        {
            get => error;
            protected set {
                error = value;
                OnPropertyChanged();
            }
        }

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
