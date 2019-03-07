using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Remotion.Linq.Clauses.ResultOperators;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous,HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel loginModel)
        {
            IActionResult response = Unauthorized();

            var user = Authenticate(loginModel);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(UserModel userModel)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Name),
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, userModel.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials =  new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        /// <summary>
        /// ログインで入力されたloginモデルが正しいか判断
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        private UserModel Authenticate(LoginModel loginModel)
        {
            UserModel user = null;
            if (loginModel.UserName == "mario" && loginModel.Password == "secret")
            {
                user = new UserModel(){Name = loginModel.UserName, Email = "hoge@hoge.com",Birthdate = new DateTime(2000,12,12)};
            }

            return user;
        }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}