using AutoMapper;
using Fullstack.Application.Contratos;
using Fullstack.Application.Dtos;
using Fullstack.Domain;
using Fullstack.Persistence.Contratos;

namespace Fullstack.Application
{
    public class RedeSocialService : IRedeSocialService
    {
        private readonly IRedeSocialPersist _redeSocialPersist;
        private readonly IMapper mapper;
        public RedeSocialService(IRedeSocialPersist redeSocialPersist, IMapper mapper)
        {
            _redeSocialPersist = redeSocialPersist;
            this.mapper = mapper;

        }
        public async Task AddRedeSocial(int Id, RedeSocialDto model, bool isEvento)
        {
            try
            {
                var redeSocial = mapper.Map<RedeSocial>(model);


                if (isEvento)
                {
                    redeSocial.EventoId = Id;
                    redeSocial.PalestranteId = null;
                }

                else
                {
                    redeSocial.PalestranteId = Id;
                    redeSocial.EventoId = null;
                }

                _redeSocialPersist.Add<RedeSocial>(redeSocial);

                await _redeSocialPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> UpdateByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                var redeSocials = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);
                if (redeSocials == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(eventoId, model, true);
                    }
                    else
                    {
                        var redeSocial = redeSocials.FirstOrDefault(redeSocial => redeSocial.Id == model.Id);
                        model.EventoId = eventoId;
                        mapper.Map(model, redeSocial);
                        _redeSocialPersist.Update<RedeSocial>(redeSocial);
                        await _redeSocialPersist.SaveChangesAsync();
                    }
                }
                var retorno = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);

                return mapper.Map<RedeSocialDto[]>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<RedeSocialDto[]> UpdateByPalestrante(int palestranteId, RedeSocialDto[] models)
        {
            try
            {
                var redeSocials = await _redeSocialPersist.GetAllByPalestranteIdAsync(palestranteId);
                if (redeSocials == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(palestranteId, model, false);
                    }
                    else
                    {
                        var redeSocial = redeSocials.FirstOrDefault(redeSocial => redeSocial.Id == model.Id);
                        model.PalestranteId = palestranteId;
                        mapper.Map(model, redeSocial);
                        _redeSocialPersist.Update<RedeSocial>(redeSocial);
                        await _redeSocialPersist.SaveChangesAsync();
                    }
                }
                var retorno = await _redeSocialPersist.GetAllByPalestranteIdAsync(palestranteId);

                return mapper.Map<RedeSocialDto[]>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialPersist.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (redeSocial == null) throw new Exception("Falha ao deletar redeSocial");

                _redeSocialPersist.Delete<RedeSocial>(redeSocial);
                return await _redeSocialPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByPalestrante(int palestranteId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialPersist.GetRedeSocialPalestranteByIdsAsync(palestranteId, redeSocialId);
                if (redeSocial == null) throw new Exception("Falha ao deletar redeSocial");

                _redeSocialPersist.Delete<RedeSocial>(redeSocial);
                return await _redeSocialPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId)
        {
            try
            {
                var redeSocials = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);
                if (redeSocials == null) return null;
                var resultado = mapper.Map<RedeSocialDto[]>(redeSocials);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto[]> GetAllByPalestranteIdAsync(int palestranteId)
        {
            try
            {
                var redeSocials = await _redeSocialPersist.GetAllByPalestranteIdAsync(palestranteId);
                if (redeSocials == null) return null;
                var resultado = mapper.Map<RedeSocialDto[]>(redeSocials);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto> GetAllByEventoIdsAsync(int eventoId, int id)
        {
            try
            {
                var redeSocial = await _redeSocialPersist.GetRedeSocialEventoByIdsAsync(eventoId, id);
                if (redeSocial == null) return null;
                var resultado = mapper.Map<RedeSocialDto>(redeSocial);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync(int eventoId, int id)
        {
            try
            {
                var redeSocial = await _redeSocialPersist.GetRedeSocialEventoByIdsAsync(eventoId, id);
                if (redeSocial == null) return null;
                var resultado = mapper.Map<RedeSocialDto>(redeSocial);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RedeSocialDto> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int id)
        {
            try
            {
                var redeSocial = await _redeSocialPersist.GetRedeSocialPalestranteByIdsAsync(palestranteId, id);
                if (redeSocial == null) return null;
                var resultado = mapper.Map<RedeSocialDto>(redeSocial);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}