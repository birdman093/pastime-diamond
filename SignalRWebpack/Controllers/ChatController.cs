using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRWebpack.Controllers
{
    public class ChatController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly int chatId = 1111111;

        public ChatController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("chat/{chatId:int}")]
        public async Task Chat(int chatId)
        {
            if (chatId == this.chatId)
            {
                Response.ContentType = "text/html";
                await Response.SendFileAsync(Path.Combine(_env.WebRootPath, "chat.html"));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        
    }
}

