using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Classes;
using Web.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.API.Services;

namespace Web.API.Controllers
{
    [ApiVersion("1.0")]    
    [Route("account")]    
    [ApiController]
    public class UserLogin : ControllerBase
    {
        private readonly ShopContext _context;

        public UserLogin(ShopContext context) 
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

     
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] User user)
        {

            //return Ok(new {user = user, token = "hello"});            
            
            var loggedUsr = await _context.Users.FirstAsync<User>(x=>(x.Email.ToLower()==user.Email.ToLower() && x.Password==user.Password));
            if(loggedUsr == null) return NotFound( new {message = "Usuário inexistente ou senha inválida!"});
            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return Ok(new {
                user = user,
                token = token
            });
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Usuário autenticado: {0}",User.Identity.Name);
       
    }


}