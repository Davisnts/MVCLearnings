using Fullstack.Domain;
namespace Fullstack.Persistence.Contratos
{
    public interface IEventoPersist
    {
        //EVENTO
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes=false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes=false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes=false);

    }
}