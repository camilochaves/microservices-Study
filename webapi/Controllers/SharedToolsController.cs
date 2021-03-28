using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTools;
using static System.Console;
using System.Net;

namespace Web.API.Controllers
{
    public class Msg
    {
        public string Message { get; set; }
    }


    [ApiVersion("1.0")]    
    [Route("QRCode")]    
    [ApiController]
     public class SharedToolsController: ControllerBase
     {

        [HttpPost("CreateFromHeader")]        
        [AllowAnonymous]
        public IActionResult Post([FromHeader(Name = "Mensagem")] string msg)
        {            
            var image = QrCodeGenerator.GenerateQRCodeAsByteArray(msg);
            return File(image, "image/jpeg");
        }

        [HttpPost("CreateFromBody")]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Msg msg)
        {
            //var re = Response;
            //Response.Headers.Add("X-DEVELOPED-BY", "Camilo Chaves");
            var image = QrCodeGenerator.GenerateQRCodeAsByteArray(msg.Message);
            return File(image, "image/jpeg");
        }

    }
}