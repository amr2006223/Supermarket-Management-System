using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Supermarket_Managment_System.Models;
using System.Collections.Generic;

namespace Supermarket_Managment_System.Data
{
    public class db_context: IdentityDbContext
    {
        public db_context(DbContextOptions<db_context> options) : base(options)
        {

        }
        public DbSet<bills> bill { get; set; }
        public DbSet<bill_items_details> bill_items_details { get; set; }
        public DbSet<categories> category { get; set; }
        public DbSet<offers> offers { get; set; }
        public DbSet<payments> payment { get; set; }
        public DbSet<products> product { get; set; }
        public DbSet<products_offers> products_offers { get; set; }
        public DbSet<products_categories> product_catoegories { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<users> user { get; set; }
    }
}
