using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppServices.DTOs
{
    public class TransactionCategoryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
