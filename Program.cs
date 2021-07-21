using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SorteioRifa
{
    class Program
    {
        static List<Participantes> participantes = new List<Participantes>();
        enum Menu { Sortear = 1, Lista, Adicionar, Remover, Busca, Sair }

        static void Main(string[] args)
        {
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Carregar();
                Console.Clear();
                Console.WriteLine("CHÁ DE FRALDA SAMUEL - SORTEIO RIFA\n");
                Console.WriteLine("1 - SORTEAR NÚMERO\n2 - Lista de participantes\n3 - Adicionar participante\n4 - Remover participante\n5 - Busca\n6 - Sair do programa\n");
                
                try
                {
                    int opcao = int.Parse(Console.ReadLine());
                    Menu opcaoselecionada = (Menu)opcao;
                    switch (opcaoselecionada)
                    {
                        case Menu.Sortear:
                            Sortear();
                            break;
                        case Menu.Lista:
                            Listagem();
                            break;
                        case Menu.Adicionar:
                            Adicionar();
                            break;
                        case Menu.Remover:
                            Remover();
                            break;
                        case Menu.Busca:
                            Busca();
                            break;
                        case Menu.Sair:
                            escolheuSair = true;
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Endereço Inválido");
                    Console.ReadLine();
                }
            }
        }

        static void Sortear()
        {
            Console.Clear();
            Console.WriteLine("APERTER ENTER PARA SORTEAR UM NÚMERO!!!");
            Console.ReadLine();
            Console.WriteLine("Processando...\n");

            bool fimSorteio = false;
            while (!fimSorteio)
            {
                Random sorte = new Random();
                int valor = sorte.Next(0, 150);

                foreach (Participantes pessoas in participantes)
                {
                    if (valor + 1 == pessoas.numero)
                    {
                        Console.WriteLine($"O NÚMERO SORTEADO FOI: {pessoas.numero}");
                        Console.WriteLine($"VENCEDOR: {pessoas.nome}");
                        Console.ReadLine();
                        fimSorteio = true;
                        break;
                    }
                }
                if (participantes.Count == 0)
                {
                    Console.WriteLine("Nenhum número da sorte cadastrado ainda!");
                    Console.ReadLine();
                    fimSorteio = true;
                }
            }
        }

        static void Listagem()
        {
            Console.Clear();
            Console.WriteLine("Lista de participantes:\n");

            Console.WriteLine($"Número de participantes: {participantes.Count}\n");

            if (participantes.Count > 0)
            {
                int id = 0;
                foreach (Participantes pessoa in participantes)
                {
                    Console.WriteLine($"ID: {id}");
                    Console.WriteLine($"Número de sorteio: {pessoa.numero}");
                    Console.WriteLine($"Nome: {pessoa.nome}");
                    Console.WriteLine($"Endereço: {pessoa.endereco}");
                    Console.WriteLine($"Contato: {pessoa.contato}");
                    Console.WriteLine("========================================");
                    id++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum participante cadastrado ainda!");
            }

            Console.WriteLine("\nAperte ENTER");
            Console.ReadLine();
        }

        static void Adicionar()
        {
            Console.Clear();
            Console.WriteLine("Adicione um participante:\n");
            Console.Write("Número: ");
            int numero = int.Parse(Console.ReadLine());
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();
            Console.Write("Número de contato: ");
            string contato = Console.ReadLine();

            Participantes pessoas = new Participantes(numero, nome, endereco, contato);
            participantes.Add(pessoas);
            Salvar();
            Console.WriteLine("\nAdição concluída!");
            Console.ReadLine();
        }

        static void Remover()
        {
            Console.Clear();
            
            if (participantes.Count > 0)
            {
                Listagem();
                Console.WriteLine("Remova um participante:");
                Console.Write("Digite o ID: ");
                int id = int.Parse(Console.ReadLine());

                participantes.RemoveAt(id);
                Salvar();
                Console.WriteLine("\nRemoção concluída!");
            }
            else
            {
                Console.WriteLine("Remova um participante:");
                Console.WriteLine("\nNenhum participantes cadastrado ainda!");
            }

            Console.ReadLine();
        }

        static void Salvar()
        {
            FileStream cadastrados = new FileStream("Lista de participantes", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(cadastrados, participantes);

            cadastrados.Close();
        }

        static void Carregar()
        {
            FileStream cadastrados = new FileStream("Lista de participantes", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            try
            {
                participantes = (List<Participantes>)encoder.Deserialize(cadastrados);
            }
            catch (Exception e)
            {
                participantes = new List<Participantes>();
            }

            cadastrados.Close();
        }

        static void Busca()
        {
            Console.Clear();
            Console.WriteLine("Pesquise os dados do ganhador pelo número de sorteio:\n");
            
            if (participantes.Count > 0)
            {
                Console.Write("Digite o número de sorteio dele: ");
                int sorteio = int.Parse(Console.ReadLine());
                
                foreach (Participantes pessoas in participantes)
                {
                    if (sorteio == pessoas.numero)
                    {
                        Console.WriteLine($"\nNome: {pessoas.nome}");
                        Console.WriteLine($"Endereço: {pessoas.endereco}");
                        Console.WriteLine($"Contato: {pessoas.contato}");
                    }
                    else
                    {
                        Console.WriteLine("\nNenhum participante cadastrado com esse número!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nenhum participante cadastrado ainda!");
            }
            Console.ReadLine();
        }
    }
}
