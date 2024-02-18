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

        public static string ValidateAddCustomer(string firstName, string lastName, string email)
        {
            string errorMessage = string.Empty;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
            {
                errorMessage += "Email must be in valid format(ex. pavle@gmail.com)\n";
            }

            if (firstName.Length < 2 || lastName.Length < 2)
            {
                errorMessage += "First name and last name must be longer than one characters";
            }
            return errorMessage;
        }

        public static double GetDoubleValue(string value)
        {
            string doubleValue = value.Trim().Replace(',', '.');
            bool success = double.TryParse(doubleValue, out double returnValue);
            return success ? returnValue : -1;
        }

        internal static string ValidateAddActor(string actorName)
        {
            if(actorName == null || actorName.Length < 2 || !actorName.Contains(' '))
            {
                return "Enter actor full name, with space between first and last name!";
            }
            return string.Empty;
        }

        internal static string ValidateFilm(Film film)
        {
            string errorMessage = string.Empty;
            if (film.Title == null || film.Title.Length == 0)
            {
                errorMessage += "Film title is empty!\n";
            }
            if(film.Genre == null)
            {
                errorMessage += "Film genre isn't selected!\n";
            }
            if(film.PricePerDay == -1)
            {
                errorMessage += "Film price isn't numeric value!\n";
            }
            if(film.Actors.Count == 0)
            {
                errorMessage += "Film don't have actors!\n";
            }
            return errorMessage;
        }
    }
}
