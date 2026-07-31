using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.Comparison.Auth.Pages.ServerSideSessions;

public class IndexModel : PageModel
{
    private readonly ISessionManagementService _sessionManagementService;

    public IndexModel(ISessionManagementService sessionManagementService)
    {
        _sessionManagementService = sessionManagementService;
    }

    public QueryResult<UserSession> UserSessions { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? DisplayNameFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SessionIdFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SubjectIdFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Token { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Prev { get; set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        if (_sessionManagementService != null)
        {
            UserSessions = await _sessionManagementService.QuerySessionsAsync(new SessionQuery
            {
                ResultsToken = Token,
                RequestPriorResults = Prev == "true",
                DisplayName = DisplayNameFilter,
                SessionId = SessionIdFilter,
                SubjectId = SubjectIdFilter
            }, cancellationToken);
        }
    }

    [BindProperty]
    public string? SessionId { get; set; }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await _sessionManagementService.RemoveSessionsAsync(new RemoveSessionsContext
        {
            SessionId = SessionId,
        }, cancellationToken);
        return RedirectToPage("/ServerSideSessions/Index", new { Token, DisplayNameFilter, SessionIdFilter, SubjectIdFilter, Prev });
    }
}
