using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MACPortal.Models.Users;

namespace MACPortal.Models
{
    public class SaleExcel
    {
        public string SaleExcelID { get; set; }
        public string Url { get; set; }

        public int SaleRegisterID { get; set; }

        [ForeignKey("SaleRegisterID")]
        public virtual SaleRegister SaleRegister { get; set; }
    }

    public class SaleRegister
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SaleRegisterID { get; set; }
        public DateTime Date { get; set; }

        public int SaleExcelID { get; set; }

        [ForeignKey("SaleExcelID")]
        public virtual SaleExcel Excel { get; set; }
        public virtual ICollection<SaleExcel> History { get; set; }
        public virtual ICollection<Coordinator> Coordinators { get; set; }
    }

    public class Sale
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        [Column(TypeName = "Money")]
        public decimal Value { get; set; }
        public decimal Modifier { get; set; }

        //Extras
        public SaleStyle Style { get; set; }
        public string Unit { get; set; }
        public string Tower { get; set; }
        public SaleType Type { get; set; }
        public DateTime Date { get; set; }

        //Sellers Extra
        public int NumberOfCoordinators { get; set; }

        public int EnterpriseID { get; set; }
        public int BrokerID { get; set; }
        public int ThirdPartyCoordinatorID { get; set; }
        public int ManagerID { get; set; }
        public int? OnlineCoordinatorID { get; set; }

        [ForeignKey("EnterpriseID")]
        public virtual Enterprise Enterprise { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        
        public int PointsFor(Employee employee)
        {
            if (employee.UserID == BrokerID)
            {
                return Convert.ToInt32((Value / 1000m));
            } 
            if (employee.UserID == ManagerID)
            {
                return Convert.ToInt32((Value / 1000m) / 5);
            }
            if (employee.UserID == OnlineCoordinatorID)
            {
                return Convert.ToInt32((Value / 1000m) / 4);
            }
            if (employee.UserID == ThirdPartyCoordinatorID)
            {
                return Convert.ToInt32((Value / 1000m) / 4);
            }
            if (Employees.Any(c => c.UserID == employee.UserID))
            {
                return Convert.ToInt32((Value / 1000m) / 4 * NumberOfCoordinators);
            }
            return 0;
        }
    }

    public enum SaleType
    {
        DIRECT,
        THIRD_PARTY,
        INVALID,
        RESALE
    }

    public enum SaleStyle
    {
        RELEASE,
        STOCK,
        INVALID
    }
}