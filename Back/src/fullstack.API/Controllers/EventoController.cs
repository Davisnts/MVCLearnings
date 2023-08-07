using fullstack.API.Data;
using fullstack.API.Models;
using Microsoft.AspNetCore.Mvc;
namespace fullstack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;
        public EventoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;

        }

        [HttpGet("{id}")]
        public Evento GetByGet(int id)
        {
            return _context.Eventos.FirstOrDefault(evento => evento.EventoId == id);

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
 