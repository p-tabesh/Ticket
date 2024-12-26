using Microsoft.AspNetCore.Mvc;

namespace Ticket.Presentation.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {

        [HttpPost]
        [Route("add-user")]
        public IActionResult AddUser()
        {
            return Ok();
        }


        [HttpPost]
        [Route("change-username")]
        public IActionResult ChangeUsername()
        {
            return Ok();
        }


        [HttpPost]
        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return Ok();
        }
    }
}
