using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ComandaService : IcomandaService
    {

        private readonly IMapper _mapper;
        private readonly IcomandaRepository _comandaRepository;
        private readonly ImesaRepository _mesaRepository;


        public ComandaService(IMapper mapper, IcomandaRepository comandaRepository,ImesaRepository mesaRepository)
        {
            _mapper = mapper;
            _comandaRepository = comandaRepository;
            _mesaRepository = mesaRepository;
        }

        public async Task<ComandaDto> Adicionar(ComandaDto comandaDTO)
        {
            var mesa = await _mesaRepository.BuscarPorId(comandaDTO.MesaId);

            if (mesa == null)
                throw new DomainExceptionValidation("Mesa não encontrada");

            if (mesa.StatusMesa == StatusMesaEnum.Ocupada)
                throw new DomainExceptionValidation("Mesa já está ocupada.");

            mesa.OcuparMesa();
            await _mesaRepository.Atualizar(mesa);

            var comanda = _mapper.Map<Comanda>(comandaDTO);
            await _comandaRepository.Adicionar(comanda);
            return _mapper.Map<ComandaDto>(comanda);
        }
        public async Task<ComandaDto> Atualizar(int id, ComandaDto comandaDTO)
        {
            var comanda = await _comandaRepository.BuscarPorId(id);

            if (comanda == null)
                throw new Exception("Comanda não encontrada!");

            comanda.AtualizarComanda(comandaDTO.MesaId);

            await _comandaRepository.Atualizar(comanda);
            return _mapper.Map<ComandaDto>(comanda);
        }
        public async Task<ComandaDto> BuscarComandaAbertaPorMesa(int mesaId)
        {
            var comanda = await _comandaRepository.BuscarComandaAbertaPorMesa(mesaId);
            return _mapper.Map<ComandaDto>(comanda);
        }
        public async Task<IEnumerable<ComandaDto>> BuscarComandasFechadas()
        {
           var comanda = await _comandaRepository.BuscarComandasFechadas(); 
            return _mapper.Map<IEnumerable<ComandaDto>>(comanda);
        }
        public async Task<ComandaDto> BuscarPorId(int id)
        {
            var comanda = await _comandaRepository.BuscarPorId(id);
            return _mapper.Map<ComandaDto>(comanda);
        }
        public async Task<IEnumerable<ComandaDto>> BuscarTodos()
        {
            var comanda = await _comandaRepository.BuscarTodos();
            return _mapper.Map<IEnumerable<ComandaDto>>(comanda);

        }

        public async Task<ComandaDto> FecharComanda(int comandaId)
        {
            var comanda = await _comandaRepository.BuscarPorId(comandaId);
            if (comanda == null)
                throw new DomainExceptionValidation("Comanda não encontrada");

            comanda.FecharComanda();
            
            var mesa = await _mesaRepository.BuscarPorId(comanda.MesaId);

            if (mesa != null && mesa.StatusMesa == StatusMesaEnum.Ocupada)
            {
                Console.WriteLine($"Mesa: {mesa.Id_Mesa}");
                Console.WriteLine($"Status: {mesa.StatusMesa}");
                mesa.LiberarMesa();
                await _mesaRepository.Atualizar(mesa);
            }
            await _comandaRepository.Atualizar(comanda);
            
            return _mapper.Map<ComandaDto>(comanda);
        }

        public async Task Remover(int id)
        {
           await _comandaRepository.Remover(id);
        }    
    }
}
