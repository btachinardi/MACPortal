using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MACPortal.Models.Users;

namespace MACPortal.Models
{
    public class Enterprise
    {
        public int EnterpriseID { get; set; }

        [Required]
        public string Name { get; set; }
        public decimal Modifier { get; set; }

        public virtual ICollection<Coordinator> Coordinators { get; set; }
    }
}