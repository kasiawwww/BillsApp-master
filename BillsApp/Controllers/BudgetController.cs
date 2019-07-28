using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsAppDatabase;
using BillsAppDatabase.Data;
using BillsAppServices.DTOs;
using BillsAppServices;
using Microsoft.AspNetCore.Authorization;

namespace BillsApp.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private readonly BudgetService _budgetService;

        public BudgetController(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET: Budget
        public async Task<IActionResult> Index()
        {
            var budgets = _budgetService.GetBudgets().ToList();
            return View(budgets);
        }

        //// GET: Budget/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var budget = await _context.Budgets
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (budget == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(budget);
        //}

        // GET: Budget/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Budget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Amount,From,To,CreatedDate,ModificationDate,UserId,Id")] BudgetDTO budgetDTO)
        {
            if (ModelState.IsValid)
            {
                _budgetService.AddBudget(budgetDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(budgetDTO);
        }

        //// GET: Budget/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var budget = await _context.Budgets.FindAsync(id);
        //    if (budget == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(budget);
        //}

        //// POST: Budget/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,Amount,From,To,CreatedDate,ModificationDate,UserId,Id")] Budget budget)
        //{
        //    if (id != budget.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(budget);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BudgetExists(budget.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(budget);
        //}

        //// GET: Budget/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var budget = await _context.Budgets
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (budget == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(budget);
        //}

        //// POST: Budget/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var budget = await _context.Budgets.FindAsync(id);
        //    _context.Budgets.Remove(budget);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BudgetExists(int id)
        //{
        //    return _context.Budgets.Any(e => e.Id == id);
        //}
    }
}
