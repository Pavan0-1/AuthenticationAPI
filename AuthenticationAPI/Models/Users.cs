using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AuthenticationAPI.Models
{    
    public class Users
    {        
        [Key]
        public int userid { get; set; }
        [EmailAddress]
        public string USERNAME { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string PASSWORD { get; set; }
        public string SALT { get; set; }       
        public string? PHONENUMBER { get; set; }
        public string ISACTIVE { get; set; }
        public string ROLES { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

       

       
    }
}
