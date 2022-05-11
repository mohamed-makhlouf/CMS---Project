using Demo.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Helper
{
    public static class MailSender
    {
        public static string SendEmail(string Title , string Message)
        {
            try
            {

                var smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential("elgendya160@gmail.com", "@@@AAA321_321");

                smtp.Send("elgendya160@gmail.com", "elgendya160@gmail.com", Title, Message);

                return "Succeed";

            }
            catch (Exception ex)
            {
                return "Faild";
            }
        }
    }
}
