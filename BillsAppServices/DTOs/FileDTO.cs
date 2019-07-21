using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApp.DTOs
{
    public class FileDTO
    {
        [Required]
        public string Name { get; set; }     

        public string FileScreenShot { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        [Required]
        public int TransactionId { get; set; }
    }

}
