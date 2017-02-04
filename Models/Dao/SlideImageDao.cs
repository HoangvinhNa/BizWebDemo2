using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ViewModel;

namespace Models.Dao
{
    public class SlideImageDao
    {
        private BiMartDbContext Dbcontext = new BiMartDbContext();
       
        
        public bool DeleteImage(int Id)
        {
            var imagepath = Dbcontext.SlideImages.Find(Id);
            Dbcontext.SlideImages.Remove(imagepath);
            Dbcontext.SaveChanges();
            return true;
        }
    }
}
