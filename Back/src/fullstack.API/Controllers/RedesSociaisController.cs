
using Fullstack.Persistence.Contratos;
using Microsoft.AspNetCore.Mvc;
using Fullstack.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Fullstack.Application.Contratos;
using fullstack.API.Extensions;
using Fullstack.Domain;

namespace Fullstack.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RedeSociaisController : ControllerBase
    {
        private readonly IRedeSocialService _redeSocialService;
        private readonly IEventoService _eventoService;
        private readonly IPalestranteService _palestranteService;

        public RedeSociaisController(IRedeSocialService RedeSocialService, IEventoService eventoService, IPalestranteService palestranteService)
        {
            _eventoService = eventoService;
            _palestranteService = palestranteService;
            _redeSocialService = RedeSocialService;

        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> GetByEvento(int eventoId)
        {
            try
            {
                if (!await AutorEvento(eventoId))
                    return Unauthorized();
                var redeSocial = await _redeSocialService.GetAllByEventoIdAsync(eventoId);
                if (redeSocial == null) return NoContent();
                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar redeSocial. Erro: {ex.Message}");
            }
        }

        [HttpGet("palestrante")]
        public async Task<IActionResult> GetByPalestrante()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();
                var redeSocial = await _redeSocialService.GetAllByPalestranteIdAsync(palestrante.Id);
                if (redeSocial == null) return NoContent();
                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar redeSocial. Erro: {ex.Message}");
            }
        }
        [HttpPut("evento/{eventoId}")]
        public async Task<IActionResult> UpdateByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                if (!await AutorEvento(eventoId))
                    return Unauthorized();
                var redeSocial = await _redeSocialService.UpdateByEvento(eventoId, models);
                if (redeSocial == null) return NoContent();
                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar redeSocial. Erro: {ex.Message}");
            }
        }

        [HttpPut("palestrante")]
        public async Task<IActionResult> UpdateByPalestrante(RedeSocialDto[] models)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();
                var redeSocial = await _redeSocialService.UpdateByPalestrante(palestrante.Id, models);
                if (redeSocial == null) return NoContent();
                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar redeSocial. Erro: {ex.Message}");
            }
        }
        [HttpDelete("evento/{eventoId}/{redeSocialId}")]
        public async Task<IActionResult> DeleteByEvento(int redeSocialId, int eventoId)
        {
            try
            {
                if (!await AutorEvento(eventoId))
                    return Unauthorized();
                var redeSocial = await _redeSocialService.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (redeSocial == null) return NoContent();
                return await _redeSocialService.DeleteByEvento(eventoId, redeSocialId) ? Ok(new { message = "Deletado" })
                :
                 BadRequest("Falha ao Deletar o evento");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar lote. Erro: {ex.Message}");
            }
        }

        [HttpDelete("palestrante/{redeSocialId}")]
        public async Task<IActionResult> DeleteByPalestrante(int redeSocialId)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();
                var redeSocial = await _redeSocialService.GetRedeSocialPalestranteByIdsAsync(palestrante.Id, redeSocialId);
                if (redeSocial == null) return NoContent();
                return await _redeSocialService.DeleteByPalestrante(palestrante.Id, redeSocialId)
                ? Ok(new { message = $"Rede social {redeSocial.Nome} Deletada" })
                : throw new Exception("Ocorreu um poblema não especifcicado ao tentar deletar Rede Social por Palestrante");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar lote. Erro: {ex.Message}");
            }
        }

        [NonAction]
        private async Task<bool> AutorEvento(int eventoId)
        {
            var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId, false);
            if (evento == null) return false;
            return true;

        }
    }

}
