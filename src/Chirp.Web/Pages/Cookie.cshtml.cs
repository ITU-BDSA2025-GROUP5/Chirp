// Services/CookieConsentService.cs
using Microsoft.AspNetCore.Http;

public class CookieConsentService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CookieConsentService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public bool HasConsent()
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies["CookieConsent"] != null;
    }
    
    public string? GetConsentStatus()
    {
        return _httpContextAccessor.HttpContext?.Request.Cookies["CookieConsent"];
    }
    
    public void SetConsent(string status)
    {
        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true,
            Secure = false, // Set to true in production with HTTPS
            SameSite = SameSiteMode.Lax
        };
        
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("CookieConsent", status, options);
    }
}