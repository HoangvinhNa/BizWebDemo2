using System;
using System.Collections.Generic;
using System.Linq;
using Models.EF;
using Models.ViewModel;
using PagedList;

namespace Models.Dao
{
    public class CustomerDao
    {
        private BiMartDbContext DbContext = new BiMartDbContext();

        public IEnumerable<Customer> GetallCustormer(string sortname, string searchstring, string curentfillter, int? page)
        {
            var customer = from s in DbContext.Customers
                           select s;
            switch (sortname)
            {
                case "NameSort":
                    customer = customer.OrderByDescending(s => s.Name);
                    break;
                default:
                    customer = customer.OrderByDescending(s => s.Id);
                    break;
            }
            if (!string.IsNullOrEmpty(searchstring))
            {
                customer = customer.Where(s => s.Name.Contains(searchstring));
            }
            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = curentfillter;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return customer.ToPagedList(pageNumber, pageSize);
        }
        public Customer FindIdCustormer(int id)
        {
            return DbContext.Customers.Find(id);
        }
        public int Insert(CustomerModel entity)
        {
            var custormer = new Customer()
            {
                FirstName = entity.FirstName,
                Name = entity.Name,
                Email = entity.Email,
                Marketing = entity.Marketing,
                Company = entity.Company,
                Phone = entity.Phone,
                Address = entity.Address,
                Country = entity.Country,
                PostalCode = entity.PostalCode,
                City = entity.City,
                Township = entity.Township,
                Note = entity.Note,
                Tag = entity.Tag,
                CreatedOn = DateTime.Now
            };
            var checkmail = DbContext.Customers.Any(s=>s.Email==entity.Email);
            if (checkmail)
            {
                return 0;
            }
            else
            {
                DbContext.Customers.Add(custormer);
                DbContext.SaveChanges();
                return custormer.Id;
            }
            
        }
        public bool Update(CustomerEditGetOrderModel entity)
        {
            var customer = DbContext.Customers.Find(entity.Id);
            customer.Tag = entity.Tag;
            customer.Note = entity.Note;
            DbContext.SaveChanges();
            return true;
        }
        public bool Delete(int Id)
        {
            try
            {
               
                var customer = DbContext.Customers.Find(Id);
                DbContext.Customers.Remove(customer);
                DbContext.SaveChanges();
                var DbOrder = DbContext.Orders.Where(s=>s.CustomerId == Id).ToList();
                foreach (var item in DbOrder)
                {
                    item.CustomerId = 0;
                    DbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateCustormerInformation(Customer entity)
        {
            try
            {
                var customer = DbContext.Customers.Find(entity.Id);
                customer.FirstName = entity.FirstName;
                customer.Name = entity.Name;
                customer.Email = entity.Email;
                customer.Marketing = entity.Marketing;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateAddressCustomer(Customer entity)
        {
            try
            {
                var customer = DbContext.Customers.Find(entity.Id);
                customer.FirstName = entity.FirstName;
                customer.Name = entity.Name;
                customer.Township = entity.Township;
                customer.Address = entity.Address;
                customer.City = entity.City;
                customer.PostalCode = entity.PostalCode;
                customer.Phone = entity.Phone;
                customer.Company = entity.Company;
                customer.Country = entity.Country;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Order> GetOrderByIdCustormer(int Id)
        {
            var model = DbContext.Orders.Where(s => s.CustomerId == Id);
            return model.ToList();
        }
    }
}
