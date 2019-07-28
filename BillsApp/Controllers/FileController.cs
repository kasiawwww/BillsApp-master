using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillsAppDatabase;
using BillsAppDatabase.Data;
using System.IO;
using BillsApp.DTOs;
using BillsAppServices;
using AutoMapper;
using File = BillsAppDatabase.File;
using Microsoft.AspNetCore.Authorization;

namespace BillsApp.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly FileService _fileService;

        public FileController(TransactionService transactionService, FileService fileService)
        {
            _transactionService = transactionService;
            _fileService = fileService;
           
        }

        // GET: File
        public async Task<IActionResult> Index(int? id)
        {
            List<FileDTO> filesDTO = new List<FileDTO>();
            if (id == null)
                filesDTO = _fileService.GetFilesForUser();
            else
                filesDTO = _fileService.GetFilesForTransaction((int)id);
  
            return View(filesDTO);
        }

        // GET: File/Create
        public IActionResult Create(int transactionId)
        {
            ViewData["TransactionId"] = new SelectList(_transactionService.GetTransactionById(transactionId), "Id", "Name");
            return View();
        }

        // POST: File/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, FileScreenShot, ImageFile,TransactionId,Id")] FileDTO fileDTO)
        {
            if (fileDTO.ImageFile == null || fileDTO.ImageFile.Length == 0)
                return View(fileDTO);

            if (ModelState.IsValid)
            {
                _fileService.AddFiles(fileDTO);
                if (Request.Form["AddElements"].Any())
                    return RedirectToAction("Create", "TransactionElement", new { transactionId = fileDTO.TransactionId });
                return RedirectToAction("Index", "Transaction");
            }
          
            return View(fileDTO);
        }

        //// GET: File/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var file = await _context.Files.FindAsync(id);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Name", file.TransactionId);
        //    return View(file);
        //}

        // POST: File/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,FileScreenShot,TransactionId,Id")] FileDTO file)
        //{
        //    if (id != file.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(file);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FileExists(file.Id))
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
        //    ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Name", file.TransactionId);
        //    return View(file);
        //}

        //// GET: File/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var file = await _context.Files
        //        .Include(f => f.Transaction)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(file);
        //}

        //// POST: File/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var file = await _context.Files.FindAsync(id);
        //    _context.Files.Remove(file);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool FileExists(int id)
        //{
        //    return _context.Files.Any(e => e.Id == id);
        //}


    }
}
