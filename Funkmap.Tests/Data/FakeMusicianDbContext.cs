﻿using System;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Funkmap.Common.Data.Tools;
using Funkmap.Musician.Data;
using Funkmap.Musician.Data.Entities;
using Funkmap.Musician.Data.Repositories;

namespace Funkmap.Tests.Data
{
    public class FakeMusicianDbContext : MusicianContext
    {
        public FakeMusicianDbContext() : base("TestConnection")
        {
            Database.SetInitializer(new TestDbContextInitializer());
        }

        public class TestDbContextInitializer : DropCreateDatabaseAlways<FakeMusicianDbContext>
        //DropCreateDatabaseIfModelChanges<FakeMusicianDbContext>
        {
            protected override void Seed(FakeMusicianDbContext context)
            {
                SeedMusicians(context);
                SeedBands(context);
                context.SaveChanges();
            }

            private void SeedMusicians(FakeMusicianDbContext context)
            {
                var musicianRepository = new MusicianRepository(context);

                var m1 = new MusicianEntity()
                {
                    Sex = Sex.Male,
                    Login = "rogulenkoko",
                    BirthDate = DateTime.Now,
                    Expirience = 3,
                    Description = "Описание",
                    Name = "Кирилл Рогуленко",
                    Latitude = 50,
                    Longitude = 30,
                    Styles = Styles.Funk | Styles.HipHop,
                    Instrument = InstrumentType.Brass,
                    VkLink = "https://vk.com/id30724049",
                    YouTubeLink = "https://www.youtube.com/user/Urgantshow",
                    BandId = 1
                };


                m1.AvatarImage = ImageProvider.GetAvatar();

                var m2 = new MusicianEntity()
                {
                    Sex = Sex.Female,
                    Login = "madlib",
                    BirthDate = DateTime.Now,
                    Expirience = 1,
                    Description = "Большое описание музыканта, тудым сюдым. Как дела братва?",
                    Name = "Madlib",
                    Latitude = 51,
                    Longitude = 30,
                    Styles = Styles.Funk | Styles.Rock,
                    Instrument = InstrumentType.Drums,
                    FacebookLink = "https://ru-ru.facebook.com/",
                    BandId = 1
                };

                var m3 = new MusicianEntity()
                {
                    Sex = Sex.Male,
                    Login = "razrab",
                    BirthDate = DateTime.Now,
                    Expirience = 0,
                    Description = "Razrab описание!!!",
                    Name = "Razrab Razrab",
                    Latitude = 51,
                    Longitude = 31,
                    Styles = Styles.HipHop,
                    Instrument = InstrumentType.Keyboard,
                    BandId = 2
                };

                musicianRepository.Add(m1);
                musicianRepository.Add(m2);
                musicianRepository.Add(m3);
            }

            private void SeedBands(FakeMusicianDbContext context)
            {
                var bandRepository = new BandRepository(context);
                var b1 = new BandEntity()
                {
                    Login = "test",
                    DesiredInstruments = InstrumentType.Bass | InstrumentType.Guitar,
                    Name = "The Beatles",
                    ShowPrice = 123412,
                    VideoLinks = new PersistableStringCollection() { "firstVideo", "secondVideo" },
                    Longitude = 52,
                    Latitude = 29
                };

                var b2 = new BandEntity()
                {
                    Login = "rogulenkoko",
                    Name = "Red Hot Chili Peppers",
                    ShowPrice = 123412,
                    VideoLinks = new PersistableStringCollection() { "firstVideo", "secondVideo" },
                    Longitude = 52,
                    Latitude = 28
                };

                var b3 = new BandEntity()
                {
                    Login = "rogulenkoko",
                    Name = "Coldplay",
                    ShowPrice = 123,
                    Longitude = 50,
                    Latitude = 31
                };

                bandRepository.Add(b1);
                bandRepository.Add(b2);
                bandRepository.Add(b3);
            }
        }
    }
}
