
namespace LibraryWebService
{
    public class UOWModelCreators
    {
        AuthorCreator authorCreator;
        BookCreator bookCreator;
        BorrowCreator borrowCreator;
        CityCreator cityCreator;
        CountryCreator countryCreator;
        GanreCreator ganreCreator;
        ReaderCreator readerCreator;

       public AuthorCreator AuthorCreator
        {
            get
            {
                if (this.authorCreator == null)
                    this.authorCreator = new AuthorCreator();
                return this.authorCreator;
            }
        }

        public BookCreator AuthorCrBookCreatoreator
        {
            get
            {
                if (this.authorCreator == null)
                    this.bookCreator = new BookCreator();
                return this.bookCreator;
            }
        }
        public BorrowCreator BorrowCreator
        {
            get
            {
                if (this.authorCreator == null)
                    this.borrowCreator = new BorrowCreator();
                return this.borrowCreator;
            }
        }

        public CityCreator CityCreator
        {
            get
            {
                if (this.cityCreator == null)
                    this.cityCreator = new CityCreator();
                return this.cityCreator;
            }
        }

        public CountryCreator CountryCreator
        {
            get
            {
                if (this.countryCreator == null)
                    this.countryCreator = new CountryCreator();
                return this.countryCreator;
            }
        }

        public GanreCreator GanreCreator
        {
            get
            {
                if (this.ganreCreator == null)
                    this.ganreCreator = new GanreCreator();
                return this.ganreCreator;
            }
        }

        public ReaderCreator ReaderCreator
        {
            get
            {
                if (this.readerCreator == null)
                    this.readerCreator = new ReaderCreator();
                return this.readerCreator;
            }
        }

        public BookCreator BookCreator
        {
            get
            {
                if (this.bookCreator == null)
                    this.bookCreator = new BookCreator();
                return this.bookCreator;
            }
        }

    }
}
