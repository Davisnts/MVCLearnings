using Fullstack.Domain;
namespace Fullstack.Persistence.Contratos
{
    public interface IPalestrantePersist
    {
        //PALESTRANTES
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string Nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos);
    }
}