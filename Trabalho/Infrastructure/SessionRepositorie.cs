using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trabalho.WebApi.Dominio;

namespace Trabalho.WebApi.Infrastructure
{
    public sealed class SessionRepositorie
    {
        private readonly TrabalhoDbContext _dbContext;

        public SessionRepositorie(TrabalhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(SessionMovie newSessionMovie, CancellationToken cancellationToken = default)
        {
            await _dbContext.Session.AddAsync(newSessionMovie, cancellationToken);
        }

        public async Task<SessionMovie> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Session
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
