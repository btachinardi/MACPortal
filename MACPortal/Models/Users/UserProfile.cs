using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using DataAnnotationsExtensions;
using MACPortal.Models.Rewards;
using MACPortal.Models.Validation;
using MACPortal.ViewModel;

namespace MACPortal.Models.Users
{
    public enum Gender
    {
        [Display(Name = "Masculino")]
        Masculino,

        [Display(Name = "Feminino")]
        Feminino
    }

    public class UserAgreement
    {
        public const string CurrentVersion = "version1";
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string UserName { get; set; }

        public virtual Employee Employee { get; set; }

        public string RecoverPasswordToken { get; set; }
        public DateTime? RecoverPasswordExpiration { get; set; }
    }

    [Table("Employee")]
    abstract public class Employee
    {
        public Employee CopyFrom(Employee baseEmployee)
        {
            Active = baseEmployee.Active;
            TempPoints = baseEmployee.TempPoints;
            UserID = baseEmployee.UserID;
            CurrentAcceptedAgreement = baseEmployee.CurrentAcceptedAgreement;
            new UserVM().Start(baseEmployee).Finish(this, false);
            new AvatarVM().Start(baseEmployee, false).Finish(this);
            return this;
        }

        //Required
        [Key, ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        public bool Active { get; set; }
        
        [Required]
        [Display(Name = "CPF")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000.000.000-00}")]
        [CPF(ErrorMessage = "Número de CPF Inválido: {0}")]
        public string CPF { get; set; }

        [Required]
        [Display(Name = "Email")]
        [Email(ErrorMessage = "Endereço de Email inválido")]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string CurrentAcceptedAgreement { get; set; }

        [Display(Name = "Desejo receber informações via SMS sobre a Campanha")]
        public string AcceptSMS { get; set; }

        [Display(Name = "Desejo receber informações via E-mail sobre a Campanha")]
        public string AcceptEmail { get; set; }

        //Personal Info
        [Display(Name = "Data de Nascimento")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Sexo")]
        public Gender? Gender { get; set; }

        [Display(Name = "Nome Comercial")]
        public string ComercialName { get; set; }

        //Avatar
        [HiddenInput(DisplayValue = false)]
        public int? AvatarHair { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarHairColor { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarSkinColor { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarFace { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarClothes { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarClothesColor { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarEyes { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarEyesColor { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarNose { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarEars { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarMouth { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarMouthColor { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarAccessoryHead { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarAccessoryFace { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AvatarAccessoryBody { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AvatarUrl { 
            get
            {
                if (AvatarHair == null)
                {
                    return "../../Content/avatar/default.png";
                }
                return "../../Content/uploaded/avatars/avatar_" + UserID + ".png";
            } 
        }

        //Phones
        [Display(Name = "Telefone Residencial")]
        [Numeric(ErrorMessage = "O prefixo somente pode conter números")]
        public string HomePhonePrefix { get; set; }
        public string HomePhone { get; set; }
        public string CellPhonePrefix { get; set; }
        public string CellPhone { get; set; }
        
        //Location
        [Display(Name = "CEP")]
        [RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O código postal deverá estar no formato 00000000 ou 00000-000")]
        public string CEP { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        
        public virtual UserProfile User { get; set; }
        [ForeignKey("RewardClaimID")]
        public virtual ICollection<RewardClaim> RewardsClaimed { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
        public int TempPoints { get; set; }

        [Display(AutoGenerateField = false)]
        public int Points
        {
            get
            {
                return TempPoints + Sales.Sum(sale => sale.PointsFor(this));
            }
        }
    }

    [Table("Broker")]
    public class Broker : Employee
    {
        public BrokerType Type { get; set; }
        public int ManagerID { get; set; }

        [ForeignKey("ManagerID")]
        public virtual Manager Manager { get; set; }
    }

    public enum BrokerType
    {
        ONLINE = 1,
        PRESENCIAL = 2,
        INVALID = 0
    }

    [Table("Manager")]
    public class Manager : Employee
    {
        public virtual ICollection<Broker> Brokers { get; set; }
    }

    [Table("Coordinator")]
    public class Coordinator : Employee
    {
        public virtual ICollection<Enterprise> Enterprises { get; set; }
    }
}