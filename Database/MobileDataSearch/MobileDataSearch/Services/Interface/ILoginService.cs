using MobileDataSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Services.Interface
{
    public interface ILoginService
    {
        int CreateUpdateUser(LoginUser loginUser);
        IEnumerable<LoginUser> LoginUser(string Contact, string Password);
        IEnumerable<LoginUser> GetAllUser();
    }
}
