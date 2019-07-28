using BillsAppDatabase.Data;
using BillsAppServices.ChartDTOs;
using BillsAppServices.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace BillsAppServices
{
    public class ChartService
    {
        private readonly ApplicationDbContext _context;

        public ChartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DataPoint> GetDataForChart(string procedureName)
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            _context.Database.OpenConnection();
            DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = procedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            List<ChartDTO> list;

            using (var reader = cmd.ExecuteReader())
            {
                list = reader.MapToList<ChartDTO>();
            }

            foreach (var item in list)
                dataPoints.Add(new DataPoint(item.Label, item.Value));

            return dataPoints;
        }

    }
}
