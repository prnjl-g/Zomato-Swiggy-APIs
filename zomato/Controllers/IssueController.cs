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
    [Route("api/issue")]
    public class IssueController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
        private RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;
        private IIssueRepository _issueRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IssueController> _logger;

        public IssueController(ILogger<IssueController> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IIssueRepository issueRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _issueRepository = issueRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        //API to get all the issue present in database.
        [HttpGet]
        [Route("getallissues")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> GetAllIssues()
        {
             return Ok(await _issueRepository.GetAllIssues());
        }


        // API to get the details of an issue.
        [HttpGet]
        [Route("getissue")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> GetIssue(int id)
        {
             return Ok(await _issueRepository.GetIssue(id));
        }



        //API to issues under a project.
        [HttpGet]
        [Route("getissueunderproject")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> GetIssueUnderProject(int projectId)
        {
            return Ok(await _issueRepository.GetIssueUnderProject(projectId));
        }
        

        //API to get issue detail under a project.
        [HttpGet]
        [Route("getissuedetail")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> GetIssueDetail(int projectId, int issueId)
        {
            return Ok(await _issueRepository.GetIssueDetail(projectId, issueId));
        }


        // API to search an issue by Title.
        [HttpGet]
        [Route("searchissuebytitle")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> GetIssueByTitle(string title)
        {
            return Ok(await _issueRepository.SearchIssueByTitle(title));
        }


        //API to search an issue by description
        [HttpGet]
        [Route("searchissuebydescription")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> GetIssueByDescription(string description)
        {
            return Ok(await _issueRepository.SearchIssueByDescription(description));
        }


        //API to filter issue on the basis of any zero level entity.
        [HttpGet]
        [Route("filterissue")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> FilterIssue(int issueProjectId = 0, string issueType = null, string issueTitle = null, string issueDescription = null, string issueReporter = null, string issueAssignee = null, string issueStatus = null, int issueSprintId = 0 )
        {
            return Ok(await _issueRepository.FilterIssue(issueProjectId, issueType, issueTitle, issueDescription, issueReporter, issueAssignee, issueStatus, issueSprintId));
        }

        //API to create a new issue.
        [HttpPost]
        [Route("createissue")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> CreateIssue(Issue issue)
        {
            return Ok(await _issueRepository.CreateIssue(issue));
        }


        //API to update an issue.
        [HttpPut]
        [Route("updateissue")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> UpdateIssue(Issue issue)
        {
            return Ok(await _issueRepository.UpdateIssue(issue));
        }


        //API to assign an issue.
        [HttpPut]
        [Route("assignissue")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> AssignIssue(Issue issue)
        {
            return Ok(await _issueRepository.AssignIssue(issue));
        }


        //API to update the status of an issue.
        [HttpPut]
        [Route("updatestatus")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> UpdateStatus(Issue issue)
        {
            return Ok(await _issueRepository.UpdateStatus(issue));
        }

        //API to delete an issue.
        [HttpDelete]
        [Route("deleteissue")]
        [Authorize(Roles = Role.User + "," + Role.Admin+","+Role.ProjectManager)]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            return Ok(await _issueRepository.DeleteIssue(id));
        }


    }
}