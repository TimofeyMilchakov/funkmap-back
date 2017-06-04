﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Funkmap.Common.Auth;
using Funkmap.Module.Musician.Mappers;
using Funkmap.Musician.Data.Abstract;

namespace Funkmap.Module.Musician.Controllers
{
    [RoutePrefix("api/musician")]
    public class MusicianController: ApiController
    {
        private readonly IMusicianRepository _musicianRepository;

        public MusicianController(IMusicianRepository musicianRepository)
        {
            _musicianRepository = musicianRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetMusicians()
        {
            var allMusicians = await _musicianRepository.GetAllAsync();

            return Content(HttpStatusCode.OK, allMusicians);

        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IHttpActionResult> GetMusician(long id)
        {
            var musicianEntity = await _musicianRepository.GetAsync(id);
            var musican = musicianEntity.ToMusicianModel();
            return Content(HttpStatusCode.OK, musican);

        }
    }
}
