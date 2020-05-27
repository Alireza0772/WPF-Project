using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFProject.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFProject.ViewModels
{
    public class ViewModelBase :ModelBase
    {
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
    }
}
