using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    class Library
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Tell { get; set; }
        public string Website { get; set; }
        public bool HasTables { get; set; }
        public List<Shelf> Shelfs { get; set; }

    }
}
