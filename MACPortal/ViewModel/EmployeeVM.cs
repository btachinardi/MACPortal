using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MACPortal.Extensions;
using MACPortal.Models.Users;

namespace MACPortal.ViewModel
{
    public class EmployeeVM
    {

        public EmployeeVM Start(Employee employee, int ranking)
        {
            Name = employee.Name.ShortName();
            ComercialName = employee.ComercialName;
            AvatarUrl = employee.AvatarUrl;
            Points = employee.Points;
            Ranking = ranking;

            var broker = employee as Broker;
            if (broker != null)
            {
                if (broker.Sales != null)
                {
                    Sales = broker.Sales.Count;
                }
            }
            else
            {
                var manager = employee as Manager;
                if (manager != null)
                {
                    if (manager.Sales != null)
                    {
                        Sales = manager.Sales.Count;
                    }
                }
                else
                {
                    var coordinator = employee as Coordinator;
                    if (coordinator != null)
                    {
                        if (coordinator.Sales != null)
                        {
                            Sales = coordinator.Sales.Count;
                        }
                    }
                }
            }
            return this;
        }

        public string Name { get; set; }
        public string ComercialName { get; set; }
        public string AvatarUrl { get; set; }
        public int Points { get; set; }
        public int Sales { get; set; }
        public int Ranking { get; set; }
    }
}