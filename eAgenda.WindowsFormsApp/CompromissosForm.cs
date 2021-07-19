using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eAgenda.Controladores.CompromissoModule;
using eAgenda.Controladores.ContatoModule;
using eAgenda.Dominio.CompromissoModule;
using eAgenda.Dominio.ContatoModule;

namespace eAgenda.WindowsFormsApp
{
    public partial class CompromissosForm : Form
    {
        ControladorCompromisso controladorCompromisso = new ControladorCompromisso();
        ControladorContato controladorContato = new ControladorContato();

        public CompromissosForm()
        {
            controladorCompromisso = new ControladorCompromisso();


            InitializeComponent();
            CarregarCompromissos();
            CarregarContatos();
        }

        private void CarregarCompromissos()
        {
            dtCompromissos.Clear();
            List<Compromisso> ListaCompromissos = controladorCompromisso.SelecionarTodos();

            foreach (Compromisso compromisso in ListaCompromissos)
            {
                DataRow registro = dtCompromissos.NewRow();

                registro["Id"] = compromisso.Id;
                registro["Assunto"] = compromisso.Assunto;
                registro["Local"] = compromisso.Local;
                registro["Link"] = compromisso.Link;
                registro["Data"] = compromisso.Data.ToShortDateString();
                registro["Hora Início"] = compromisso.HoraInicio;
                registro["Hora Término"] = compromisso.HoraTermino;
                registro["Contato"] = compromisso.Contato == null ? "Sem contato" : compromisso.Contato.Nome;

                dtCompromissos.Rows.Add(registro);
            }
        }

        public void InserirContato()
        {
            string assunto = textAssunto.Text;
            string local = textLocal.Text;
            string link = textLocal.Text;
            DateTime data = dtpData.Value;
            TimeSpan horaInicio = TimeSpan.Parse(maskHoraInicio.Text);
            TimeSpan horaTermino = TimeSpan.Parse(maskHoraTermino.Text);
            Contato contato = cmbContatos.SelectedItem is Contato ? (Contato)cmbContatos.SelectedItem : null;
            
            controladorCompromisso.InserirNovo(new Compromisso(assunto, local, link, data, horaInicio, horaTermino, contato));

            CarregarCompromissos();
        }

        public void CarregarContatos()
        {
            var ListaContatos = controladorContato.SelecionarTodos();
            foreach (var contato in ListaContatos)
            {
                cmbContatos.Items.Add(contato.Nome);
            }
        }

        public void EditarCompromissos()
        {
            int id = Convert.ToInt32(textId.Text);
            string assunto = textAssunto.Text;
            string local = textLocal.Text;
            string link = textLocal.Text;
            DateTime data = dtpData.Value;
            TimeSpan horaInicio = TimeSpan.Parse(maskHoraInicio.Text);
            TimeSpan horaTermino = TimeSpan.Parse(maskHoraTermino.Text);
            Contato contato = cmbContatos.SelectedItem is Contato ? (Contato)cmbContatos.SelectedItem : null;

            Compromisso compromissoEditado = new Compromisso(assunto, local, link, data, horaInicio, horaTermino, contato);

            controladorCompromisso.Editar(id, compromissoEditado);

            CarregarCompromissos();
        }

        public void ExcluirCompromissos()
        {
            int id = Convert.ToInt32(textId);
            controladorCompromisso.Excluir(id);

            CarregarCompromissos();
            LimparCampos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            TelaPrincipal form1 = new TelaPrincipal();
            form1.ShowDialog();
            this.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            InserirContato();
            LimparCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarCompromissos();
            LimparCampos();
        }

        private void LimparCampos()
        {
            textAssunto.Clear();
            textId.Clear();
            textLink.Clear();
            textLocal.Clear();
            maskHoraInicio.Clear();
            maskHoraTermino.Clear();

        }

        private void textId_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Utlize o campo Id só para editar e excluir");
        }
    }


}
