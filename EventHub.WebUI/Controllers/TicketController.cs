using EventHub.Application.Services;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EventHub.WebUI.Controllers;

[Authorize]
public class TicketController : Controller
{
    private readonly ITicketService ticketService;

    public TicketController(ITicketService ticketService)
    {
        this.ticketService = ticketService;
    }

    public async Task<IActionResult> Purchase([FromQuery] Guid eventId, [FromQuery] Guid ticketId, [FromQuery] int quantity)
    {
        if (Request.Method == "POST")
        {
            var purchaseResult = await ticketService.PurchaseTicketAsync(eventId, ticketId, quantity);

            if (!purchaseResult.Success)
            {
                TempData["messageJson"] = JsonSerializer
                    .Serialize(new MessageViewModel(purchaseResult.Message, MessageType.Error));
                return RedirectToAction("Index", "Home");
            }

            TempData["messageJson"] = JsonSerializer
                .Serialize(new MessageViewModel(purchaseResult.Message, MessageType.Success));
            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Use([FromQuery] Guid ticketId)
    {
        if (Request.Method == "POST")
        {
            var result = await ticketService.UseTicketAsync(ticketId);

            if (!result)
            {
                TempData["messageJson"] = JsonSerializer
                    .Serialize(new MessageViewModel("Ticket was not found or already used", MessageType.Error));
                return RedirectToAction("Index", "Home");
            }

            TempData["messageJson"] = JsonSerializer
                .Serialize(new MessageViewModel("Ticket used", MessageType.Success));
            return RedirectToAction("Index", "Home");
        }
        var ticket = await ticketService.GetTicketAsync(ticketId);
        return View(ticket);
    }
}
