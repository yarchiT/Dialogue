using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dialogue.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dialogue.Services
{
    public class ServiceConnector
    {
        static HttpClient client = new HttpClient();

        public static async Task<List<ChatRow>> GetChatHistoryAsync(string path)
        {
            List<ChatRow> chat = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                chat = await response.Content.ReadAsAsync<List<ChatRow>>();
            }
            return chat;
        }

        public static async Task<IActionResult> Login(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new {userName = username, passwordString = password});
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/users/login", stringContent);// URRIIIII
            if (response.IsSuccessStatusCode)
            {
                return new OkResult();//await response.Content.ReadAsAsync<IActionResult>();
            }
            else{
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
                return new OkResult();//await response.Content.ReadAsAsync<ActionResult>();
            }
            return new BadRequestResult();
        }

        
    }
}
