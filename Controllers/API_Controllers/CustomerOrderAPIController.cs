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
    public class CustomerOrderAPIController : ControllerBase
    {
        private HSP_CustomerOrderBAL custorder_bal;
        private INonGenric nongen;
        public CustomerOrderAPIController(HSP_CustomerOrderBAL userbal, INonGenric gen)
        {
            custorder_bal = userbal;
            nongen = gen;
        }

        [HttpGet]
        public IActionResult getallServiceProvider()
        {
            return Ok(custorder_bal.GetCustomerOrder());

        }
        [HttpGet("{UID}")]
        public IActionResult getuserbyid(int UID)
        {
            return Ok(nongen.getOrderDetailById(UID));
        }

        [HttpPost]
        public IActionResult InsertCustomerOrder(CustomerOrder data)
        {
            return Ok(custorder_bal.AddCustomerOrder(data));
        }
        [HttpPut]
        public IActionResult updateCustomerOrder(CustomerOrder data)
        {
            return Ok(custorder_bal.UpdateCustomerOrder(data));
        }

        /*[HttpGet("{UID}")]
        public IActionResult ShowOrderStatusToCustomerById(int UID)
        {
            return Ok(custorder_bal.ShowOrderStatusToCustomer(UID));
        }*/


    }
}
