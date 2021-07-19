using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using eAgenda.Controladores.ContatoModule;
using eAgenda.Controladores.Shared;
using eAgenda.Dominio.ContatoModule;

namespace eAgenda.WindowsFormsApp
{
    public partial class Form3 : Form
    {
        ControladorContato controladorContato = new ControladorContato();
        Contato contato = new Contato();

        public Form3()
        {
            controladorContato = new ControladorContato();
            InitializeComponent();
            CarregarContatos();

        }

        private void CarregarContatos()
        {

            dtContatos.Clear();
            List<Contato> listaContatos = controladorContato.SelecionarTodos();


            foreach (var contato in listaContatos)
            {
                DataRow registro = dtContatos.NewRow();

                registro["Id"] = contato.Id;
                registro["Nome"] = contato.Nome;
                registro["Telefone"] = contato.Telefone;
                registro["Email"] = contato.Email;
                registro["Empresa"] = contato.Empresa;
                registro["Cargo"] = contato.Cargo;

                dtContatos.Rows.Add(registro);
            }

        }

        private void EditarRegistro()
        {
            int id = Convert.ToInt32(textBoxId.Text);
            string nome = textNome.Text;
            string email = textEmail.Text;
            string telefone = textTelefone.Text;
            string empresa = textEmpresa.Text;
            string cargo = textCargo.Text;

            controladorContato.Editar(id, new Contato(nome, email, telefone, empresa, cargo));
            CarregarContatos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            TelaPrincipal form1 = new TelaPrincipal();
            form1.ShowDialog();
            this.Close();
        }

        public void btnCadastar_Click(object sender, EventArgs e)
        {
            AdiconarContatos();
            LimparCampos();
        }

        private void AdiconarContatos()
        {
            string nome = textNome.Text;
            string email = textEmail.Text;
            string telefone = textTelefone.Text;
            string empresa = textEmpresa.Text;
            string cargo = textCargo.Text;

            controladorContato.InserirNovo(new Contato(nome, email, telefone, empresa, cargo));

            CarregarContatos();

    
        }

        private void ExcluirContato()
        {
            int id = Convert.ToInt32(textBoxId.Text);
            controladorContato.Excluir(id);
            CarregarContatos();
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            CarregarContatos();
            LimparCampos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExcluirContato();
            LimparCampos();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditarRegistro();
            LimparCampos();
        }

        private void LimparCampos()
        {
            textBoxId.Clear();
            textNome.Clear();
            textEmail.Clear();
            textTelefone.Clear();
            textEmpresa.Clear();
            textCargo.Clear();
        }

        private void textBoxId_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Utlize o campo Id só para editar e excluir");
        }
    }
}
