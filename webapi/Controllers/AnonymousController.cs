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
using Microsoft.Extensions.Logging;

namespace Web.API.Controllers
{
    [ApiVersion("1.0")]    
    [Route("/")]    
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly ILogger _logger;

        public AnonymousController(ShopContext context, ILogger<AnonymousController> logger) {
            _context = context;
            _context.Database.EnsureCreated();
            _logger = logger;
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous()
        {
            //_logger.Log<string>(LogLevel.Information, 1000, "GET Anonymo called", null, (state, msg) => state);
            _logger.LogInformation("Info: GET ANONYMOUS CALLED");
            _logger.LogWarning("Warn: From Anonymous");
            _logger.LogTrace("Trace: From Anonymous");
           return  "Anonymous Get called";
        }

    }

   
}