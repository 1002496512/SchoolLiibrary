using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    public class Book
    {
        string bookId;
        string bookName;
        string bookDescription;
        string bookImage;
        string bookCopies;

        public Book()
        {
        }
        [Required(ErrorMessage = "Book ID cannot be empty")]
        [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "The book ID is digits only")]
        public string BookId
        {
            get { return this.bookId; }
            set
            {
                this.bookId = value;
            }
        }
        [Required(ErrorMessage = "Book Name cannot be empty")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = " A book name cannot be less than two characters or more than 150 characters.")]
        [FirstLetterCapital(ErrorMessage = "The first letter of the name must be a capital letter and the rest must be lowercase.")]    
        public string BookName
        {
                get { return this.bookName; }
                set { this.bookName = value;
                      //ValidateProperty(value);
            }  
        }

        [Required(ErrorMessage = "Book Description cannot be empty")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = " A book description cannot be less than ten characters or more than five hundred characters.")]
        public string BookDescription
        {
            get { return this.bookDescription; }
            set { this.bookDescription = value;
                //ValidateProperty(value);
            }   
        }
        [Required(ErrorMessage = "Book Image cannot be empty")]
        [OnlyImage(ErrorMessage = "The image must be in image format (jpg, png, gif).")]
        public string BookImage
        {
            get { return this.bookImage; }
            set { this.bookImage = value; } 
        }

        public string BookCopies
        {
            get { return this.bookCopies; }
            set { this.bookCopies = value; }
        }
    }
}
