using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace EventHub.WebUI.Filters;

public class MessageActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Controller is Controller controller)
        {
            MessageViewModel? message;
            try
            {
                message = JsonSerializer
                    .Deserialize<MessageViewModel>(controller.TempData["messageJson"]?.ToString() ?? string.Empty);
            }
            catch (Exception)
            {
                message = null;
            }
            controller.ViewBag.Message = message;
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}