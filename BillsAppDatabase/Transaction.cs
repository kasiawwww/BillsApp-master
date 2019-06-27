using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillsAppDatabase
{
    public class Transaction : Entity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ModificationDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int? TransactionCategoryId { get; set; }
        public virtual TransactionCategory TransactionCategory { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int? PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual ICollection<TransactionTag> TransactionTags { get; set; }
        public virtual ICollection<TransactionElement> TransactionElements { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
