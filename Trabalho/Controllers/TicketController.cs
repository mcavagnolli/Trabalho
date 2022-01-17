using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trabalho.Dominio;
using Trabalho.Infrastructure;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly TicketRepositorie _ticketRepositorie;
        private readonly SessionRepositorie _sessionRepositorie;

        public TicketController(TicketRepositorie ticketRepositorie, SessionRepositorie sessionRepositorie)
        {
            _ticketRepositorie = ticketRepositorie;
            _sessionRepositorie = sessionRepositorie;
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTodos(CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepositorie.RecuperarTodos(cancellationToken);

            return Ok(ticket);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Problema ao converter ID");

            var ticket = await _ticketRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NewTicketInputModel newTicketInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(newTicketInputModel.SessionId, out var guid))
                return BadRequest("Problema ao converter ID");

            var ticketSession = await _sessionRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            if (ticketSession == null)
                return NotFound("Sessão não encontrada");

            var soldi = ticketSession.Tickets.Select(x => x.Amount).Sum();

            var ticket = Ticket.Criar(newTicketInputModel, ticketSession.Seats, soldi);
            if (ticket.IsFailure)
            {
                return BadRequest(ticket.Error);
            }
            
            await _ticketRepositorie.InserirAsync(ticket.Value, cancellationToken);
            await _ticketRepositorie.CommitAsync(cancellationToken);

            return Ok(ticket.Value);
        }
    }
}
