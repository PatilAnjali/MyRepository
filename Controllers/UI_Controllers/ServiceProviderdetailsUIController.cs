using HSP_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI.Controllers.UI_Controllers
{
    public class ServiceProviderdetailsUIController : Controller
    {
        /* public IActionResult Index()
         {
             return View();
         }*/

        public IActionResult showSPdetails()
        {
            IEnumerable<ServiceProviderDetail> usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var us = cln.GetAsync("ServiceProviderdetailsAPI");
            us.Wait();
            var usd = us.Result;
            if (usd.IsSuccessStatusCode)
            {
                var data = usd.Content.ReadAsAsync<IEnumerable<ServiceProviderDetail>>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }//to display all service provider detils

        public IActionResult showSPbyid()
        {
            ServiceProviderDetail usr = null;
            var cln = new HttpClient();
            var UID = HttpContext.Session.GetInt32("ServiceProviderId");
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusrID = cln.GetAsync("ServiceProviderdetailsAPI/" + UID);
            getusrID.Wait();
            var result = getusrID.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<ServiceProviderDetail>();
                data.Wait();
                usr = data.Result;

            }
            return View(usr);

        }//to display all service provider detils by ID
        public IActionResult insertServiceProvider()
        {
            IEnumerable<ServiceProviderDetail> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getuser = cln.GetAsync("ServiceProviderdetailsAPI");
            getuser.Wait();
            var result = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<ServiceProviderDetail>>();
                data.Wait();
                docs = data.Result;

            }
            return View();
        }//to insert all service provider detils
        [HttpPost]
        public IActionResult insertServiceProvider(ServiceProviderDetail userdet)
        {
            send_email(userdet.SpuserName, userdet.Spemail);
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var post = cln.PostAsJsonAsync<ServiceProviderDetail>("ServiceProviderdetailsAPI/", userdet);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("LoginPageForServiceProvider", "Home");
            }
            return View(userdet);
        }

        public void send_email(string name, string email)
        {
            var senderEmail = new MailAddress("mahesh.official.1818@gmail.com", "Home service provider");
            var receiverEmail = new MailAddress(email, "Receiver");
            var password = "Forgotprevious18@";
            var sub = "Welcome";
            var body = ("Welcome " + name);
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }

        }
        public IActionResult updateserviceproviderdetails(int UID)
        {
            //Userdetail docs = null;
            ServiceProviderDetail userdet = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusr = cln.GetAsync("ServiceProviderdetailsAPI/" + UID);
            getusr.Wait();
            var result = getusr.Result;
            /*var getuser = cln.GetAsync("UserdetailAPI/");
            getuser.Wait();*/
            //var resuod = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<ServiceProviderDetail>();
                data.Wait();
                userdet = data.Result;
                /*var usrdata = resuod.Content.ReadAsAsync<Userdetail>();
                usrdata.Wait();
                docs = usrdata.Result;*/

            }
            return View(userdet);
        }
        [HttpPost]
        public IActionResult updateserviceproviderdetails(ServiceProviderDetail data)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var putpat = cln.PutAsJsonAsync<ServiceProviderDetail>("ServiceProviderdetailsAPI/", data);
            putpat.Wait();
            var res = putpat.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showSPbyid", "ServiceProviderdetailsUI");
            }
            return View(data);
        }
    }
}
