using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ItemCardapioService : IitemCardapioService
    {

        private readonly IitemCardapioRepository _itemCardapioRepository;
        private readonly IMapper _mapper;


        public ItemCardapioService(IitemCardapioRepository itemCardapioRepository,IMapper mappper)
        {
            _itemCardapioRepository = itemCardapioRepository;
            _mapper = mappper;   
        }
        

        public async Task<ItemCardapioDto> AlterarCategoria(int id, CategoriaEnum categoria)
        {
            var item = await _itemCardapioRepository.BuscarPorId(id);
                if (item == null)
                    throw new DomainExceptionValidation("Item cardapio não encontrado!");

            item.AlterarCategoria(categoria);
            
            await _itemCardapioRepository.Atualizar(item);
            return _mapper.Map<ItemCardapioDto>(item);


        }

        public async Task<ItemCardapioDto> AlterarDescricao(int id, string descricao)
        {
            var item = await _itemCardapioRepository.BuscarPorId(id);
            if (item == null)
                throw new DomainExceptionValidation("Item cardapio não encontrado");

            item.AlterarDescricao(descricao);
            await _itemCardapioRepository.Atualizar(item);
            return _mapper.Map<ItemCardapioDto>(item);
        }

        public  async Task<ItemCardapioDto> AlterarNome(int id, string nome)
        {
            var item = await _itemCardapioRepository.BuscarPorId(id);
            if (item == null)
                throw new DomainExceptionValidation("Item cardapio não encontrado");

            item.AlterarNome(nome);
            await _itemCardapioRepository.Atualizar(item);
            return _mapper.Map<ItemCardapioDto>(item);
        }

        public async  Task<ItemCardapioDto> AlterarPreco(int id, decimal preco)
        {
            var item = await _itemCardapioRepository.BuscarPorId(id);
            if (item == null)
                throw new DomainExceptionValidation("Item cardapio não encontrado");

            item.AlterarPreco(preco);
            await _itemCardapioRepository.Atualizar(item);
            return _mapper.Map<ItemCardapioDto>(item);
        }

        public async Task<ItemCardapioDto> Atualizar(int id,ItemCardapioDto itemDTO)
        {
            var item = await _itemCardapioRepository.BuscarPorId(id);

            if (item == null)
                throw new DomainExceptionValidation("Itens do cardapio não encontrado");

            item.AtualizarItemCardapio(itemDTO.Nome,item.Descricao,itemDTO.Preco,item.Categoria);
           // var item = _mapper.Map<ItemCardapio>(itemDTO);
            var atualizar = await _itemCardapioRepository.Atualizar(item);
            return _mapper.Map<ItemCardapioDto>(atualizar);
        }

        public async Task<IEnumerable<ItemCardapioDto>> BuscarDisponiveis()
        {
            var item = await _itemCardapioRepository.BuscarDisponiveis();
            return _mapper.Map<IEnumerable<ItemCardapioDto>>(item);    

        }

        public async Task<IEnumerable<ItemCardapioDto>> BuscarPorCategoria(CategoriaEnum categoria)
        {
            var item = await _itemCardapioRepository.BuscarPorCategoria(categoria);
            return _mapper.Map<IEnumerable<ItemCardapioDto>>(item);
        }

        public async Task<ItemCardapioDto> BuscarPorId(int id)
        {
            var item = await _itemCardapioRepository.BuscarPorId(id);
            return _mapper.Map<ItemCardapioDto>(item);

        }

        public async Task<ItemCardapioDto> BuscarPorNome(string nome)
        {
            var item = await _itemCardapioRepository.BuscarPorNome(nome);
            return _mapper.Map<ItemCardapioDto>(item);
        }

        public async Task<IEnumerable<ItemCardapioDto>> BuscarTodos()
        {
           var item = await _itemCardapioRepository.BuscarTodos();
            return _mapper.Map<IEnumerable<ItemCardapioDto>>(item);
        }
  
    }
}
