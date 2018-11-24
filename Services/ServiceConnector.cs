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

        public static async Task<ActionResult> Login(string username, string password)
        {
            ActionResult res = null;
            var json = JsonConvert.SerializeObject(new {username = username, password = password});
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("",stringContent);// URRIIIII
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsAsync<ActionResult>();
            }
            return res;
        }

        public static async Task<ActionResult> Register(string username, string password)
        {
            ActionResult res = null;
            var json = JsonConvert.SerializeObject(new { username = username, password = password });
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("", stringContent);// URRIIIII
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsAsync<ActionResult>();
            }
            return res;
        }

        
    }
}
