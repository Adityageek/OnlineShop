using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapOnlineShop.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BootstrapOnlineShop.Context
{
    public class ApplicationDataContext : IdentityDbContext<AppUser>
    {
        public ApplicationDataContext()
            : base("DefaultConnection")
        { }

        public System.Data.Entity.DbSet<AppUser> AppUsers { get; set; }
    }
}