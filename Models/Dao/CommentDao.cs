using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Models.ViewModel;
using PagedList;

namespace Models.Dao
{
    public class CommentDao
    {
        private BiMartDbContext dbcontext = new BiMartDbContext();
        public int Insert(ContactUser entity)
        {
            try
            {
                var Comments = new CommentUser()
                {
                    EmailUser = entity.EmailUser,
                    Comment = entity.Comment,
                    Send_On=DateTime.Now
                };
                dbcontext.CommentUsers.Add(Comments);
                dbcontext.SaveChanges();
                return Comments.Id;
            }
            catch
            {
                return 0;
            }
           
        }
        public IEnumerable<CommentUser> GetallComment(string sortname, string searchstring, string curentfillter, int? page)
        {
            var CommentDB = from s in dbcontext.CommentUsers
                           select s;
            switch (sortname)
            {
                case "NameSort":
                    CommentDB = CommentDB.OrderByDescending(s => s.EmailUser);
                    break;
                default:
                    CommentDB = CommentDB.OrderByDescending(s => s.Id);
                    break;
            }
            if (!string.IsNullOrEmpty(searchstring))
            {
                CommentDB = CommentDB.Where(s => s.EmailUser.Contains(searchstring));
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
            return CommentDB.ToPagedList(pageNumber, pageSize);
        }
        public CommentUser DetailComment(int Id)
        {
            var model = dbcontext.CommentUsers.Where(s=>s.Id==Id).SingleOrDefault();
            return model;
        }
        
    }
}
