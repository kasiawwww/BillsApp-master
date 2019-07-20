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

namespace BillsApp.Controllers
{
    public class FileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TransactionService _transactionService;
        private readonly IMapper _mapper;

        public FileController(ApplicationDbContext context, TransactionService transactionService, IMapper mapper)
        {
            _context = context;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // GET: File
        public async Task<IActionResult> Index()
        {
            List<FileDTO> filesDTO = new List<FileDTO>();
            var applicationDbContext = _context.Files.Include(f => f.Transaction);
            foreach(var item in applicationDbContext)
            {
                filesDTO.Add(_mapper.Map<FileDTO>(item));
            }
                
            return View(filesDTO);
        }

        // GET: File/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .Include(f => f.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
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
            File file = new File();
            if (fileDTO.ImageFile == null || fileDTO.ImageFile.Length == 0)
                return View(fileDTO);

            await convertToBase64Async(fileDTO);

            if (ModelState.IsValid)
            {
                file = _mapper.Map<File>(fileDTO);
                _context.Add(file);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Set<CategoryDTO>(), "ID", "Name", recipesDTO.CategoryId);
            return View(fileDTO);
        }

        // GET: File/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            ViewData["TransactionId"] = new SelectList(_context.Transactions, "Id", "Name", file.TransactionId);
            return View(file);
        }

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

        // GET: File/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.Files
                .Include(f => f.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: File/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var file = await _context.Files.FindAsync(id);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
            return _context.Files.Any(e => e.Id == id);
        }

        private static async Task convertToBase64Async(FileDTO fileDTO)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileDTO.ImageFile.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await fileDTO.ImageFile.CopyToAsync(stream);
            }
            var byteArray = await System.IO.File.ReadAllBytesAsync(path);
            fileDTO.FileScreenShot = Convert.ToBase64String(byteArray);
        }
    }
}
