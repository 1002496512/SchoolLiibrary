using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels
{
    public class UpdateBookViewModel
    {
        public Book Book { get; set; }
        public List<string> AuthorsToDelete { get; set; }
        public List<string> GanresToDelete { get; set; }
        public List<string> AuthorsToAdd { get; set; }
        public List<string> GanresToAdd { get; set; }
    }
}
