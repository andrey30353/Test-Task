using System;
using System.Collections.Generic;
using System.Linq;
using CacheService.Customization;
using CacheService.Models;
using Microsoft.EntityFrameworkCore;

namespace CacheService.Services
{
    public class DataService : IDataService
    {
        private readonly MsSqlContext _context;

        public DataService(MsSqlContext context)
        {
            _context = context;
        }

        public List<Configuration> EnableConfigurations => _context.Configuration.Where(t => t.IsEnabled).ToList();

        public List<Sport> SportWithMatches => _context.Sports
            .Include(s => s.Categories)
            .ThenInclude(c => c.Matches)
            .ToList();

        public double GetSportMargin(int configurationId, int sportId)
        {
            var sportMargin = _context.ConfigurationSportMargin
                .Find(configurationId, sportId);

            return sportMargin?.MarginValue ?? 1;
        }

        public double GetSportMargin_Test(int configurationId, int sportId)
        {
            var sportMargin = _context.ConfigurationSportMargin.ToList()
                .FirstOrDefault(t=>t.ConfigurationId == configurationId 
                                   && t.SportId == sportId);

            return sportMargin?.MarginValue ?? 1;
        }

        /// <summary>
        /// Отфильтровать недоступные матчи
        /// </summary>
        /// <param name="matches">Исходная коллекция матчей</param>
        /// <param name="configurationId">Ид конфигурации</param>
        /// <returns>Перечисление матчей без отключенных</returns>
        public IEnumerable<Match> FilterDisabledMatches(ICollection<Match> matches, int configurationId)
        {
            return matches.Where(match => !_context.ConfigurationMatchDisabled
                .Any(t => t.ConfigurationId == configurationId && t.MatchId == match.MatchId));
        }

        /// <summary>
        /// Отфильтровать недоступные матчи из всего списка
        /// </summary>
        /// <param name="configurationId">Ид конфигурации</param>
        /// <returns>Матчи без отключенных</returns>
        public IQueryable<Match> FilterDisabledMatches(int configurationId)
        {
            return _context.Matches.Where(match => !_context.ConfigurationMatchDisabled
                .Any(t => t.ConfigurationId == configurationId && t.MatchId == match.MatchId));
        }

        /// <summary>
        /// Отфильтровать недоступные матчи из всего списка
        /// </summary>
        /// <param name="configurationId">Ид конфигурации</param>
        /// <returns>Перечисление матчей без отключенных</returns>
        public List<Match> FilterDisabledMatches_Test(int configurationId)
        {
            return _context.Matches.ToList().Where(match => !_context.ConfigurationMatchDisabled.ToList()
                .Any(t => t.ConfigurationId == configurationId && t.MatchId == match.MatchId)).ToList();
        }
    }
}