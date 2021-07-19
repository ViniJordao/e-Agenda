using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace eAgenda.WindowsFormsApp
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Obrigado por utilizar a e-Agenda");
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TarefasForm tarefas = new TarefasForm();
            tarefas.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 contatos = new Form3();
            contatos.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CompromissosForm compromissos = new CompromissosForm();
            compromissos.ShowDialog();
            this.Close();
        }


    }
}
