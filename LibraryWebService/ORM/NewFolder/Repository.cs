using DTOLibrary;

namespace LibraryWebService
{`
    public class Repository
    {
       protected DbContext dbContext;
        protected UOWModelCreators modelCreators;
    
        public Repository(DbContext dbContext, 
                          UOWModelCreators modelCreators)
        {
            this.dbContext = dbContext;
            this.modelCreators = modelCreators;
        }

    }
}
