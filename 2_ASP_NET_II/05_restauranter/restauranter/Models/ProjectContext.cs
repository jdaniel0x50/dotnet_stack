using Microsoft.EntityFrameworkCore;

namespace restauranter.Models
{
    public class RestauranterContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public RestauranterContext(DbContextOptions<RestauranterContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
