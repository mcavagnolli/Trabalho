using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Trabalho.Models;

namespace Trabalho.Dominio
{
    public sealed class SessionMovie
    {
        private IList<Ticket> _tickets;

        public Guid Id { get; private set; }
        public Guid FilmeId { get; private set; }
        public DateTime Date { get; private set; }
        public int Seats { get; private set; }
        public double Price { get; private set; }
        public IEnumerable<Ticket> Tickets => _tickets;

        private SessionMovie() { }

        private SessionMovie(Guid id, Guid filmeId, DateTime date, int seats, double price, List<Ticket> tickets)
        {
            Id = id;
            FilmeId = filmeId;
            Date = date;
            Seats = seats;
            Price = price;
            _tickets = tickets;
        }

        public static Result<SessionMovie> Criar(Guid filmeId, DateTime date, int seats, double price)
        {
            return new SessionMovie(Guid.NewGuid(), filmeId, date, seats, price, new List<Ticket>());
        }

        public void Update(UpdateSessionInputModel inputModel)
        {
            FilmeId = Guid.Parse(inputModel.MovieId);
            Date = DateTime.Parse(inputModel.Date);
            Seats = inputModel.Seats;
            Price = inputModel.Price;
        }

    }
}