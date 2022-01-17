using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trabalho.Dominio;
using Trabalho.Infrastructure;
using Trabalho.Models;

namespace Trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepositorie _movieRepositorie;

        public MovieController(MovieRepositorie filmesRepositorio)
        {
            _movieRepositorie = filmesRepositorio;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarPorId(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Id inválido");

            var movie = await _movieRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            return Ok(movie);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTodos(CancellationToken cancellationToken)
        {
            var filmes = await _movieRepositorie.RecuperarTodos(cancellationToken);

            return Ok(filmes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewMovieInputModel inputModel, CancellationToken cancellationToken)
        {
            var newMovie = Movie.Criar(inputModel);

            if (newMovie.IsFailure)
            {
                return BadRequest(newMovie.Error);
            }

            await _movieRepositorie.InserirAsync(newMovie.Value, cancellationToken);
            await _movieRepositorie.CommitAsync(cancellationToken);

            return Ok(newMovie.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] UpdateMovieInputModel updateMovieInputModel, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pode ser convertida");

            var movie = await _movieRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            if (movie == null)
                return NotFound();

            movie.Update(updateMovieInputModel);
            _movieRepositorie.Atualizar(movie);
            await _movieRepositorie.CommitAsync(cancellationToken);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("ID não pode ser convertida");

            var movie = await _movieRepositorie.RecuperarPorIdAsync(guid, cancellationToken);

            if (movie == null)
                return NotFound();

            _movieRepositorie.Remover(movie);
            await _movieRepositorie.CommitAsync(cancellationToken);

            return Ok("Filme foi removido com sucesso!");
        }
    }
}
