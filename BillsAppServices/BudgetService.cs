using AutoMapper;
using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BillsAppServices.DTOs
{
    public class BudgetService
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BudgetService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void AddBudget(BudgetDTO budgetDTO)
        {
            Budget budget = new Budget();
            budget = _mapper.Map<Budget>(budgetDTO);
            budget.UserId = GetCurrentUserId();
            budget.CreatedDate = DateTime.Now;
            budget.ModificationDate = DateTime.Now;
            _context.Add(budget);
            _context.SaveChanges();
        }

        public void Editbudget(Budget budget)
        {
            _context.Update(budget);
            _context.SaveChanges();
        }

        public void Deletebudget(int id)
        {
            var budget = _context.Budgets.Find(id);
            _context.Budgets.Remove(budget);
            _context.SaveChangesAsync();
        }

        public IQueryable<Budget> GetBudgets()
        {
            var budgets = _context.Budgets.Where(t => t.UserId == GetCurrentUserId());
            return budgets;
        }
    }
}
