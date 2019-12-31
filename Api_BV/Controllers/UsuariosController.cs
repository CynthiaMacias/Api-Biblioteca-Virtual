using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_BV.Models;
using Api_BV.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Api_BV.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : Controller
    {
           
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IConfiguration _configuration;
            private readonly ApplicationDbContext _context;
         
        public UsuariosController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                ApplicationDbContext contexto)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                this._configuration = configuration;
                _context = contexto;
            }
        [Route("List")]
        [HttpGet]
        public IActionResult List()
        {
            var users = _userManager.Users;
            return Ok(users);
        }
        [Route("Create")]
            [HttpPost]
            public async Task<IActionResult> CreateUser([FromBody] Usuarios model)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return BuildToken(model);
                    }
                    else
                    {
                        return BadRequest("Username or password invalid");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }


        [HttpPost]
            [Route("Login")]
            public async Task<IActionResult> Login([FromBody] Usuarios userInfo)
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return BuildToken(userInfo);
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Invalid login attempt.");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

        private IActionResult BuildToken(Usuarios userInfo)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("asdfghjklqwertyuiopldfhhfhfghdsfasfsdgdfgfdg", "Lo que yo quiera"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddHours(1);

                JwtSecurityToken token = new JwtSecurityToken(
                   issuer: "yourdomain.com",
                   audience: "yourdomain.com",
                   claims: claims,
                   expires: expiration,
                   signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = expiration
                });

            }
      
       
    }
    
}