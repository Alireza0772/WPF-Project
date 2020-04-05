using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProject.Models
{
    class Book
    {
        public enum BookCategory
        {
            Educational,
            Scifi,
            Horror,
            Romance,
            Poem
        }
        public enum BookLanguage
        {
            English,
            Persian,
            Deutch,
            Spanish
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public BookCategory Category { get; set; }
        public string Publisher { get; set; }
        public BookLanguage Language { get; set; }
        public Shelf Shelf { get; set; }
    }
}
