using Microsoft.EntityFrameworkCore;
using MusicMarket.Core.Models;
using MusicMarket.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarket.Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(MusicMarketDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Artist>> GetAllWithMusicAsync()
        {
            return await MusicMarketDbContext.Artists.Include(x => x.Musics).ToListAsync();
        }

        public Task<Artist> GetWithMusicByIdAsync(int id)
        {
            return MusicMarketDbContext.Artists.Include(x => x.Musics).SingleOrDefaultAsync(x => x.Id == id);
        }

        private MusicMarketDbContext MusicMarketDbContext
        {
            get { return Context as MusicMarketDbContext; }
            //bu bi property inherit aldıgın yerdeki context i buna çeviriyo
        }
    }
}
