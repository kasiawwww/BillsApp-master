using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsAppServices
{
    public class PaymentTypeService
    {
        private readonly ApplicationDbContext _context;

        public PaymentTypeService(ApplicationDbContext context)
        {
            _context = context;

        }

        public DbSet<PaymentType> GetPaymentTypes()
        {
            var paymentTypes = _context.PaymentTypes;
            return paymentTypes;
        }

    }
}
