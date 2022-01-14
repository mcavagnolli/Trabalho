using System;
using CSharpFunctionalExtensions;

namespace Trabalho.WebApi.Dominio
{
    public sealed class SessionMovie
    {
        public Guid Id { get; private set; }

        public Guid FilmeId { get; private set; }

        public DateTime Date { get; private set; }

        public DateTime TimeStart { get; }

        public int Seats { get; private set; }

        public double Price { get; private set; }

        private SessionMovie() { }

        private SessionMovie(Guid id, Guid filmeId, DateTime date, DateTime timeStart, int seats, double price)
        {
            Id = id;
            FilmeId = filmeId;
            Date = date;
            TimeStart = timeStart;
            Seats = seats;
            Price = price;
        }

        public static Result<SessionMovie> Criar(Guid id, Guid filmeId, DateTime date, DateTime timeStart, int seats, double price)
        {
            return new SessionMovie(Guid.NewGuid(), filmeId, date, timeStart, seats, price);
        }

    }
}
