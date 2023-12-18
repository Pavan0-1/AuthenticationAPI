using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AuthenticationAPI.Models;
//using System.Data.Entity.SqlServer;

namespace AuthenticationAPI
{
    public class DbAuthentication: DbContext
    {
        public DbAuthentication(DbContextOptions<DbAuthentication> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }

        
    }
}
