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
    [Route("api/label")]
    public class LabelController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
        private RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;
        private ILabelRepository _labelRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IssueController> _logger;

        public LabelController(ILogger<IssueController> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ILabelRepository labelRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _labelRepository = labelRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        //API to add new label.
        [HttpPost]
        [Route("addlabel")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AddLabel(Label label)
        {
            return Ok(await _labelRepository.AddLabel(label));
        }


        //API to delete a label.
        [HttpDelete]
        [Route("deletelabel")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteLabel(int labelId, int issueId)
        {
            return Ok(await _labelRepository.DeleteLabel(labelId, issueId));
        }

        //API to filter label on the basis of any zero level entity.
        [HttpGet]
        [Route("filterlabel")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> FilterLabel(int issueId = 0, string label = null)
        {
            return Ok(await _labelRepository.FilterLabel(issueId, label));
        }
                
    }
}