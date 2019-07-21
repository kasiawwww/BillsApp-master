using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public PaymentType GetPaymentType(int paymentTypeId)
        {
            var paymentType = _context.PaymentTypes.Where(p => p.Id == paymentTypeId).FirstOrDefault();
            return paymentType;
        }
    }
}
