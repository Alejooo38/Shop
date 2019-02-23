using Microsoft.AspNetCore.Identity;
using Shop.Web.Data.Entities;
using Shop.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private Random random;

        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userManager.FindByEmailAsync("alejandro.ruiz@correo.policia.gov.co");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Daniel",
                    LastName = "Ruiz",
                    Email = "alejandro.ruiz@correo.policia.gov.co",
                    UserName = "alejandro.ruiz@correo.policia.gov.co",
                    PhoneNumber = "3115918342"
                };

                var result = await this.userManager.CreateAsync(user, "DarbNho_78");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X Product", user);
                this.AddProduct("Magic Product", user);
                this.AddProduct("iWatch Series 4", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailable = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}
