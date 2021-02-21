using System.Collections.Generic;
using MovieAPI.Model;

namespace MovieAPI.Service
{
    public interface IMovieService
    {
        void SaveMetaData(MetaData metaData);

        List<MetaData> GetMetaDataMovieById(int movieId);

        List<Stats> GetMovieStats();
    }
}
