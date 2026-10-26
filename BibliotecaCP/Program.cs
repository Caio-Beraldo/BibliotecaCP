using System.Net.NetworkInformation;

public class Pessoa
{
    public string Nome { get; private set; }
    public int Identificador { get; private set; }
    public string TipoUsuario { get; private set; }
    public Pessoa(string nome, int identificador, string tipoUsuario)
    {
        Nome = nome;
        Identificador = identificador;
        TipoUsuario = tipoUsuario;
    }
    public void ConsultarDados()
    {
        Console.WriteLine("ID: " + Identificador);
        Console.WriteLine("Nome: " + Nome);
        Console.WriteLine("Tipo de usuário: " + TipoUsuario);
    }
    public void ApresentarDados()
    {
        Console.WriteLine(Identificador + " " + Nome + "" + TipoUsuario);
    }
}
public class Livro
{
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public string ISBN { get; private set; }
    public bool Disponivel { get; private set; }
    public Livro(string titulo, string autor, string isbn)
    {
        Titulo = titulo;
        Autor = autor;
        ISBN = isbn;
        Disponivel = true;
    }
    public void ConsultarDados()
    {
        Console.WriteLine("Titulo: " + Titulo);
        Console.WriteLine("Autor: " + Autor);
        Console.WriteLine("ISBN: " + ISBN);
        Console.WriteLine("Disponibilidade " + (Disponivel ? "Disponivel" : "Emprestado"));
    }
    public void AtualizarDisponibilidade(bool disponivel)
    {
        Disponivel = disponivel;
    }
}
public class Emprestimo
{
    public Pessoa Usuario { get; private set; }
    public Livro LivroEmprestado { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime DataPrevistaDevolucao { get; private set; }
    public bool Devolvido { get; private set; }

    public Emprestimo(Pessoa usuario, Livro livroEmprestado, DateTime dataEmprestimo, DateTime dataPrevistaDevolucao)
    {
        Usuario = usuario;
        LivroEmprestado = livroEmprestado;
        DataEmprestimo = dataEmprestimo;
        DataPrevistaDevolucao = dataPrevistaDevolucao;
        Devolvido = false;
        LivroEmprestado.AtualizarDisponibilidade(false);
    }
    public void RegistrarDevolucao()
    {
        if (Devolvido)
        {
            Console.WriteLine("Este livro já foi devolvido.");
            return;
        }
        Devolvido = true;
        LivroEmprestado.AtualizarDisponibilidade(true);
        Console.WriteLine("Devolução registrada com sucesso!");
    }
    public void ApresentarDados()
    {
        Console.WriteLine("Usuário: " + Usuario.Nome);
        Console.WriteLine("Livro: " + LivroEmprestado.Titulo);
        Console.WriteLine("Data do empréstimo " + DataEmprestimo.ToString("dd/MM/yyyy"));
        Console.WriteLine("Devolução prevista: " + DataPrevistaDevolucao.ToString("dd/MM/yyyy"));
        Console.WriteLine("Situação: " + (Devolvido ? "Devolvido" : "Em andamento"));
    }

}
public class Reserva
{
    public Pessoa Usuario { get; private set; }
    public Livro LivroReservado { get; private set; }
    public DateTime DataReserva { get; private set; }
    public Reserva(Pessoa usuario, Livro livro)
    {
        Usuario = usuario;
        LivroReservado = livro;
        DataReserva = DateTime.Today;
    }
    public void ApresentarDados()
    {
        Console.WriteLine("Usuário: " + Usuario.Nome);
        Console.WriteLine("Livro: " + LivroReservado.Titulo);
        Console.WriteLine("Data da reserva: " + DataReserva.ToString("dd/MM/yyyy"));
    }

}
class Program
{
    static List<Pessoa> usuarios = new List<Pessoa>();
    static List<Livro> livros = new List<Livro>();
    static List<Emprestimo> emprestimos = new List<Emprestimo>();
    static List<Reserva> reservas = new List<Reserva>();
    static int proximoId = 1;
    static void Main()
    {
        static int LerInteiro()
        {
            int numero;
            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.Write("Digite um número válido: ");
            }
            return numero;
        }
        static Pessoa BuscarUsuario(int id)
        {
            foreach (Pessoa pessoa in usuarios)
            {
                if (pessoa.Identificador == id)
                {
                    return pessoa;
                }
            }
            return null;
        }
        static Livro BuscarLivro(string isbn)
        {
            foreach (Livro livro in livros)
            {
                if (livro.ISBN == isbn)
                {
                    return livro;
                }
            }
            return null;
        }
        static void CadastrarUsuario()
        {
            Console.WriteLine("Cadastro de usuário");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Tipo de usuário: ");
            Console.WriteLine("1 - Aluno");
            Console.WriteLine("2 - Professor");
            Console.WriteLine("3 - Funcionario");
            Console.Write("Escolha: ");
            int tipo = int.Parse(Console.ReadLine());
            string tipoUsuario;
            if (tipo == 1)
            {
                tipoUsuario = " (Aluno)";
            }
            else if (tipo == 2)
            {
                tipoUsuario = " (Professor) ";
            }
            else if (tipo == 3)
            {
                tipoUsuario = " (Funcionário)";
            }
            else
            {
                Console.WriteLine("Tipo inválido!");
                return;
            }
            Pessoa pessoa = new Pessoa(nome, proximoId, tipoUsuario);
            usuarios.Add(pessoa);
            Console.WriteLine("Usuario cadastrado!");
            Console.WriteLine("ID: " + proximoId);
            proximoId++;

        }
        static void CadastrarLivro()
        {
            Console.WriteLine("Cadastro de livro");

            Console.Write("Titulo: ");
            string titulo = Console.ReadLine();
            Console.Write("Autor: ");
            string autor = Console.ReadLine();
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();
            if (BuscarLivro(isbn) != null)
            {
                Console.WriteLine("Este livro já esta cadastrado ");
                return;
            }
            Livro livro = new Livro(titulo, autor, isbn);
            livros.Add(livro);
            Console.WriteLine("Livro cadastrado com sucesso");
        }
        static void RealizarEmprestimo()
        {
            Console.WriteLine("Realizar empréstimo");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Pessoa usuario = BuscarUsuario(id);
            if (usuario != null)
            {
                Console.WriteLine("Usuario não encontrado");
                return;
            }
            Console.Write("ISBN do livro: ");
            string isbn = Console.ReadLine();
            Livro livro = BuscarLivro(isbn);
            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado");
                return;
            }
            if (livro.Disponivel == false)
            {
                Console.WriteLine("Livro indisponivel. Voce consegue reservar se quiser");
                return;
            }
            DateTime dataEmprestimo = DateTime.Today;
            DateTime dataDevolucao = dataEmprestimo.AddDays(7);
            Emprestimo emprestimo = new Emprestimo(usuario, livro, dataEmprestimo, dataDevolucao);
            emprestimos.Add(emprestimo);
            Console.WriteLine("Emprestimo realizado com sucesso!");
            Console.WriteLine("Data prevista de devolução: " + dataDevolucao.ToString("dd/MM/yyyy"));
        }
        static void RegistrarDevolucao()
        {
            Console.WriteLine("Registrar devolução");
            Console.Write("ISBN do livro: ");
            string isbn = Console.ReadLine();

            Livro livro = BuscarLivro(isbn);
            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado!");
                return;
            }
            Emprestimo emprestimoAtivo = null;
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.LivroEmprestado == livro &&
                    emprestimo.Devolvido == false)
                {
                    emprestimoAtivo = emprestimo;
                    break;
                }
            }
            if (emprestimoAtivo == null)
            {
                Console.WriteLine("Não existe empréstimo ativo para esse livro.");
                return;
            }

            emprestimoAtivo.RegistrarDevolucao();
        }

        static void RealizarReserva()
        {
            Console.WriteLine("Realizar reserva");

            Console.Write("ID do usuário: ");
            int id = int.Parse(Console.ReadLine());

            Pessoa usuario = BuscarUsuario(id);

            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado!");
                return;
            }

            Console.Write("ISBN do livro: ");
            string isbn = Console.ReadLine();

            Livro livro = BuscarLivro(isbn);

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado!");
                return;
            }

            if (livro.Disponivel == true)
            {
                Console.WriteLine("O livro está disponível para empréstimo");
                return;
            }

            Reserva reserva = new Reserva(usuario, livro);

            reservas.Add(reserva);

            Console.WriteLine("Reserva realizada com sucesso!");
        }

        static void ListarLivros()
        {
            Console.WriteLine("Livros cadastrados");

            if (livros.Count == 0)
            {
                Console.WriteLine("Nenhum livro cadastrado.");
                return;
            }

            foreach (Livro livro in livros)
            {
                livro.ConsultarDados();
                Console.WriteLine(" ");
            }
        }

        static void ListarUsuarios()
        {
            Console.WriteLine("Usuários cadastrados");

            if (usuarios.Count == 0)
            {
                Console.WriteLine("Nenhum usuário cadastrado.");
                return;
            }
            foreach (Pessoa pessoa in usuarios)
            {
                pessoa.ApresentarDados();
            }
        }

        static void ListarEmprestimos()
        {
            Console.WriteLine("Empréstimos realizados");

            if (emprestimos.Count == 0)
            {
                Console.WriteLine("Nenhum empréstimo realizado.");
            }
            else
            {
                foreach (Emprestimo emprestimo in emprestimos)
                {
                    emprestimo.ApresentarDados();
                }
            }

            Console.WriteLine("Livros indisponíveis");

            bool encontrou = false;

            foreach (Livro livro in livros)
            {
                if (livro.Disponivel == false)
                {
                    Console.WriteLine(livro.Titulo);
                    encontrou = true;
                }
            }
            if (encontrou == false)
            {
                Console.WriteLine("Todos os livros estão disponíveis");
            }
        }

        static void ListarReservas()
        {
            Console.WriteLine("Reservas realizadas");

            if (reservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva realizada.");
                return;
            }
            foreach (Reserva reserva in reservas)
            {
                reserva.ApresentarDados();
            }
        }
        int opcao;
        do
        {
            Console.WriteLine("Bem Vindo a biblioteca!");
            Console.WriteLine("1 - Cadastrar usuario");
            Console.WriteLine("2 - Cadastrar livro");
            Console.WriteLine("3 - Realizar empréstimo");
            Console.WriteLine("4 - Registrar devolução");
            Console.WriteLine("5 - Realizar reserva");
            Console.WriteLine("6 - Listar livros");
            Console.WriteLine("7 - Listar usuários");
            Console.WriteLine("8 - Listar empréstimos");
            Console.WriteLine("9 - Listar reservas");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            opcao = LerInteiro();
            switch (opcao)
            {
                case 1:
                    CadastrarUsuario();
                    break;
                case 2:
                    CadastrarLivro();
                    break;
                case 3:
                    RealizarEmprestimo();
                    break;
                case 4:
                    RegistrarDevolucao();
                    break;
                case 5:
                    RealizarReserva();
                    break;
                case 6:
                    ListarLivros();
                    break;
                case 7:
                    ListarUsuarios();
                    break;
                case 8:
                    ListarEmprestimos();
                    break;
                case 9:
                    ListarReservas();
                    break;
                case 0:
                    Console.WriteLine("Fim do programa");
                    break;
            }
        } while (opcao != 0);
    }
}
