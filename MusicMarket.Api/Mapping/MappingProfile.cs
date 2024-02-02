using AutoMapper;
using MusicMarket.Api.DTO;
using MusicMarket.Core.Models;

namespace MusicMarket.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Music, MusicDTO>();
            CreateMap<Artist, ArtistDTO>();

            CreateMap<MusicDTO, Music>();
            CreateMap<ArtistDTO, Artist>();

            CreateMap<SaveArtistDTO, Artist>();
            CreateMap<SaveMusicDTO, Music>();
        }
    }
}
