//Projeto Registro aluno
//Wendi Silva Ramos
//CJ 3003809
using System;
using System.IO;


namespace Registro_Aluno
{
    class Program
    {
        //Método para armazenamento de dados em arquivo.
        public static void ArmazenarDados(Aluno[] vetAlunos, int cont)
        {
            string path = @"../Dados.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (int i = 0; i < cont; i++)
                    {
                        sw.WriteLine(vetAlunos[i].Nome + ";" + vetAlunos[i].Nascimento.ToShortDateString() + ";"
                            + vetAlunos[i].Status + ";" + vetAlunos[i].Matricula + ";" + vetAlunos[i].Email
                            + ";" + vetAlunos[i].Cpf + ";" + vetAlunos[i].Rg + ";" + vetAlunos[i].Curso);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Problema com a leitura do arquivo: {0}", e.ToString());
            }

        }

        //Método para carregamento de dados de arquivo.
        public static void CarregarDados(Aluno[] vetAlunos, ref int cont)
        {
            string path = @"../Dados.txt";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        string entrada = sr.ReadLine();
                        string[] vet = entrada.Split(";");
                        Aluno objAux = new Aluno(vet[0], Convert.ToDateTime(vet[1]), vet[2], vet[3], vet[4], vet[5], vet[6], vet[7]);
                        vetAlunos[cont++] = objAux;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Problema com a leitura do arquivo: {0}", e.ToString());
            }
        }

        //Método para cadastro de um novo aluno.
        public static Aluno Cadastrar()
        {
            Console.Write("Entre com o nome do aluno: ");
            string nome = Console.ReadLine();
            Console.Write("Entre com a data de nascimento do aluno: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Entre com o Status do aluno(Matriculado, Formado, Evadido, Trancado): ");
            string status = Console.ReadLine();
            Console.Write("Entre com a matricula do aluno: ");
            string mat = Console.ReadLine();
            Console.Write("Entre com o email do aluno: ");
            string email = Console.ReadLine();
            string cpf;
            do
            {
                Console.Write("Entre com o cpf do aluno: ");
                cpf = Console.ReadLine();


            } while (ValidarCpf(cpf) == false);

            Console.Write("Entre com o rg do aluno: ");
            string rg = Console.ReadLine();
            Console.Write("Entre com o curso do aluno: ");
            string curso = Console.ReadLine();
            Aluno alunoNovo = new Aluno(nome, data, status, mat.ToUpper(), email, cpf, rg, curso);
            return alunoNovo;

        }
        //Método para exibir todos os alunos
        public static void ExibirTodos(Aluno[] vetAlunos, int cont)
        {
            Console.WriteLine("\nTotal de alunos cadastrados: " + cont);
            for (int i = 0; i < cont; i++)
            {
                Console.WriteLine(vetAlunos[i].ToString());
                Console.WriteLine();
            }
        }
        //Método para visualização de dados de um aluno.
        public static void VisualizarAluno(Aluno[] vetAlunos, int cont)
        {
            Console.WriteLine("Digite 1 para pesquisar por matrícula ou 2 para pesquisar por CPF: ");
            int x = int.Parse(Console.ReadLine());

            switch (x)
            {
                case 1:
                    Console.Write("Entre com o número de matrícula:");
                    string mat = Console.ReadLine();
                    int i;
                    for (i = 0; i < cont; i++)
                    {
                        if (vetAlunos[i].ExistMatricula(mat))
                        {
                            Console.WriteLine(vetAlunos[i].ToString() + "\n");
                            break;
                        }
                    }
                    if (i == cont)
                    {
                        Console.WriteLine("Número de matrícula inválido!");
                    }
                    break;
                case 2:
                    Console.Write("Entre com o número de CPF: ");
                    string cpf = Console.ReadLine();
                    for (i = 0; i < cont; i++)
                    {
                        if (vetAlunos[i].ExistCpf(cpf))
                        {
                            Console.WriteLine(vetAlunos[i].ToString() + "\n");
                            break;
                        }
                    }
                    if (i == cont)
                    {
                        Console.WriteLine("Número de CPF inválido!");
                    }
                    break;
                default:
                    Console.WriteLine("Opção inexistente");
                    break;
            }
        }

        //Método para atualização de dados de um aluno.
        public static void AtualizarAluno(Aluno[] vetAlunos, int cont)
        {
            Console.Write("Entre com o número de matrícula:");
            string mat = Console.ReadLine();
            int i;
            for (i = 0; i < cont; i++)
            {
                if (vetAlunos[i].ExistMatricula(mat))
                {
                    Console.WriteLine("Dados antigos:");
                    Console.WriteLine(vetAlunos[i].ToString() + "\n");
                    vetAlunos[i] = Cadastrar();
                    break;
                }
            }
            if (i == cont)
            {
                Console.WriteLine("Número de matrícula inválido!");
            }
        }
        //Método para atualização somente do status de um aluno.
        public static void AtualizarStatus(Aluno[] vetAlunos, int cont)
        {
            Console.Write("Entre com o número de matrícula:");
            string mat = Console.ReadLine();
            int i;
            for (i = 0; i < cont; i++)
            {
                if (vetAlunos[i].ExistMatricula(mat))
                {
                    Console.WriteLine("Dados antigos:");
                    Console.WriteLine(vetAlunos[i].ToString() + "\n");
                    Aluno aluno = vetAlunos[i];
                    Console.WriteLine("Entre com um novo 'Status': ");
                    aluno.Status = Console.ReadLine();

                    vetAlunos[i] = aluno;
                    break;
                }
            }
            if (i == cont)
            {
                Console.WriteLine("Número de matrícula inválido!");
            }
        }
        //Validação de cpf.
        public static bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] multiplicador2 = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Replace(".", "").Replace("-", "").Replace(" ", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto == 10)
                resto = 0;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto == 10)
                resto = 0;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        //Métdo para exibir menu de opções.
        static int Menu()
        {
            Console.Clear();
            Console.WriteLine("MENU");
            Console.WriteLine("0- Para sair.");
            Console.WriteLine("1- Cadastrar um novo aluno.");
            Console.WriteLine("2- Para exibir todos os alunos.");
            Console.WriteLine("3- Para exibir dados de um aluno por meio de pesquisa.");
            Console.WriteLine("4- Para atualizar um aluno.");
            Console.WriteLine("5- Para atualizar o 'Status' de um aluno.");


            int opção = int.Parse(Console.ReadLine());
            return opção;
        }


        static void Main(string[] args)
        {
            Aluno[] vetAlunos = new Aluno[1000];
            int cont = 0;
            int prog;

            CarregarDados(vetAlunos, ref cont);
            do
            {
                prog = Menu();
                switch (prog)
                {
                    case 0:
                        Console.WriteLine("Obrigada por utilizar o sistema Wendi's Ltda!\n");
                        break;
                    case 1:
                        vetAlunos[cont++] = Cadastrar();
                        break;
                    case 2:
                        ExibirTodos(vetAlunos, cont);
                        break;
                    case 3:
                        VisualizarAluno(vetAlunos, cont);
                        break;
                    case 4:
                        AtualizarAluno(vetAlunos, cont);
                        break;
                    case 5:
                        AtualizarStatus(vetAlunos, cont);
                        break;
                    default:
                        Console.WriteLine("Opção não corresponde a nenhuma opção.");
                        break;

                }
                Console.WriteLine("Tecle alguma tecla para continuar...");
                Console.ReadKey();
            } while (prog != 0);
            ArmazenarDados(vetAlunos, cont);

        }
    }

}

