using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary

{
    public class NewBookViewModel
    {
        public Book Book { get; set; }
        public List<Author> Authors { get; set; }
        public List<Ganre> Genres { get; set; }


    }
}
