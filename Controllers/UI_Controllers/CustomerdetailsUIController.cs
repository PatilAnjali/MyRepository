using HomeServiceProvieders_MVC_UI.Models;
using HSP_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI.Controllers.UI_Controllers
{
    public class CustomerdetailsUIController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        public IActionResult showuserdetails()
        {
            IEnumerable<Userdetail> usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var us = cln.GetAsync("UserdetailAPI");
            us.Wait();
            var usd = us.Result;
            if(usd.IsSuccessStatusCode)
            {
                var data = usd.Content.ReadAsAsync<IEnumerable<Userdetail>>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }//MVC to show all the customer details in the table format

        public IActionResult showuserbyid()
        {
            Userdetail usr = null;
            var cln = new HttpClient();
            var UID = HttpContext.Session.GetInt32("CustomerId");
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusrID = cln.GetAsync("UserdetailAPI/" + UID);
            getusrID.Wait();
            var result = getusrID.Result;
            if(result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<Userdetail>();
                data.Wait();
                usr = data.Result;

            }
            return View(usr);

        }////MVC to show the customer details by ID
        public IActionResult insertuser()
        {
            IEnumerable<Userdetail> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getuser = cln.GetAsync("UserdetailsAPI");
            getuser.Wait();
            var result = getuser.Result;
            if(result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<Userdetail>>();
                data.Wait();
                docs = data.Result;
               
            }
            return View();
        }//MVC to insert the customer details in the table format
        [HttpPost]
        public IActionResult insertuser(Userdetail userdet)
        {
            send_email(userdet.UserName, userdet.EmailId);// to get the details from the user and pass it on to the send_email method
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var post = cln.PostAsJsonAsync<Userdetail>("UserdetailAPI/", userdet);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("LoginPageForCustomer","Home");
            }
            return View(userdet);
        }//MVC to insert the customer details in the table format

        public void send_email(string name, string email)
        {
            var senderEmail = new MailAddress("mahesh.official.1818@gmail.com", "Home service provider");
            var receiverEmail = new MailAddress(email, "Receiver");
            var password = "Forgotprevious18@";
            var sub = "Welcome";
            var body = ("Welcome " + name+" to Home Service Provider");
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

        }//MVC to insert the customer details in the table format
        public IActionResult updateuserdetails()
        {
            //Userdetail docs = null;
            Userdetail userdet = null;
            var cln = new HttpClient();
            var UID = HttpContext.Session.GetInt32("CustomerId");
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusr = cln.GetAsync("UserdetailAPI/" + UID);
            getusr.Wait();
            var result = getusr.Result;
            /*var getuser = cln.GetAsync("UserdetailAPI/");
            getuser.Wait();*/
            //var resuod = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<Userdetail>();
                data.Wait();
                userdet = data.Result;
                /*var usrdata = resuod.Content.ReadAsAsync<Userdetail>();
                usrdata.Wait();
                docs = usrdata.Result;*/

            }
            return View(userdet);
        }//MVC to update the customer details 
        [HttpPost]
        public IActionResult updateuserdetails(Userdetail data)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var putpat = cln.PutAsJsonAsync<Userdetail>("UserdetailAPI/", data);
            putpat.Wait();
            var res = putpat.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showuserbyid", "CustomerdetailsUI");
            }
            return View(data);
        }//MVC to update the customer details 


        public IActionResult deleteuserdetailsbyid(Userdetail userdetail)
        {
            Userdetail userdet = null;
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:21738/api/")
            };
            var responseMessage = httpClient.GetAsync("UserdetailAPI/" + userdetail.Uid);
            responseMessage.Wait();
            var resoponse = responseMessage.Result;
            if (resoponse.IsSuccessStatusCode)
            {
                var details = resoponse.Content.ReadAsAsync<Userdetail>();
                details.Wait();
                userdet = details.Result;

            }
            return View(userdet);

        }//MVC to delete the customer details 
        [HttpPost]
        public IActionResult deleteuserdetailsbyid(int UID)//MVC to delete the customer details 
        {
            Userdetail userdetail = null;
            var httpClient = new HttpClient
            { BaseAddress=new Uri("http://localhost:21738/api/")};
            var responseMessage = httpClient.DeleteAsync("UserdetailAPI/" + UID);
            responseMessage.Wait();
            var response = responseMessage.Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("showuserdetails");
            }
            return View(userdetail);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
