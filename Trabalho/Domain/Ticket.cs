using System;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Trabalho.Models;

namespace Trabalho.Dominio
{
    public sealed class Ticket
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
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

        public static Result<Ticket> Criar(NewTicketInputModel inputModel, int seats, int amount)
        {
            if (amount == seats)
                return Result.Failure<Ticket>("Sessão cheia");

            var restTickets = seats - amount;

            if (restTickets < inputModel.Amount)
                return Result.Failure<Ticket>($"Pode comprar apenas {restTickets} tickets!");

            return new Ticket(Guid.NewGuid(), Guid.Parse(inputModel.SessionId), inputModel.ClientName, inputModel.Amount);
        }

    }
}