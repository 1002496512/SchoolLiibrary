
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTOLibrary
{
    public class CatalogViewModel: CatalogBooks
    {
        public List<Ganre> Ganres { get; set; }
        public List<Author> Authors { get; set; }
     
    }

    public class CatalogBooks
    {
        public List<Book> Books { get; set; }

        public string AuthorId { get; set; }
        public string GanreId { get; set; }
        public int Page { get; set; }
        public int PagePerPage { get; set; }

        public int PageCount { get; set; }



    }
}