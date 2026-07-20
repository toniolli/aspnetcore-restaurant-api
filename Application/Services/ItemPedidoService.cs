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
    public class ItemPedidoService : IitemPedidoService
    {

        private readonly IitemPedidoRepository _itemPedidoRepository;

        private readonly IMapper _mapper;

        public ItemPedidoService(IitemPedidoRepository iitemPedidoRepository,IMapper mapper)
        {
            _itemPedidoRepository = iitemPedidoRepository;
            _mapper = mapper;    
        }


        public async Task<ItemPedidoDto> AlterarObservacao(int id, string? observacao)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);
            if (item == null)
                throw new DomainExceptionValidation("Item não encontrado");

            item.AlterarObservacao(observacao);

            await _itemPedidoRepository.Atualizar(item);
            return _mapper.Map<ItemPedidoDto>(item);
        }

        public async Task<ItemPedidoDto> AlterarQuantidade(int id, int quantidade)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);
            if (item == null)
                throw new DomainExceptionValidation("Item não encontrado");

            item.AlterarQuantidade(quantidade);
            await _itemPedidoRepository.Atualizar(item);

            return _mapper.Map<ItemPedidoDto>(item);
        }

        public async Task<ItemPedidoDto> Atualizar(int id ,ItemPedidoDto itemPedidoDto)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);

            if (item == null)
                throw new DomainExceptionValidation("Itens do pedido não encontrado");

            item.AtualizarItemPedido(itemPedidoDto.ItemCardapioId, itemPedidoDto.SetorProducaoId,itemPedidoDto.Quantidade, itemPedidoDto.Observacao);

            var atualizar = await _itemPedidoRepository.Atualizar(item);
            return _mapper.Map<ItemPedidoDto>(item);

        }

        public async Task<IEnumerable<ItemPedidoDto>> BuscarItensAbertos()
        {
            
            var item = await _itemPedidoRepository.BuscarItensAbertos();
            return _mapper.Map<IEnumerable<ItemPedidoDto>>(item);

        }

        public async Task<ItemPedidoDto> BuscarPorId(int id)
        {
            
            var item = await _itemPedidoRepository.BuscarPorId(id);
            return _mapper.Map<ItemPedidoDto>(item);
        }

        public async Task<IEnumerable<ItemPedidoDto>> BuscarPorPedidoId(int pedidoId)
        {
            var item = await _itemPedidoRepository.BuscarPorPedidoId(pedidoId);
            return _mapper.Map<IEnumerable<ItemPedidoDto>>(item);
        }

        public async Task<IEnumerable<ItemPedidoDto>> BuscarPorStatus(StatusItemPedidoEnum status)
        {
           var item = await _itemPedidoRepository.BuscarPorStatus(status);
            return _mapper.Map<IEnumerable<ItemPedidoDto>>(item);
        }

        public async Task<IEnumerable<ItemPedidoDto>> BuscarTodos()
        {
            var item = await _itemPedidoRepository.BuscarTodos();
            return _mapper.Map<IEnumerable<ItemPedidoDto>>(item);
        }

        public async Task<ItemPedidoDto> CancelarItem(int id)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);
            if (item == null)
                throw new  DomainExceptionValidation("Item não encontrado");

            item.Cancelar();
            await _itemPedidoRepository.Atualizar(item);

            return _mapper.Map<ItemPedidoDto>(item);

        }

        public async Task<ItemPedidoDto> FinalizarItem(int id)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);

            if (item == null)
                throw new DomainExceptionValidation("Item não encontrado");

            item.Finalizar();
            await _itemPedidoRepository.Atualizar(item);
            return _mapper.Map<ItemPedidoDto>(item);

        }

        public async Task<ItemPedidoDto> IniciarPreparo(int id)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);
            if (item == null)
                throw new DomainExceptionValidation("Item não encontrado");

            item.IniciarPreparo();
            await _itemPedidoRepository.Atualizar(item);
            return _mapper.Map<ItemPedidoDto>(item);
        }

        public async Task<ItemPedidoDto> MarcarComoPronto(int id)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);
            if (item == null)
                throw new DomainExceptionValidation("Item não encontrado");

            item.MarcarComoPronto();
            await _itemPedidoRepository.Atualizar(item);
            return _mapper.Map<ItemPedidoDto>(item);
        }

        public async Task<decimal> ObterTotal(int id)
        {
            var item = await _itemPedidoRepository.BuscarPorId(id);

            if (item == null)
                throw new DomainExceptionValidation("Item não encontrado!");


            return item.ObterTotal();
        }

    }
}
