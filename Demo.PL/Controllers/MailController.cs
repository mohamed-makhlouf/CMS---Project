using Demo.BL.Helper;
using Demo.BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;


namespace Demo.PL.Controllers
{


    [Authorize]
    public class MailController : Controller
    {
        public IActionResult SendMail()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SendMail(MailVM mail)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    //TempData["Msg"] = MailSender.SendEmail(mail);
                    return RedirectToAction("SendMail");

                }

                //ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Faild";
                //ModelState.Clear();
                return View();
            }
        }
    }
}
