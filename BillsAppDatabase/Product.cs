﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppDatabase
{
    public class Product : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        
        public virtual ICollection<TransactionElement> TransactionElements { get; set; }
    }
}
