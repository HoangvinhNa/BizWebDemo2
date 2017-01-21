using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizwebTutorial.Dao
{
    public class CollectionViewDao
    {
        private BiMartDbContext Dbcontext=new BiMartDbContext();
        public IEnumerable<Collection> GetCollectHasIdcategory( int CategoryId)
        {
            return Dbcontext.Collections.Where(x=>x.CategoryId==CategoryId).ToList();
        }
    }
}