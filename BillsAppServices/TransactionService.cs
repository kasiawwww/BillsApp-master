using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BillsAppServices
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public void AddTransaction(Transaction transaction)
        {
            var transactionCategories = _context.TransactionCategories.Where(t => t.Name != null);

            _context.Add(transaction);
            _context.SaveChanges();           
        }

        public List<Transaction> GetTransactions()
        {
            var transactions = _context.Transactions.Include(t => t.PaymentType).Include(t => t.TransactionCategory);
            return transactions.ToList();

        }

        public List<PaymentType> GetPaymentTypes()
        {
            var paymentTypes = new SelectList(_context.PaymentTypes, "Id", "Id");
            return paymentTypes;
        }

        public List<TransactionCategory> GetTransactionCategories()
        {
            var transactionCategories = _context.TransactionCategories.Where(t => t.Name != null).ToList();
            return transactionCategories;
        }
    }
}
