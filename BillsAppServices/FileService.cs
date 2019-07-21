using AutoMapper;
using BillsApp.DTOs;
using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using File = BillsAppDatabase.File;

namespace BillsAppServices
{
    public class FileService
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public FileService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public void AddFiles(FileDTO fileDTO)
        {
            File file = new File();
            convertToBase64(fileDTO);
            file = _mapper.Map<File>(fileDTO);
            _context.Add(file);
            _context.SaveChanges();
        }

        public List<FileDTO> GetFilesForTransaction(int transactionId)
        {
            var files = _context.Files.Where(t => t.TransactionId == transactionId);
            var filesDTO = new List<FileDTO>();
            foreach (var item in files)
            {
                var fileDTO = _mapper.Map<FileDTO>(item);
                filesDTO.Add(fileDTO);
            }
            return filesDTO;

        }

        public List<FileDTO> GetFilesForUser()
        {
            var files = _context.Files.Where(f => f.Transaction.UserId ==  GetCurrentUserId());
            var filesDTO = new List<FileDTO>();
            foreach (var item in files)
            {
                var fileDTO = _mapper.Map<FileDTO>(item);
                filesDTO.Add(fileDTO);
            }
            return filesDTO;

        }
        private void convertToBase64(FileDTO fileDTO)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileDTO.ImageFile.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                fileDTO.ImageFile.CopyTo(stream);
            }
            var byteArray = System.IO.File.ReadAllBytes(path);
            fileDTO.FileScreenShot = Convert.ToBase64String(byteArray);
        }

    }
}
