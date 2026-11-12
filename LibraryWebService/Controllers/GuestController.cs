using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOLibrary;
using System.Text.Json;

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
        public CatalogViewModel GetCatalogViewModel(int page = 1, string authorId = null, string ganreId = null)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            catalogViewModel.BooksPerPage = 10;
            catalogViewModel.Page = page;
            catalogViewModel.AuthorId = authorId;
            catalogViewModel.GanreId = ganreId;
            try
            {
                this.dbContext.OpenConnection();
                catalogViewModel.Ganres = repositoryFactory.GanreRepository.GetAll();
                catalogViewModel.Authors = repositoryFactory.AuthoRepository.GetAll();
                if (authorId == null && ganreId == null)
                {
                    catalogViewModel.Books = repositoryFactory.BookRepository.GetBokByPage(catalogViewModel.Page, catalogViewModel.BooksPerPage);
                }
                else if (authorId == null && ganreId != null)
                {
                    catalogViewModel.Books = repositoryFactory.BookRepository.GetBooksByGanre(catalogViewModel.Page, ganreId, catalogViewModel.BooksPerPage);
                }

                else if (authorId != null && ganreId == null)
                {
                    catalogViewModel.Books = repositoryFactory.BookRepository.GetBooksByAuthor(catalogViewModel.Page, authorId, catalogViewModel.BooksPerPage);
                }
                catalogViewModel.PageCount = repositoryFactory.BookRepository.AllBooks / catalogViewModel.BooksPerPage;

                if (repositoryFactory.BookRepository.AllBooks % catalogViewModel.BooksPerPage > 0)
                    catalogViewModel.PageCount++;

                return catalogViewModel;
            }
            catch (Exception exception)
            {
                string err = exception.Message;
                return null;
            }
            finally
            {
                this.dbContext.CloseConnection();
            }





            {


            }
        }

      
        [HttpGet]
        public CatalogBooks GetBookList(string authorId = null, string ganreId = null, int page = 1)
        {
            CatalogBooks catalogBooks = new CatalogBooks();
            catalogBooks.GanreId = ganreId;
            catalogBooks.AuthorId = authorId;
            catalogBooks.Page = page;
            catalogBooks.BooksPerPage = 10;
            try
            {
                int booksperPage = 10;
                this.dbContext.OpenConnection();
                if (authorId == null && ganreId == null && page >= 1)
                {
                    catalogBooks.Books = this.repositoryFactory.BookRepository.GetAll();
                }

                else if (authorId != null && ganreId == null && page == 1)
                {
                    catalogBooks.Books = this.repositoryFactory.BookRepository.GetBooksbyAuthor(authorId);


                }
                else if (authorId == null && ganreId != null && page >= 1)
                {
                    catalogBooks.Books = this.repositoryFactory.BookRepository.GetBooksbyGanre(ganreId);

                }

                else if (authorId != null && ganreId == null && page != 1)
                {
                    catalogBooks.Books = this.repositoryFactory.BookRepository.GetBooksbyAuthor(authorId);
                }
                else if (authorId == null && ganreId != null && page != 1)
                {
                    catalogBooks.Books = this.repositoryFactory.BookRepository.GetBooksbyGanre(ganreId);
                }
                int books = catalogBooks.Books.Count;
                //if (books > booksperPage)
                //    catalogBooks.Books = catalogBooks.Books.Skip(catalogBooks.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();
                catalogBooks.PageCount = books / catalogBooks.BooksPerPage;
                if (books % catalogBooks.BooksPerPage > 0)
                    catalogBooks.PageCount++;
                return catalogBooks;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.dbContext.CloseConnection();
            }

        }

        private int BookCount(int allBooks)
        {
            int pageCount = allBooks / 10;
            if (allBooks % 10 > 0)
            {
                pageCount += 1;
            }
            return pageCount;
        }
        [HttpGet]
        public BookViewModel ViewBook(string bookId)
        {
            BookViewModel bookViewModel = new BookViewModel();
            try
            {
                this.dbContext.OpenConnection();
                bookViewModel.Book = this.repositoryFactory.BookRepository.GetById(bookId);
                bookViewModel.Authors = this.repositoryFactory.BookRepository.GetAuthorsByBook(bookId);
                bookViewModel.Ganres = this.repositoryFactory.GanreRepository.GetGanresByBook(bookId);
                return bookViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.dbContext.CloseConnection();
            }
        }

        [HttpPost]
        public bool RegisterReader([FromForm] string data, IFormFile file)
        {
            try
            {
                Reader reader = JsonSerializer.Deserialize<Reader>(data);
                this.dbContext.OpenConnection();
                return this.repositoryFactory.ReaderRepository.Create(reader);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                this.dbContext.CloseConnection();
            }
        }

        [HttpGet]
        public RegistrationViewModel GetRegistrationViewModel()
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            try
            {
                this.dbContext.OpenConnection();
                registrationViewModel.Cities = this.repositoryFactory.CityRepository.GetAll();
                registrationViewModel.Reader = null;
                return registrationViewModel;
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }

            finally
            {
                this.dbContext.CloseConnection();
            }

        }

    }
}
