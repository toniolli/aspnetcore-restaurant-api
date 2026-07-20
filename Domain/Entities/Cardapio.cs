using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cardapio
    {
        public int Id_Cardapio { get; private set; }
        public string Nome { get; private set; }
        public List<ItemCardapio> Itens { get; private set; } = new();

        protected Cardapio() { }

        public Cardapio(string nome)
        {
            Nome = nome;
        }


        public void AtualizarCardapio(string nome)
        {
            Nome = nome;
        }

        public void AdicionarItem(ItemCardapio itemNovo)
        {
            if (itemNovo == null)
                throw new DomainExceptionValidation("Item do cardápio é obrigatório");

            if (!Itens.Any(i => i.Id_ItemCardapio == itemNovo.Id_ItemCardapio))
            {
                Itens.Add(itemNovo);
            }
        }

        public void RemoverItem(int itemCardapioId)
        {

            Console.WriteLine($"Itens carregados:{Itens.Count}");

            foreach(var itemLista in Itens)
            {
                Console.WriteLine(itemLista.Id_ItemCardapio);
            }

            var item = Itens.FirstOrDefault(i => i.Id_ItemCardapio == itemCardapioId);


            if (item == null)
                throw new DomainExceptionValidation("Item não encontrado no cardápio");

            Itens.Remove(item);
        }

    }
}
