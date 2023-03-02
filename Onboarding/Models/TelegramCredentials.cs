using Microsoft.AspNetCore.Mvc;

namespace Onboarding.Models;

public class TelegramCredentials
{
    [FromHeader]
    public long Id { get; set; }
    
    [FromHeader]
    public string Username { get; set; }
}