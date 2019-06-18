using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppDatabase
{
    public class Tag: Entity
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<TransactionTag> TransactionTags { get; set; }

    }
}
