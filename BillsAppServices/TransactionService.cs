using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web;
using System.Security.Claims;

namespace BillsAppServices
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            _context.SaveChanges();           
        }

        public void EditTransaction(Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);
            _context.Transactions.Remove(transaction);
            _context.SaveChangesAsync();
        }

        public DbSet<Transaction> GetTransactions()
        {
            var transactions = _context.Transactions;//.Include(t => t.PaymentType).Include(t => t.TransactionCategory);
            return transactions;

        }

        public DbSet<PaymentType> GetPaymentTypes()
        {
            var paymentTypes = _context.PaymentTypes;
            return paymentTypes;
        }

        public DbSet<TransactionCategory> GetTransactionCategories()
        {
            var transactionCategories = _context.TransactionCategories;
            return transactionCategories;
        }
    }
}
