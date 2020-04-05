using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    class Shelf
    {
        public int BookCount { get; set; }
        public string Position { get; set; }
        public int Level { get; set; }
        public int Floor { get; set; }
        public Library Library { get; set; }
        public List<Book> Books { get; set; }
    }
}
