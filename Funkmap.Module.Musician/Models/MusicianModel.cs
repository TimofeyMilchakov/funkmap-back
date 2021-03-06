﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funkmap.Musician.Data.Entities;

namespace Funkmap.Module.Musician.Models
{
    public class MusicianModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public int Expirience { get; set; }
        public Styles[] Styles { get; set; }

        [Required]
        public InstrumentType Instrument { get; set; }

        public byte[] Avatar { get; set; }

        public string VkLink { get; set; }
        public string YouTubeLink { get; set; }
        public string FacebookLink { get; set; }

    }
}
