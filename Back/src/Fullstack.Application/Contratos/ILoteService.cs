using Fullstack.Application.Dtos;
namespace Fullstack.Persistence.Contratos
{
    public interface ILoteService
    {
        Task<LoteDto[]> UpdateLote(int loteId,LoteDto[] model);
        Task<bool> DeleteLote(int loteId,int eventoId);

        Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int loteId,int eventoId);
    }
}