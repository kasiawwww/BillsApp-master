using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppDatabase
{
    public class TransactionElement : Entity
    {
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
