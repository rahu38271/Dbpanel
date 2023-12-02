using MobileDataSearch.Model;
using MobileDataSearch.Repository.Interface;
using MobileDataSearch.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Services.ServiceClasses
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

      

        public int CreateUpdateUser(LoginUser loginUser)
        {
            return _loginRepository.CreateUpdateUser(loginUser);
        }

        public IEnumerable<LoginUser> GetAllUser()
        {
            return _loginRepository.GetAllUser();
        }

        public IEnumerable<LoginUser> LoginUser(string Contact, string Password)
        {
            return _loginRepository.LoginUser(Contact, Password);
        }
    }
}
