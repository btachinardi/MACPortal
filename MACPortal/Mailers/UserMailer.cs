using System.Collections.Generic;
using Mvc.Mailer;

namespace MACPortal.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer
	{
		public UserMailer()
		{
            MasterName = "_Layout";
		}
		
		public virtual MvcMailMessage Welcome()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Welcome";
				x.ViewName = "Welcome";
				x.To.Add("some-email@example.com");
			});
		}

        public MvcMailMessage Contact(string name, string cpf, string email, string title, string message, string logoPath)
        {
            ViewBag.Name = name;
            ViewBag.CPF = cpf;
            ViewBag.Email = email;
            ViewBag.Title = title;
            ViewBag.Message = message;
            var mailMessage = Populate(x =>
            {
                x.Subject = "Portal MAC Contato - " + title;
                x.ViewName = "Contact";
                x.To.Add("meritomac@mac.com.br");
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(mailMessage, "Contact", resources);
            return mailMessage;
        }

        public virtual MvcMailMessage ForgotPassword(string email, string recoverPasswordKey, int userId, string logoPath)
		{
            ViewBag.RecoverPasswordKey = recoverPasswordKey;
            ViewBag.UserID = userId;
            var message = Populate(x =>
			{
				x.Subject = "Esqueci Minha Senha";
                x.ViewName = "ForgotPassword";
                x.To.Add(email);
            }); 
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "ForgotPassword", resources);
		    return message;
		}
 	}
}