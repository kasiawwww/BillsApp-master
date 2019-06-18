using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppDatabase
{
    public class TransactionCategory : Entity
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
