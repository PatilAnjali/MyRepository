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
    public class CustomerBillAPIController : ControllerBase
    {
        private HSP_CustomerBillBAL user_bal;
        public CustomerBillAPIController(HSP_CustomerBillBAL userbal)
        {
            user_bal = userbal;
        }
        [HttpGet]
        public IActionResult getalluser()
        {
            return Ok(user_bal.GetCustomerBill());

        }
        [HttpGet("{bill_number}")]
        public IActionResult getuserbyid(int bill_number)
        {
            return Ok(user_bal.ShowBillToCustomerById(bill_number));
        }
        [HttpPost]
        public IActionResult insertuserdetails(CustomerBill data)
        {
            return Ok(user_bal.AddCustomerBillDetails(data));
        }
    }
}
