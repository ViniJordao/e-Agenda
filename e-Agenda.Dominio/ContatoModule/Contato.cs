﻿using eAgenda.Dominio.Shared;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace eAgenda.Dominio.ContatoModule
{
    public class Contato : EntidadeBase, IEquatable<Contato>
    {        
        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Empresa = empresa;
            Cargo = cargo;
        }

        public Contato()
        {
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }

        public override string Validar()
        {
            Regex templateEmail = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            string resultadoValidacao = "";

            if (Telefone.Length < 7)
                resultadoValidacao = "O campo Telefone está inválido";

            if (templateEmail.IsMatch(Email) == false)
                resultadoValidacao += QuebraDeLinha(resultadoValidacao) + "O campo Email está inválido";

            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Contato);
        }

        public bool Equals(Contato other)
        {
            return other != null
                && Id == other.Id
                && Nome == other.Nome
                && Email == other.Email
                && Telefone == other.Telefone
                && Cargo == other.Cargo
                && Empresa == other.Empresa;
        }

        public override int GetHashCode()
        {
            int hashCode = 1695060689;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Telefone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cargo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Empresa);
            return hashCode;
        }
        public override string ToString()
        {

            return Nome + ", " + Email + ", " + Telefone + ", " + Empresa + ", " + Cargo;

        }
    }
}
