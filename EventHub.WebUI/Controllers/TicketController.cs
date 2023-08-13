using EventHub.Application.Services;
using EventHub.Domain.Entities;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

    [HttpPost]
    public async Task<IActionResult> Purchase([FromQuery]Guid eventId, [FromQuery]Guid ticketId, [FromQuery]int quantity)
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
}
