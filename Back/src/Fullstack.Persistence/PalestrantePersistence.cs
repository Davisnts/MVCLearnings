using Fullstack.Domain;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence
{
    public class PalestrantePersistence : IPalestrantePersist
    {
        private readonly FullstackContext _context;
        public PalestrantePersistence(FullstackContext context)
        {
            _context = context;
        }
       public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedeSociais);
            if (includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(pe => pe.Evento);
            }
            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {

            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedeSociais);

            if (includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(pe => pe.Evento);
            }
            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
           .Include(p => p.RedeSociais);

            if (includeEventos)
            {
                query = query
                .Include(p => p.PalestranteEventos)
                .ThenInclude(pe => pe.Evento);
            }
            query = query.OrderBy(p => p.Id).Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
        
    }
}