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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BillsApp.DTOs;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BillsApp.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly TransactionCategoryService _transactionCategoryService;
        private readonly PaymentTypeService _paymentTypeService;
        private readonly UnitService _unitService;
        private readonly IMapper _mapper;

        public TransactionController(TransactionService transactionService, TransactionCategoryService transactionCategoryService, PaymentTypeService paymentTypeService, IMapper mapper, UnitService unitService)
        {
            _transactionService = transactionService;
            _transactionCategoryService = transactionCategoryService;
            _paymentTypeService = paymentTypeService;
            _unitService = unitService;
            _mapper = mapper;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            return View(_transactionService.GetTransactions().ToList());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionService.GetTransactions()
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
            ViewData["PaymentTypeId"] = new SelectList(_paymentTypeService.GetPaymentTypes(), "Id", "Name");
            ViewData["TransactionCategoryId"] = new SelectList(_transactionCategoryService.GetTransactionCategories(), "Id", "Name");
            return View(new TransactionDTO());
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction();
            int transactionId;
            List<TransactionElement> transactionElementsList = new List<TransactionElement>();
            if (Request.Form["AddElement"].Any())
            {
                //Dodawanie elemetu do listy
                transactionDTO.Elements.Add(new TransactionElementDTO());
                ViewData["PaymentTypeId"] = new SelectList(_paymentTypeService.GetPaymentTypes(), "Id", "Name", transaction.PaymentTypeId);
                ViewData["TransactionCategoryId"] = new SelectList(_transactionCategoryService.GetTransactionCategories(), "Id", "Name", transaction.TransactionCategoryId);
                foreach (var item in transactionDTO.Elements)
                    ViewBag.Units = new SelectList(_unitService.GetUnits(), "Id", "Name");
            }
            else if (Request.Form["Create"].Any())
            {
                if (ModelState.IsValid)
                {
                    transaction = _mapper.Map<Transaction>(transactionDTO);

                    //foreach (var item in transactionDTO.Elements)
                    //    transactionElementsList.Add(_mapper.Map<Product>(item.Product));

                    //foreach (var item in transactionDTO.Elements)
                    //    transactionElementsList.Add(_mapper.Map<TransactionElement>(item));

                    //foreach (var item in transactionElementsList)
                    //    item.TransactionId = transaction.Id;

                    _transactionService.AddTransaction(transaction, out transactionId);
                    //TempData["transactionId"] = transactionId;
                    return RedirectToAction("Create", "File", new { transactionId = transactionId });
                }
                ViewData["PaymentTypeId"] = new SelectList(_paymentTypeService.GetPaymentTypes(), "Id", "Name", transaction.PaymentTypeId);
                ViewData["TransactionCategoryId"] = new SelectList(_transactionCategoryService.GetTransactionCategories(), "Id", "Name", transaction.TransactionCategoryId);
            }

            return View(transactionDTO);           
           // return RedirectToAction("Create", "Transaction");
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionService.GetTransactions().FirstAsync(t => t.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["PaymentTypeId"] = new SelectList(_paymentTypeService.GetPaymentTypes(), "Id", "Name", transaction.PaymentTypeId);
            ViewData["TransactionCategoryId"] = new SelectList(_transactionCategoryService.GetTransactionCategories(), "Id", "Name", transaction.TransactionCategoryId);
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
                    _transactionService.EditTransaction(transaction);
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
            ViewData["PaymentTypeId"] = new SelectList(_paymentTypeService.GetPaymentTypes(), "Id", "Name", transaction.PaymentTypeId);
            ViewData["TransactionCategoryId"] = new SelectList(_transactionCategoryService.GetTransactionCategories(), "Id", "Name", transaction.TransactionCategoryId);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _transactionService.GetTransactions()
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
            _transactionService.DeleteTransaction(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _transactionService.GetTransactions().Any(e => e.Id == id);
        }

    
    }
}
