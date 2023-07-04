using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        [HttpGet("")]
        public async Task Login()
        {
            Response.ContentType = "text/html";
            await Response.SendFileAsync(Path.Combine(_env.WebRootPath, "login.html"));
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

        [HttpPost("check-login")]
        public async Task CheckLogin()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(body);

            if (data != null && data.ContainsKey("username") && data["username"] == "YES")
            {
                Response.Redirect($"/chat/{chatId}");
            }
            else
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}

