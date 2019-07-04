using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BillsAppDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BillsAppDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransactionTag>()
                .HasKey(x => new { x.TransactionId, x.TagId });

            modelBuilder.Entity<TransactionTag>()
                 .HasOne<Transaction>(sc => sc.Transaction)
                 .WithMany(s => s.TransactionTags)
                 .HasForeignKey(sc => sc.TransactionId);


            modelBuilder.Entity<TransactionTag>()
                .HasOne<Tag>(sc => sc.Tag)
                .WithMany(s => s.TransactionTags)
                .HasForeignKey(sc => sc.TagId);

            //modelBuilder
            //    .Entity<PaymentType>()
            //    .Property(e => e.Name)
            //    .HasConversion(
            //        v => v.ToString(),
            //        v => (PaymentTypeEnum)Enum.Parse(typeof(PaymentTypeEnum), v));

            modelBuilder
                .Entity<PaymentType>()
                .HasData(Helpers.EntitiesFromEnum
                .BuildEntityObjectsFromEnum<PaymentType, PaymentTypeEnum>());

            modelBuilder
                .Entity<Unit>()
                .HasData(Helpers.EntitiesFromEnum
                .BuildEntityObjectsFromEnum<Unit, UnitEnum>());
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<TransactionElement> TransactionElements { get; set; }
        public DbSet<TransactionTag> TransactionTags { get; set; }
        public new DbSet<User> Users { get; set; }

    }
}
