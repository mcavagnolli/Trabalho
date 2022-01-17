using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Trabalho.Models;

namespace Trabalho.Dominio
{
    public sealed class Movie

    {
        private IList<SessionMovie> _sessions;

        [Key]
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Duration { get; private set; }
        public string Synopsis { get; private set; }
        public IEnumerable<SessionMovie> Sessions => _sessions;
        private Movie() { }

        private Movie(Guid id, string title, string duration, string synopsis, List<SessionMovie> sessions)
        {
            Id = id;
            Title = title;
            Duration = duration;
            Synopsis = synopsis;
            _sessions = sessions;
        }

        public static Result<Movie> Criar(NewMovieInputModel newMovieInputModel)
        {

            if (string.IsNullOrEmpty(newMovieInputModel.Title))
                return Result.Failure<Movie>("Titulo deve ser preenchido");
            return new Movie(Guid.NewGuid(), newMovieInputModel.Title, newMovieInputModel.Duration, newMovieInputModel.Synopsis, new List<SessionMovie>());
        }

        public void Update(UpdateMovieInputModel inputModel)
        {
            Title = inputModel.Title;
            Duration = inputModel.Duration;
            Synopsis = inputModel.Synopsis;
        }
    }
}
