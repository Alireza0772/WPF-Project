using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFProject.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFProject.ViewModels
{
    public class ViewModelBase : ModelBase
    {
        private ServiceManager serviceManager;
        protected ServiceManager ServiceManager { get =>serviceManager;  }
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ViewModelBase()
        {
            serviceManager = ServiceManager.That;
        }
    }
}
