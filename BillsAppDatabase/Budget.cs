using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppDatabase
{
    public class Budget: Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModificationDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int? TransactionCategoryId { get; set; }
        public virtual TransactionCategory Category { get; set; }
    }
}
