using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CollectionDao
    {
        private BiMartDbContext _dbcontext = new BiMartDbContext();
        public int Add(int categoryId, int productId)
        {
            var collection = new Collection()
            {
                CategoryId = categoryId,
                ProductId = productId
            };
            _dbcontext.Collections.Add(collection);
            _dbcontext.SaveChanges();
            return collection.Id;
        }
        public bool DeleteBycategoryID(int Id)
        {
            var collection = _dbcontext.Collections.Where(c => c.CategoryId == Id).ToList();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    _dbcontext.Collections.Remove(item);
                    _dbcontext.SaveChanges();
                }
            }
            return true;
        }
        public bool DeleteByProductID(int Id)
        {
            var collection = _dbcontext.Collections.Where(c => c.ProductId == Id).ToList();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    _dbcontext.Collections.Remove(item);
                    _dbcontext.SaveChanges();
                }
            }
            return true;
        }
        public bool Delete(int categoryId, int productId)
        {
            var collection = _dbcontext.Collections.Where(c => c.CategoryId == categoryId && c.ProductId == productId).SingleOrDefault();
            if (collection != null)
            {
                _dbcontext.Collections.Remove(collection);
                _dbcontext.SaveChanges();
            }
            return true;
        }
        public IEnumerable<Collection> GetByCategoryId(int Id)
        {
            var collection = _dbcontext.Collections.Where(c => c.CategoryId == Id);
            return collection;
        }
        public IEnumerable<Collection> GetByProductId(int Id)
        {
            var collectionOfIdproduct = _dbcontext.Collections.Where(c => c.ProductId == Id);
            return collectionOfIdproduct;
        }
    }
}
