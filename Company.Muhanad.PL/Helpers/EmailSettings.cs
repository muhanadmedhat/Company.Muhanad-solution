using Company.Muhanad.DAL.Models;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Net;
using System.Net.Mail;

namespace Company.Muhanad.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email mail)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);

			client.EnableSsl = true;

			client.Credentials = new NetworkCredential("muhanadronaldo@gmail.com", "vbygyxeigxhgvjhf");

			client.Send("muhanadronaldo@gmail.com", mail.To, mail.Subject, mail.Body);
				//vbygyxeigxhgvjhf
		}
	}
}
