using Fullstack.Domain;
namespace Fullstack.Persistence.Contratos
{
    public interface IRedeSocialPersist: IGeralPersist
    {
        Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id);
        Task<RedeSocial> GetRedeSocialPalestranteByIdsAsync(int PalestestanteId, int id);
        Task<RedeSocial[]> GetAllByEventoIdAsync(int id);
        Task<RedeSocial[]> GetAllByPalestranteIdAsync(int id);


    }
}