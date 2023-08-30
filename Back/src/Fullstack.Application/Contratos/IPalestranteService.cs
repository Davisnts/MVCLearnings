using Fullstack.Application.Dtos;
using Fullstack.Persistence.Models;

namespace Fullstack.Persistence.Contratos
{
    public interface IPalestranteService
    {
        Task<PalestranteDto> AddPalestrante(int userId,PalestranteAddDto model);
        Task<PalestranteDto> UpdatePalestrante(int userId,int eventoId,PalestranteUpdateDto model);

        Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams,bool includeEventos = false);
        Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);



    }
}