using System;
using System.Windows.Forms;
using ClinicaEstetica.DLL;
using ClinicaEstetica.DTO;

namespace ClinicaEstetica
{
    public partial class frmUsuarios : Form
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        UsuarioDTO usuarioDTO = new UsuarioDTO();
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void CarregarUsuarios()
        {
            var usuarios = usuarioBLL.ListarTodosUsuarios();
            dgvUsuarios.DataSource = usuarios;
            dgvUsuarios.ClearSelection();
        }

        private void frmUsuarios_Load(object sender, System.EventArgs e)
        {
            CarregarUsuarios();
            cboTipo.DisplayMember = "Nome";
            cboTipo.DataSource = usuarioBLL.GetTipoUsuario();
        }

        private void LimparCampos()
        {
            txtId.Text = string.Empty;
            cboTipo.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSenha.Text = string.Empty;
            chkStatus.Checked = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                var usuario = (UsuarioDTO)dgvUsuarios.SelectedRows[0].DataBoundItem;
                usuarioDTO = usuario;

                txtId.Text = usuario.IdUsuario.ToString();
                var tipo = usuario.IdTipoUsuario == 1 ? "Administrador" : "Operador";
                cboTipo.Text = tipo;

                txtNome.Text = usuarioDTO.Nome.ToString();
                txtEmail.Text = usuarioDTO.Email.ToString();
                txtSenha.Text = usuarioDTO.Senha.ToString();
                chkStatus.Checked = usuario.Status;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim() == "")
            {
                MessageBox.Show("Preencha o nome!");
                return;
            }
            UsuarioDTO usuarioDTO = new UsuarioDTO
            {
                IdTipoUsuario = cboTipo.Text == "Administrador" ? 1 : 2,
                Nome = txtNome.Text,
                Email = txtEmail.Text,
                Senha = txtSenha.Text,
                Status = chkStatus.Checked
            };

            if (string.IsNullOrEmpty(txtId.Text))
            {
                usuarioBLL.CreateUsuario(usuarioDTO);
                MessageBox.Show($"Usuário {usuarioDTO.Nome} cadastrado com sucesso!");
                CarregarUsuarios();
            }
            else
            {
                usuarioDTO.IdUsuario = int.Parse(txtId.Text);
                usuarioBLL.UpdateUsuario(usuarioDTO);
                MessageBox.Show($"Usuário {usuarioDTO.Nome} atualizado com sucesso!");
                CarregarUsuarios();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (usuarioDTO == null) return;

            var escolha = MessageBox.Show($"Deseja realmente excluir o usuário {usuarioDTO.Nome}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (escolha == DialogResult.Yes)
            {
                usuarioBLL.DeleteUsuario(usuarioDTO.IdUsuario);
                MessageBox.Show($"Usuário {usuarioDTO.Nome} excluido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information );
                CarregarUsuarios();
                LimparCampos();

            }

        }
    }
}
