using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using MACPortal.Filters;
using MACPortal.Models.Users;
using MACPortal.Models.Validation;

namespace MACPortal.ViewModel
{
    public enum EditUserMode
    {
        COMPLETE,
        INFO,
        PASSWORD
    }

    public class UserVM
    {
        public UserVM Start(Employee employee, EditUserMode mode = EditUserMode.COMPLETE, string submitButtonText = "Registrar")
        {
            Mode = mode;
            OldPassword = employee.CPF;
            UserID = employee.UserID;
            Name = employee.Name;
            CPF = employee.CPF;
            Email = employee.Email;
            Birthday = employee.Birthday == null ? "" : employee.Birthday.Value.Date.ToString("yyyy-MM-dd");
            //Birthday = employee.Birthday;
            Gender = employee.Gender ?? Models.Users.Gender.Masculino;
            ComercialName = employee.ComercialName;
            HomePhone = employee.HomePhone;
            CellPhone = employee.CellPhone;
            CEP = employee.CEP;
            Street = employee.Street;
            Number = employee.Number;
            Complement = employee.Complement;
            Neighborhood = employee.Neighborhood;
            City = employee.City;
            State = employee.State;
            SubmitButtonText = submitButtonText;

            return this;
        }

        public void Finish(Employee employee, bool updateAgreement = true)
        {
            if (updateAgreement)
            {
                employee.CurrentAcceptedAgreement = UserAgreement.CurrentVersion;
            }
            employee.AcceptEmail = AcceptEmail;
            employee.AcceptSMS = AcceptSMS;
            employee.Name = Name;
            employee.Email = Email;
            employee.Birthday = String.IsNullOrEmpty(Birthday) ? employee.Birthday : DateTime.Parse(Birthday);
            employee.Gender = Gender;
            employee.HomePhone = HomePhone;
            employee.CellPhone = CellPhone;
            employee.CEP = CEP;
            employee.Street = Street;
            employee.Number = Number;
            employee.Complement = Complement;
            employee.Neighborhood = Neighborhood;
            employee.City = City;
            employee.State = State;
        }

        public EditUserMode Mode { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "O campo 'Nova Senha' é obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve conter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "A senha de confirmação e a nova senha não são iguais.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo 'CPF' é obrigatório.")]
        [Display(Name = "CPF")]
        [CPF(ErrorMessage = "Número de CPF Inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é obrigatório.")]
        [Display(Name = "Email")]
        [Email(ErrorMessage = "Endereço de Email inválido")]
        public string Email { get; set; }

        //Personal Info
        [Required(ErrorMessage = "O campo 'Data de Nascimento' é obrigatório.")]
        [Display(Name = "Data de Nascimento")]
        [DateRange("01/01/1800", "31/12/2020")]
        public string Birthday { get; set; }


        [Required(ErrorMessage = "O campo 'Sexo' é obrigatório.")]
        [Display(Name = "Sexo")]
        public Gender? Gender { get; set; }


        [Required(ErrorMessage = "O campo 'Nome Comercial' é obrigatório.")]
        [Display(Name = "Nome Comercial")]
        public string ComercialName { get; set; }

        //Phones
        [Required(ErrorMessage = "O campo 'Tel Residencial' é obrigatório.")]
        [Display(Name = "Tel Residencial")]
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "O campo 'Tel Celular' é obrigatório.")]
        [Display(Name = "Tel Celular")]
        public string CellPhone { get; set; }

        //Location
        [Required(ErrorMessage = "O campo 'CEP' é obrigatório.")]
        [Display(Name = "CEP")]
        [RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O código postal deverá estar no formato 00000000 ou 00000-000")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo 'Rua' é obrigatório.")]
        [Display(Name = "Rua")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo 'Número' é obrigatório.")]
        [Display(Name = "Número")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O campo 'Complemento' é obrigatório.")]
        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo 'Bairro' é obrigatório.")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório.")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo 'Estado' é obrigatório.")]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Display(Name = "Desejo receber informações via SMS sobre a Campanha")]
        public string AcceptSMS { get; set; }

        [Display(Name = "Desejo receber informações via E-mail sobre a Campanha")]
        public string AcceptEmail { get; set; }

        public string SubmitButtonText { get; set; }
    }
}