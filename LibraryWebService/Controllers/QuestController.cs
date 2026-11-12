using LibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestController : ControllerBase
    {

        RepositoryFactory repositoryFactory;
        public QuestController()
        {
            this.repositoryFactory = new RepositoryFactory();

        }


        [HttpGet]
        public CatalogViewModel GetCatalog(string authorId=null, string ganreId=null, int page=0)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            //open connection
            try
            {
                this.repositoryFactory.ConnectDb();
               
                if(authorId == null && ganreId == null && page == 0)
                   catalogViewModel.Authors = this.repositoryFactory.AuthorRepository.GetAll();
                else if(authorId != null && ganreId == null && page == 0)
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyAuthor(authorId);
                else if (authorId == null && ganreId != null && page == 0)
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyGanre(ganreId);
                else if (authorId == null && ganreId == null && page != 0)
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksByPage(page);


                catalogViewModel.Authors = this.repositoryFactory.AuthorRepository.GetAll();    
                catalogViewModel.Ganres = this.repositoryFactory.GanreRepository.GetAll();
                return catalogViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Console.WriteLine(error);
                return null;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }


            //close connection
           
        }
    }
}
        