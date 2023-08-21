
using Fullstack.Persistence.Contratos;
using Microsoft.AspNetCore.Mvc;
using Fullstack.Application.Dtos;

namespace Fullstack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;
        public LotesController(ILoteService LoteService)
        {
            _loteService = LoteService;

        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var lotes = await _loteService.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return NoContent();
                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar lotes. Erro: {ex.Message}");
            }
        }
        [HttpPut("{eventoId}")]
        public async Task<IActionResult> UpdateLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await _loteService.UpdateLote(eventoId, models);
                if (lotes == null) return NoContent();
                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar lotes. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int loteId,int eventoId)
        {
             try
            {
                var lote = await _loteService.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) return NoContent();
                return await _loteService.DeleteLote(lote.EventoId,lote.Id) ? Ok(new { message = "Deletado"})
                :
                 BadRequest("Falha ao Deletar o evento");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar lote. Erro: {ex.Message}");
            }
        }
    }
}