using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FIFO.Models
{
    public class FIFODBContext : DbContext
    {
        public FIFODBContext() : base("FIFO.Properties.Settings.FIFODBConnectionString")
            { }
        public DbSet<User> Users { get; set; }
    }

    public class UserDbInitializer : DropCreateDatabaseAlways<FIFODBContext>
    {
        protected override void Seed(FIFODBContext db)
        {
            //db.Users.Add(new User
            //{
            //    Email = "andry@logvin.net",
            //    Password = "******",
            //    Login = "gotthit"
            //});

            base.Seed(db);
        }
    }
}