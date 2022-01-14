using System;
using CSharpFunctionalExtensions;

namespace Trabalho.WebApi.Dominio
{
    public sealed class Ticket
    {
        public Guid Id { get; private set; }

        public Guid SessionId { get; private set; }

        public string ClientName { get; private set; }

        public int Amount { get; private set; }

        private Ticket() { }

        private Ticket(Guid id, Guid sessionId, string clientName, int amount)
        {
            Id = id;
            SessionId = sessionId;
            ClientName = clientName;
            Amount = amount;
        }

        public static Result<Ticket> Criar(Guid id, Guid sessionId, string clientName, int amount)
        {
            return new Ticket(Guid.NewGuid(), sessionId, clientName, amount);
        }

    }
}