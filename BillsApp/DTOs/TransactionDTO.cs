using BillsAppDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BillsAppDatabase.Data;
using System.ComponentModel.DataAnnotations;

namespace BillsApp.DTOs
{
    public class TransactionDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Price { get; set; }
        public int? TransactionCategoryId { get; set; }

        public int? PaymentTypeId { get; set; }

        public List<TransactionElementDTO> Elements { get; set; }

        [Required]
        public List<FileDTO> Files { get; set; }

        public TransactionDTO()
        {
            Files = new List<FileDTO>();
            Elements = new List<TransactionElementDTO>();
        }

    }
}
