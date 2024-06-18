using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ShopingCart.Models
{
    public class EmailService
    {
        public object SendVerificationEmail( string password,string email,string name)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("mauryaashu523@gmail.com");
                message.To.Add(new MailAddress("mauryaashutosh604@gmail.com"));
                message.Subject = "Welcome  to shoping cart ";
                message.Body = $"Shoping cart add new User whose email Id  <b>{email}</b> and Password is  <b>{password}</b> and His name is {name}";
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("mauryaashu523@gmail.com", "zgsc fpwt stvg jjyi"),
                    EnableSsl = true
                };

                smtp.Send(message);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
