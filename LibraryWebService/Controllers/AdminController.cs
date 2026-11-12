using LibraryModels;
using LibraryWebService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        DbContext dbContext;
        RepositoryFactory repositoryFactory;
        UOWModelCreators uowModelCreators;

        public AdminController()
        {
            this.dbContext = new DbContext();
            this.uowModelCreators = new UOWModelCreators();
            this.repositoryFactory = new RepositoryFactory(this.dbContext, this.uowModelCreators);
        }
        [HttpPost]
        public bool AddNewBook()
        {
            string jsonString = Request.Form["data"];
            NewBookViewModel newBookViewModel = JsonSerializer.Deserialize<NewBookViewModel>(jsonString);
            IFormFile file = Request.Form.Files[0];
            try
            {
                this.repositoryFactory.ConnectDb();
                this.repositoryFactory.BeginTransaction();
                bool ok = this.repositoryFactory.BookRepository.Create(newBookViewModel.Book);
                string bookId =this.repositoryFactory.GetLastInsertedId().ToString();
                foreach (Author author in newBookViewModel.Authors)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AddBookAuthor(bookId, author.AuthorId);
                }
                foreach (Ganre ganre in newBookViewModel.Genres)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AdBookGanre(bookId, ganre.GanreId);
                }
                // Image saving logic here
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(),
                                                                "wwwroot", "Images","Books",
                                                                bookId + newBookViewModel.Book.BookImage), FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                this.repositoryFactory.Commit();
                return true;
            }
            catch (Exception ex)
            {
                this.repositoryFactory.RollBack();
                return false;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpPost]
        public bool UpdateBook(UpdateBookViewModel  updateBookViewModel)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                this.repositoryFactory.BeginTransaction();
                bool ok = this.repositoryFactory.BookRepository.Update(updateBookViewModel.Book);
                foreach(string authorid in updateBookViewModel.AuthorsToDelete)
                {
                    ok = ok && this.repositoryFactory.BookRepository.DeleteBookAuthor(updateBookViewModel.Book.BookId, authorid);
                }
                foreach (string ganreId in updateBookViewModel.GanresToDelete)
                {
                    ok = ok && this.repositoryFactory.BookRepository.DeleteBookGanre(updateBookViewModel.Book.BookId, ganreId);
                }   
                foreach (string authorid in updateBookViewModel.AuthorsToAdd)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AddBookAuthor(updateBookViewModel.Book.BookId, authorid);
                }
                foreach (string ganreId in updateBookViewModel.GanresToAdd)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AdBookGanre(updateBookViewModel.Book.BookId, ganreId);
                }
                this.repositoryFactory.Commit();
                return true;
            }
            catch (Exception ex)
            {
                this.repositoryFactory.RollBack();
                return false;
            }
            finally
            {
                 this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public bool DeleteBook(string bookId)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                this.repositoryFactory.BeginTransaction();
                bool ok = this.repositoryFactory.BookRepository.GeleteBookAuthors(bookId);
                ok = ok && this.repositoryFactory.BookRepository.DeleteBookGanres(bookId);
                ok = ok && this.repositoryFactory.BookRepository.Delete(bookId);
                this.repositoryFactory.Commit();
                return ok;
            }
            catch (Exception ex)
            {
                this.repositoryFactory.RollBack();
                return false;
            }
            finally
            {
                 this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                return this.repositoryFactory.BookRepository.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }

        }

        [HttpGet]
        public NewBookViewModel GetnewBookViewModel()
        {
            NewBookViewModel newBookViewModel = new NewBookViewModel();
            newBookViewModel.Book = null;
            try
            {
                this.repositoryFactory.ConnectDb();
                newBookViewModel.Genres = this.repositoryFactory.GanreRepository.GetAll();
                newBookViewModel.Authors = this.repositoryFactory.AuthorRepository.GetAll();
                return newBookViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

    }
}
