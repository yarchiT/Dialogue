using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
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

        public static async Task<(IActionResult,bool)> Login(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new {userName = username, passwordString = password});
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            bool serviceIsRunning = true;
            try
            {
                HttpResponseMessage response =
                    await client.PostAsync("http://localhost:58707/api/users/login", stringContent); // URRIIIII
                if (response.IsSuccessStatusCode)
                {
                    return (new OkResult(), serviceIsRunning);
                }

                return (new NotFoundResult(), serviceIsRunning);
            }
            catch (Exception e)
            {
                serviceIsRunning = false;
            }
            return (new BadRequestResult(),serviceIsRunning);
        }

        public static async Task<(IActionResult, bool)> Register(UserRegisterViewModel user)
        {
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            bool serviceIsRunning = true;
            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/users", stringContent);// URRIIIII
                if (response.IsSuccessStatusCode)
                {
                    return (new OkResult(), serviceIsRunning);
                }
                return (new BadRequestResult(), serviceIsRunning);
            }
            catch (Exception e)
            {
                serviceIsRunning = false;
                return (new BadRequestResult(), serviceIsRunning);
            }  
        }

        public static async Task<(IActionResult, bool)> AddMessage(string username, MessageDto message)
        {
            var json = JsonConvert.SerializeObject(message);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            bool serviceIsRunning = true;
            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/messages/" + username, stringContent);// URRIIIII

                if (response.IsSuccessStatusCode)
                {
                    return (new OkResult(), serviceIsRunning);
                }
                return (new BadRequestResult(), serviceIsRunning);
            }
            catch (Exception e)
            {
                serviceIsRunning = false;
                return (new BadRequestResult(), serviceIsRunning);
            }
            
        }

        public static async Task<(List<Message> chatList, bool serviceIsRunning)> GetChatHistory(string username)
        {
            var json = JsonConvert.SerializeObject(new { userName = username });
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            bool serviceIsRunning = true;
            List<Message> chat = new List<Message>();
            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:58707/api/messages/" + username + "/all",stringContent); // URRIIIII

                if (response.IsSuccessStatusCode)
                {
                    var chatHistory = await response.Content.ReadAsAsync<List<Message>>();
                    chat.AddRange(chatHistory);
                }
            }
            catch (Exception ex)
            {
                serviceIsRunning = false;
            }

            return (chat, serviceIsRunning);
        }

    }
}
