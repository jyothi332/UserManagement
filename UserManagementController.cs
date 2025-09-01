
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.Application_Layer;
using EmployeeModelPackage;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManagementIL _usermanagementService;

        public UserManagementController()
        {
            _usermanagementService = new UserManagementAL();
        }

        
        [HttpGet("GetRegistrationDetails/{UserName}/{Password}")]
        public IActionResult GetRegistrationDetails(string UserName, string Password)
        {
            var response = _usermanagementService.GetRegistrationDetails(UserName , Password);
            return Ok(response);
        }

        
        [HttpPost("RegisterEmployee")]
        public IActionResult RegisterEmployee([FromBody] RegisterModel model)
        { 

            var result = _usermanagementService.SaveEmployee(model);
            return new JsonResult(result);
        }

        [HttpGet("IsUserExists/{UserName}")]
        public IActionResult IsUserExists(string UserName)
        {
            var response = _usermanagementService.IsUserExists(UserName);
            return Ok(response);
        }


    }
}
