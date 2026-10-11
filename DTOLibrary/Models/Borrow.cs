using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    public class Borrow
    {
        string borrowId;
        string readerId;
        string borrowDate;
        string borrowStatus;
        string bookId;


        public string BorrowId {  get { return borrowId; } set { borrowId = value; } }

        public string ReaderId { get { return readerId; } set { readerId = value; } }   

        public string BorrowDate { get { return borrowDate; } set { borrowDate = value; } }

        public string BorrowStatus { get { return borrowStatus; } set { borrowStatus = value; } }

        public string BookId { get { return bookId; } set { bookId = value; } }
    }
}
