using AutoMapper;
using BillsApp.DTOs;
using BillsAppDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillsApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<TransactionElement, TransactionElementDTO>();
            CreateMap<TransactionElementDTO, TransactionElement>();
            CreateMap<File, FileDTO>();
            CreateMap<FileDTO, File>();
        }
    }
}
