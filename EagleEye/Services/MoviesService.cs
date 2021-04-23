using EagleEye.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEye.Services
{
    public interface IMoviesService
    {
        List<MovieDto> AddMovie(MovieDto movie);
    }

    public class MoviesService : IMoviesService
    {
        public MoviesService()
        {

        }

        public List<MovieDto> AddMovie(MovieDto movie)
        {
            var db = new List<MovieDto>();
            db.Add(movie);
            return db;
        }
    }
}
