using BlazorUI.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Text.Json;

namespace BlazorUI.Services
{
    public class GameFactory
    {
        private readonly HttpClient _httpClient;

        public GameFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ImageSearchGameScene> BuildImageSearchGame(string gameCode)
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/data/games/{gameCode}.json");
            httpRequest.SetBrowserRequestCache(BrowserRequestCache.NoCache);

            var httpRequestResult = await _httpClient.SendAsync(httpRequest);
            if (httpRequestResult.IsSuccessStatusCode)
            {
                var rawData = await httpRequestResult.Content.ReadAsStringAsync();
                var spots = JsonSerializer.Deserialize<List<ImageSpotInfo>>(rawData) ?? throw new Exception("Error parsing game");
                return new ImageSearchGameScene(spots);
            }
            throw new Exception("Error requesting game");
        }
    }
}
