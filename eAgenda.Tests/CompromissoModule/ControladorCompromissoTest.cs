using eAgenda.Controladores.CompromissoModule;
using eAgenda.Controladores.ContatoModule;
using eAgenda.Controladores.Shared;
using eAgenda.Dominio.CompromissoModule;
using eAgenda.Dominio.ContatoModule;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace eAgenda.Tests.CompromissoModule
{
    /// <summary>
    /// Descrição resumida para UnitTest2
    /// </summary>
    [TestClass]
    public class ControladorCompromissoTest
    {
        ControladorCompromisso controladorCompromisso = null;
        ControladorContato controladorContato = null;


        public ControladorCompromissoTest()
        {
            controladorCompromisso = new ControladorCompromisso();
            controladorContato = new ControladorContato();
            Db.Update("DELETE FROM [TBCOMPROMISSO]");
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
        public void InserirCompromissoSemContato()
        {
            //arrange    
             Compromisso novoCompromisso = new Compromisso("Infraestrutura", "Orion Parque", "",DateTime.Today,new TimeSpan(13,30,00),new TimeSpan(14,30,00),null);

            //action
            controladorCompromisso.InserirNovo(novoCompromisso);

            //assert
            Compromisso compromissoMarcado = controladorCompromisso.SelecionarPorId(novoCompromisso.Id);
            compromissoMarcado.Should().Be(novoCompromisso);
        }

        [TestMethod]

        public void InserirCompromissoComContato()
        {
            //arrange 
            Contato contato = new Contato("Pedro", "pedro@gmail.com", "32517600", "NDD", "Diretor");
            controladorContato.InserirNovo(contato);

            Compromisso novoCompromisso = new Compromisso("Montar plano de negócios", "Serra shopping", "", DateTime.Today, new TimeSpan(15, 00, 00), new TimeSpan(16, 00, 00), contato);

            //action
            controladorCompromisso.InserirNovo(novoCompromisso);

            //assert
            var contatoEncontrado = controladorCompromisso.SelecionarPorId(novoCompromisso.Id);
            contatoEncontrado.Should().Be(novoCompromisso);
        }

        [TestMethod]
        public void DeveAtualizarCompromisso()
        {
            //arrange
            Compromisso compromisso = new Compromisso("Infraestrutura", "Orion Parque", "", DateTime.Today, new TimeSpan(13, 30, 00), new TimeSpan(14, 30, 00), null);
            controladorCompromisso.InserirNovo(compromisso);
            Compromisso novoCompromisso = new Compromisso("Montar plano de negócios", "Serra shopping", "", DateTime.Today, new TimeSpan(15, 00, 00), new TimeSpan(16, 00, 00), null);

            //action
            controladorCompromisso.Editar(compromisso.Id,novoCompromisso);

            //assert
            var compromissoMarcado = controladorCompromisso.SelecionarPorId(novoCompromisso.Id);
            compromissoMarcado.Should().Be(novoCompromisso);
        }
        [TestMethod]
        public void DeveExcluirCompromisso()
        {
            //arrange
            Compromisso compromisso = new Compromisso("Infraestrutura", "Orion Parque", "", DateTime.Today, new TimeSpan(13, 30, 00), new TimeSpan(14, 30, 00), null);
            controladorCompromisso.InserirNovo(compromisso);

            //action
            controladorCompromisso.Excluir(compromisso.Id);

            //assert
            var compromissoMarcado = controladorCompromisso.SelecionarPorId(compromisso.Id);
            compromissoMarcado.Should().BeNull();
        }
        [TestMethod]
        public void DeveselecionarCompromissoPorId()
        {
            //arrange
            Compromisso compromisso = new Compromisso("Infraestrutura", "Orion Parque", "", DateTime.Today, new TimeSpan(13, 30, 00), new TimeSpan(14, 30, 00), null);
            controladorCompromisso.InserirNovo(compromisso);

            //action
            Compromisso compromissoEncontrado = controladorCompromisso.SelecionarPorId(compromisso.Id);

            //assert
            compromissoEncontrado.Should().Be(compromisso);
        }
        [TestMethod]
        public void DeveSelecionarTodosCompromissos()
        {
            //arrange
            var c1 = new Compromisso("Infraestrutura", "Orion Parque", "", DateTime.Today, new TimeSpan(13, 30, 00), new TimeSpan(14, 30, 00), null);
            controladorCompromisso.InserirNovo(c1);

            var c2 = new Compromisso("Montar plano de negócios", "Serra shopping", "", DateTime.Today, new TimeSpan(15, 00, 00), new TimeSpan(16, 00, 00), null);
            controladorCompromisso.InserirNovo(c2);

            var c3 = new Compromisso("Falar sobre a gestão", "NDD", "", DateTime.Today, new TimeSpan(16, 30, 00), new TimeSpan(17, 00, 00), null);
            controladorCompromisso.InserirNovo(c3);

            //action
            var compromissos = controladorCompromisso.SelecionarTodos();

            //assert
            compromissos.Should().HaveCount(3);
            compromissos[0].Assunto.Should().Be("Infraestrutura");
            compromissos[1].Assunto.Should().Be("Montar plano de negócios");
            compromissos[2].Assunto.Should().Be("Falar sobre a gestão");
          
        }
        [TestMethod]
        public void DeveSelecionarCompromissosPassado()
        {
            //arrange
            var c1 = new Compromisso("Infraestrutura", "Orion Parque", "", new DateTime(2020, 12, 10), new TimeSpan(13, 30, 00), new TimeSpan(14, 30, 00), null);
            controladorCompromisso.InserirNovo(c1);

            var c2 = new Compromisso("Montar plano de negócios", "Serra shopping", "", new DateTime(2020, 12, 11), new TimeSpan(15, 00, 00), new TimeSpan(16, 00, 00), null);
            controladorCompromisso.InserirNovo(c2);

            var c3 = new Compromisso("Falar sobre a gestão", "NDD", "",new DateTime(2020,12,12), new TimeSpan(16, 30, 00), new TimeSpan(17, 00, 00), null);
            controladorCompromisso.InserirNovo(c3);

            //action
            var compromissos = controladorCompromisso.SelecionarTodos();

            //assert
            compromissos.Should().HaveCount(3);
            compromissos[0].Data.Should().Be(new DateTime(2020, 12, 10));
            compromissos[1].Data.Should().Be(new DateTime(2020, 12, 11));
            compromissos[2].Data.Should().Be(new DateTime(2020, 12, 12));

        }

        [TestMethod]
        public void NewMethod3()
        {
            //arrange

            //action

            //assert
        }
    }
}
