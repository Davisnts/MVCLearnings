using Fullstack.Domain;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence
{
    public class LotePersistence : ILotePersist
    {
        private readonly FullstackContext _context;
        public LotePersistence(FullstackContext context)
        {
            _context = context;
        }
        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking().Where(lote => lote.Id == id && lote.EventoId == eventoId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking().Where(lote => lote.EventoId == eventoId);
            return await query.ToArrayAsync();
        }
    }
}