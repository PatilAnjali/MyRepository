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
    public class CustomerOrderUIController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        public IActionResult showServiceProviderdetails()
        {
            IEnumerable<CustomerOrder> usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var us = cln.GetAsync("CustomerOrderAPI");
            us.Wait();
            var usd = us.Result;
            if (usd.IsSuccessStatusCode)
            {
                var data = usd.Content.ReadAsAsync<IEnumerable<CustomerOrder>>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }

        public IActionResult showOrderbyid()
        {
            CustomerOrder usr=null;
            var cln = new HttpClient();
            var UID = HttpContext.Session.GetInt32("ServiceProviderId");
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusrID = cln.GetAsync("CustomerOrderAPI/" + UID);
            getusrID.Wait();
            var result = getusrID.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<CustomerOrder>();
                data.Wait();
                usr = data.Result;

            }
            return View(usr);

        }
        public IActionResult InsertCustomerOrder()
        {
            IEnumerable<CustomerOrder> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getuser = cln.GetAsync("CustomerOrderAPI");
            getuser.Wait();
            var result = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<CustomerOrder>>();
                data.Wait();
                docs = data.Result;

            }
            return View();
        }
        [HttpPost]
        public IActionResult InsertCustomerOrder(CustomerOrder userdet)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var post = cln.PostAsJsonAsync<CustomerOrder>("CustomerOrderAPI/", userdet);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("ThankYouAfterOrder");
            }
            return View(userdet);
        }
        public IActionResult updateCustomerOrder(int Spid)
        {
            //Userdetail docs = null;
            CustomerOrder userdet = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusr = cln.GetAsync("CustomerOrderAPI/" + Spid);
            getusr.Wait();
            var result = getusr.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<CustomerOrder>();
                data.Wait();
                userdet = data.Result;
            }
            return View(userdet);
        }
        [HttpPost]
        //for service provider
        public IActionResult updateCustomerOrder(CustomerOrder data)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var putpat = cln.PutAsJsonAsync<CustomerOrder>("CustomerOrderAPI/", data);
            putpat.Wait();
            var res = putpat.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showCustomerOrders");
            }
            return View(data);
        }
        //for customer to view their order status
        public IActionResult ShowCustomerOrderStatus(int UID)
        {
            CustomerOrder usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusrID = cln.GetAsync("CustomerOrderAPI/" + UID);
            getusrID.Wait();
            var result = getusrID.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<CustomerOrder>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }
        public IActionResult ThankYouAfterOrder()
        {
            return View();
        }
    }
}
