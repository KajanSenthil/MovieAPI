using CsvHelper.Configuration;

namespace MovieAPI.Model
{
    public class MetaDataMap : ClassMap<MetaData>
    {
        public MetaDataMap()
        {
            Map(x => x.Id);
            Map(x => x.MovieId);
            Map(x => x.Title);
            Map(x => x.Language);
            Map(x => x.Duration);
            Map(x => x.ReleaseYear);
        }
    }
}
