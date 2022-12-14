using FinanceApp.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.HasData(CreateUsers());
        }

        private List<User> CreateUsers()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                UserName = "guest_user",
                NormalizedUserName = "guest_user".Normalize(),
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com".Normalize(),
                Currency = "BGN"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "guest123");

            users.Add(user);

            return users;
        }
    }
}
