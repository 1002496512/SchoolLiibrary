
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using static System.Net.Mime.MediaTypeNames;

namespace DTOLibrary
{
    public class FirstLetterCapitalAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            string str = value as string;   
            foreach (char c in str)
                if (char.IsDigit(c))
                    return false;
            if (str[0] < 'A' || str[0] > 'Z')
                return false;
            for (int i = 1;i<str.Length;i++) 
                if (str[i] < 'a' || str[i] > 'z')
                    return false;
            return true;
        }
    }
}