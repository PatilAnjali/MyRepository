using HomeServiceProvieders_MVC_UI.Models;
using HSP_Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HomeServiceProviderContext HSPcontext ;
        public HomeController(ILogger<HomeController> logger,HomeServiceProviderContext hsp)
        {
            _logger = logger;
            HSPcontext = hsp;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminMenuLandingPage()
        {
            return View();
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

        
        public IActionResult ShowAll()
        {

            return View();
        }
        public IActionResult Showservicemenu()
        {
            return View();
        }

        public IActionResult LoginPage(string msg)
        {
            ViewBag.mess = msg;
            Response.Cookies.Delete("mycookie");
            return View();
        }
        [HttpPost]
        public IActionResult LoginPage(string Aname, string Password)
        {
            AdminLogin usr = HSPcontext.AdminLogins.Where(user => user.Aname == Aname && user.Password == Password).FirstOrDefault();
            //
            //AdminLogin usr = _dbcontext.AdminLogins.Where(v => v.Aname == AdminName && v.Password == PassWord).FirstOrDefault();

            //AdminLogin usr = userdb().Where(v => v.Aname == AdminName && v.Password == PassWord).FirstOrDefault();
            if (usr != null)
            {
                var token = Createtoken();
                savetoken(token);
                
                return RedirectToAction("AdminMenuLandingPage");
            }
            return View("LoginPage", "Username or Password is incorrect");
        }


        public IActionResult LoginPageForCustomer(string msg)
        {
            ViewBag.mess = msg;
            Response.Cookies.Delete("mycookie");
            return View();
        }
        [HttpPost]
        public IActionResult LoginPageForCustomer(string UserName, string Password)
        {
            Userdetail usr = HSPcontext.Userdetails.Where(user => user.UserName == UserName && user.Password == Password).FirstOrDefault();

            if (usr != null)
            {
                var token = Createtoken();
                savetoken(token);
                HttpContext.Session.SetInt32("CustomerId", usr.Uid);
                return RedirectToAction("ShowAll");
            }


            return View("LoginPageForCustomer", "Username or Password is incorrect");
        }

        public IActionResult LoginPageForServiceProvider(string msg)
        {
            ViewBag.mess = msg;
            Response.Cookies.Delete("mycookie");
            return View();
        }
        [HttpPost]
        public IActionResult LoginPageForServiceProvider(string SpuserName, string Password)
        {
            ServiceProviderDetail usr = HSPcontext.ServiceProviderDetails.Where(user => user.SpuserName == SpuserName && user.Password == Password).FirstOrDefault();

            if (usr != null)
            {
                var token = Createtoken();
                savetoken(token);
                HttpContext.Session.SetInt32("ServiceProviderId", usr.Spid);
                return RedirectToAction("Showservicemenu");
            }


            return View("LoginPageForServiceProvider", "Username or Password is incorrect");
        }

        public async Task<IActionResult> ShowAllAPI()
        {
            var Jwt = Request.Cookies["mycookie"];
            List<ServiceProviderDetail> depts = new List<ServiceProviderDetail>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt);


                using (var response = await client.GetAsync("http://localhost:3105/api/Service_Provider_API_"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string readdata = await response.Content.ReadAsStringAsync();
                        depts = JsonConvert.DeserializeObject<List<ServiceProviderDetail>>(readdata);
                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("LoginPage", new { msg = "Unauthorized user" });

                    }

                }
            }


            return View(depts);

        }






        private string Createtoken()
        {
            var Skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrst"));
            var credentials = new SigningCredentials(Skey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(

                issuer: "abc",
                audience: "abc",
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials


                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void savetoken(string token)
        {
            var cookdet = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(2),
            };
            Response.Cookies.Append("mycookie", token, cookdet);
        }


    }
}
