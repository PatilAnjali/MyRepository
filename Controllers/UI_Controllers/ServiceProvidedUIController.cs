using HSP_Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI.Controllers.UI_Controllers
{
    public class ServiceProvidedUIController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/

        public IActionResult showSPdetails()
        {
            IEnumerable<ServicesProvided> usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var us = cln.GetAsync("ServiceProvidedAPI");
            us.Wait();
            var usd = us.Result;
            if (usd.IsSuccessStatusCode)
            {
                var data = usd.Content.ReadAsAsync<IEnumerable<ServicesProvided>>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }

        public IActionResult showSPbyid(int Spid)
        {
            ServicesProvided SP = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getSPbyID = cln.GetAsync("ServiceProvidedAPI/" + Spid);
            getSPbyID.Wait();
            var result = getSPbyID.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<ServicesProvided>();
                data.Wait();
                SP = data.Result;

            }
            return View(SP);
        }

        public IActionResult insertServicesProvideduser()
        {
            IEnumerable<ServicesProvided> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getuser = cln.GetAsync("ServiceProvidedAPI");
            getuser.Wait();
            var result = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<ServicesProvided>>();
                data.Wait();
                docs = data.Result;

            }
            return View();
        }
        [HttpPost]
        public IActionResult insertServicesProvideduser(ServicesProvided userdet)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var post = cln.PostAsJsonAsync<ServicesProvided>("ServiceProvidedAPI/", userdet);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showSPdetails");
            }
            return View(userdet);
        }

        public IActionResult updateServiceProvideddetails(int Spid)
        {
            //Userdetail docs = null;
            ServicesProvided userdet = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var getusr = cln.GetAsync("ServiceProvidedAPI/" + Spid);
            getusr.Wait();
            var result = getusr.Result;
            /*var getuser = cln.GetAsync("UserdetailAPI/");
            getuser.Wait();*/
            //var resuod = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<ServicesProvided>();
                data.Wait();
                userdet = data.Result;
                /*var usrdata = resuod.Content.ReadAsAsync<Userdetail>();
                usrdata.Wait();
                docs = usrdata.Result;*/

            }
            return View(userdet);
        }
        [HttpPost]
        public IActionResult updateServiceProvideddetails(ServicesProvided data)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:21738/api/");
            var putpat = cln.PutAsJsonAsync<ServicesProvided>("ServiceProvidedAPI/", data);
            putpat.Wait();
            var res = putpat.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showSPdetails");
            }
            return View(data);
        }

        public IActionResult getspbyname(string Servicetype)
        {
            IEnumerable <ServicesProvided> p = null;
            int id = 0;
            using (var c = new HttpClient())
            {
                using (var resp = c.GetAsync("http://localhost:21738/api/ServiceProvidedAPI/Servicetype?Servicetype=" + Servicetype).Result)
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var data = resp.Content.ReadAsAsync<IEnumerable<ServicesProvided>>();
                        data.Wait();
                        p = data.Result;
                       

                    }
                }
            }
            return View(p);
        }

    }
}
