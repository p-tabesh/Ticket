using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using Ticket.Infrastructure.Context;

namespace Ticket.Presentation.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("add-user")]
        public IActionResult AddUser([FromBody] UserModel userModel)
        {
            try
            {

                _userService.AddUser(userModel);
                return Ok("User Added Success");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(ex.Message + "Has Inner: " + ex.InnerException.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("users/{id?}")]
        public IActionResult GetUser([FromRoute]int? id)
        {
            //try
            //{
                if (id != null)
                {
                    var user = _userService.GetUser(id.Value);
                    if (user != null)
                        return Ok(user);
                    return Ok("user not found");
                }
                var users = _userService.GetUsers();
                return Ok(users);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message + " \ninner ex?" + e.InnerException);
            //}

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
