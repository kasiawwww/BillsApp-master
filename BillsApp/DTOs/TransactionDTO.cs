using BillsAppDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BillsAppDatabase.Data;


namespace BillsApp.DTOs
{
    public class TransactionDTO: Transaction
    {
        //private readonly UserManager<User> _userManager;
        private readonly HttpContextAccessor _httpContextAccessor;

        public TransactionDTO(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;//_userManager.GetUserId(HttpContext.User);
            CreateDate = DateTime.Now;
            ModificationDate = DateTime.Now;

        }

    }
}
