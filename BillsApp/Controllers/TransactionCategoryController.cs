﻿using System;
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
    public class TransactionCategoryController : Controller
    {
        private readonly TransactionCategoryService _transactionCategoryService;

        public TransactionCategoryController(TransactionCategoryService transactionCategoryService)
        {
            _transactionCategoryService = transactionCategoryService;
        }

        // GET: TransactionCategory
        public async Task<IActionResult> Index()
        {
            var transactionCategoryDTOs = _transactionCategoryService.GetTransactionCategories();
            return View(transactionCategoryDTOs);
        }

        //// GET: TransactionCategory/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transactionCategory = await _context.TransactionCategories
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (transactionCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transactionCategory);
        //}

        // GET: TransactionCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCategoryDTO transactionCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                _transactionCategoryService.AddTransactionCategory(transactionCategoryDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(transactionCategoryDTO);
        }

        //// GET: TransactionCategory/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transactionCategory = await _context.TransactionCategories.FindAsync(id);
        //    if (transactionCategory == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(transactionCategory);
        //}

        //// POST: TransactionCategory/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] TransactionCategoryDTO transactionCategory)
        //{
        //    if (id != transactionCategory.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(transactionCategory);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TransactionCategoryExists(transactionCategory.Id))
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
        //    return View(transactionCategory);
        //}

        //// GET: TransactionCategory/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transactionCategory = await _context.TransactionCategories
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (transactionCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transactionCategory);
        //}

        //// POST: TransactionCategory/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var transactionCategory = await _context.TransactionCategories.FindAsync(id);
        //    _context.TransactionCategories.Remove(transactionCategory);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TransactionCategoryExists(int id)
        //{
        //    return _context.TransactionCategories.Any(e => e.Id == id);
        //}
    }
}
