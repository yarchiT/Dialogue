using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dialogue.Models;
using Dialogue.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dialogue.Services
{
    public class ServiceConnector
    {
        static HttpClient client = new HttpClient();

        public static async Task<IActionResult> Login(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new {userName = username, passwordString = password});
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/users/login", stringContent);// URRIIIII
            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            else
            {
                return new NotFoundResult();
            }
            return new BadRequestResult();
        }

        public static async Task<IActionResult> Register(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new { userName = username, passwordString = password });
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/users", stringContent);// URRIIIII
            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            return new BadRequestResult();
        }

        public static async Task<IActionResult> AddMessage(string username, MessageDto message)
        {
            var json = JsonConvert.SerializeObject(new { userName = username, messageDto = message});
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/messages/" + username, stringContent);// URRIIIII

            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            return new BadRequestResult();
        }

        public static async Task<List<Message>> GetChatHistory(string username)
        {
            var json = JsonConvert.SerializeObject(new { userName = username });
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/messages/" + username + "/all", stringContent);// URRIIIII
            List<Message> chat = new List<Message>();
            if (response.IsSuccessStatusCode)
            {
                var chatHistory = await response.Content.ReadAsAsync<List<Message>>();
                chat.AddRange(chatHistory);
            }
            return chat;
        }

    }
}
