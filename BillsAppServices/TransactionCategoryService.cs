using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsAppServices
{
    public class TransactionCategoryService
    {
        private readonly ApplicationDbContext _context;

        public TransactionCategoryService(ApplicationDbContext context)
        {
            _context = context;

        }
        public void AddTransactionCategory(TransactionCategory transactionCategory)
        {
            _context.Add(transactionCategory);
            _context.SaveChanges();
        }

        public DbSet<TransactionCategory> GetTransactionCategories()
        {
            var transactionCategories = _context.TransactionCategories;
            return transactionCategories;
        }
    }
}
