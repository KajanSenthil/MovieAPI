using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using MovieAPI.Model;

namespace MovieAPI.Service
{
    public class MovieService : IMovieService
    {
        public void SaveMetaData(MetaData metaData)
        {
            var dataBase = new List<MetaData>();
            dataBase.Add(metaData);
        }

        public List<MetaData> GetMetaDataMovieById(int movieId)
        {
            var moviewMetaData = GetAllMataData();

            var movieRecords = moviewMetaData.Where(m => m.MovieId == movieId).GroupBy(p => p.Language).Select(g => g.First()).ToList();

            return movieRecords.OrderByDescending(m => m.Id).OrderBy(m => m.Language).ToList();
        }

        public List<Stats> GetMovieStats()
        {
            var movieMetaData = GetAllMataData();
            var allMovieStats = GetAllStats();

            var listOfMovieStats = new List<Stats>();

            foreach (var item in movieMetaData.GroupBy(m => m.MovieId).Select(m => m.First().MovieId).ToList())
            {
                var movieStats = allMovieStats.Where(m => m.MovieId == item).ToList();
                var movie = movieMetaData.Where(m => m.MovieId == item).FirstOrDefault();

                listOfMovieStats.Add(
                    new Stats
                    {
                        MovieId = movie.MovieId,
                        Title = movie.Title,
                        AverageWatchDurationS = (int)movieStats.Average(m => m.WatchDurationMs),
                        Watches = movieStats.Count,
                        ReleaseYear = movie.ReleaseYear
                    });
            }

            return listOfMovieStats.OrderByDescending(m => m.Watches).OrderByDescending(m => m.ReleaseYear).ToList();
        }

        private List<MetaData> GetAllMataData()
        {
            var filePath = "Data/metadata.csv";

            using (var streamReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<MetaDataMap>();
                    return csvReader.GetRecords<MetaData>().ToList();
                }
            }
        }

        private List<MovieStats> GetAllStats()
        {
            var filePath = "Data/stats.csv";

            using (var streamReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<MovieStatsMap>();
                    return csvReader.GetRecords<MovieStats>().ToList();
                }
            }
        }
    }
}
