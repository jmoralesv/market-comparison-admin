using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.Comparison.Admin.Pages;

public class CallApiModel(HttpClient httpClient) : PageModel
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    public string Json { get; set; } = string.Empty;

    public async Task OnGetAsync()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var content = await httpClient.GetStringAsync("https://localhost:7101/weather-forecast");

        var parsed = JsonDocument.Parse(content);
        var formatted = JsonSerializer.Serialize(parsed, _jsonSerializerOptions);

        Json = formatted;
    }
}
