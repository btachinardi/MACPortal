using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MACPortal.Extensions;
using MACPortal.Models.Users;

namespace MACPortal.ViewModel
{
    public class RankingVM
    {
        public readonly List<EmployeeRankingVM> Rankings;
        public readonly string Type;

        public RankingVM(IEnumerable<Employee> employees, Type targetType)
        {
            if (targetType.ToString().Contains("Broker"))
            {
                Type = "Corretores";
            }
            else if (targetType.ToString().Contains("Manager"))
            {
                Type = "Gerentes";
            }
            else if (targetType.ToString().Contains("Coordinator"))
            {
                Type = "Coordenadores";
            }
            else
            {
                Type = "Inválido";
            }

            Rankings = new List<EmployeeRankingVM>();
            foreach (var employee in employees.Where(e => e.Active && e.GetType() == targetType))
            {
                var ranking = new EmployeeRankingVM
                    {
                        UserID = employee.UserID,
                        Name = employee.ComercialName,
                        Points = employee.Points,
                        AvatarUrl = employee.AvatarUrl
                    };
                Rankings.Add(ranking);
            }
            Rankings = Rankings.OrderByDescending(e => e.Points).ToList();
        }

        public int RankingFor(Employee employee)
        {
            return Rankings.IndexOf(Rankings.FirstOrDefault(e => e.UserID == employee.UserID)) + 1;
        }

        public void SetCurrentUser(Employee employee)
        {
            var current = Rankings.FirstOrDefault(e => e.CurrentUser);
            if (current != null)
            {
                current.CurrentUser = false;
            }

            current = Rankings.FirstOrDefault(e => e.UserID == employee.UserID);
            if (current != null)
            {
                current.CurrentUser = true;
            }
        }
    }

    public class EmployeeRankingVM
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public string AvatarUrl { get; set; }
        public bool CurrentUser { get; set; }
    }
}