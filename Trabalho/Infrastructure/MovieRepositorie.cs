using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trabalho.WebApi.Dominio;

namespace Trabalho.WebApi.Infrastructure
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

        public async Task<Movie> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Movie
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
