
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsAppDatabase
{
    public enum UnitEnum
    {
        Item = 1,
        Kg = 2,
        Litr = 3
    }
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

}
