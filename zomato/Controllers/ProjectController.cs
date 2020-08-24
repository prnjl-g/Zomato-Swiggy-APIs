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
    [Route("api/project")]
    public class ProjectController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
        private RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;
        private IProjectRepository _projectRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IssueController> _logger;

        public ProjectController(ILogger<IssueController> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IProjectRepository projectRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        //API to get the details of a project
        [HttpGet]
        [Route("getproject")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> GetProject(int id){
             return Ok(await _projectRepository.GetProject(id));
        }

        //API to get all the projects
        [HttpGet]
        [Route("getallproject")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> GetAllProject()
        {
            return Ok(await _projectRepository.GetAllProject());
        }

        //API to filter project on the basis of any zero level entity.
        [HttpGet]
        [Route("filterproject")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> FilterProject(string projectDescription = null, string createrOfProject = null)
        {
            return Ok(await _projectRepository.FilterProject(projectDescription, createrOfProject));
        }


        //API to create a project.
        [HttpPost]
        [Route("createproject")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> CreateProject(Project project){
            return Ok(await _projectRepository.CreateProject(project));
        }


        //API to update a project.
        [HttpPut]
        [Route("updateproject")]
        [Authorize (Roles = Role.Admin)]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            return Ok(await _projectRepository.UpdateProject(project));
        }


        //API to delete a project.
        [HttpDelete]
        [Route("deleteproject")]
        [Authorize (Roles = Role.Admin)]
        public async Task<IActionResult> DeleteProject(int id)
        {
            return Ok(await _projectRepository.DeleteProject(id));
        }
    }
}