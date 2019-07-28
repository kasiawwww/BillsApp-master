using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BillsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using Newtonsoft.Json;
using BillsAppDatabase.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Reflection;
using BillsAppServices.ChartDTOs;
using BillsAppServices;

namespace BillsApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ChartService _chartService;

        public HomeController(ChartService chartService)
        {
            _chartService = chartService;
        }

        public IActionResult Index()
        {
            ViewBag.DataPoints_PaymentType = JsonConvert.SerializeObject(_chartService.GetDataForChart("TransactionsByPaPaymentType"));
            ViewBag.DataPoints_TransactionCategory = JsonConvert.SerializeObject(_chartService.GetDataForChart("TransactionsByTransactionCategory"));
            ViewBag.DataPoints_Budget = JsonConvert.SerializeObject(_chartService.GetDataForChart("UpdateBudgetAfterTransaction"));
            ViewBag.DataPoints_BudgetMoneyLeft = _chartService.GetDataForChart("ActualBudgetMoneyLeft").ToList().FirstOrDefault().Y.ToString().Replace(",",".");
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}


