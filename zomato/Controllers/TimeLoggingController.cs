using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Service.Database;

namespace graphql_create.Controllers
{
    [ApiController]
    [Route("api/timelog")]
    public class TimeLoggingController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
        private RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;
        private ITimeLoggingRepository _timeLoggingRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IssueController> _logger;

        public TimeLoggingController(ILogger<IssueController> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITimeLoggingRepository timeLoggingRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _timeLoggingRepository = timeLoggingRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        //API to create a log.
        [HttpPost]
        [Route("createlog")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> CreateLog(TimeLogging timeLogging)
        {
            timeLogging.logCreater = _httpContextAccessor.HttpContext?.User?.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            return Ok(await _timeLoggingRepository.CreateLog(timeLogging));
        } 

        //API to update a log.
        [HttpPut]
        [Route("updatelog")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> UpdateLog(UpdateLog logDetails)
        {
            logDetails.logUpdater = _httpContextAccessor.HttpContext?.User?.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            return Ok(await _timeLoggingRepository.UpdateLog(logDetails));
        }   

        //API to delete a log.
        [HttpDelete]
        [Route("deletelog")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> DeleteLog(int logId)
        {
            var user = _httpContextAccessor.HttpContext?.User?.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            return Ok(await _timeLoggingRepository.DeleteLog(logId, user));
        }

        //API to filter logs on the basis of any zero level entity.
        [HttpGet]
        [Route("filtertimelog")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> FilterTimeLog(string logCreater = null, long logTime = 0, int issueId = 0)
        {
            return Ok(await _timeLoggingRepository.FilterTimeLog(logCreater, logTime, issueId));
        }
    }
}