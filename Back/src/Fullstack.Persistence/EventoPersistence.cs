using Fullstack.Domain;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence
{
    public class EventoPersistence : IEventoPersist
    {
        private readonly FullstackContext _context;
        public EventoPersistence(FullstackContext context)
        {
            _context = context;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lote)
            .Include(e => e.RedeSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lote)
            .Include(e => e.RedeSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
             .Include(e => e.Lote)
             .Include(e => e.RedeSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.OrderBy(e => e.Id).Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

    }
}