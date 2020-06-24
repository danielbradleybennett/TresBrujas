using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TresBrujas.Models;


namespace TresBrujas.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TresBrujas.Models.Spell> Spell { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<SpellCaster> SpellCaster { get; set; }

        public DbSet<SpellSpellCaster> SpellSpellCaster { get; set; }

        public DbSet<SpellType> SpellType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Thania",
                LastName = "Thania",
                UserName = "admin@admin.com",
                IsAdmin = true,
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "10000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<Spell>().HasData(
               new Spell()
               {
                   Id = 1,
                   Name = "Fireball",
                   SpellTypeId = 1
               },
               new Spell()
               {
                   Id = 2,
                   Name = "Divination",
                   SpellTypeId = 2
               });
                modelBuilder.Entity<SpellType>().HasData(
               new SpellType()
               {
                   Id = 1,
                   Type = "Divination"
               },
               new SpellType()
               {
                   Id = 2,
                   Type = "Evocation",
               });
                modelBuilder.Entity<SpellCaster>().HasData(
               new SpellCaster()
               {
                   Id = 1,
                   Name = "Wizard"


               },
               new SpellCaster()
               {
                   Id = 2,
                   Name = "Warlock"
               });
                modelBuilder.Entity<SpellSpellCaster>().HasData(
               new SpellSpellCaster()
               {
                   Id = 1,
                   SpellId = 1,
                   SpellCasterId = 1
               },
               new SpellSpellCaster()
               {
                   Id = 2,
                   SpellId = 2,
                   SpellCasterId = 2
               });




        }
    }
}
