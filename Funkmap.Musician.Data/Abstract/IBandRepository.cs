﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funkmap.Common.Data.Abstract;
using Funkmap.Common.Data.Parameters;
using Funkmap.Musician.Data.Entities;

namespace Funkmap.Musician.Data.Abstract
{
    public interface IBandRepository : IRepository<BandEntity>
    {
        Task<ICollection<BandEntity>> GetBandsPreviews();
        Task<ICollection<BandEntity>> GetNearestBandsPreviews(LocationParameter parameter);
    }
}
