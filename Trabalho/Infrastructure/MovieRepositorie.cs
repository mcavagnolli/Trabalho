using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Trabalho.Dominio;

namespace Trabalho.Infrastructure
{
    public sealed class MovieRepositorie
    {
        private readonly TrabalhoDbContext _dbContext;

        public MovieRepositorie(TrabalhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Movie newMovie, CancellationToken cancellationToken = default)
        {
            await _dbContext.Movie.AddAsync(newMovie, cancellationToken);
        }

        public void Atualizar(Movie movie)
        {
        }

        public async Task<Movie> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Movie
                .FirstOrDefaultAsync(movie => movie.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Movie>> RecuperarTodos(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Movie.Include(movie => movie.Sessions).ToListAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Remover(Movie removerFilme)
        {
            _dbContext.Movie.Remove(removerFilme);
        }
    }
}
