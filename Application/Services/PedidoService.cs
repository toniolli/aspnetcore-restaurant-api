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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidoService : IpedidoService
    {

        private readonly IMapper _mapper;
        private readonly IpedidoRepository _pedidoRepository;
        private readonly IitemCardapioRepository _itemCardapioRepository;

        public PedidoService(IMapper mapper, IpedidoRepository pedidoRepository, IitemCardapioRepository itemCardapioRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _itemCardapioRepository = itemCardapioRepository;
        }


        public async Task<PedidoDto> Adicionar(PedidoDto pedidoDTO)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDTO);
            var criar = await _pedidoRepository.Adicionar(pedido);
            return _mapper.Map<PedidoDto>(criar);
        }


        public async Task<PedidoDto> AdicionarItemPedido(int pedidoId,ItemPedidoDto itemPedidoDto)
        {
            var pedido = await _pedidoRepository.BuscarPorId(pedidoId);

            if (pedido == null)
                throw new DomainExceptionValidation("Pedido não encontrado");
            var itemCardapio = await _itemCardapioRepository.BuscarPorId(itemPedidoDto.ItemCardapioId);


            var itemPedido = new ItemPedido(
                itemPedidoDto.ItemCardapioId,
                itemPedidoDto.SetorProducaoId,
                itemCardapio.Nome,//snapShot
                itemCardapio.Preco,//snapShot
                itemPedidoDto.Quantidade,
                itemPedidoDto.Observacao
            );

            pedido.AdicionarItem(itemPedido);

            await _pedidoRepository.Atualizar(pedido);

            return _mapper.Map<PedidoDto>(pedido);
        }



        public async Task<PedidoDto> Atualizar(int id,PedidoDto pedidoDTO)
        {
            var pedido = await _pedidoRepository.BuscarPorId(id);

            if (pedido == null)
                throw new DomainExceptionValidation("Pedido não encontrado!");

            pedido.AtualizarPedido(pedidoDTO.MesaId, pedidoDTO.ComandaId);

            
            var atualizar = await _pedidoRepository.Atualizar(pedido);
            return _mapper.Map<PedidoDto>(atualizar);

        }

        public async Task<IEnumerable<PedidoDto>> BuscarPedidosAbertos()
        {    
            var pedido = await _pedidoRepository.BuscarPedidosAbertos();
            return _mapper.Map<IEnumerable<PedidoDto>>(pedido);
        }

        public async Task<IEnumerable<PedidoDto>> BuscarPorComandaId(int comandaId)
        {
            var pedido = await _pedidoRepository.BuscarPorComandaId(comandaId);
            return _mapper.Map<IEnumerable<PedidoDto>>(pedido);
        }

        public async Task<PedidoDto> BuscarPorId(int id)
        {
            var pedido = await _pedidoRepository.BuscarPorId(id);
            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task<IEnumerable<PedidoDto>> BuscarPorMesaId(int mesaId)
        {
            var pedido = await _pedidoRepository.BuscarPorMesaId(mesaId);
            return _mapper.Map<IEnumerable<PedidoDto>>(pedido);
        }

        public async Task<IEnumerable<PedidoDto>> BuscarPorStatus(StatusPedidoEnum status)
        {
            var pedido = await _pedidoRepository.BuscarPorStatus(status);
            return _mapper.Map<IEnumerable<PedidoDto>>(pedido);

        }

        public async Task<IEnumerable<PedidoDto>> BuscarTodos()
        {
           var pedido = await _pedidoRepository.BuscarTodos();
            return _mapper.Map<IEnumerable<PedidoDto>>(pedido);
        }

        public async Task<PedidoDto> CancelarPedido(int id)
        {

            var pedido = await _pedidoRepository.BuscarPorId(id);

            if (pedido == null)
                throw new DomainExceptionValidation("Pedido não encontrado!");

            pedido.CancelarPedido();

            await _pedidoRepository.Atualizar(pedido);

            return _mapper.Map<PedidoDto>(pedido);



        }

        public async Task<PedidoDto> FinalizarPedido(int id)
        {
            
            var pedido = await _pedidoRepository.BuscarPorId(id);

            if (pedido == null)
                throw new DomainExceptionValidation("Pedido não encontrado!");

            pedido.FinalizarPedido();

            await _pedidoRepository.Atualizar(pedido);

            return _mapper.Map<PedidoDto>(pedido);

        }

        public async Task Remover(int id)
        {
            await _pedidoRepository.Remover(id);
        }

        public async Task<decimal> TotalPedido(int id)
        {
            var pedido = await _pedidoRepository.BuscarPorId(id);

            if (pedido == null)
                throw new DomainExceptionValidation("Pedido não encontrado!");


            return pedido.TotalPedido();
        }

        public async Task<PedidoDto> RemoverItemPedido(int pedidoId, int itemPedidoId)
        {
            var pedido = await _pedidoRepository.BuscarPorId(pedidoId);

            if (pedido == null)
                throw new DomainExceptionValidation("Pedido não encontrado");

            pedido.RemoverItem(itemPedidoId);

            await _pedidoRepository.Atualizar(pedido);

            return _mapper.Map<PedidoDto>(pedido);
        }
    }
}
