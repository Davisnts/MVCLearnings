using Fullstack.Persistence.Contratos;
using Microsoft.AspNetCore.Mvc;
using Fullstack.Application.Dtos;
using fullstack.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Fullstack.Persistence.Models;

namespace Fullstack.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PalestranteController : ControllerBase
    {
        private readonly IPalestranteService _palestranteService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAccountService _accountService;
        public PalestranteController(IPalestranteService palestranteService, IWebHostEnvironment hostEnvironment, IAccountService accountService)
        {
            _palestranteService = palestranteService;
            _accountService = accountService;
            _hostEnvironment = hostEnvironment;

        }

        [HttpGet("all")]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var palestrantes = await _palestranteService.GetAllPalestrantesAsync(pageParams);
                if (palestrantes == null) return NoContent();
                Response.AddPagination(palestrantes.CurrentPage, palestrantes.PageSize, palestrantes.TotalCount, palestrantes.TotalPages);
                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPalestranteByUserIdAsync(int id)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(id);
                if (palestrante == null) return NoContent();
                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }
        [HttpGet()]
        public async Task<IActionResult> GetPalestrantes()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId(), true);
                if (palestrante == null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(PalestranteAddDto model)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId(), false);
                if (palestrante == null)
                    palestrante = await _palestranteService.AddPalestrante(User.GetUserId(), model);

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(PalestranteUpdateDto model)
        {
            try
            {
                var palestrante = await _palestranteService.UpdatePalestrante(User.GetUserId(), model);
                if (palestrante == null) return NoContent();
                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }

    }
}