using Application.Dto;
using Application.Interfaces;
using Application.Mapping;
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
    public class PermissaoService : IPermissaoService
    {

        private readonly IMapper _mapper;
        private readonly IpermissaoRepository _permissaoRepository;

        public PermissaoService(IMapper mapper, IpermissaoRepository permissaoRepository)
        {
            _mapper = mapper;
            _permissaoRepository = permissaoRepository;
        }

        public async Task<PermissaoDto> Adicionar(PermissaoDto permissaoDto)
        {
            var permissao = _mapper.Map<Permissao>(permissaoDto);
            var criar = await _permissaoRepository.Adicionar(permissao);
            return _mapper.Map<PermissaoDto>(criar);
        }

        public async Task<PermissaoDto> Atualizar(int id, PermissaoDto permissaoDto)
        {
            var permissao = await _permissaoRepository.BuscarPorId(id);

            if (permissao == null)
                throw new DomainExceptionValidation("permissao não encontrada.");

            permissao.Atualizar(permissaoDto.Nome, permissaoDto.Descricao);

            return _mapper.Map<PermissaoDto>(permissao);

        }

        public async Task<PermissaoDto?> BuscarPorId(int id)
        {
            var permissao = await _permissaoRepository.BuscarPorId(id);
            return _mapper.Map<PermissaoDto>(permissao);

        }

        public async Task<IEnumerable<PermissaoDto>> BuscarTodos()
        {
            var permissao = await _permissaoRepository.BuscarTodos();   
            return _mapper.Map<IEnumerable<PermissaoDto>>(permissao);
        }

        public async Task Remover(int id)
        {
            await _permissaoRepository.Remover(id); 
        }
    }
}
