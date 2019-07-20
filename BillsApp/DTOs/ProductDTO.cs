using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApp.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }

        public List<TransactionElementDTO> Elements { get; set; }
        public ProductDTO()
        {
            Elements = new List<TransactionElementDTO>();
        }

    }
}
