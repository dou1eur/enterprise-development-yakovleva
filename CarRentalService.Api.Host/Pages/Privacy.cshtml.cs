using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRentalService.Api.Host.Pages;
public class PrivacyModel(ILogger<PrivacyModel> logger) : PageModel
{
    private readonly ILogger<PrivacyModel> _logger = logger;

    public void OnGet()
    {
    }
}

