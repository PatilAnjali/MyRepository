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
    public class UserdetailAPIController : ControllerBase
    {
        private HSP_UserBAL user_bal;
        public UserdetailAPIController(HSP_UserBAL userbal)
        {
            user_bal = userbal;

        }
        [HttpGet]
        public IActionResult getalluser()
        {
            return Ok(user_bal.GetUserdetails());

        }
        [HttpGet("{UID}")]
        public IActionResult getuserbyid(int UID)
        {
            return Ok(user_bal.GetUserdetail(UID));
        }
        [HttpPost]
        public IActionResult insertuserdetails(Userdetail data)
        {
            return Ok(user_bal.AddCustomer(data));
        }
        [HttpPut]
        public IActionResult updateuserdetails(Userdetail data)
        {
            return Ok(user_bal.UpdateCustomer(data));
        }
        [HttpDelete("{UID}")]
        public IActionResult deleteCustomer(int UID)
        {
            user_bal.DeleteCustomer(UID);
            return Ok("Record deleted");
        }
    }
}
