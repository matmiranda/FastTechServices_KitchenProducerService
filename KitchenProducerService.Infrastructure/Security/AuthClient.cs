using System.Net.Http.Headers;
using System.Text.Json;

namespace KitchenProducerService.Infrastructure.Security
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("http://132.196.119.86/api/auth/validate");
            return response.IsSuccessStatusCode;
        }
    }
}