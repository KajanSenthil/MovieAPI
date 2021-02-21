using CsvHelper.Configuration;

namespace MovieAPI.Model
{
    public class MovieStatsMap : ClassMap<MovieStats>
    {
        public MovieStatsMap()
        {
            Map(x => x.MovieId).Name("movieId");
            Map(x => x.WatchDurationMs).Name("watchDurationMs");
        }
    }
}
