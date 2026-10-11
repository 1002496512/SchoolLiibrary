using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    public class IsraelId : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string id = value as string;

            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            if (id.Length != 9 || !id.All(char.IsDigit))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = int.Parse(id[i].ToString());
                int multiplier = (i % 2 == 0) ? 1 : 2;
                int product = digit * multiplier;
                sum += (product > 9) ? product - 9 : product;
            }

            return sum % 10 == 0;
        }   
    }
}
