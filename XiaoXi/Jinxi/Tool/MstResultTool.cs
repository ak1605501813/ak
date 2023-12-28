using Microsoft.AspNetCore.Mvc;

namespace Jinxi.Tool
{
    public static class MstResultTool
    {
        public static IActionResult Success(object rtnData)
        {

            JsonResult JsonResult = new JsonResult(new
            {
                Code = "200",
                Message = "",
                Data = rtnData ?? string.Empty,
                MsgType = "Success"
            });
            return JsonResult;

        }
        public static IActionResult Error(object rtnData)
        {

            JsonResult JsonResult = new JsonResult(new
            {
                Code = "400",
                Message = "",
                Data = rtnData ?? string.Empty,
                MsgType = "Error"
            });
            return JsonResult;

        }
    }
}
