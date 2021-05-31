using System;
namespace Registro_Aluno
{
    class Aluno
    {
        public string Nome;
        public DateTime Nascimento;
        public string Status;
        public string Matricula;
        public string Email;
        public string Cpf;
        public string Rg;
        public string Curso;

        //Método construtor 
        public Aluno(string nome, DateTime nascimento, string status, string matricula, string email, string cpf, string rg, string curso)
        {
            Nome = nome;
            Nascimento = nascimento;
            Status = status;
            Matricula = matricula;
            Email = email;
            Cpf = cpf;
            Rg = rg;
            Curso = curso;
        }
        // Método verificador de cpf
        public bool ExistCpf(string cpf)
        {
            if (Cpf.Equals(cpf))
                return true;
            else
                return false;
        }
        // Método verificador de matricula
        public bool ExistMatricula(string matricula)
        {
            if (Matricula.Equals(matricula.ToUpper()))//ToUpper= deixa letra maiúscula
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return "Nome: " + Nome
                + "\nData de Nascimento: " + Nascimento.ToShortDateString()
                + "\nStatus: " + Status
                + "\nMatricula: " + Matricula
                + "\nEmail: " + Email
                + "\nCPF: " + Cpf
                + "\nRG: " + Rg
                + "\nCurso: " + Curso;
        }
    }
}
