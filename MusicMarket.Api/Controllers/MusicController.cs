using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Api.DTO;
using MusicMarket.Api.Validators;
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

        [HttpPost]
        public async Task<ActionResult<MusicDTO>> CreateMusic(SaveMusicDTO saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var musicToCreate = _mapper.Map<SaveMusicDTO, Music>(saveMusicResource);

            var newMusic = await _musicService.CreateMusic(musicToCreate);

            var music = await _musicService.GetMusicById(newMusic.Id);

            var musicResource = _mapper.Map<Music, MusicDTO>(music);
            return Ok(musicResource);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMusic(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var music = await _musicService.GetMusicById(id);
            if (music == null)
            {
                return NotFound();
            }

            await _musicService.DeleteMusic(music);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<MusicDTO>> UpdateMusic(int id, SaveMusicDTO saveMusicResource)
        {
            var validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
            {
                return BadRequest(validationResult.Errors);
            }

            var musicToUpdated = await _musicService.GetMusicById(id);

            if (musicToUpdated == null)
                return NotFound();

            var music = _mapper.Map<SaveMusicDTO, Music>(saveMusicResource);

            await _musicService.UpdateMusic(musicToUpdated, music);
            var updatedMusic = await _musicService.GetMusicById(id);

            var updatedMusicResource = _mapper.Map<Music, MusicDTO>(updatedMusic);

            return Ok(updatedMusicResource);
        }
    }
}
