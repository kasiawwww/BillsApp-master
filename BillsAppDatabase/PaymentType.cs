
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsAppDatabase
{
    public enum PaymentTypeEnum
    {
        OnlineCardPayment = 1,
        Blik = 2,
        Transfer = 3,
        Paypal = 4,
        UsmiechBabelka = 5
    }
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
