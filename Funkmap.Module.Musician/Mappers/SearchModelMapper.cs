﻿using Funkmap.Common;
using Funkmap.Common.Abstract.Search;
using Funkmap.Module.Musician.Models;
using Funkmap.Musician.Data.Entities;

namespace Funkmap.Module.Musician.Mappers
{
    public static class SearchModelMapper
    {
        public static SearchModel ToSearchModel(this MusicianEntity musician)
        {
            if (musician == null) return null;
            return new MusicianSearchModel
            {
                Name = musician.Name,
                Latitude = musician.Latitude,
                Longitude = musician.Longitude,
                Id = musician.Id,
                ModelType = EntityType.Musician,
                Instrument = musician.Instrument
            };
        }

        public static SearchModel ToSearchModel(this BandEntity band)
        {
            if (band == null) return null;
            return new BandSearchModel()
            {
                Id = band.Id,
                Longitude = band.Longitude,
                Latitude = band.Latitude,
                Name = band.Name,
                ModelType = EntityType.Band
            };
        }
    }
}
