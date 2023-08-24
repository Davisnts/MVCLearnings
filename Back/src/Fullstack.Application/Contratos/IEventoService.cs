using Fullstack.Application.Dtos;
namespace Fullstack.Persistence.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId,EventoDto model);
        Task<EventoDto> UpdateEvento(int userId,int eventoId,EventoDto model);
        Task<bool> DeleteEventos(int userId,int eventoId);

        Task<EventoDto[]> GetAllEventosByTemaAsync(int userId,string tema, bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosAsync(int userId,bool includePalestrantes = false);
        Task<EventoDto> GetEventoByIdAsync(int userId,int EventoId, bool includePalestrantes = false);
    }
}