using Microsoft.EntityFrameworkCore;
using MobileDataSearch.Model;
using MobileDataSearch.Model.Data;
using MobileDataSearch.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Repository.RepositoryClasses
{
    public class LoginRepository : ILoginRepository
    {
        private CustomContext _custonContext = new CustomContext();
        public int CreateUpdateUser(LoginUser loginUser)
        {
            try
            {
                return _custonContext.Database.ExecuteSqlRaw("Exec CreateUpdateUser {0},{1},{2},{3},{4},{5},{6}",loginUser.Id, loginUser.Name, loginUser.Contact, loginUser.UserName, loginUser.Password, loginUser.Email, loginUser.State);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<LoginUser> GetAllUser()
        {
            try
            {
                return _custonContext.Set<LoginUser>().FromSqlRaw("Exec GetAllUser");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<LoginUser> LoginUser(string Contact, string Password)
        {
            try
            {
                return _custonContext.Set<LoginUser>().FromSqlRaw("Exec LoginUser {0},{1}", Contact, Password).ToList();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
