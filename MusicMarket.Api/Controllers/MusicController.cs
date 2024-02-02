using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Api.DTO;
using MusicMarket.Core.Models;
using MusicMarket.Core.Services;

namespace MusicMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;

        public MusicController(IMusicService musicService, IMapper mapper)
        {
            _musicService = musicService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicDTO>>> GetAllMusics()
        {
            var musics = await _musicService.GetAllWithArtist();

            var musicResource = _mapper.Map<IEnumerable<Music>, IEnumerable<MusicDTO>>(musics);
            return Ok(musics);
        }

        [HttpGet("id")]
        public async Task<ActionResult<MusicDTO>> GetMusicById(int id)
        {
            var music = await _musicService.GetMusicById(id);
            var musicResource = _mapper.Map<Music, MusicDTO>(music);
            return Ok(musicResource);
        }
    }
}
