
namespace LibraryWebService
{
    public class RepositoryFactory
    {
        AuthoRepository authoRepository;
        BookRepository bookRepository;
        BorrowRepository borrowRepository;
        CityRepository cityRepository;
        CountryRepository countryRepository;
        GanreRepository ganreRepository;
        ReaderRepository readerRepository;

        DbContext dbContext;
        UOWModelCreators uowModelCreators;

        public RepositoryFactory(DbContext dbContext, UOWModelCreators uowModelCreators )
        {
            this.dbContext = dbContext;
            this.uowModelCreators = uowModelCreators;
        }

  
        public AuthoRepository AuthoRepository
        {
            get
            {
                if (this.authoRepository == null)
                    return new AuthoRepository(this.dbContext, this.uowModelCreators);
                return this.authoRepository;
            }
        }
        public BookRepository BookRepository
        {
            get
            {
                if (this.bookRepository == null)
                    return new BookRepository(this.dbContext, this.uowModelCreators);
                return this.bookRepository;
            }
        }

        public CountryRepository CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                    return new CountryRepository(this.dbContext, this.uowModelCreators);
                return this.countryRepository;
            }
        }

        public CityRepository CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                    return new CityRepository(this.dbContext, this.uowModelCreators);
                return this.cityRepository;
            }
        }

        public BorrowRepository BorrowRepository
        {
            get
            {
                if (this.borrowRepository == null)
                    return new BorrowRepository(this.dbContext, this.uowModelCreators);
                return this.borrowRepository;
            }
        }
        public GanreRepository GanreRepository
        {
            get
            {
                if (this.ganreRepository == null)
                    return new GanreRepository(this.dbContext, this.uowModelCreators);
                return this.ganreRepository;
            }
        }

        public ReaderRepository ReaderRepository
        {
            get
            {
                if (this.readerRepository == null)
                    return new ReaderRepository(this.dbContext, this.uowModelCreators);
                return this.readerRepository;
            }
        }

        public void BeginTransaction()
        {
            this.dbContext.BeginTransaction();
        }
        public void Commit()
        {
            this.dbContext.Commit();
        }

        public void Rollback()
        {
            this.dbContext.RollBack();
        }

       
    }
}
