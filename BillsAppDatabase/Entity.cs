using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppDatabase
{
    public class Entity
    {
        [Required]
        public int Id { get; set; }
    }
}
