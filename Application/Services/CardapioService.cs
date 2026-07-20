using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CardapioService : IcardapioService
    {

        private readonly IcardapioRepository _cardapioRepository;
        private readonly IMapper _mapper;
        private readonly IitemCardapioRepository _itemCardapioRepository;
        public CardapioService(IcardapioRepository cardapioRepository, IMapper mapper, IitemCardapioRepository itemCardapioRepository)
        {
            _cardapioRepository = cardapioRepository;
            _mapper = mapper;
            _itemCardapioRepository = itemCardapioRepository;
        }


        public async Task<CardapioDto> Adicionar(CardapioDto cardapioDto)
        {
            var cardapio = _mapper.Map<Cardapio>(cardapioDto);
            var criar = await _cardapioRepository.Adicionar(cardapio);
            return _mapper.Map<CardapioDto>(criar);
        }

        public async Task<CardapioDto> AdicionarItem(int id, ItemCardapioDto itemCardapioDto)
        {
            var cardapio = await _cardapioRepository.BuscarPorId(id);
            if (cardapio == null)
                throw new DomainExceptionValidation("cardapio não encontrado");


            var itemCardapio = new ItemCardapio(
                itemCardapioDto.Nome,
                itemCardapioDto.Descricao, 
                itemCardapioDto.Preco, 
                itemCardapioDto.Categoria
                );


            cardapio.AdicionarItem(itemCardapio);
            await _cardapioRepository.Atualizar(cardapio);
            return _mapper.Map<CardapioDto>(cardapio);
        }

        public async Task<CardapioDto> Atualizar(int id, CardapioDto cardapioDTO)
        {
            var cardapio = await _cardapioRepository.BuscarPorId(id);

            if (cardapio == null)
                throw new DomainExceptionValidation("Cadapio não encontrado");

            cardapio.AtualizarCardapio(cardapioDTO.Nome);

            //var cardapio = _mapper.Map<Cardapio>(cardapioDTO);
            var atualizar = await _cardapioRepository.Atualizar(cardapio);
            return _mapper.Map<CardapioDto>(atualizar);
        }

        public async Task<CardapioDto> BuscarPorId(int id)
        {
            var cardapio = await _cardapioRepository.BuscarPorId(id);
            return _mapper.Map<CardapioDto>(cardapio);
        }

        public async Task<CardapioDto> BuscarPorNome(string nome)
        {
            var cardapio = await _cardapioRepository.BuscarPorNome(nome);
            return _mapper.Map<CardapioDto>(cardapio);
        }

        public async Task<IEnumerable<CardapioDto>> BuscarTodos()
        {
            var cardapio = await _cardapioRepository.BuscarTodos();
            return _mapper.Map<IEnumerable<CardapioDto>>(cardapio);
        }

        public async Task Remover(int id)
        {
           await _cardapioRepository.Remover(id);
        }

        public async Task<CardapioDto> RemoverItem(int cardapioId,int itemCardapioId)
        {
            var cardapio = await _cardapioRepository.BuscarPorId(cardapioId);
            if (cardapio == null)
                throw new DomainExceptionValidation("Cardapio não encontrado");

            cardapio.RemoverItem(itemCardapioId);
            await _itemCardapioRepository.Remover(itemCardapioId);

            return _mapper.Map<CardapioDto>(cardapio);

        }

       
    }
}
