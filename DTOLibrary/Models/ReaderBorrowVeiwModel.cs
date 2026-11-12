using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels
{
    public class ReaderBorrow
    {
        public Borrow Borrow { get; set; }
        public Book Book { get; set; }
    }
}
