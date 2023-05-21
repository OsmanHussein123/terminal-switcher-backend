using LoginService.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace LoginService.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Container> Containers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            User user1 = new User { Id = 1, Username = "User1", Password = "Password1" };
            User user2 = new User { Id = 2, Username = "User2", Password = "Password2" };

            Container container1 = new Container { Id=1, UserId = user1.Id,Image ="alpine", ContainerName=System.Guid.NewGuid().ToString()};
            Container container2 = new Container { Id=2, UserId = user1.Id,Image ="ubuntu", ContainerName=System.Guid.NewGuid().ToString()};
            Container container3 = new Container { Id=3, UserId = user1.Id,Image ="debian", ContainerName=System.Guid.NewGuid().ToString()};
            Container container4 = new Container { Id=4, UserId = user1.Id, Image = "fedora/memcached", ContainerName = System.Guid.NewGuid().ToString() };
            Container container5 = new Container { Id = 5, UserId = user2.Id, Image = "alpine", ContainerName = System.Guid.NewGuid().ToString() };
            Container container6 = new Container { Id = 6, UserId = user2.Id, Image = "ubuntu", ContainerName = System.Guid.NewGuid().ToString() };
            Container container7 = new Container { Id = 7, UserId = user2.Id, Image = "debian", ContainerName = System.Guid.NewGuid().ToString() };
            Container container8 = new Container { Id = 8, UserId = user2.Id, Image = "fedora/memcached", ContainerName = System.Guid.NewGuid().ToString() };

            builder.Entity<User>().HasData(user1,user2);
            builder.Entity<Container>().HasData(container1, container2,container3,container4,container5,container6,container7,container8);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
