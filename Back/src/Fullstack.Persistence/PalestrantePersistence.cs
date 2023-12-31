using Fullstack.Domain;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Fullstack.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence;

public class PalestrantePersistence : GeralPersistence, IPalestrantePersist
{
    private readonly FullstackContext _context;
    public PalestrantePersistence(FullstackContext context) : base(context)
    {
        _context = context;
    }
    public async Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes
        .Include(p => p.User)
        .Include(p => p.RedeSociais);
        if (includeEventos)
        {
            query = query
            .Include(p => p.PalestranteEventos)
            .ThenInclude(pe => pe.Evento);
        }
        query = query.AsNoTracking()
        .Where(p => (p.MiniCurriculo.ToLower().Contains(pageParams.Term.ToLower()) ||
               p.User.PrimeiroNome.ToLower().Contains(pageParams.Term.ToLower()) ||
               p.User.UltimoNome.ToLower().Contains(pageParams.Term.ToLower())) &&
               p.User.Funcao == Domain.Enum.Funcao.Palestrante)
               .OrderBy(p => p.Id);

        return await PageList<Palestrante>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
    }

    public async Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includeEventos)
    {
        IQueryable<Palestrante> query = _context.Palestrantes
        .Include(p => p.User)
       .Include(p => p.RedeSociais);

        if (includeEventos)
        {
            query = query
            .Include(p => p.PalestranteEventos)
            .ThenInclude(pe => pe.Evento);
        }
        query = query.AsNoTracking().OrderBy(p => p.Id)
        .Where(p => p.UserId == userId);

        return await query.FirstOrDefaultAsync();
    }

}