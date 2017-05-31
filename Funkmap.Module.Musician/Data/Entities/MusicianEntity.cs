﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funkmap.Common.Abstract.Data;

namespace Funkmap.Module.Musician.Data
{
    public class MusicianEntity : Entity
    {
        public string Login { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public int Expirience { get; set; }


    }

    public enum Sex
    {
        Male = 1,
        Female = 2
    }
}
