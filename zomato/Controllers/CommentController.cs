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
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
        private RoleManager<IdentityRole> _roleManager;
        private IUserService _userService;
        private ICommentRepository _commentRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IssueController> _logger;

        public CommentController(ILogger<IssueController> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ICommentRepository commentRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        // API to filter comment on the basis of any zero level entities.
        [HttpGet]
        [Route("filtercomment")]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> FilterComment(int issueId = 0, string comment = null, string userName = null)
        {
            return Ok(await _commentRepository.FilterComment(issueId, comment, userName));
        }


        // API for comment on a issue.
        [HttpPost]
        [Route("commentonissue")]
        [Authorize (Roles= Role.User)]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> CommentOnIssue(Comment commentDetails)
        {
            commentDetails.userName = _httpContextAccessor.HttpContext?.User?.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            return Ok(await _commentRepository.CommentOnIssue(commentDetails));
        }


        //APi to update a comment.
        [HttpPut]
        [Route("updatecomment")]
        [Authorize (Roles= Role.User)]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> UpdateComment(EditComment editComment)
        {
            editComment.userName = _httpContextAccessor.HttpContext?.User?.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            return Ok(await _commentRepository.UpdateComment(editComment));
        }


        //API to delete a comment.
        [HttpDelete]
        [Route("deletecomment")]
        [Authorize (Roles = Role.User)]
        [Authorize(Roles = Role.ProjectManager + "," + Role.Admin + "," + Role.User)]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userNmae = _httpContextAccessor.HttpContext?.User?.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            return Ok(await _commentRepository.DeleteComment(commentId, userNmae));
        }

    }
}