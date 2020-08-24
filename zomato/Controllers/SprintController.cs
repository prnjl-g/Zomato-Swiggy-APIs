using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Service.Database;

namespace graphql_create.Controllers
{
    [ApiController]
    [Route("api/sprint")]
    public class SprintController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
        private RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;
        private ISprintRepository _sprintRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IssueController> _logger;

        public SprintController(ILogger<IssueController> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ISprintRepository sprintRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _sprintRepository = sprintRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        //API to create a sprint.
        [HttpPost]
        [Route("createsprint")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> CreateSprint(Sprint sprintDetails)
        {
            return Ok(await _sprintRepository.CreateSprint(sprintDetails));
        }

        //API to start a sprint.
        [HttpPut]
        [Route("startsprint")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> StartSprint(int sprintId)
        {
            return Ok(await _sprintRepository.StartSprint(sprintId));
        }

        //API to stop a sprint.
        [HttpPut]
        [Route("stopsprint")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> StopSprint(int sprintId)
        {
            return Ok(await _sprintRepository.StopSprint(sprintId));
        }

        //API to add an existing issue to the sprint.
        [HttpPut]
        [Route("addissue")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> AddIssue(int sprintId, int issueId)
        {
            return Ok(await _sprintRepository.AddIssue(sprintId, issueId));
        }

        //API to add new issue to the sprint.
        [HttpPut]
        [Route("addnewissue")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> AddNewIssue(Issue issue)
        {
            return Ok(await _sprintRepository.AddNewIssue(issue));
        }

        //API to delete a sprint.
        [HttpDelete]
        [Route("deletesprint")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> DeleteSprint(int sprintId)
        {
            return Ok(await _sprintRepository.DeleteSprint(sprintId));
        }

        //API to remove a issue from the sprint.
        [HttpDelete]
        [Route("removeissue")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> RemoveIssue(int sprintId, int issueId)
        {
            return Ok(await _sprintRepository.RemoveIssue(sprintId, issueId));
        }


        //API to filter sprint on the basis of any zero level entity.
        [HttpGet]
        [Route("filtersprint")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin)]
        public async Task<IActionResult> Filter(DateTime sprintStartDate, DateTime sprintEndTime, int projectId = 0, string sprintStatus = null)
        {
             return Ok(await _sprintRepository.FilterSprint(projectId, sprintStartDate, sprintEndTime, sprintStatus));
        }

        
    }
}