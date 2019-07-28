using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsAppServices.ChartDTOs
{
    public class ChartDTO
    {
        [Required]
        public string Label { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}
