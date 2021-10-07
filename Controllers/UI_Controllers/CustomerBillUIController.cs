using HSP_Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI.Controllers.UI_Controllers
{
    public class CustomerBillUIController : Controller
    {
        public IActionResult showAllBilldetails()
        {
            IEnumerable<CustomerBill> usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var us = cln.GetAsync("CustomerBillAPI");
            us.Wait();
            var usd = us.Result;
            if (usd.IsSuccessStatusCode)
            {
                var data = usd.Content.ReadAsAsync<IEnumerable<CustomerBill>>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }
        public IActionResult ShowBillById(int UID)
        {
            CustomerBill usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusrID = cln.GetAsync("CustomerBillAPI/" + UID);
            getusrID.Wait();
            var result = getusrID.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<CustomerBill>();
                data.Wait();
                usr = data.Result;

            }
            return View(usr);

        }
        public IActionResult insertBillDetails()
        {
            IEnumerable<CustomerBill> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getuser = cln.GetAsync("CustomerBillAPI");
            getuser.Wait();
            var result = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<CustomerBill>>();
                data.Wait();
                docs = data.Result;

            }
            return View();
        }
        [HttpPost]
        public IActionResult insertBillDetails(CustomerBill userdet)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var post = cln.PostAsJsonAsync<CustomerBill>("CustomerBillAPI/", userdet);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showAllBilldetails");
            }
            return View(userdet);
        }
    }
}
