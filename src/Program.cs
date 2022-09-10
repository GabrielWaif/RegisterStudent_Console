using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Cadastro_Alunos
{
    internal class Program
    {
        static Dictionary<int, Estudante> signedUsers = new Dictionary<int, Estudante>();
        static Random rnd = new Random();
        static string Endereco = System.AppContext.BaseDirectory + @"\Cadastros.txt";
        static void Main(string[] args)
        {
            RecarregarCadastros();
            int escolha;
            do
            {
                Console.WriteLine("[1] - Cadastrar");
                Console.WriteLine("[2] - Editar");
                Console.WriteLine("[3] - Pesquisar por ID");
                Console.WriteLine("[4] - Mostrar Todos os Cadastrados");
                Console.WriteLine("[5] - Sair\n");
                Console.WriteLine("Qual funcao deseja fazer:");
                if (!int.TryParse(Console.ReadLine(), out escolha)) escolha = 6;
                Console.Clear();
                switch (escolha)
                {
                    case 1:
                        Cadastrar();
                        AtualizarCadastro();
                        break;
                    case 2:
                        EditarEstudantes();
                        AtualizarCadastro();
                        break;
                    case 3:
                        PesquisarID();
                        AtualizarCadastro();
                        break;
                    case 4:
                        MostrarCadastrados();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Escolha Invalida!!!\n");
                        break;
                }
            } while (escolha != 5);
        }

        static void PesquisarID()
        {
            int ID;
            do
            {
                Console.WriteLine("Digite o ID que deseja pesquisar:");
                if (int.TryParse(Console.ReadLine(), out ID) && signedUsers.ContainsKey(ID)) continue;
                Console.Clear();
                Console.WriteLine("ID Invalido...\n");
            }
            while (!signedUsers.ContainsKey(ID));
            Console.Clear();
            Estudante searchedID = (Estudante)signedUsers[ID];
            Console.WriteLine("Info de estudante com ID: {0}\n", searchedID.ID);
            Console.WriteLine("Name: {0}", searchedID.Name);
            Console.WriteLine("Age: {0}", searchedID.Age);
            Console.ReadKey();
            Console.Clear();
        }

        static void RecarregarCadastros()
        {
            string[] bufferText = File.ReadAllLines(Endereco);
            Estudante bufferEstudante = new Estudante();
            for(int i = 0, j = 1; i<bufferText.Length; i++, j++)
            {
                switch (j)
                {
                    case 1:
                        bufferEstudante.ID = int.Parse(bufferText[i]);
                        break;
                    case 2:
                        bufferEstudante.Name = bufferText[i];
                        break;
                    case 3:
                        bufferEstudante.Age = int.Parse(bufferText[i]);
                        break;
                }
                if (j == 3)
                {
                    j = 0;
                    signedUsers.Add(bufferEstudante.ID, bufferEstudante);
                    bufferEstudante = new Estudante();
                }
            }
            
        }

        static void AtualizarCadastro()
        {
            string[] bufferText = new string[signedUsers.Count*3];
            int contador = 0;
            foreach(Estudante estudante in signedUsers.Values)
            {
                bufferText[contador] = estudante.ID.ToString();
                bufferText[contador+1] = estudante.Name.ToString();
                bufferText[contador+2] = estudante.Age.ToString();
                contador += 3;
            }
            File.WriteAllLines(Endereco, bufferText);
        }

        static void Cadastrar()
        {
            Estudante Temporario = new Estudante();
            Temporario.ID = IDGenerator();

            Console.WriteLine("O ID do Cadastro atual sera: {0}", Temporario.ID);

            bool validAge = false, boolName = false;
            do
            {
                do
                {
                    Console.WriteLine("Digite o nome a cadastrar: ");
                    Temporario.Name = Console.ReadLine();
                    boolName = validName(Temporario.Name);
                } while (boolName);


                do
                {
                    int tmp;
                    Console.WriteLine("Digite a idade a cadastrar: ");
                    validAge = int.TryParse(Console.ReadLine(), out tmp);
                    if (!validAge) Console.WriteLine("Idade Invalida!!!");
                    else Temporario.Age = tmp;
                } while (!validAge);

            } while (!validAge || boolName);

            signedUsers.Add(Temporario.ID, Temporario);

            Console.Clear();
            Console.WriteLine("Estudante {0} cadastrado com sucesso...\n", Temporario.ID);
        }

        static int IDGenerator()
        {
            int generatedID;
            do
            {
                generatedID = rnd.Next(1000, 9999);
            }
            while (signedUsers.ContainsKey(generatedID));
            return generatedID;
        }

        static void MostrarCadastrados()
        {
            Console.Clear();
            Console.WriteLine("Estudantes Cadastrados: \n");
            foreach (Estudante info in signedUsers.Values)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("ID: {0}", info.ID);
                Console.WriteLine("Name: {0}", info.Name);
                Console.WriteLine("Idade: {0}", info.Age);

            }
            Console.WriteLine("----------------------");
            Console.ReadKey();
            Console.Clear();
        }

        static void EditarEstudantes()
        {
            int escolha;
            int ID;
            do
            {
                Console.WriteLine("Digite o ID de quem deseja editar:");
                if (int.TryParse(Console.ReadLine(), out ID) && signedUsers.ContainsKey(ID)) continue;
                Console.Clear();
                Console.WriteLine("ID Invalido...\n");
            }
            while (!signedUsers.ContainsKey(ID));
            Estudante updatedID = (Estudante)signedUsers[ID];
            Console.Clear();
            Console.WriteLine("----------------------");
            Console.WriteLine("ID: {0}", updatedID.ID);
            Console.WriteLine("Nome: {0}", updatedID.Name);
            Console.WriteLine("Idade: {0}", updatedID.Age);
            Console.WriteLine("----------------------\n");
            string novoNome, tipoDeEscolha, valorInicial, valorFinal;
            novoNome = tipoDeEscolha = valorInicial = valorFinal = string.Empty;
            int novaIdade, novoID;
            do
            {
                Console.WriteLine("[1] - ID");
                Console.WriteLine("[2] - Nome");
                Console.WriteLine("[3] - Idade");
                Console.WriteLine("Qual deseja editar:");
                if (!int.TryParse(Console.ReadLine(), out escolha)) escolha = 6;
                Console.Clear();
                switch (escolha)
                {
                    case 1:
                        bool boolID;
                        tipoDeEscolha = "ID";
                        valorInicial = Convert.ToString(updatedID.ID);

                        do
                        {
                            Console.WriteLine("Digite o novo ID: ");
                            novoID = int.Parse(Console.ReadLine());
                            boolID = signedUsers.ContainsKey(novoID);
                            if (boolID) Console.WriteLine("ID já cadastrado\n");
                            else if (novoID < 1000 || novoID > 9999)
                            {
                                Console.WriteLine("ID invalido\n");
                                boolID = true;

                            }

                        } while (boolID);


                        updatedID.ID = novoID;
                        signedUsers.Add(novoID, updatedID);
                        signedUsers.Remove(ID);
                        valorFinal = Convert.ToString(updatedID.ID);
                        break;
                    case 2:
                        tipoDeEscolha = "Nome";
                        valorInicial = updatedID.Name;
                        bool boolName;

                        do
                        {
                            Console.WriteLine("Digite o novo nome: ");
                            novoNome = Console.ReadLine();
                            boolName = validName(novoNome);
                        } while (boolName);

                        updatedID.Name = novoNome;
                        signedUsers[ID] = updatedID;
                        valorFinal = updatedID.Name;
                        break;
                    case 3:
                        tipoDeEscolha = "Idade";
                        valorInicial = Convert.ToString(updatedID.Age);
                        bool boolAge = true;

                        do
                        {
                            int tmp;
                            Console.WriteLine("Digite a nova idade: ");
                            boolAge = int.TryParse(Console.ReadLine(), out novaIdade);
                            if (!boolAge) Console.WriteLine("Idade Invalida!!!");
                        } while (!boolAge);

                        updatedID.Age = novaIdade;
                        signedUsers[ID] = updatedID;
                        valorFinal = Convert.ToString(updatedID.Age);
                        break;
                    default:
                        Console.WriteLine("Escolha Invalida!!!\n");
                        break;
                }
            } while (escolha < 0 && escolha < 4);
            Console.Clear();
            Console.WriteLine("{0} atualizado de {1} para {2}...\n", tipoDeEscolha, valorInicial, valorFinal);
        }

        static bool validName(string name)
        {
            bool HasNumber = false;
            foreach (char letra in name)
            {
                if (!char.IsLetter(letra) && letra != ' ')
                {
                    HasNumber = true;
                    break;
                }
            }
            if (HasNumber)
            {
                Console.WriteLine("Nome Invalido!!!\n");
                return true;
            }
            return false;
        }


    }
}
