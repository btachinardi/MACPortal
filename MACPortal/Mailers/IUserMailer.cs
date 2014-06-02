using Mvc.Mailer;

namespace MACPortal.Mailers
{ 
    public interface IUserMailer
    {
		MvcMailMessage Welcome();
        MvcMailMessage Contact(string name, string cpf, string email, string title, string message, string logoPath);
        MvcMailMessage ForgotPassword(string email, string recoverPasswordKey, int userId, string logoPath);
	}
}