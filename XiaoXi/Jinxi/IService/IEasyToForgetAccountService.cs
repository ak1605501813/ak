using HelpClassLibrary.Dto;
using Jinxi.DTO;
using Jinxi.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Jinxi.IService
{
    public interface IEasyToForgetAccountService
    {
        dynamic GetAll(QueryParametersDto input);
        IActionResult ADD(AccountDetails input);
        IActionResult Delete(PublicIdsDTO input);
        IActionResult Update(AccountDetails input);
    }
}
