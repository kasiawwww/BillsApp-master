using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsAppDatabase;
using BillsAppDatabase.Data;
using BillsApp.DTOs;
using Microsoft.AspNetCore.Authorization;
using BillsAppServices;

namespace BillsApp.Controllers
{
    [Authorize]
    public class TransactionElementController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly TransactionElementService _transactionElementService;

        public TransactionElementController(TransactionService transactionService, TransactionElementService transactionElementService)
        {
            _transactionService = transactionService;
            _transactionElementService = transactionElementService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            List<TransactionElementDTO> elements = new List<TransactionElementDTO>();
            if (id != null)
                elements = _transactionElementService.GetElementsForTransaction((int)id);

            return View(elements);
        }

        // GET: TransactionElement/Create
        public IActionResult Create(int transactionId)
        {
            return View(new List<TransactionElementDTO>());
           
        }

        // POST: TransactionElement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]List<TransactionElementDTO> data)
        {
            if (ModelState.IsValid)
            {
                _transactionElementService.AddElements(data);
                return RedirectToAction("Index", "Transaction");
            }

            return RedirectToAction("Index", "Transaction");
        }

        // GET: TransactionElement/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transactionElement = await _context.TransactionElements.FindAsync(id);
        //    if (transactionElement == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Name", transactionElement.TransactionId);
        //    return View(transactionElement);
        //}

        // POST: TransactionElement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("TransactionId,Price,Amount,Id")] TransactionElement transactionElement)
        //{
        //    if (id != transactionElement.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(transactionElement);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TransactionElementExists(transactionElement.Id))
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
        //    ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Name", transactionElement.TransactionId);
        //    return View(transactionElement);
        //}

        //// GET: TransactionElement/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transactionElement = await _context.TransactionElements
        //        .Include(t => t.Transaction)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (transactionElement == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transactionElement);
        //}

        //// POST: TransactionElement/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var transactionElement = await _context.TransactionElements.FindAsync(id);
        //    _context.TransactionElements.Remove(transactionElement);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TransactionElementExists(int id)
        //{
        //    return _context.TransactionElements.Any(e => e.Id == id);
        //}
    }
}
