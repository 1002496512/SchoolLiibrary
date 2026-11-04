
using DTOLibrary;
using LibraryWebService;

namespace LibraryWebService
{
    public class OOWRepository
    {
        AuthorRepository authorRepository;
        BookRepository bookRepository;
        BorrowRepository borrowRepository;
        CityRepository cityRepository;
        CountryRepository countryRepository;
        GanreRepository ganreRepository;
        ReaderRepository readerRepository;
        DbContext dbContext;
        UOWModelCreators uowModelCreators;

        public OOWRepository()
        {
            this.dbContext = new DbContext(); ;
            this.uowModelCreators = new UOWModelCreators(); ;
        }

        public AuthorRepository AuthorRepository
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new AuthorRepository(dbContext, uowModelCreators);
                }
                return authorRepository;
            }
        }
        public BookRepository BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(dbContext, uowModelCreators);
                }
                return bookRepository;
            }
        }
        public BorrowRepository BorrowRepository
        {
            get
            {
                if (borrowRepository == null)
                {
                    borrowRepository = new BorrowRepository(dbContext, uowModelCreators);
                }
                return borrowRepository;
            }
        }
        public CityRepository CityRepository
        {
            get {

                if (cityRepository == null)
                {
                    cityRepository = new CityRepository(dbContext, uowModelCreators);
                }
           
            return cityRepository;
            }
        }
        public CountryRepository CountryRepository
        {
            get
            {
                if (countryRepository == null)
                {
                    countryRepository = new CountryRepository(dbContext, uowModelCreators);
                }
                return countryRepository;
            }
        }
        public GanreRepository GanreRepository
        {
            get
            {
                if (ganreRepository == null)
                {
                    ganreRepository = new GanreRepository(dbContext, uowModelCreators);
                }
                return ganreRepository;
            }
        }
        public ReaderRepository ReaderRepository
        {
            get
            {
                if (readerRepository == null)
                {
                    readerRepository = new ReaderRepository(dbContext, uowModelCreators);
                }
                return readerRepository;
            }
        }

        public void ConnectDb()
        {
            this.dbContext.OpenConnection();
        }   
        public void DisconnectDb()
        {
            this.dbContext.CloseConnection();
        }

        internal void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        internal void Commit()
        {
            throw new NotImplementedException();
        }

        public object GetLastInsertedId()
        {
            string sql= "SELECT @@IDENTITY";
            return this.dbContext.GetValue(sql);
        }

        internal void RollBack()
        {
            throw new NotImplementedException();
        }
    }
}
