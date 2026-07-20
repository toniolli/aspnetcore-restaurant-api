using Domain.Enum;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mesa
    {
        public int Id_Mesa {get; private set;}
        public int NumeroMesa {get; private set;}
        public StatusMesaEnum StatusMesa {get; private set;}
        public List<Comanda> Comandas { get; set; } = new();

        protected Mesa(){}

        //Navegação
        

        public Mesa(int numeroMesa)
        {

            ValidateDomain(numeroMesa);

            NumeroMesa = numeroMesa;
            StatusMesa = StatusMesaEnum.Livre;
            Comandas = new List<Comanda>();
                        
        }
        public void ValidateDomain(int numeroMesa)
        {
            DomainExceptionValidation.when(numeroMesa <= 0,"Numero da mesa não pode ser vazio!");
        }
        public void AtualizarMesa(int numeroMesa)
        {
            ValidateDomain(numeroMesa);

            NumeroMesa = numeroMesa;
          
        }


        public void OcuparMesa()
        {

            if (StatusMesa == StatusMesaEnum.Ocupada)
                throw new DomainExceptionValidation("Mesa já está ocupada");

            StatusMesa = StatusMesaEnum.Ocupada;

        }

        public void LiberarMesa()
        {

            if (StatusMesa == StatusMesaEnum.Livre)
                throw new DomainExceptionValidation("A mesa já está livre.");

            StatusMesa = StatusMesaEnum.Livre;

        }

    }
}
