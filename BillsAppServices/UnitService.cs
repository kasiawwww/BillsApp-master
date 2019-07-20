using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsAppServices
{
    public class UnitService
    {
        private readonly ApplicationDbContext _context;

        public UnitService(ApplicationDbContext context)
        {
            _context = context;

        }

        public DbSet<Unit> GetUnits()
        {
            var units = _context.Units;
            return units;
        }
    }
}
