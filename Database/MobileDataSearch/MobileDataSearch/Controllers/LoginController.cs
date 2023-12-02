using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileDataSearch.Model;
using MobileDataSearch.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("CreateUpdateUser")]
        public IActionResult CreateUpdateUser(LoginUser loginUser)
        {
            try
            {
                return Ok(_loginService.CreateUpdateUser(loginUser));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("LoginUser")]
        public IActionResult LoginUser(string Contact,string Password)
        {
            try
            {
                var login = _loginService.LoginUser(Contact, Password);
                if (login.Count() == 0)
                    return Ok("InCorrect Credential Login Failed");
                else
                    return Ok(login);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllUser")]
        public IActionResult GetAllUser()
        {
            try
            {
                return Ok(_loginService.GetAllUser());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetOTPForLogin")]
        public IActionResult GetOTPForLogin(int lenght)
        {
            try
            {
                return Ok(GenerateOTP(lenght));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        static string GenerateOTP(int length)
        {
            // Define the characters from which the OTP will be generated
            const string characters = "0123456789";

            // Create a random number generator
            Random random = new Random();

            // Generate the OTP by randomly selecting characters from the character set
            char[] otp = new char[length];
            for (int i = 0; i < length; i++)
            {
                otp[i] = characters[random.Next(characters.Length)];
            }

            // Convert the OTP characters to a string
            return new string(otp);
        }
    }
}
