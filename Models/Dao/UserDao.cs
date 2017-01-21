using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
namespace Models.Dao
{
    public class UserDao
    {
        private BiMartDbContext Dbcontext = new BiMartDbContext();
        public bool CheckLogin(string email,string password)
        {
            return Dbcontext.Users.SingleOrDefault(c => c.Email == email && c.Password == password) != null;
        }
        public bool Logout()
        {
            return true;
        }
        public User getbyId(string email)
        {
            return Dbcontext.Users.SingleOrDefault(s => s.Email == email);
        }
    }
}
