using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEye.Dtos
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }
    }

    public class MovieStatsDto : MovieDto
    {
        public decimal WatchDurationMs { get; set; }
    }

    public class StatsDto
    {
        public int MovieId { get; set; }
        public decimal WatchDurationMs { get; set; }

    }

    public class MovieWithStatsDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public decimal AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }

    }
}
