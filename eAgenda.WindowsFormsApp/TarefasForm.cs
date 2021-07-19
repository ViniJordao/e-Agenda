using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eAgenda.Dominio.TarefaModule;
using eAgenda.Controladores.TarefaModule;
using eAgenda.Controladores.Shared;


namespace eAgenda.WindowsFormsApp
{
    public partial class TarefasForm : Form
    {
        ControladorTarefa controladorTarefa = new ControladorTarefa();
      
        public TarefasForm()
        {
            InitializeComponent();
           
            controladorTarefa = new ControladorTarefa();
            CarregarTarefasPendentes();
            CarregarTarefasConcluidas();
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            TelaPrincipal form1 = new TelaPrincipal();
            form1.ShowDialog();
            this.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadastrarTarefas();

        }

        private void CadastrarTarefas()
        {
            int prioridade = DefinirPrioridade(cmbPrioridade.SelectedItem);
            string titulo = txtTitulo.Text;

            Tarefa tarefa = new Tarefa(titulo, DateTime.Now, (PrioridadeEnum)prioridade);
           
           controladorTarefa.InserirNovo(tarefa);
             
            CarregarTarefasPendentes();
            LimparCampos();
        }

        private int DefinirPrioridade(object selectedItem)
        {
            switch (selectedItem.ToString())
            {
                case "Alta": return 2;
                case "Normal": return 1;
                case "Baixa": return 0;
            }
            return 0;
        }

        private void CarregarTarefasPendentes()
        {
            dtTarefasPendentes.Clear();
            List<Tarefa> listaTarefasPendentes = controladorTarefa.SelecionarTodasTarefasPendentes();

            foreach (var tarefaPendentes in listaTarefasPendentes)
            {
                DataRow registro = dtTarefasPendentes.NewRow();

                registro["Id"] = tarefaPendentes.Id;
                registro["Titulo"] = tarefaPendentes.Titulo;
                registro["Prioridade"] = tarefaPendentes.Prioridade;
                registro["Percentual"] = tarefaPendentes.Percentual;
                registro["Data de Criacão"] = tarefaPendentes.DataCriacao.ToShortDateString();
               

                dtTarefasPendentes.Rows.Add(registro);
            }
            
        }

        private void CarregarTarefasConcluidas()
        {
            dtTarefasConcluidas.Clear();
            List<Tarefa> listaTarefasConcluidas = controladorTarefa.SelecionarTodasTarefasConcluidas();

            foreach (var tarefaConcluidas in listaTarefasConcluidas)
            {
                DataRow registro = dtTarefasConcluidas.NewRow();

                registro["Id"] = tarefaConcluidas.Id;
                registro["Titulo"] = tarefaConcluidas.Titulo;
                registro["Prioridade"] = tarefaConcluidas.Prioridade;
                registro["Percentual"] = tarefaConcluidas.Percentual;
                registro["Data de Conclusão"] = ((DateTime)tarefaConcluidas.DataConclusao).ToShortDateString();

                dtTarefasConcluidas.Rows.Add(registro);
            }

        }
        private void EditarTarefa()
        {
            int id = Convert.ToInt32(textBoxId.Text);
            Tarefa tarefaSelecionada = controladorTarefa.SelecionarPorId(id);

            int prioridadeEditar = DefinirPrioridade(cmbPrioridade.SelectedItem);

            Tarefa tarefa = new Tarefa(txtTitulo.Text, tarefaSelecionada.DataCriacao, (PrioridadeEnum)prioridadeEditar);
            tarefa.AtualizarPercentual(Convert.ToInt32(textPercentual.Text), DateTime.Now);

            controladorTarefa.Editar(id, tarefa);

            CarregarTarefasPendentes();
            CarregarTarefasConcluidas();
        }
        public void ExcluirTarefa()
        {
            int id = Convert.ToInt32(textBoxId.Text);
            controladorTarefa.Excluir(id);
            CarregarTarefasPendentes();
            CarregarTarefasConcluidas();
        }

        public void LimparCampos()
        {
            textBoxId.Clear();
            txtTitulo.Clear();
            textPercentual.Clear();
           

        }


        private void button5_Click(object sender, EventArgs e)
        {
            ExcluirTarefa();
            LimparCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarTarefa();
            LimparCampos();
        }

        private void textBoxId_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Utlize o campo Id só para editar e excluir");
        }
    }
}
