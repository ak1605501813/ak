using Jinxi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Jinxi.IService
{
    public interface IAuthorityAuthenticationService
    {
        IActionResult Login(LoginDTO input);
    }
}
