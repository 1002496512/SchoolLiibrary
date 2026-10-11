using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    public class Reader
    {
        string readerId;
        string readerFirstName;
        string readerLastName;
        string readerAdress;
        string readerTelephone;
        string readerImage;
        string cityId;
        string readerNickName;
        string readerPassword;

        public string Salt { get; set; } = "";

        [Required(ErrorMessage = "You must enter Id")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "reader Id cannot must be 9 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Id must be numeric")]
        [IsraelId(ErrorMessage = "The Id is not valid")]
        public string ReaderId
        {
            get { return readerId; }
            set { readerId = value; }
        }
        [Required(ErrorMessage = "You must enter Reader First Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "First Name cannot be longer than 15 characters and less than 2")]
        [FirstLetterCapital(ErrorMessage = "First letter must be capital")] 
        public string ReaderFirstName
        {
            get { return readerFirstName; }
            set { readerFirstName = value; }
        }
        [Required(ErrorMessage = "You must enter Reader Last Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Last Name cannot be longer than 15 characters and less than 2")]
        [FirstLetterCapital(ErrorMessage = "First letter must be capital")]
        public string ReaderLastName
        {
            get { return readerLastName; }
            set { readerLastName = value; }
        }
        [Required(ErrorMessage = "You must enter Reader Adress")]
        public string ReaderAdress
        {
            get { return readerAdress; }
            set { readerAdress = value; }
        }
        [Required(ErrorMessage = "You must enter Reader Telephone")]
        [Phone(ErrorMessage = "Invalid phone number")]  
        public string ReaderTelephone
        {
            get { return readerTelephone; }
            set { readerTelephone = value; }
        }

        [Required(ErrorMessage = "You must enter Reader Image")]
        [OnlyImage(ErrorMessage = "Image must be in jpg, jpeg, png or bmp format")]
        public string ReaderImage
        {
            get { return readerImage; }
            set { readerImage = value; }
        }

        public string CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }

        [Required(ErrorMessage = "You must enter Reader Nick Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Nick Name cannot be longer than 15 characters and less than 2")]
        public string ReaderNickName
        {
            get { return readerNickName; }
            set { readerNickName = value; }
        }
        [Required(ErrorMessage = "You must enter Reader Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password cannot be longer than 20 characters and less than 6")]
        public string ReaderPassword
        {
            get { return readerPassword; }
            set { readerPassword = value; }
        }



    }
}
