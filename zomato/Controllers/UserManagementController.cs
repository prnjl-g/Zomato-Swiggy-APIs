using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace graphql_create.Controllers
{
    [ApiController]
    [Route("api/usermanagement")]
    public class UserManagementController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
        private RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;
        private readonly ILogger<UserManagementController> _logger;

        public UserManagementController(ILogger<UserManagementController> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        [HttpPost]
        [Route("createuser")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> createUser([FromBody] ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user, user.Password);
            var xx = user.Role;
            _logger.LogInformation(JsonConvert.SerializeObject(xx));
            var roleResult = await _userManager.AddToRolesAsync(user, user.Role);
            if (roleResult.Succeeded) {
                return Ok();
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("createrole")]
        [Authorize(Roles= Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> createRole([FromBody] IdentityRole role)
        {
           _logger.LogInformation(JsonConvert.SerializeObject(role));
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("assignRole")]
        [Authorize(Roles = Role.ProjectManager)]
        public async Task<IActionResult> assignRole([FromBody] ApplicationUser user)
        {
             return Ok(await Task.FromResult("Ok"));
        }
        
        [HttpGet]
        [Route("getAllUser")]
        [Authorize(Roles = Role.Admin + "," + Role.ProjectManager)]
        public async Task<IActionResult> getAllUsers()
        {
            return Ok( _userService.GetAllUser());
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> login([FromBody] ApplicationUser user)
        {
           return Ok(await _userService.Authenticate(user));
        }
    }
}
