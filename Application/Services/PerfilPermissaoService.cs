using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PerfilPermissaoService : IperfilPermissaoService
    {

        private readonly IMapper _mapper;
        private readonly IperfilPermissaoRepository _perfilPermissaoRepository;

        public PerfilPermissaoService(IMapper mapper, IperfilPermissaoRepository perfilPermissaoRepository)
        {
            _mapper = mapper;
            _perfilPermissaoRepository = perfilPermissaoRepository;
        }

        public async Task<IEnumerable<PerfilDto>> BuscarPerfisPermissao(int permissaoId)
        {
            var perfilPermissao = await _perfilPermissaoRepository.BuscarPerfisPermissao(permissaoId);
            return _mapper.Map<IEnumerable<PerfilDto>>(perfilPermissao);
        }

        public async Task<IEnumerable<PermissaoDto>> BuscarPermissoesPerfil(int perfilId)
        {
           var perfilPermissao = await _perfilPermissaoRepository.BuscarPermissoesPerfil(perfilId);
            return _mapper.Map<IEnumerable<PermissaoDto>>(perfilPermissao);

        }

        public async Task<PerfilPermissaoDto?> Desvincular(int perfilId, int permissaoId)
        {
            var perfilPermissao = await _perfilPermissaoRepository.Desvincular(perfilId,permissaoId);

            if(perfilPermissao == null)
                return null;    

            return _mapper.Map<PerfilPermissaoDto>(perfilPermissao);

        }

        public async Task<PerfilPermissaoDto> Vincular(PerfilPermissaoDto perfilPermissaoDto)
        {
            var existeVinculo = await _perfilPermissaoRepository.ExisteVinculo(perfilPermissaoDto.PerfilId, perfilPermissaoDto.PermissaoId);

            if (existeVinculo)
                throw new DomainExceptionValidation("Prmissao ja vinculada ao perfil");

            var entidade = _mapper.Map<PerfilPermissao>(perfilPermissaoDto);
            var resultado = await _perfilPermissaoRepository.Vincular(entidade);

            return _mapper.Map<PerfilPermissaoDto>(resultado);
        }
    }
}
