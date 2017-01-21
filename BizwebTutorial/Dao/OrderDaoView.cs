using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.EF;
using BizwebTutorial.Models;

namespace BizwebTutorial.Dao
{
    public class OrderDaoView
    {
        private BiMartDbContext Dbcontext = new BiMartDbContext();

        public int Insert(OrderModelView entity)
        {
            var orderview = new Order()
            {
                Name = entity.Name,
                CustomerId = entity.CustomerId,
                CustomerAddress=entity.AddressCustomer,
                CustomerEmail=entity.EmailCustomer,
                CustomerName=entity.NameCustomer,
                CustomerPhone=entity.PhoneCustomer,
                TotalMoney=entity.TotalMoney,
                LineItems=entity.LineItemString,
                CreatedOn=DateTime.Now,
                Payment=true
            };
            Dbcontext.Orders.Add(orderview);
            Dbcontext.SaveChanges();
            return orderview.Id;
        }
        public int InsertCus(OrderModelView entity)
        {
            var customer = new Customer()
            {
                Name=entity.NameCustomer,
                Email=entity.EmailCustomer,
                Address=entity.AddressCustomer,
                Phone=entity.PhoneCustomer,
                CreatedOn=DateTime.Now
            };
            var checkemail = Dbcontext.Customers.Any(s => s.Email == entity.EmailCustomer);
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
    }
}