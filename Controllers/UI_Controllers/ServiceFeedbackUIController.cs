using HSP_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI.Controllers.UI_Controllers
{
    public class ServiceFeedbackUIController : Controller
    {
        public IActionResult showuserdetails()
        {
            IEnumerable<ServiceFeedback> usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var us = cln.GetAsync("ServiceFeedbackAPI");
            us.Wait();
            var usd = us.Result;
            if (usd.IsSuccessStatusCode)
            {
                var data = usd.Content.ReadAsAsync<IEnumerable<ServiceFeedback>>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }

        public IActionResult showFeedbackbyId()
        {
            IEnumerable<ServiceFeedback> usr = null;
            var cln = new HttpClient();
            var UID = HttpContext.Session.GetInt32("ServiceProviderId");
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusrID = cln.GetAsync("ServiceFeedbackAPI/" + UID);
            getusrID.Wait();
            var result = getusrID.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<ServiceFeedback>>();
                data.Wait();
                usr = data.Result;

            }
            return View(usr);
        }
        public IActionResult insertfeedbackuser()
        {
            IEnumerable<ServiceFeedback> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getuser = cln.GetAsync("ServiceFeedbackAPI");
            getuser.Wait();
            var result = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<ServiceFeedback>>();
                data.Wait();
                docs = data.Result;

            }
            return View();
        }
        [HttpPost]
        public IActionResult insertfeedbackuser(ServiceFeedback userdet)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var post = cln.PostAsJsonAsync<ServiceFeedback>("ServiceFeedbackAPI/", userdet);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("SuccessfullFeedback");
            }
            return View(userdet);
        }
        public IActionResult SuccessfullFeedback()
        {
            return View();
        }
    }
}
