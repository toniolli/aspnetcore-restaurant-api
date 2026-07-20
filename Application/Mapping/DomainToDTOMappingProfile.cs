using Application.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Mesa, MesaDto>().ReverseMap();
            CreateMap<Comanda, ComandaDto>().ReverseMap();
            CreateMap<Pedido, PedidoDto>().ReverseMap();
            CreateMap<ItemPedido, ItemPedidoDto>().ReverseMap();
            CreateMap<Cardapio, CardapioDto>().ReverseMap();
            CreateMap<ItemCardapio, ItemCardapioDto>().ReverseMap();
            CreateMap<SetorProducao, SetorProducaoDto>().ReverseMap();
            CreateMap<Perfil, PerfilDto>().ReverseMap();
            CreateMap<Permissao,PermissaoDto>().ReverseMap();
            CreateMap<PerfilPermissao, PerfilPermissaoDto>().ReverseMap();
            CreateMap<UsuarioPerfil, UsuarioPerfilDto>().ReverseMap();
        }
    }
}
