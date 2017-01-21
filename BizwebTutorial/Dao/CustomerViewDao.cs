using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.EF;
using BizwebTutorial.Models;
namespace BizwebTutorial.Dao
{
    public class CustomerViewDao
    {
        private BiMartDbContext Dbcontext = new BiMartDbContext();
        public int CustomerRegis(CustomerLoginModel entity)
        {
            var customer = new Customer()
            {
                Email=entity.Email,
                Password=entity.Password,
                Address=entity.Address,
                Name=entity.Name,
                CreatedOn=DateTime.Now
            };
            var checkemail = Dbcontext.Customers.Any(s=>s.Email==entity.Email);
            if (checkemail)
            {
                return 0;
            }
            else
            {
                Dbcontext.Customers.Add(customer);
                Dbcontext.SaveChanges();
                return customer.Id;
            }
        }
        public Customer getbyId(string email)
        {
            return Dbcontext.Customers.SingleOrDefault(s => s.Email == email);
        }
        public bool Login(string email, string password)
        {
            var result = Dbcontext.Customers.Count(x => x.Email == email && x.Password == password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}