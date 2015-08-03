using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeyahatIstanbul.Models;
using System.Net.Mail;

namespace SeyahatIstanbul.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        SeyahatIstanbulEntities dm;

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CustomerLogin cus)
        {
            dm = new SeyahatIstanbulEntities();
            int durum = -2;
            try
            {
                Customer find = (from c in dm.Customer
                                 where c.chEmail == cus.chEmail
                                   && c.chPassword == cus.chPassword
                                 select c).FirstOrDefault();
                if (find != null)
                {
                    Session["kId"] = find.sqCustomerId;
                    Session["kAdSoyad"] = find.chName + " " + find.chSurname;
                    durum = 1;
                    return Json(durum, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    durum = 0;
                    return Json(durum, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json(durum, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Login2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login2(CustomerLogin cus)
        {
            dm = new SeyahatIstanbulEntities();
            int durum = -2;
            try
            {
                Customer find = (from c in dm.Customer
                                 where c.chEmail == cus.chEmail
                                   && c.chPassword == cus.chPassword
                                 select c).FirstOrDefault();
                if (find != null)
                {
                    Session["kId"] = find.sqCustomerId;
                    Session["kAdSoyad"] = find.chName + " " + find.chSurname;
                    durum = 1;
                    return Json(durum, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    durum = 0;
                    return Json(durum, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json(durum, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer cus)
        {
            int durum = -2;
            //return Json(durum, JsonRequestBehavior.AllowGet);
            if (ModelState.IsValid)
            {
                try
                {
                    dm = new SeyahatIstanbulEntities();

                    var find = (from c in dm.Customer
                                where c.chEmail == cus.chEmail
                                select c).ToList();
                    if (find.Count != 0)
                    {
                        durum = -1;
                        return Json(durum, JsonRequestBehavior.AllowGet);
                    }
                    cus.cdCreator = "İnternet";
                    cus.dtCreated = DateTime.Now;
                    dm.Customer.Add(cus);
                    dm.SaveChanges();
                    durum = 1;
                    Session["kId"] = (from c in dm.Customer
                                      where c.chEmail == cus.chEmail
                                      select new { id = c.sqCustomerId }).FirstOrDefault().ToString();
                    Session["kAdSoyad"] = cus.chName + " " + cus.chSurname;
                }
                catch (Exception)
                {

                    durum = -2;
                }

                return Json(durum, JsonRequestBehavior.AllowGet);
            }
            return Json(durum, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(CustomerLogin cus)
        {
            int durum = 0;
            try
            {
                dm = new SeyahatIstanbulEntities();

                Customer customer = (from c in dm.Customer
                                 where c.chEmail == cus.chEmail
                                 select c).FirstOrDefault();
                if (customer !=null)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(cus.chEmail);
                    mail.From = new MailAddress("info@seyahatistanbul.com");
                    mail.Subject = "Şifre Hatırlatma";
                    //string Body = @"<div> Sayın "+customer.chName+" "+customer.chSurname+", </br></br>"+ "Şifreniz : " +customer.chPassword +"</div>";
                    string Body = @"<!DOCTYPE html>
                                    <html>
                                    <head>
                                        <title></title>
                                        <meta http-equiv=content-type content=text/html;charset =iso-8859-9>
                                        <meta http-equiv=content-type content=text/html;charset =windows-1254>
                                    </head>"
                                    +"<body style=\"padding: 40px;\">"
                                        +"<div style=\"color: #494848; padding: 20px; font-size: 16px; font-family: Arial, Helvetica, sans-serif; width: 600px; height: 450px; border: #000000 solid 2px; border-radius: 4px; background-image: linear-gradient(to right, #c4baba 0%, #f7f9fb 100%);\">"
                                            +"<div style=\"height: 80px; width: 180px; margin-left:auto;margin-right:auto; border-radius:4px; background-repeat:no-repeat; background-image: url('ftp://seyahatistanbul.com/www/Content/img/logo/logo.fw.png')\"></div>"

                                            +"<div style=\" margin-left:-20px; font-family: Trebuchet MS; font-size: 18px; font-weight: bold; line-height: 40px; padding-left: 20px; border-radius: 4px; background-image: linear-gradient(to right, #2D5A84 0%, #5F9FD8 100%); width: 500px; height: 40px; color: #ffffff; \">Şifre Hatırlatma Talebi</div>"

                                            +"<p>Sayın "+customer.chName+ " " +customer.chSurname+" ,</p>"

                                            +"<p>"
                                                +"Şifreniz : "+customer.chPassword
                                            +"</p>"

                                            +"<div style=\"color:#808080\">"
                                                +"<p>"
                                                    +"Bu e-posta seyahatistanbul.com üzerinden şifre yardımı servisi tarafından gönderilmektedir."
                                                +"</p>"
                                                +"<p> Seyahat İstanbu'u tercih ettiğiniz için teşekkür ederiz.</p>"
                                                +"<p>"
                                                    +"<a style=\"color:#000000;\" href=\"http://www.seyahatistanbul.com/\"> www.seyahatistanbul.com</a>"
                                                +@"</p>
                                            </div>
                                        </div>
                                    </body>
                                    </html>";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail.seyahatistanbul.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("info@seyahatistanbul.com", "seyahatIstanbul34");
                    smtp.EnableSsl = false;
                    smtp.Send(mail);

                    return Json(durum, JsonRequestBehavior.AllowGet);

                }
                
            }
            catch (Exception ex)
            {

                durum = -2;
            }
            return Json(durum, JsonRequestBehavior.AllowGet);
        }

    }
}
