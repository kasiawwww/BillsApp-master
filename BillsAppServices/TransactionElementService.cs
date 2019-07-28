using AutoMapper;
using BillsApp.DTOs;
using BillsAppDatabase;
using BillsAppDatabase.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BillsAppServices
{
    public class TransactionElementService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransactionElementService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public void AddElements(List<TransactionElementDTO> elementsDTOs)
        {
            TransactionElement transactionElement = new TransactionElement();
            foreach (var item in elementsDTOs)
            {

                transactionElement = _mapper.Map<TransactionElement>(item);
                if (_context.Products.Any(p => p.Name == item.ProductName))
                    transactionElement.ProductId = _context.Products.FirstOrDefault(p => p.Name == item.ProductName).Id;
                else
                {
                    AddProduct(item);
                    transactionElement.ProductId = _context.Products.FirstOrDefault(p => p.Name == item.ProductName).Id;
                }
                _context.Add(transactionElement);
            }
            _context.SaveChanges();

        }

        private void AddProduct(TransactionElementDTO element)
        {
            Product product = new Product();
            product.Name = element.ProductName;
            product.Unit = element.Unit;
            _context.Add(product);
            _context.SaveChanges();

        }

        public List<TransactionElementDTO> GetElementsForTransaction(int transactionId)
        {
            var transactionElements = _context.TransactionElements.Where(t => t.TransactionId == transactionId).ToList();
            var products = _context.Products.ToList();

            var elements = from p in products
                                         join t in transactionElements
                                         on p.Id equals t.ProductId
                                         select new TransactionElementDTO {
                                             ProductName = p.Name,
                                             Price = t.Price,
                                             Amount = t.Amount,
                                             Unit = p.Unit
                                         };

            return elements.ToList();
        }

    }
}
