using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorteioRifa
{
    [System.Serializable]
    class Participantes
    {
        public int numero;
        public string nome;
        public string endereco;
        public string contato;

        public Participantes(int numero, string nome, string endereco, string contato)
        {
            this.numero = numero;
            this.nome = nome;
            this.endereco = endereco;
            this.contato = contato;
        }
    }
}
