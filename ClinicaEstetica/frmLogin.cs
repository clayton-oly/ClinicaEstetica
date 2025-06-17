using System;
using System.Windows.Forms;
using ClinicaEstetica.DLL;
using ClinicaEstetica.DTO;

namespace ClinicaEstetica
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            UsuarioDTO usuarioDTO = new UsuarioDTO();
            UsuarioBLL uSuarioBLL = new UsuarioBLL();

            usuarioDTO = uSuarioBLL.AutenticarUsuario(email, senha);

            if (usuarioDTO != null)
            {
                MessageBox.Show($"Bem vindo(a) {usuarioDTO.Nome}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmUsuarios frmUsuarios = new frmUsuarios();
                frmUsuarios.Show();
            }
            else
            {
                MessageBox.Show($"Não foi possivel efetuar o login, tente novamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
