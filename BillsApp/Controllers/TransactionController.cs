using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsAppDatabase.Data;
using BillsAppDatabase;
using BillsAppServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BillsApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private TransactionService _transactionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(_transactionService.GetTransactions());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.PaymentType)
                .Include(t => t.TransactionCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            ViewData["PaymentTypeId"] = _transactionService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _transactionService.GetTransactionCategories(); //new SelectList(_context.TransactionCategories, "Id", "Name", transaction.TransactionCategoryId);
            //ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Id");
            //ViewData["TransactionCategoryId"] = new SelectList(_context.TransactionCategories, "Id", "Name");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,TransactionDate,CreateDate,ModificationDate,Price,TransactionCategoryId,UserId,PaymentTypeId,Id")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _transactionService.AddTransaction(transaction);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = _transactionService.GetPaymentTypes();
            ViewData["TransactionCategoryId"] = _transactionService.GetTransactionCategories(); //new SelectList(_context.TransactionCategories, "Id", "Name", transaction.TransactionCategoryId);
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Id", transaction.PaymentTypeId);
            ViewData["TransactionCategoryId"] = new SelectList(_context.TransactionCategories, "Id", "Name", transaction.TransactionCategoryId);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,TransactionDate,CreateDate,ModificationDate,Price,TransactionCategoryId,UserId,PaymentTypeId,Id")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Id", transaction.PaymentTypeId);
            ViewData["TransactionCategoryId"] = new SelectList(_context.TransactionCategories, "Id", "Name", transaction.TransactionCategoryId);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.PaymentType)
                .Include(t => t.TransactionCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
