using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.Comparison.Admin.Pages;

public class CallApiModel : PageModel
{
    private readonly HttpClient _httpClient;

    public CallApiModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string Json { get; set; } = string.Empty;

    public async Task OnGetAsync()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var content = await _httpClient.GetStringAsync("https://localhost:7101/weather-forecast");

        var parsed = JsonDocument.Parse(content);
        var formatted = JsonSerializer.Serialize(parsed, new JsonSerializerOptions { WriteIndented = true });

        Json = formatted;
    }
}
