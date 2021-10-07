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
    public class ServiceProviderdetailsAPIController : ControllerBase
    {
        private HSP_ServiceProviderBAL user_bal;
        public ServiceProviderdetailsAPIController(HSP_ServiceProviderBAL userbal)
        {
            user_bal = userbal;
            // _bloodcontext = context;
        }
        [HttpGet]
        public IActionResult getallserviceprovider()
        {
            return Ok(user_bal.GetSPdetails());

        }
        [HttpGet("{UID}")]
        public IActionResult getuserbyid(int UID)
        {
            return Ok(user_bal.GetSPdetail(UID));
        }
        [HttpPost]
        public IActionResult insertuserdetails(ServiceProviderDetail data)
        {
            return Ok(user_bal.AddServiceProvider(data));
        }
        [HttpPut]
        public IActionResult updateuserdetails(ServiceProviderDetail data)
        {
            return Ok(user_bal.UpdateServiceProvider(data));
        }
    }
}
