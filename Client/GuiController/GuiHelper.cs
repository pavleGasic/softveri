using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.GuiController
{
    internal static class GuiHelper
    {
        internal static bool IsPasswordValid(string password)
        {
            if(password.Length >= 5)
            {
                string pattern = @"^(?=.*[A-Z])(?=.*\d).+$";
                Regex regex = new Regex(pattern);
                return regex.IsMatch(password);
            }
            else { return false; }
        }

        internal static bool IsTextBoxValid(string text)
        {
            if (text.Length >= 3)
            {
                return true;
            }
            return false;
        }

        public static bool ValidateAddCustomer(string firstName, string lastName, string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
            {
                return false;
            }
            else
            {
                if (firstName.Length < 2 || lastName.Length < 2)
                {
                    return false;
                }
                return true;
            }
        }

        public static double GetDoubleValue(string value)
        {
            string doubleValue = value.Trim().Replace(',', '.').Replace(" RSD", "");
            bool success = double.TryParse(doubleValue, out double returnValue);
            return success ? returnValue : -1;
        }
    }
}
