using DataServicesLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiInnoCV.Models
{
    public class UsersContext:DbContext
    {
        public UsersContext()
           : base("name=UsersDatabaseEntities")
        {
        }
        public DbSet<User> Users { get; set; }

    }
}