using AuthenticationAPI.AuthenticationRepository;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AuthenticationAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserRipository _context;               
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DbAuthentication context)
        {    
            _logger = logger;
            _context = new UserRipository(context);
            


        }        

        [HttpGet]
        [Route("GetAllUsers")]
        public List<Users> GetUser()
        {

            return _context.GetUsers();
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<string> Adduser(Users userdate)
        {
            try
            {               
                HashingAlgorithms hashingAlgorithm = new HashingAlgorithms(1000);
                string[] generatedPasswordSalt = hashingAlgorithm.GeneratePasswordHash(userdate.PASSWORD);
                userdate.PASSWORD = generatedPasswordSalt[0];
                userdate.SALT = generatedPasswordSalt[1];                
                if (await _context.AddUser(userdate))
                {
                    string body = "";
                    string subject = "";
                    AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    _context.sendmail(userdate.USERNAME, subject, alternateView);
                    return "Registered successfully and Activation Link has been mailed";
                }
                return "Registration failed for the user "+ userdate.USERNAME;
            }
            catch(Exception ex) { return "Something went wrong Please see exception"+ex; }
        }

        [HttpGet]
        [Route("Login")]
        public string LoginCheck(string username, string password)
        {
            if (username !=null || password != null)
            {
                string data = (_context.LoginCheck(username, password)) ? "Login success" : "Login Failed";
                return data;
            }
            return "Login Failed";
        }



    }
}
