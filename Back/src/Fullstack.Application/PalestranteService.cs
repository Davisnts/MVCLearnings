
using AutoMapper;
using Fullstack.Application.Dtos;
using Fullstack.Domain;
using Fullstack.Persistence.Contratos;
using Fullstack.Persistence.Models;


namespace Fullstack.Application
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestrantePersist _palestrantePersist;
        private readonly IMapper mapper;
        public PalestranteService(IPalestrantePersist palestrantePersist, IMapper mapper)
        {
            _palestrantePersist = palestrantePersist;
            this.mapper = mapper;
        }
        public async Task<PalestranteDto> AddPalestrante(int userId, PalestranteAddDto model)

        {
            try
            {
                var palestrante = mapper.Map<Palestrante>(model);
                palestrante.UserId = userId;

                _palestrantePersist.Add<Palestrante>(palestrante);
                if (await _palestrantePersist.SaveChangesAsync())
                {
                    var retorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
                    return mapper.Map<PalestranteDto>(retorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<PalestranteDto> UpdatePalestrante(int userId, int eventoId, PalestranteUpdateDto model)
        {
            try
            {
                var palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
                if (palestrante == null) return null;

                model.Id = palestrante.Id;
                model.UserId = userId;
                mapper.Map(model, palestrante);
                _palestrantePersist.Update<Palestrante>(palestrante);

                if (await _palestrantePersist.SaveChangesAsync())
                {
                    var retorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
                    return mapper.Map<PalestranteDto>(retorno);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestrantePersist.GetAllPalestrantesAsync(pageParams, includeEventos);
                if (palestrantes == null) return null;
                var resultado = mapper.Map<PageList<PalestranteDto>>(palestrantes);
                resultado.CurrentPage = palestrantes.CurrentPage;
                resultado.PageSize = palestrantes.PageSize;
                resultado.TotalCount = palestrantes.TotalCount;
                resultado.TotalPages = palestrantes.TotalPages;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false)
        {
            try
            {
                var palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, includeEventos);
                if (palestrante == null) return null;

                var resultado = mapper.Map<PalestranteDto>(palestrante);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}