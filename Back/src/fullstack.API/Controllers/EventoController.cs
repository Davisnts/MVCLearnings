
using Fullstack.Domain;
using Fullstack.Persistence;
using Fullstack.Persistence.Contexto;
using Fullstack.Persistence.Contratos;
using Microsoft.AspNetCore.Mvc;
namespace Fullstack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly FullstackContext _context;
        private readonly IEventoService _eventoService;
        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
            
        }

        [HttpGet]
        public async IEnumerable<Evento> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public Evento GetByGet(int id)
        {
            return _context.Eventos.FirstOrDefault(evento => evento.Id == id);

        }

        [HttpPost]
        public string Post()
        {
            return "Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Exemplo de put com id = {id}";
        }
    }
}
 