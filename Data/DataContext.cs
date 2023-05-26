using FlashDeals.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlashDeals.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> context) : base(context)
        {

        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}
