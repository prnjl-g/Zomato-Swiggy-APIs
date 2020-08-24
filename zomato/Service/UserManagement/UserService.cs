using System.Linq;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Service.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

public class TokeResult {
    public string token{get;set;}
    public string id {get;set;}
    public string errorMessage {get;set;}
}
public interface IUserService
    {
        Task<TokeResult> Authenticate(ApplicationUser user);
        IEnumerable GetAllUser();
    }
    public class UserService : IUserService
    {
        
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;
    private ILogger<UserService> _logger;

    public UserService(ILogger<UserService> logger,
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<TokeResult> Authenticate(ApplicationUser user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            //_logger.LogInformation(JsonConvert.SerializeObject(user));
            if(!result.Succeeded){
                return (new TokeResult{
                token = "", errorMessage = "Invalid User"
            }); 
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
            user = _userManager.Users.FirstOrDefault(f=> f.UserName == user.UserName);
            var associatedRole = await _userManager.GetRolesAsync(user);
            _logger.LogInformation("data" + JsonConvert.SerializeObject(associatedRole));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, string.Join(",", associatedRole.ToList())),  
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return (new TokeResult{
                id = user.Id,
                token = tokenString
            });
        }

    public IEnumerable GetAllUser()
    {
        return _signInManager.UserManager.Users.ToList();
    }
}