using EagleEye.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye.Queries
{
    public interface IMovieQueries
    {
        MovieDto GetMovie(int id);
        List<MovieDto> GetMovies(int movieId);
        List<MovieWithStatsDto> GetMovieStats();

    }

    public class MovieQueries : IMovieQueries
    {
        public MovieQueries()
        {

        }

        public MovieDto GetMovie(int id)
        {
            string path = @"Files\stats.csv";

            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MovieDto>();

                    var res = records.Where(m => m.MovieId == id).FirstOrDefault();

                    return res;
                }
            }
        }

        public List<MovieDto> GetMovies(int movieId)
        {
            var movies = QueryMovies();

            var res = movies.Where(m => m.MovieId == movieId).OrderBy(m => m.Language);

            return res.ToList();
        }

        public List<MovieWithStatsDto> GetMovieStats()
        {
            var movies = QueryMovies();
            var stats = QueryMovieStats().GroupBy(t => t.MovieId).Select(s =>new StatsDto { MovieId = s.Key, WatchDurationMs = s.Average(c => c.WatchDurationMs)});

            var res = movies.Join(stats, movie => movie.MovieId, stats => stats.MovieId, (first, second)
                 => new MovieStatsDto { MovieId = first.MovieId, Duration = first.Duration, Language = first.Language, ReleaseYear = first.ReleaseYear, Title = first.Title, WatchDurationMs = second.WatchDurationMs }).Distinct();


            var result = res.GroupBy(t => t.MovieId).Select(t =>
            new MovieWithStatsDto
            {
                MovieId = t.Key,
                ReleaseYear = t.First().ReleaseYear,
                AverageWatchDurationS = decimal.Round(t.Average(s => s.WatchDurationMs)),
                Title = t.First().Title,
                Watches = t.Count()
            });

            return result.OrderByDescending(o => o.Watches).ThenBy(o => o.ReleaseYear).ToList();
        }

        private List<MovieDto> QueryMovies()
        {
            string path = @"Files\metadata.csv";

            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MovieDto>();

                    return records.ToList();
                }
            }
        }

        private List<StatsDto> QueryMovieStats()
        {
            string path = @"Files\stats.csv";

            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<StatsDto>();

                    return records.ToList();
                }
            }
        }
    }
}
