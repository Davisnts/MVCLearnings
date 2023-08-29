using Fullstack.Domain;
using Fullstack.Persistence.Models;

namespace Fullstack.Persistence.Contratos
{
    public interface IEventoPersist
    {
        //EVENTO
        // Task<PageList<Evento>> GetAllEventosByTemaAsync(int userId,PageParams pageParams,string tema, bool includePalestrantes=false);
        Task<PageList<Evento>> GetAllEventosAsync(int userId,PageParams pageParams,bool includePalestrantes=false);
        Task<Evento> GetEventoByIdAsync(int userId,int eventoId, bool includePalestrantes=false);

    }
}