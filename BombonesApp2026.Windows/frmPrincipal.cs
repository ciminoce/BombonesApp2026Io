using BombonesApp2026.Windows.Classes;

namespace BombonesApp2026.Windows
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"{Sesion.UsuarioActual!.UserName}";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
