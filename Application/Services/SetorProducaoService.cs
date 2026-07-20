using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SetorProducaoService : IsetorProducaoService
    {

        private readonly IsetorProducaoRepository _setorProducaoRespository;
        private readonly IMapper _mapper;

        public SetorProducaoService(IsetorProducaoRepository setorProducaoRepository, IMapper mapper)
        {
            _setorProducaoRespository = setorProducaoRepository;
            _mapper = mapper;
        }


        public async Task<SetorProducaoDto> Adicionar(SetorProducaoDto setorDTO)
        {
            var setor = _mapper.Map<SetorProducao>(setorDTO);
            var criar = await _setorProducaoRespository.Adicionar(setor);
            return _mapper.Map<SetorProducaoDto>(criar);

        }

        public async Task<SetorProducaoDto> Atualizar(int id,SetorProducaoDto setorDTO)
        {
            var setor = await _setorProducaoRespository.BuscarPorId(id);

            if (setor == null)
                throw new Exception("Setor não encontrado");


            setor.AtualizarSetorProducao(setorDTO.Nome);
            //var setor = _mapper.Map<SetorProducao>(setorDTO);
            var criar = await _setorProducaoRespository.Atualizar(setor);
            return _mapper.Map<SetorProducaoDto>(criar);
        }

        public async Task<IEnumerable<SetorProducaoDto>> BuscarAtivos()
        {
            var setor = await _setorProducaoRespository.BuscarAtivos();
            return _mapper.Map<IEnumerable<SetorProducaoDto>>(setor);
        }

        public async Task<SetorProducaoDto> BuscarPorId(int id)
        {
            var setor = await _setorProducaoRespository.BuscarPorId(id);
            return _mapper.Map<SetorProducaoDto>(setor);
        }

        public async  Task<SetorProducaoDto> BuscarPorNome(string nome)
        {
            var setor = await _setorProducaoRespository.BuscarPorNome(nome);
            return _mapper.Map<SetorProducaoDto>(setor);
        }

        public async Task<IEnumerable<SetorProducaoDto>> BuscarTodos()
        {
            var setor = await _setorProducaoRespository.BuscarTodos();
            return _mapper.Map<IEnumerable<SetorProducaoDto>>(setor);
        }

        public async Task Remover(int id)
        {
            await _setorProducaoRespository.Remover(id);    
        }



    }
}
