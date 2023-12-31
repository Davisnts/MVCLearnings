using Fullstack.Domain;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Fullstack.Persistence.Models;
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
        public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(e => e.Lotes)
            .Include(e => e.RedeSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.AsNoTracking().Where(e => (e.Tema.ToLower().Contains(pageParams.Term.ToLower()) ||
                                                       e.Local.ToLower().Contains(pageParams.Term.ToLower()))
            &&
           e.UserId == userId)

           .OrderBy(e => e.Id);
            return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
             .Include(e => e.Lotes)
             .Include(e => e.RedeSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            query = query.AsNoTracking()

            .OrderBy(e => e.Id).Where(e => e.Id == eventoId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }

    }
}