using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppDatabase
{
    public class File : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string FileScreenShot { get; set; }
        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
