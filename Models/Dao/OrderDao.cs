using System.Collections.Generic;
using System.Linq;
using Models.EF;
using PagedList;
using Models.ViewModel;
using System;
using Models.ProductModelFix;

public class OrderDao
{
    private BiMartDbContext Dbcontext = new BiMartDbContext();
    public IEnumerable<ProductListTbModel> GetProductForOrder(string sortname, string searchstring, string curentfillter, int? page)
    {
        var product = from s in Dbcontext.Products
                      select s;
        var listmodel = new List<ProductListTbModel>();
        foreach (var item in product)
        {
            var imageFirst = Dbcontext.ImagePaths.Where(s => s.ProductId == item.Id).FirstOrDefault();
            var productmodel = new ProductListTbModel()
            {
                Id = item.Id,
                Price=item.Price,
                Name = item.Name,
                Image = imageFirst != null ? imageFirst.PathImage : ("~/Upload/No_image.png")
            };
            listmodel.Add(productmodel);
        }
        var lis2model = listmodel.AsEnumerable();
        switch (sortname)
        {
            case "NameSort":
                lis2model = lis2model.OrderByDescending(s => s.Name);
                break;
            default:
                lis2model = lis2model.OrderBy(s => s.Name);
                break;
        }
        if (!string.IsNullOrEmpty(searchstring))
        {
            lis2model = lis2model.Where(s => s.Name.Contains(searchstring));
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
        return lis2model.ToPagedList(pageNumber, pageSize);
    }
    public IEnumerable<Customer> GetCustormerForOrder(string sortname, string searchstring, string curentfillter, int? page)
    {
        var custormer = from s in Dbcontext.Customers select s;
        switch (sortname)
        {
            case "NameSort":
                custormer = custormer.OrderByDescending(s => s.Name);
                break;
            default:
                custormer = custormer.OrderBy(s => s.Name);
                break;
        }
        if (!string.IsNullOrEmpty(searchstring))
        {
            custormer = custormer.Where(s => s.Name.Contains(searchstring));
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
        return custormer.ToPagedList(pageNumber, pageSize);
    }
    public int InsertOrder(OrderModel entity)
    {
        var order = new Order()
        {
            LineItems = entity.LineItems,
            CustomerId = entity.CustomerId,
            CustomerAddress = entity.CustomerAdress,
            CustomerName = entity.CustomerName,
            CustomerPhone = entity.CustomerPhone,
            CustomerEmail = entity.CustomerEmail,
            Payment = entity.Payment,
            Note=entity.Note,
            CreatedOn = DateTime.Now
        };
        Dbcontext.Orders.Add(order);
        Dbcontext.SaveChanges();
        return order.Id;
    }
    public bool Update(OrderEditModel entity)
    {
        var order = Dbcontext.Orders.Find(entity.Id);
        order.Note = entity.Note;
        order.TotalMoney = entity.TotalMoney;
        order.Payment = entity.Payment;
        order.ModifiedOn = DateTime.Now;
        order.Transport = entity.Transport;
        Dbcontext.SaveChanges();
        return true;
    }
    public IEnumerable<Order> GetOrderToIndex(string sortname, string searchstring, string curentfillter, int? page)
    {
        var order = from s in Dbcontext.Orders
                    select s;
        switch (sortname)
        {
            case "NameSort":
                order = order.OrderByDescending(s => s.CustomerName);
                break;
            default:
                order = order.OrderByDescending(s => s.Id);
                break;
        }
        if (!string.IsNullOrEmpty(searchstring))
        {
            order = order.Where(s => s.CustomerName.Contains(searchstring));
        }
        if (searchstring != null)
        {
            page = 1;
        }
        else
        {
            searchstring = curentfillter;
        }
        int pageSize = 20;
        int pageNumber = (page ?? 1);
        return order.ToPagedList(pageNumber, pageSize);
    }
}
