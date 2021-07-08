using Microsoft.VisualStudio.TestTools.UnitTesting;
using eAgenda.Dominio.CompromissoModule;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using eAgenda.Controladores.CompromissoModule;
using eAgenda.Controladores.Shared;

namespace eAgenda.Tests.CompromissoModule
{

    [TestClass]
    public class CompromissoTest
    {
        ControladorCompromisso controladorCompromisso = null;

        protected Compromisso compromissoTest = new Compromisso("Palestra sobre testes", "NDD", "Presencial", DateTime.Today, new TimeSpan(13, 30, 00),
        new TimeSpan(17, 30, 00), null);
        public CompromissoTest()
        {
            LimparTabelas();
            controladorCompromisso = new ControladorCompromisso();
        }

        [TestCleanup()]
        public void LimparTabelas()
        {
            Db.Update("DELETE FROM TBCOMPROMISSO");
        }

        #region Atributos de teste adicionais
        //
        // É possível usar os seguintes atributos adicionais enquanto escreve os testes:
        //
        // Use ClassInitialize para executar código antes de executar o primeiro teste na classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para executar código após a execução de todos os testes em uma classe
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize para executar código antes de executar cada teste 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para executar código após execução de cada teste
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
  
        public void DeveInserir()
        {
            Assert.AreEqual("ESTA_VALIDO", controladorCompromisso.InserirNovo(compromissoTest));
        }

        [TestMethod]
        public void DeveEditar()
        {
            Compromisso compromisso = new Compromisso("Palestra sobre testes", "NDD", "Presencial", DateTime.Today, new TimeSpan(13, 30, 00),
                new TimeSpan(17, 30, 00), null);

            Assert.AreEqual("ESTA_VALIDO", controladorCompromisso.Editar(compromissoTest.Id, compromisso));
        }

        [TestMethod]

        public void DeveValidar()
        {
            Compromisso compromisso = new Compromisso("Palestra sobre banco de dados", "NDD", "Presencial", DateTime.Today, new TimeSpan(13, 30, 00),
                new TimeSpan(17, 30, 00), null);

            Assert.AreEqual("ESTA_VALIDO", compromisso.Validar());
        }

        public void DeveSerInvalido()
        {
            Compromisso compromisso = new Compromisso("", "NDD", "Presencial", DateTime.Today, new TimeSpan(13, 30, 00),
                new TimeSpan(17, 30, 00), null);

            Assert.AreEqual("O campo Assunto é obrigatório", compromisso.Validar());
        }
    }
}