using Fullstack.Domain;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence
{
    public class RedeSocialPersistence : GeralPersistence, IRedeSocialPersist
    {
        private readonly FullstackContext _context;

        public RedeSocialPersistence(FullstackContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking()
            .Where(rs => rs.EventoId == eventoId && rs.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<RedeSocial>  GetRedeSocialPalestranteByIdsAsync(int palestestanteId, int id)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;
            query = query.AsNoTracking()

            .Where(rs => rs.PalestranteId == palestestanteId && rs.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;
            query = query.AsNoTracking()

                        .Where(rs => rs.EventoId == eventoId);
            return await query.ToArrayAsync();
        }
        public async Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestestanteId)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;
            query = query.AsNoTracking().Where(rs => rs.PalestranteId == palestestanteId);
            return await query.ToArrayAsync();
        }


    }
}