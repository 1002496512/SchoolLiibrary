using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    public class Author
    {
        string authorId;
        string authorFirstName;
        string authorLastName;
        int authorYear;
        int countryId;
        string authorPicture;


        public string AuthorId
        {
            get { return this.authorId; }
            set { this.authorId = value; }
        }
        [Required(ErrorMessage = "Author First Name cannot be empty")]
        [StringLength(20,MinimumLength =2,ErrorMessage = " A firs name cannot be less than two characters or more than twenty characters.")]
        [FirstLetterCapital(ErrorMessage = "The first letter of the name must be a capital letter and the rest must be lowercase.")]
        public string AuthorFirstName
        {
            get { return this.authorFirstName; }
            set { this.authorFirstName = value;
                //ValidateProperty(value, "AuthorFirstName");
            }
        }

        [Required(ErrorMessage = "Author Last Name cannot be empty")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = " A last name cannot be less than two characters or more than twenty characters.")]
        [FirstLetterCapital(ErrorMessage = "The first letter of the name must be a capital letter and the rest must be lowercase.")]
        public string AuthorLastName
        {
            get { return this.authorLastName; }
            set { this.authorLastName = value;
                //ValidateProperty(value, "AuthorLastName");
            }
        }

        [Required(ErrorMessage = "Year cannot be empty")]
        [RegularExpression(@"^(1[0-9]{3}|20[0-2][0-9])$", ErrorMessage = "The year must be between 1000 and the current year.")]    
        public int AuthorYear
        {
            get { return this.authorYear; ; }
            set { this.authorYear = value;
                //ValidateProperty(value, "AuthorYear");
            }
        }
        public int CountryId
        {
            get { return this.countryId; ; }
            set { this.countryId = value; }
        }

        [Required(ErrorMessage = "Author Picture cannot be empty")]
        [OnlyImage(ErrorMessage = "The picture must be in image format (jpg, png, gif).")]
        public string AuthorPicture
        {
            get { return this.authorPicture; }
            set { this.authorPicture = value;
                //ValidateProperty(value, "AuthorPicture");
            }
        }

    }
}
