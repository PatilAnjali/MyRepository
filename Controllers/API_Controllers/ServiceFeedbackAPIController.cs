using HSP_BusinessLogic;
using HSP_Models.Models;
using HSP_Repository;
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
    public class ServiceFeedbackAPIController : ControllerBase
    {
        private HSP_ServiceFeedbackBAL user_bal;
        private INonGenric gen;
        public ServiceFeedbackAPIController(HSP_ServiceFeedbackBAL userbal,INonGenric ge)
        {
            user_bal = userbal;
            gen = ge;
            // _bloodcontext = context;
        }
        [HttpGet]
        public IActionResult getalluser()
        {
            return Ok(user_bal.GetFeedbackdetails());
        }
        [HttpGet("{Spid}")]
        public IActionResult getfeedbackbyid(int Spid)
        {
            return Ok(gen.getFeedbackByserviceId(Spid));
        }
        [HttpPost]
        public IActionResult insertuserdetails(ServiceFeedback data)
        {
            return Ok(user_bal.insertfeedbackdetails(data));
        }
    }
}
