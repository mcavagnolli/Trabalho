using System;
using CSharpFunctionalExtensions;

namespace Trabalho.WebApi.Dominio
{
    public sealed class Movie
    {
        private Movie() { }

        private Movie(Guid id, string title, DateTime duration, string synopsis)
        {
            Id = id;
            Title = title;
            Duration = duration;
            Synopsis = synopsis;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Duration { get; private set; }
        public string Synopsis { get; private set; }

        public static Result<Movie> Criar(string title, DateTime duration, string synopsis)
        {
            if (string.IsNullOrEmpty(title))
                return Result.Failure<Movie>("Titulo deve ser preenchido");
            return new Movie(Guid.NewGuid(), title, duration, synopsis);
        }
    }
}
