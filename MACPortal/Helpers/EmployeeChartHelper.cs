using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MACPortal.Models.Users;

namespace MACPortal.Helpers
{
    public class EmployeeChartHelper
    {
        public object[] Points;
        public object[] Sales;
        public object[] PointsTotal;
        public object[] SalesTotal;

        public EmployeeChartHelper(Employee employee)
        {
            Points = Empty();
            Sales = Empty();
            PointsTotal = Empty();
            SalesTotal = Empty();

            foreach (var sale in employee.Sales)
            {
                var month = sale.SaleDate.Month;
                Points[month - 1] = (int)Points[month - 1] + sale.PointsFor(employee);
                Sales[month - 1] = (int)Sales[month - 1] + 1;

                for (var i = month - 1; i < 12; i++)
                {
                    PointsTotal[i] = (int)PointsTotal[i] + sale.PointsFor(employee);
                    SalesTotal[i] = (int)SalesTotal[i] + 1;
                }
            }
        }

        private object[] Empty()
        {
            var empty = new object[12];
            for (int i = 0; i < empty.Count(); i++)
            {
                empty[i] = 0;
            }
            return empty;
        }
    }
}