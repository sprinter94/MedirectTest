using Microsoft.AspNetCore.Mvc;

namespace Medirect.Test.API.Extension
{
    internal static class ApiExtensions
    {
        public static IActionResult Respond(this ControllerBase controllerBase, Application.Model.IResult result)
            => controllerBase.StatusCode(result.StatusCode, result);
    }
}