using Jinxi.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Jinxi.IService
{
    public interface IEasyToForgetAccountService
    {
        IActionResult ADD(AccountDetails input);
    }
}
