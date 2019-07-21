using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using AutoMapper;
using BillsApp.DTOs;

namespace BillsAppServices
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public TransactionService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void AddTransaction(TransactionDTO transactionDTO, out int transactionId)
        {
            Transaction transaction = new Transaction();
            transaction = _mapper.Map<Transaction>(transactionDTO);
            transaction.UserId = GetCurrentUserId();
            transaction.CreateDate = DateTime.Now;
            transaction.ModificationDate = DateTime.Now;
            _context.Add(transaction);
            _context.SaveChanges();
            transactionId = transaction.Id;
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

        public IQueryable<Transaction> GetTransactions()
        {
            var transactions = _context.Transactions.Where(t => t.UserId == GetCurrentUserId());//.Include(t => t.PaymentType).Include(t => t.TransactionCategory);
            return transactions;

        }

        public IQueryable<Transaction> GetTransactionById(int transactionId)
        {
            var transaction = _context.Transactions.Where(t => t.Id == transactionId);
            return transaction;

        }


    }
}
