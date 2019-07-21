using AutoMapper;
using BillsAppDatabase;
using BillsAppDatabase.Data;
using BillsAppServices.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillsAppServices
{
    public class TransactionCategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransactionCategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public void AddTransactionCategory(TransactionCategoryDTO transactionCategoryDTO)
        {
            TransactionCategory transactionCategory = new TransactionCategory();           
            transactionCategory = _mapper.Map<TransactionCategory>(transactionCategoryDTO);
            _context.Add(transactionCategory);
            _context.SaveChanges();
        }

        public TransactionCategory GetTransactionCategory(int categoryId)
        {
            var transactionCategory = _context.TransactionCategories.Where(c => c.Id == categoryId).FirstOrDefault();
            return transactionCategory;
        }

        public IQueryable<TransactionCategory> GetTransactionCategories()
        {
            var transactionCategories = _context.TransactionCategories;
            return transactionCategories;
        }
    }
}
