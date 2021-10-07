using HSP_BusinessLogic;
using HSP_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidedAPIController : ControllerBase
    {
        private HSP_ServicesProvidedBAL ServiceProvider_BAL;
        private HomeServiceProviderContext dbcon;
        public ServiceProvidedAPIController(HSP_ServicesProvidedBAL SPbal, HomeServiceProviderContext dbco)
        {
            ServiceProvider_BAL = SPbal;
            dbcon = dbco;
        }
        [HttpGet]
        public IActionResult getalluser()
        {
            return Ok(ServiceProvider_BAL.GetSPdetails());

        }

        [HttpGet("{Spid}")]
        public IActionResult getuserbyid(int Spid)
        {
            return Ok(ServiceProvider_BAL.GetSPdetail(Spid));
        }
        [HttpPost]
        public IActionResult insertuserdetails(ServicesProvided data)
        {
            return Ok(ServiceProvider_BAL.AddServicesProvided(data));
        }
        [HttpPut]
        public IActionResult updateuserdetails(ServicesProvided data)
        {
            return Ok(ServiceProvider_BAL.UpdateServiceProvider(data));
        }


        [HttpGet("Servicetype")]
        public IActionResult getspname(string Servicetype)
        {
            IEnumerable<ServicesProvided> spid = null;
            spid = dbcon.ServicesProvideds.Where(v => v.Servicetype == Servicetype).OrderByDescending(p => p.ServiceId).ToList();
            return Ok(spid);
        }

    }
}
