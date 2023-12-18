using AuthenticationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AuthenticationAPI.AuthenticationRepository
{
    public class UserRipository
    {
        private readonly DbAuthentication _context;
        public UserRipository(DbAuthentication context)
        {
            _context = context;
            
        }
        public UserRipository()
        {
           
        }
        public async Task<bool> AddUser(Users users)
        {
            try
            {
                if (users != null)
                {
                    Users data = _context.Users.Where(x => x.USERNAME == users.USERNAME).Select(x => x).SingleOrDefault();
                    if (data != null)
                    {
                      await _context.Users.AddAsync(users);
                      await  _context.SaveChangesAsync();
                      return true;
                    }

                }
                return false;


            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Users> GetUsers()
        {
            return _context.Users.Where(x => x.ISACTIVE == "Y").Select(x => new Users
            {
                USERNAME = x.USERNAME,
                FIRSTNAME = x.FIRSTNAME,
                LASTNAME = x.LASTNAME,
                PHONENUMBER = x.PHONENUMBER,
                ISACTIVE = x.ISACTIVE,
                CreatedDate = x.CreatedDate.Date,
                UpdatedDate = x.UpdatedDate.Date,
                PASSWORD = "*****",
                SALT = "******"
            }).ToList();
        }

        public bool LoginCheck(string username, string planepassword)
        {
            Users data = _context.Users.Where(x => x.USERNAME == username && x.ISACTIVE == "Y").Select(x => x).SingleOrDefault();
            HashingAlgorithms hashingAlgorithms = new HashingAlgorithms(1000);
            return hashingAlgorithms.AuthenticateUser(planepassword, data.PASSWORD, data.SALT);             
        }

        public void  sendmail(string tomail, string subject, AlternateView body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("pavan@gmail.com");
                mail.To.Add(tomail);
                mail.Subject = subject;
                mail.AlternateViews.Add(body);
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Host = "localhost";
                    smtp.Credentials = new NetworkCredential("pavan@gmail.com", "111222");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
