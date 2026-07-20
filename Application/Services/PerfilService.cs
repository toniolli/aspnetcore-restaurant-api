using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IMapper _mapper;
        private readonly IperfilRepository _perfilRepository;

        public PerfilService(IMapper mapper, IperfilRepository perfilRepository)
        {
            _mapper = mapper;
            _perfilRepository = perfilRepository;
        }


        public async Task<PerfilDto> Adicionar(PerfilDto perfilDto)
        {
            var perfil = _mapper.Map<Perfil>(perfilDto);
            var criar = await _perfilRepository.Adicionar(perfil);
            return _mapper.Map<PerfilDto>(criar);

        }

        public async Task<PerfilDto> Atualizar(int id , PerfilDto perfilDto)
        {
            var perfil = await _perfilRepository.BuscarPorId(id);

            if (perfil == null)
                throw new DomainExceptionValidation("Perfil não encontrado.");


            perfil.Atualizar(perfilDto.Nome, perfilDto.Descricao);
           
            var criar = await _perfilRepository.Atualizar(perfil);
            return _mapper.Map<PerfilDto>(criar);
        }

        public async Task<PerfilDto?> BuscarPorId(int id)
        {
            var perfil = await _perfilRepository.BuscarPorId(id);
            return _mapper.Map<PerfilDto>(perfil);
        }

        public async Task<IEnumerable<PerfilDto>> BuscarTodos()
        {
            var perfil = await _perfilRepository.BuscarTodos();
            return _mapper.Map<IEnumerable<PerfilDto>>(perfil);
        }

        public async Task Remover(int id)
        {
          await _perfilRepository.Remover(id);
        }
    }
}
