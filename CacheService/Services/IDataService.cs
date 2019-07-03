using System.Collections.Generic;
using System.Linq;
using CacheService.Customization;
using CacheService.Models;

namespace CacheService.Services
{
    public interface IDataService
    {
        List<Configuration> EnableConfigurations { get; }
        List<Sport> SportWithMatches { get; }

        IEnumerable<Match> FilterDisabledMatches(ICollection<Match> matches, int configurationId);
        IQueryable<Match> FilterDisabledMatches(int configurationId);
        List<Match> FilterDisabledMatches_Test(int configurationId);
        double GetSportMargin(int configurationId, int sportId);
        double GetSportMargin_Test(int configurationId, int sportId);
    }
}