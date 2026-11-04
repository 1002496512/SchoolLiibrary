namespace LibraryWebService
{
    public class Repository
    {
       protected DbContext dbContext;
       protected UOWModelCreators UOWModelCreators;

        public Repository(DbContext dbContext,
                          UOWModelCreators UOWModelCreators)
        {
            this.dbContext =  dbContext;
            this.UOWModelCreators = UOWModelCreators;
        }

        

    }
}
