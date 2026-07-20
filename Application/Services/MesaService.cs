using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Dto;
using Domain.Entities;
using Domain.Enum;

namespace Application.Services
{
    public class MesaService : ImesaService
    {
        private readonly IMapper _mapper;
        private readonly ImesaRepository _mesaRepository;

        public MesaService(IMapper mapper, ImesaRepository mesaRepository)
        {
            _mapper = mapper;
            _mesaRepository = mesaRepository;
        }

        public async Task<MesaDto> Adicionar(MesaDto mesaDTO)
        {
            var m = _mapper.Map<Mesa>(mesaDTO);
            var criar = await _mesaRepository.Adicionar(m);
            return _mapper.Map<MesaDto>(criar);
        }

        public async Task<MesaDto> Atualizar(int id, MesaDto mesaDTO)
        {
            var mesa = await _mesaRepository.BuscarPorId(id);

            if (mesa == null)
                throw new Exception("Mesa não encontrada!");

            mesa.AtualizarMesa(mesaDTO.NumeroMesa);

            var atualizar = await _mesaRepository.Atualizar(mesa);
            return _mapper.Map<MesaDto>(atualizar);
        }

        

        public async Task<MesaDto> BuscarPorId(int id)
        {
            var m = await _mesaRepository.BuscarPorId(id);
            return _mapper.Map<MesaDto>(m);
        }

        public async Task<MesaDto> BuscarPorNumeroMesa(int numero)
        {
            var m = await _mesaRepository.BuscarPorNumeroMesa(numero);
            return _mapper.Map<MesaDto>(m);
        }

        public async Task<IEnumerable<MesaDto>> BuscarTodos()
        {
           var m = await _mesaRepository.BuscarTodos();
           return _mapper.Map<IEnumerable<MesaDto>>(m);
        }




        public async Task<MesaDto> LiberarMesa(int numeroMesa)
        {
            var mesa = await _mesaRepository.BuscarPorNumeroMesa(numeroMesa);
            if (mesa == null)
                throw new Exception("Mesa não encontrada!");

            mesa.LiberarMesa();

            await _mesaRepository.Atualizar(mesa);

            return _mapper.Map<MesaDto>(mesa);
        }

        public async Task<MesaDto> OcuparMesa(int numeroMesa)
        {
            var mesa = await _mesaRepository.BuscarPorNumeroMesa(numeroMesa);

            if (mesa == null)
                throw new Exception("Mesa não encontrada!");

            mesa.OcuparMesa();

            await _mesaRepository.Atualizar(mesa);


            return _mapper.Map<MesaDto>(mesa);
        }

        public async Task Remover(int id)
        {
          await _mesaRepository.Remover(id);
        }
    }
}
