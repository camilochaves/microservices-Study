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

     
        [HttpPost("[action]")]        
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] User user)
        {
            if (user==null) { return NotFound(new {message = "Request must have a user body"}); }
            User loggedUsr = await _context.Users.FirstOrDefaultAsync<User>(x=>x.Email.ToLower()==user.Email.ToLower());            
            if(loggedUsr == null) return NotFound( new {message = "Usuário inexistente!"});
            if (loggedUsr.Password != user.Password) return BadRequest(new {message = "Senha inválida!"});


            var token = TokenService.GenerateToken(loggedUsr);
            loggedUsr.Password = "";
            return Ok(new {
                user = loggedUsr,
                token = token
            });
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Usuário autenticado: {0}",User.Identity.Name);
       
    }


}