using DTOLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        DbContext dbContext;
        RepositoryFactory repositoryFactory;
        UOWModelCreators uowModelCreators;
       

        public ReaderController()
        {
            this.dbContext = new DbContext();
            this.uowModelCreators = new UOWModelCreators();
            this.repositoryFactory = new RepositoryFactory(this.dbContext, this.uowModelCreators);
        }
        [HttpGet]
        public string Login(string nickName, string password)
        {
            try
            {
                this.dbContext.OpenConnection();
                return this.repositoryFactory.ReaderRepository.Login(nickName, password);
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

        [HttpGet]
        public Reader GetReaderById(string readerId)
        {
            try
            {
                this.dbContext.OpenConnection();
                return this.repositoryFactory.ReaderRepository.GetById(readerId);
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

        [HttpGet]
        public List<ReaderBorrow> GetReaderBorrows(string readerId)
        {
            List<ReaderBorrow> readerBorrows = new List<ReaderBorrow>();
            try
            {
                this.dbContext.OpenConnection();
                List<Borrow> borrows = this.repositoryFactory.BorrowRepository.GetReaderBorrows(readerId);
                foreach (var borrow in borrows)
                {
                    Book book = this.repositoryFactory.BookRepository.GetById(borrow.BookId);
                    ReaderBorrow readerBorrow = new ReaderBorrow()
                    {
                        Borrow = borrow,
                        Book = book
                    };
                    readerBorrows.Add(readerBorrow);
                }
                return readerBorrows;
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


        [HttpGet]
        public bool AddBorrow(string readerId, string bookId)
        {
            try
            {
                this.dbContext.OpenConnection();
                Borrow borrow = new Borrow()
                {
                    ReaderId = readerId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    BorrowStatus = "1"
                };
                return this.repositoryFactory.BorrowRepository.Create(borrow);
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
    }
}
