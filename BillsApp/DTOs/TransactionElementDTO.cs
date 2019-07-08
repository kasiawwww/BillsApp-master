using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApp.DTOs
{
    public class TransactionElementDTO
    {
        [Required]
        public string Product { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
