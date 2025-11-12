using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class Auth0Repository : IAuth0Repository
    {
        private readonly HttpClient _http;
        private readonly string _domain;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public Auth0Repository(HttpClient http, IConfiguration config)
        {
            _http = http;
            _domain = config["Auth0:Domain"]!;
            _clientId = config["Auth0:M2MClientId"]!;
            _clientSecret = config["Auth0:M2MClientSecret"]!;
        }

        private async Task<string> GetManagementTokenAsync()
        {
            var res = await _http.PostAsJsonAsync($"https://{_domain}/oauth/token", new
            {
                client_id = _clientId,
                client_secret = _clientSecret,
                audience = $"https://{_domain}/api/v2/",
                grant_type = "client_credentials"
            });

            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("access_token").GetString()!;
        }

        public async Task<string> CreateUserAsync(string email, string password)
        {
            var token = await GetManagementTokenAsync();
            _http.DefaultRequestHeaders.Authorization = new("Bearer", token);

            var body = new
            {
                email,
                password,
                connection = "Username-Password-Authentication"
            };

            var res = await _http.PostAsJsonAsync($"https://{_domain}/api/v2/users", body);
            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("user_id").GetString()!;
        }

        public async Task<HttpResponseMessage> DeleteUserAsync(string auth0Id)
        {
            var token = await GetManagementTokenAsync();
            _http.DefaultRequestHeaders.Authorization = new("Bearer", token);
            var res = await _http.DeleteAsync($"https://{_domain}/api/v2/users/{auth0Id}");
            return res.EnsureSuccessStatusCode();
            
        }
    }
}
