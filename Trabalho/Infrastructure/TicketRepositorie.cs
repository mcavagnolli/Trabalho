using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trabalho.WebApi.Dominio;

namespace Trabalho.WebApi.Infrastructure
{
    public sealed class TicketRepositorie
    {
        private readonly TrabalhoDbContext _dbContext;

        public TicketRepositorie(TrabalhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Ticket newTicket, CancellationToken cancellationToken = default)
        {
            await _dbContext.Ticket.AddAsync(newTicket, cancellationToken);
        }

        public async Task<Ticket> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Ticket
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}