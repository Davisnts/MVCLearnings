using Fullstack.Domain;
using Fullstack.Persistence.Models;

namespace Fullstack.Persistence.Contratos
{
    public interface IPalestrantePersist : IGeralPersist
    {
        //PALESTRANTES
        Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
        Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);
    }
}