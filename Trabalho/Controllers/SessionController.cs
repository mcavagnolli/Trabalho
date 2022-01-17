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
    public class SessionController : ControllerBase
    {
        private readonly SessionRepositorie _sessionRepositorie;

        public SessionController(SessionRepositorie sessionRepositorie)
        {
            _sessionRepositorie = sessionRepositorie;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Problema ao converter ID");

            var session = await _sessionRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            return Ok(session);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTodas([FromQuery] string movie, [FromQuery] string date, CancellationToken cancellationToken)
        {
            var session = await _sessionRepositorie.RecuperarTodas(cancellationToken);

            if (!string.IsNullOrEmpty(movie))
            {
                if (!Guid.TryParse(movie, out var movieGuid))
                    return BadRequest("Problema ao converter ID");

                session = session.Where(sessao => sessao.FilmeId == movieGuid);
            }

            if (!string.IsNullOrEmpty(date))
            {
                if (!DateTime.TryParse(date, out var datetime))
                    return BadRequest();

                session = session.Where(sessao => sessao.Date == datetime);
            }

            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NewSessionInputModel newSessionInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(newSessionInputModel.FilmeId, out var guid))
                return BadRequest("Problema ao converter ID");

            var sessao = SessionMovie.Criar(Guid.Parse(newSessionInputModel.FilmeId), DateTime.Parse(newSessionInputModel.Date),
                newSessionInputModel.Seats, newSessionInputModel.Price);

            if (sessao.IsFailure)
                return BadRequest(sessao.Error);

            await _sessionRepositorie.InserirAsync(sessao.Value, cancellationToken);
            await _sessionRepositorie.CommitAsync(cancellationToken);

            return CreatedAtAction("RecuperarPorId", new { id = sessao.Value.Id }, sessao.Value.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] UpdateSessionInputModel updateSessionInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Problema ao converter ID");

            var session = await _sessionRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            if (session == null)
                return NotFound();

            session.Update(updateSessionInputModel);
            _sessionRepositorie.Atualizar(session);
            await _sessionRepositorie.CommitAsync(cancellationToken);

            return Ok(session);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Problema ao converter ID");

            var session = await _sessionRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            if (session == null)
                return NotFound();

            _sessionRepositorie.Remover(session);
            await _sessionRepositorie.CommitAsync(cancellationToken);

            return Ok("Sessão foi removida com sucesso!");
        }
    }
}
