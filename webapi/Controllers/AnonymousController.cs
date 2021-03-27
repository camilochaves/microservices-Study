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

namespace Web.API.Controllers
{
    [ApiVersion("1.0")]    
    [Route("/")]    
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly ShopContext _context;

        public AnonymousController(ShopContext context) {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonymous Get called";
    }

   
}