using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server
{
    internal class Session
    {
        private static Session instance;
        public static Session Instance
        {
            get
            {
                if (instance == null) 
                    instance = new Session();
                return instance;
            }
        }
        private Session() { }

        public bool ValidateRegister(Worker worker)
        {
            if(worker != null)
            {
                string pattern = @"^(?=.*[A-Z])(?=.*\d).+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(worker.Password))
                {
                    return false;
                }
                else
                {
                    if (worker.FirstName.Length < 3 || worker.LastName.Length < 3 || worker.Username.Length < 3 || worker.Password.Length < 3)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool ValidateLogin(Worker worker)
        {
            if(worker != null)
            {
                if (worker.Username.Length < 3 || worker.Password.Length < 3)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool ValidateCustomer(Customer customer)
        {
           if(customer != null) {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(customer.Email))
                {
                    return false;
                }
                else
                {
                    if (customer.FirstName.Length < 2 || customer.LastName.Length < 2)
                    {
                        return false;
                    }
                    return true;
                }
           }
           return false;
        }

        internal bool ValidateFilm(Film film)
        {
            if(film != null) 
            { 
                if(film.Title.Length > 0 && film.Genre != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
