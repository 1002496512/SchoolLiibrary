using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOLibrary;

namespace LibraryWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        DbContext dbContext;
        RepositoryFactory repositoryFactory;
        UOWModelCreators uowModelCreators;

        public GuestController()
        {
            this.dbContext = new DbContext();
            this.uowModelCreators = new UOWModelCreators();
            this.repositoryFactory = new RepositoryFactory(this.dbContext, this.uowModelCreators);
        }

        [HttpGet]
        public CatalogViewModel GetCatalogViewModel(int page=0, string authorId=null, string ganreId=null)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            if(page==0 && authorId==null && ganreId==null)
            {
                catalogViewModel.Page = 1;
                catalogViewModel.BooksPerPage = 10;
                catalogViewModel.Ganres = repositoryFactory.GanreRepository.GetAll();
                catalogViewModel.Authors = repositoryFactory.AuthoRepository.GetAll();
                catalogViewModel.Books = repositoryFactory.BookRepository.GetBokByPage(catalogViewModel.Page,catalogViewModel.BooksPerPage);
                catalogViewModel.AuthorId = authorId;                                                                   
                catalogViewModel.GanreId = ganreId;
                catalogViewModel.PageCount = repositoryFactory.BookRepository.AllBooks / catalogViewModel.BooksPerPage;
                if (repositoryFactory.BookRepository.AllBooks % catalogViewModel.BooksPerPage > 0)
                    catalogViewModel.PageCount++;

            }



            return catalogViewModel;
        {

       
    }
}
