using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    public class Country
    {
        string countryId;
        string countryName;
        
        public string CountryName
        {
            get { return countryName; }
            set { countryName = value; }
        }
        public int CountryId { get; set; }
    }
}
